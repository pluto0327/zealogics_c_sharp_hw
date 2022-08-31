using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using SocketDemo;

namespace file_client
{
    public partial class fmclient : Form
    {
        private TcpClient _tcpClient;
        private string _hostIP;
        private int _hostPort;
        private IPAddress _ipa;
        private IPEndPoint _ipe;
        public fmclient()
        {
            InitializeComponent();
            
            // get default path as downing file path
            string defaultFilePath = Path.Combine(Environment.CurrentDirectory, "");

            //預設主機IP
            _hostIP = "127.0.0.1";
            _hostPort = 35000;
            //先建立IPAddress物件,IP為欲連線主機之IP
            _ipa = IPAddress.Parse(_hostIP);

            //建立IPEndPoint
            _ipe = new IPEndPoint(_ipa, _hostPort);

            //先建立一個TcpClient;
            _tcpClient = new TcpClient();
            
            txtFilePath.Text = defaultFilePath;
        }

        private void btnSendReq_Click(object sender, EventArgs e)
        {
            try
            {
                int bufferSize = 1024;
                int bytesRead = 0;
                int allBytesRead = 0;

                _tcpClient.Connect(_ipe);
                if (_tcpClient.Connected)
                {
                    CommunicationBase cb = new CommunicationBase();
                    string msgToHost = txtServerIp.Text + "_" + txtServerPort.Text + "_" + txtFileName.Text;
                    cb.SendMsg("MessageFromClient_" + msgToHost, _tcpClient);

                    NetworkStream netStream = _tcpClient.GetStream();

                    // Read length of incoming data
                    byte[] length = new byte[4];
                    bytesRead = netStream.Read(length, 0, 4);
                    int dataLength = BitConverter.ToInt32(length, 0);

                    // Read the data
                    int bytesLeft = dataLength;
                    byte[] data = new byte[dataLength];

                    while (bytesLeft > 0)
                    {

                        int nextPacketSize = (bytesLeft > bufferSize) ? bufferSize : bytesLeft;

                        bytesRead = netStream.Read(data, allBytesRead, nextPacketSize);
                        allBytesRead += bytesRead;
                        bytesLeft -= bytesRead;

                    }
                    // Save image to desktop
                    File.WriteAllBytes("recieved_test.txt", data);

                    string rmsg = cb.ReceiveMsg(_tcpClient);
                    txtResult.Text = rmsg;
                    System.Diagnostics.Debug.WriteLine(rmsg);

                     // Clean up
                     netStream.Close();
                     _tcpClient.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                //_tcpClient.Close();
            }
        }

        private void fmclient_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tcpClient.Close();
        }
    }
}