﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    class Client
    {
        public static int dataBufferSize = 4096;

        public int id;
        public TCP tcp;
        public Client(int _clientId)
        {
            id = _clientId;
            tcp = new TCP(id);
        }
        
        public class TCP
        {
            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            private byte[] receiveBuffer;
            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceieveCallback, null);

                // TODO: Send welcome packet
            }

            private void ReceieveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        // TODO: disconnect
                        return;
                    }
                    // If we receieve data create a new array to capture the data we are reading
                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    // TODO: Handle Data
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceieveCallback, null);
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error receiving TCP data: {_ex}");
                    // TODO: Disconnect 
                }
            }
        }
    }
}
