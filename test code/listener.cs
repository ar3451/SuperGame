/*
# Network Programming 
by Richard Blum

Publisher: Sybex 
ISBN: 0782141765
*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UdpSrvrSample
{
   public static void Main()
   {
      var Server = new UdpClient(8888);
var ResponseData = Encoding.ASCII.GetBytes("My response written here");

while (true)
{
    var ClientEp = new IPEndPoint(IPAddress.Any, 0);
    var ClientRequestData = Server.Receive(ref ClientEp);
    var ClientRequest = Encoding.ASCII.GetString(ClientRequestData);

    Console.WriteLine("Recived {0} from {1}, sending response", ClientRequest, ClientEp.Address.ToString());
    Server.Send(ResponseData, ResponseData.Length, ClientEp);
}
   }
}
