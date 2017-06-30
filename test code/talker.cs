using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HelloWorldApplication
{
   class HelloWorld
   {
      static void Main(string[] args)
      {
         var Client = new UdpClient();
         var RequestData = Encoding.ASCII.GetBytes("Hello there");
         var ServerEp = new IPEndPoint(IPAddress.Any, 0);

         Client.EnableBroadcast = true;
         Client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, 8888));

         var ServerResponseData = Client.Receive(ref ServerEp);
         var ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
         Console.WriteLine("Recived {0} from {1}", ServerResponse, ServerEp.Address.ToString());

         Client.Close();
      }
   }
 }