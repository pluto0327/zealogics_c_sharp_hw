using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SocketDemo;

namespace file_server
{
    public partial class fmServer : Form
    {

        private IPAddress _hostIP = IPAddress.Parse("127.0.0.1");
        private int _hostPort = 35000;

        private Thread _oListener;

        private delegate void UpdateCurrentClientIpDelegate(string message);
        private delegate void UpdateCurrentClientPortDelegate(string message);
        private delegate void UpdateCurrentFileRequestDelegate(string message);

        private  void UpdateCurrentClientIp(string message)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateCurrentClientIpDelegate(UpdateCurrentClientIp), message);
                return;
            }

            txtCurrentClientIp.Text = message;
        }

        private void UpdateCurrentClientPort(string message)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateCurrentClientPortDelegate(UpdateCurrentClientPort), message);
                return;
            }

            txtCurrentClientPort.Text = message;
        }

        private void UpdateCurrentFileRequest(string message)
        {
            if (this.InvokeRequired)
            {
                Invoke(new UpdateCurrentFileRequestDelegate(UpdateCurrentFileRequest), message);
                return;
            }

            txtCurrentFileRequest.Text = message;
        }

        public fmServer()
        {
            InitializeComponent();
        }

        private void fmServer_Load(object sender, EventArgs e)
        {
            IPEndPoint ipe = new IPEndPoint(_hostIP, _hostPort);

            //建立TcpListener物件
            TcpListener tcpListener = new TcpListener(ipe);
            tcpListener.Start();
            System.Diagnostics.Debug.WriteLine("wait for connecting... \n");

            _oListener = new Thread(new ParameterizedThreadStart(TcpListen));
            _oListener.Start(tcpListener);
        }

        private void fmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO: using EventWaitHandle to confirm thread is terminated
        } 

        private void TcpListen(object value)
        {
            TcpListener listener = (TcpListener)value;
            TcpClient tmpTcpClient;
            while (true)
            {
                tmpTcpClient = listener.AcceptTcpClient();

                if (tmpTcpClient.Connected)
                {
                    System.Diagnostics.Debug.WriteLine("connected...!");
                    string s = tmpTcpClient.Client.LocalEndPoint.ToString();
                    //HandleClient handleClient = new HandleClient(tmpTcpClient);
                    Thread myThread = new Thread(new ParameterizedThreadStart(Communicate));
                    myThread.IsBackground = true;
                    myThread.Start(tmpTcpClient);
                    myThread.Name = tmpTcpClient.Client.RemoteEndPoint.ToString();
                }
                System.Diagnostics.Debug.WriteLine("end of while");
            }
        }

        /// <summary>
        /// Communicate
        /// </summary>
        private  void Communicate(object value)
        {
            TcpClient mTcpClient = (TcpClient)value;
            try
            {
                int bufferSize = 1024;

                CommunicationBase cb = new CommunicationBase();
                string msg = cb.ReceiveMsg(mTcpClient);
                System.Diagnostics.Debug.WriteLine(msg + "\n");

                // to parse message from client
                string[] words = msg.Split('_');

                UpdateCurrentClientIp(words[1]);
                UpdateCurrentClientPort(words[2]);
                UpdateCurrentFileRequest(words[3]);

                // to find file
                string targetFile = words[3];
                string cfolder = Directory.GetCurrentDirectory();
                string sfind = cfolder+"\\"+targetFile;
              
                bool fileExist = isFileExist(sfind);

                if (fileExist)
                { // start send file to client 
                    NetworkStream netStream = mTcpClient.GetStream();
                    byte[] data = File.ReadAllBytes(sfind);

                    // Build the package
                    byte[] dataLength = BitConverter.GetBytes(data.Length);
                    byte[] package = new byte[4 + data.Length];
                    dataLength.CopyTo(package, 0);
                    data.CopyTo(package, 4);

                    // Send to server
                    int bytesSent = 0;
                    int bytesLeft = package.Length;

                    while (bytesLeft > 0)
                    {
                        int nextPacketSize = (bytesLeft > bufferSize) ? bufferSize : bytesLeft;

                        netStream.Write(package, bytesSent, nextPacketSize);
                        bytesSent += nextPacketSize;
                        bytesLeft -= nextPacketSize;
                    }
                   
                    cb.SendMsg("MessageFromHost_Success", mTcpClient);
                    
                    // Clean up
                    netStream.Close();
                    mTcpClient.Close();
                }
                else
                { //
                    cb.SendMsg("MessageFromHost_Failure", mTcpClient);
                }
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                //this.mTcpClient.Close();
            }
        }  

        public bool isFileExist(string sfind)
        {
            return File.Exists(sfind);
        }

    }
}