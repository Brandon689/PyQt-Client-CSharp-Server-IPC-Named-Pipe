using System;
using System.IO.Pipes;
using System.Text;

namespace NamedPipeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("test_pipe", PipeDirection.InOut))
            {
                Console.WriteLine("NamedPipeServerStream object created.");

                // Wait for a client to connect
                Console.Write("Waiting for client connection...");
                pipeServer.WaitForConnection();
                Console.WriteLine("Client connected.");

                try
                {
                    // Read the request from the client. Once the client has
                    // written to the pipe its security token will be available.
                    using (StreamReader sr = new StreamReader(pipeServer))
                    {
                        // This call to ReadLine blocks until a line is received from the client
                        string temp;
                        while ((temp = sr.ReadLine()) != null)
                        {
                            Console.WriteLine("Received from client: " + temp);
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }
            }
        }
    }
}
