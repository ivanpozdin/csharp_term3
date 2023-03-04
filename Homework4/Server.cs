using System.Net;
using System.Net.Sockets;

namespace Homework4;

public class Server
{
    private readonly IPAddress _ip;
    private readonly int _port;

    public Server(IPAddress ip, int port, string defaultDirectory)
    {
        _ip = ip;
        _port = port;
        Directory.SetCurrentDirectory(defaultDirectory);
    }

    private static async Task WriteAndFlush(StreamWriter writer, string message)
    {
        await writer.WriteAsync(message);
        await writer.FlushAsync();
    }

    private static async Task List(Stream stream, string path)
    {
        var writer = new StreamWriter(stream);
        var isPathDirectory = (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
        if (isPathDirectory)
        {
            var (directories, files) = (Directory.GetDirectories(path), Directory.GetFiles(path));
            await WriteAndFlush(writer, $"{files.Length + directories.Length} ");
            var isDir = false;
            foreach (var file in files)
                await WriteAndFlush(writer, $"{file} {isDir} ");
            isDir = true;
            foreach (var directory in directories)
                await WriteAndFlush(writer, $"{directory} {isDir} ");
        }
        else
        {
            await WriteAndFlush(writer, $"{1} {path} {false} ");
        }
    }

    private static async Task Get(Stream stream, string filePath)
    {
        var writer = new StreamWriter(stream);
        if (!File.Exists(filePath))
            await WriteAndFlush(writer, "-1 ");
        var size = new FileInfo(filePath).Length;

        await WriteAndFlush(writer, size + " ");
        const int bufferSize = 1024;
        var buffer = new byte[bufferSize];
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        while (fileStream.Position < fileStream.Length)
        {
            var count = await fileStream.ReadAsync(buffer);
            await stream.WriteAsync(buffer.AsMemory(0, count));
        }

        await stream.FlushAsync();
        fileStream.Close();
    }

    public async void StartServer(CancellationTokenSource cancellationTokenSource)
    {
        var tcpListener = new TcpListener(_ip, _port);
        tcpListener.Start();
        while (!cancellationTokenSource.IsCancellationRequested)
            try
            {
                using var socket = await tcpListener.AcceptSocketAsync();
                await using var stream = new NetworkStream(socket);
                using var streamReader = new StreamReader(stream);
                var receivedStrings = (await streamReader.ReadLineAsync())?.Split(' ');

                if (receivedStrings == null || receivedStrings.Length != 2) continue;
                switch (receivedStrings[0])
                {
                    case "1":
                        await Task.Run(() => List(stream, receivedStrings[1]));
                        break;
                    case "2":
                        await Task.Run(() => Get(stream, receivedStrings[1]));
                        break;
                }
            }
            catch
            {
                cancellationTokenSource.Cancel();
            }

        tcpListener.Stop();
    }
}