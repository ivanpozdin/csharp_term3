// See https://aka.ms/new-console-template for more information

using System.Net;
using Homework4;

var ip = IPAddress.Parse("127.0.0.1");
var port = 1111;
var separator = Path.DirectorySeparatorChar;
var path = $"..{separator}..{separator}..{separator}Directory1";

new Server(ip, port, path).StartServer(new CancellationTokenSource());
await new Client(ip, port).StartClient();