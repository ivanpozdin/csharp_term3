using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Homework4;

public class Client
{
    private readonly IPAddress _ip;
    private readonly int _port;
    private bool _isStopped;

    public Client(IPAddress ip, int port)
    {
        _ip = ip;
        _port = port;
    }

    // Function to start console client app.
    public async Task StartClient()
    {
        await PrintStartInfo();
        while (!_isStopped)
        {
            Console.WriteLine("Enter command:");
            var command = Console.ReadLine();
            if (string.IsNullOrEmpty(command)) continue;
            var commandSplit = command.Trim().Split(" ");
            switch (commandSplit[0])
            {
                case "0":
                    _isStopped = true;
                    break;
                case "1":
                    if (commandSplit.Length != 2) continue;
                    await PrintListOfFilesAndDirectories(commandSplit[1]);
                    break;
                case "2":
                    if (commandSplit.Length != 3) continue;
                    await DownloadFiles(commandSplit[1], commandSplit[2]);
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }

    private async Task<(int, List<(string, bool)>)> List(string path)
    {
        var receivedData = await ReceiveListStringFromServer(path);

        if (receivedData == null) throw new InvalidDataException();
        var receivedDataSplit = receivedData.Split(' ');

        if (!int.TryParse(receivedDataSplit[0], out var size)) throw new InvalidDataException();
        if (size == -1) throw new DirectoryNotFoundException();

        var directoryContent = GetDirectoryContentFromReceivedData(receivedDataSplit);
        return (size, directoryContent);
    }

    private async Task DownloadFiles(string serverPath, string clientPath)
    {
        try
        {
            await GetAllFilesFromFolder(serverPath, clientPath);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File Not Found On Server1");
        }
    }

    private async Task GetOneFile(string serverPath, string clientPath)
    {
        var client = new TcpClient();
        await client.ConnectAsync(_ip, _port);
        await using var stream = client.GetStream();
        await using var streamWriter = new StreamWriter(stream);

        await streamWriter.WriteLineAsync($"2 {serverPath}");
        await streamWriter.FlushAsync();

        var length = GetFileLengthFromServer(stream);
        if (length == -1)
        {
            Console.WriteLine("File Not Found On Server2");
            return;
        }

        await DownloadFileToPath(stream, serverPath, clientPath, length);
    }

    private List<(string, bool)> GetDirectoryContentFromReceivedData(string[] data)
    {
        var directoryContent = new List<(string, bool)>();
        for (var i = 1; i < data.Length - 1; i += 2)
        {
            var path = data[i];
            var isDir = data[i + 1] == true.ToString();
            directoryContent.Add((path, isDir));
        }

        return directoryContent;
    }

    private async Task<string?> ReceiveListStringFromServer(string serverPath)
    {
        var client = new TcpClient();
        await client.ConnectAsync(_ip, _port);

        await using var stream = client.GetStream();
        await using var streamWriter = new StreamWriter(stream);

        await streamWriter.WriteLineAsync($"1 {serverPath}");
        await streamWriter.FlushAsync();

        using var streamReader = new StreamReader(stream);
        var receivedData = await streamReader.ReadLineAsync();
        return receivedData;
    }


    private long GetFileLengthFromServer(NetworkStream stream)
    {
        var lengthString = "";
        int nextByte;
        const int space = 32;

        while ((nextByte = stream.ReadByte()) != space)
            lengthString += Encoding.UTF8.GetString(new[] { Convert.ToByte(nextByte) });

        return long.Parse(lengthString);
    }

    private async Task DownloadFileToPath(NetworkStream stream, string serverPath, string clientPath, long length)
    {
        var filename = Path.GetFileName(serverPath);
        try
        {
            await using var fileStream = File.Create(clientPath +
                                                     (clientPath[^1] != Path.DirectorySeparatorChar
                                                         ? Path.DirectorySeparatorChar
                                                         : "") + filename);
            while (length > 0)
            {
                var buffer = new byte[stream.Socket.ReceiveBufferSize];
                var receivedLength = await stream.ReadAsync(buffer);
                await fileStream.WriteAsync(buffer.AsMemory(0, receivedLength));
                length -= receivedLength;
            }

            Console.WriteLine($"Downloaded {Path.GetFileName(filename)} to {clientPath} successfully");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Client Directory Not Found");
        }
    }

    private async Task GetAllFilesFromFolder(string serverPath, string clientPath)
    {
        try
        {
            var (_, list) = await List(serverPath);
            var listOfDownloads = new List<Task>();

            foreach (var (file, isDir) in list)
                if (!isDir)
                    listOfDownloads.Add(GetOneFile(file, clientPath));

            foreach (var download in listOfDownloads)
                await download;
        }
        catch (InvalidDataException)
        {
            Console.WriteLine("Directory Not Found On Server");
        }
    }

    private async Task PrintListOfFilesAndDirectories(string path)
    {
        try
        {
            var (_, filesAndDirectories) = await List(path);
            var number = 1;
            foreach (var fileOrDirectory in filesAndDirectories)
                Console.WriteLine(
                    $"{number++}) {fileOrDirectory.Item1} {(fileOrDirectory.Item2 ? " is directory" : " is file")}");
        }
        catch (InvalidDataException)
        {
            Console.WriteLine("Request failed. Try again.");
        }
    }

    private async Task PrintStartInfo()
    {
        Console.WriteLine("There are 3 commands:");
        Console.WriteLine("\"0\" = stop client.");
        Console.WriteLine(
            "\"1 <path_on_server>\" = command to get list of all files and folders in specified <path_on_server> on server.");
        Console.WriteLine("\"2 <path_on_server> <path_on_client>\" = " +
                          "command to download specified file or files from folder on server to <specified path_on_client> (folder) on client computer.");
        Console.WriteLine(
            $"\n List of files and directories that you can access by specifying:\".{Path.DirectorySeparatorChar}<path_to_file_or_directory>\":");
        await PrintListOfFilesAndDirectories($".{Path.DirectorySeparatorChar}");
    }
}