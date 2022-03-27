﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocket
{
    public class Client<T>
    {
        public int ClientId { get; set; }
        public bool CanSend { get; set; }
        ObservableCollection<string> Data;
        List<Task> Tasks;
        Socket MySocket;
        public Client()
        {

        }
        public void DataConnection(ref ObservableCollection<string> data)
        {
            this.Data = data;
        }
        public void Connect(string ip = "127.0.0.1")
        {
            MySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            MySocket.Connect(IPAddress.Parse(ip), 10000);
            Tasks = new List<Task>();

            byte[] id = new byte[1];
            MySocket.Receive(id);
            ClientId = int.Parse(Encoding.ASCII.GetString(id));

            Task send = new Task(() =>
            {
                while (true)
                {
                    ;
                    if (CanSend)
                    {
                        string json = JsonConvert.SerializeObject(Data);
                        MySocket.Send(Encoding.ASCII.GetBytes(json));
                        Data.Add("red");
                        ;
                        //nem küldi le elég mélyre 
                        ;
                    }
                    ;
                    System.Threading.Thread.Sleep(1000);
                }
            }, TaskCreationOptions.LongRunning);

            Task receive = new Task(() =>
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    MySocket.Receive(buffer);

                    string message = "";
                    int idx = 0;
                    while (buffer[idx] != 0)
                    {
                        byte[] helper = new byte[1];
                        helper[0] = buffer[idx];
                        message = message + Encoding.ASCII.GetString(helper);
                        idx++;
                    }

                    if (message.Equals("false") || message.Equals("true"))
                    {
                        CanSend = bool.Parse(message);
                    }
                    else
                    {
                        Data = JsonConvert.DeserializeObject<ObservableCollection<string>>(message);
                        ;
                    }
                }
            }, TaskCreationOptions.LongRunning);

            Tasks.Add(send);
            Tasks.Add(receive);

            receive.Start();
            send.Start();
        }
        public void Disconnect()
        {
            if (MySocket != null && MySocket.Connected)
            {
                MySocket.Shutdown(SocketShutdown.Both);
                MySocket.Close();
                MySocket.Dispose();
            }
        }
    }
}
