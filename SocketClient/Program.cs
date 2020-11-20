using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        { 
            //Creare il mio socketlistener
            //1) specifico che versione IP
            //2) tipo di socket. Stream.
            //3) protocollo a livello di trasporto
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string strIPAdress = "192.168.5.198", strPort = "";
            
            IPAddress ipaddr = null;
            int nPort;


            try
            {
                
                Console.WriteLine("IP del Server: ");
                strIPAdress = Console.ReadLine();
                Console.WriteLine("Port del Server: ");
                strPort = Console.ReadLine();
                if(!IPAddress.TryParse(strIPAdress.Trim(),out ipaddr))
                {
                    Console.WriteLine("IP non valido.");
                    return;
                }
                if(!int.TryParse(strPort,out nPort))
                {
                    Console.WriteLine("Porta non valido.");
                    return;
                }
                if(nPort<=0 || nPort >= 65535)
                {
                    Console.WriteLine("Porta non valido.");
                    return;
                }
                Console.WriteLine("Endpoint del server " + ipaddr.ToString() + ":" + nPort);
                client.Connect(ipaddr, nPort);

                byte[] buff = new byte[128];
                string sendString = "";
                string ReciveString = "";
                int recivedButes = 0;
                while (true)
                {
                    Console.WriteLine("Manda un messaggio");
                    sendString = Console.ReadLine();
                    Encoding.ASCII.GetBytes(sendString).CopyTo(buff,0);
                    client.Send(buff);

                    Array.Clear(buff, 0, buff.Length);
                    recivedButes = client.Receive(buff);
                    ReciveString = Encoding.ASCII.GetString(buff, 0, recivedButes);
                    Console.WriteLine(ReciveString);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
            
        }
    }
}
