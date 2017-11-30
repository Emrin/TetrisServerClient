using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

/// <summary>
/// SERVER
/// </summary>

namespace Tetris
{
    public class ServerApp
    {
        public static int Main(String[] args)
        {
            int user = 0;
            //TODO multithread this part
            //TODO Settings class
            SynchronousSocketListener.StartListening();
            Console.WriteLine("Stopping program. Press any key to close.");
            Console.ReadKey();
            return 0;
        }
    }
}
