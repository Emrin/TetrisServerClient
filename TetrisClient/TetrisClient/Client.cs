using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


/// <summary>
/// CLIENT [receives game state and 
/// </summary>
public class SynchronousSocketClient
{

    public static void StartClient()
    {
        bool started = true;  
        byte[] bytes = new byte[1024]; // Data buffer for incoming data.

        try // Connect to a remote device. 
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // Establish the remote endpoint for the socket.
            IPAddress ipAddress = ipHostInfo.AddressList[0]; // change to server IP adress (as parameter)
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000); // port = 11000
            
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // Create a TCP/IP  socket.  

            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);
                Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());
                
                byte[] msg;
                int bytesSent;
                int bytesRec;

                while (started)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Play your next move!");
                    msg = Encoding.ASCII.GetBytes(Console.ReadLine() + "<EOF>"); // Encode the data string into a byte array.
                    
                    bytesSent = sender.Send(msg); // Send the data through the socket. 

                    bytesRec = sender.Receive(bytes); // Receive the response from the remote device.  
                    Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    //("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    //TODO game over message and tracker
                }
                
                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            /////////////// EXCEPTIONS ///////////////////
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        StartClient();
        Console.WriteLine("Finishged StartClient()! Press any key to close.");
        Console.ReadKey();
        return 0;
    }
}