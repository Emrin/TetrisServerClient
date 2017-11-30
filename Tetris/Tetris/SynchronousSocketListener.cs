using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Tetris
{
    public class SynchronousSocketListener //maybe put class separately
    {
        public static string data = null; // Incoming data from the client. 

        public static void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Listening for connections...");
            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Socket handler = listener.Accept();
                Console.WriteLine("Connected.");


                GameManager game = new GameManager();
                GameManager.GameInstance.InitGame();
                Console.WriteLine("Created new instance of game.");
                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Waiting user..");
                    // Program is suspended while waiting for an incoming connection.  
                    //Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    // Show the data on the console.
                    Console.WriteLine("Text received : {0}", data);

                    // Update the game instance with the data sent from the client.
                    GameManager.GameInstance.Updater(data);
                    

                    // Echo the data back to the client.  
                    //byte[] msg = Encoding.ASCII.GetBytes(data);
                    byte[] msg = Encoding.ASCII.GetBytes(GameManager.GameInstance.ToString());


                    handler.Send(msg);
                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();
                }
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
    }
}
