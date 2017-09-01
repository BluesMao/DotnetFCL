using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AsyncCallbackExample
{
    class Program
    {
        static int requestCounter;
        static ArrayList hostData = new ArrayList();
        static System.Collections.Specialized.StringCollection hostNames = new System.Collections.Specialized.StringCollection();

        static void UpdateUserInterface()
        {
            Console.WriteLine("{0} requests remaining.",requestCounter);
        }

        static void Main(string[] args)
        {
            AsyncCallback callBack = new AsyncCallback(ProcessDnsInformation);
            string host = string.Empty;
            do
            {
                Console.WriteLine("Enter the name of a host computer or <enter> to finish: ");
                host = Console.ReadLine();

                if (host.Length > 0)
                {
                    Interlocked.Increment(ref requestCounter);

                    Dns.BeginGetHostEntry(host, callBack, host);
                }
            }
            while (host.Length > 0);

            while (requestCounter > 0)
            {
                UpdateUserInterface();
            }

            for (int i = 0; i < hostNames.Count; i++)
            {
                object data = hostData[i];
                string message = data as string;
                if (message != null)
                {
                    Console.WriteLine("Request for {0} returned message: {1}",hostNames[i],message);
                    continue;
                }

                IPHostEntry h = (IPHostEntry)data;
                string[] aliases = h.Aliases;
                IPAddress[] addressed = h.AddressList;
                if (aliases.Length > 0)
                {
                    Console.WriteLine("Aliases for {0}", hostNames[i]);
                    for (int j = 0; j < aliases.Length; j++)
                    {
                        Console.WriteLine("{0}",aliases[j]);
                    }
                }

                if (addressed.Length > 0)
                {
                    Console.WriteLine("Addresses for {0}",hostNames[i]);
                    for (int k = 0; k < addressed.Length; k++)
                    {
                        Console.WriteLine("{0}",addressed[k].ToString());
                    }
                }

                Console.ReadKey();
            }
        }

        static void ProcessDnsInformation(IAsyncResult result)
        {
            string hostName = (string)result.AsyncState;
            hostNames.Add(hostName);
            try
            {
                IPHostEntry host = Dns.EndGetHostEntry(result);
                hostData.Add(host);
            }
            catch (SocketException e)
            {
                hostData.Add(e.Message);
            }
            finally
            {
                Interlocked.Decrement(ref requestCounter);
            }
        }
    }
}
