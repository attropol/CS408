using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            
            string Username = textBox_Username.Text;

            

            int portNum;
            
            if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    Byte[] buffer = new Byte[64];
                    buffer = Encoding.Default.GetBytes(Username);
                    clientSocket.Send(buffer);

                    //----TAKE AN ACK ON THE USERNAME, IF THERE IS SUCH A USER IN THE SERVER, DON'T CONNECT-------//
                    Byte[] usernameAckBuffer = new byte[4];
                    clientSocket.Receive(usernameAckBuffer);
                    string usernameAck = Encoding.Default.GetString(usernameAckBuffer);
                    usernameAck = usernameAck.Substring(0, usernameAck.IndexOf("\0"));

                    if (usernameAck == "0")
                    {
                        logs.AppendText("There is a client with username " + Username + "who is already connected. Try another username!\n");
                    }
                    else if (usernameAck == "2")
                    {
                        logs.AppendText("There is no " + Username + " in database try another username!\n");
                    }
                    else
                    {
                        button1.Enabled = true;
                        button_seefollowers.Enabled = true;
                        button3.Enabled = true;
                        button_blckuser.Enabled = true;
                        button_seefollowed.Enabled = true;
                        button_follow.Enabled = true;
                        button_getSweets.Enabled = true;
                        button_connect.Enabled = false;
                        textBox_message.Enabled = true;
                        button_send.Enabled = true;
                        button_getusernames.Enabled = true;
                        button_disconnect.Enabled = true;
                        connected = true;
                        logs.AppendText("Connected to the server!\n");
                        

                    }

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }

        }

        private void Receive()
        {
            while(connected)
            {
                try
                {
                    Byte[] buffer = new Byte[10000];
                    clientSocket.Receive(buffer);
                    
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (incomingMessage.StartsWith("msg"))
                    {
                        incomingMessage = incomingMessage.Remove(0,3);
                        logs.AppendText("Server: " + incomingMessage + "\n");
                    }
                    else if (incomingMessage.StartsWith("usrlist"))
                    {
                        incomingMessage = incomingMessage.Remove(0,7);
                        logs.AppendText("Username List: \n");
                        logs.AppendText(incomingMessage+"\n");
                    }
                    else if (incomingMessage.StartsWith("flwrq"))
                    {
                        incomingMessage = incomingMessage.Remove(0,5);
                        logs.AppendText(incomingMessage);
                    }
                    else if (incomingMessage.StartsWith("swts"))
                    {
                        incomingMessage = incomingMessage.Remove(0, 4);
                        logs.AppendText(incomingMessage);
                    }
                    else if (incomingMessage.StartsWith("flwswts"))
                    {
                        incomingMessage = incomingMessage.Remove(0, 7);
                        logs.AppendText(incomingMessage);
                    }
                    else if (incomingMessage.StartsWith("blckrq"))
                    {
                        incomingMessage = incomingMessage.Remove(0, 6);
                        logs.AppendText(incomingMessage);
                    }
                    else if (incomingMessage.StartsWith("seflwd"))
                    {
                        incomingMessage = incomingMessage.Remove(0, 6);
                        logs.AppendText(incomingMessage);
                    }
                    else if (incomingMessage.StartsWith("seflwrs"))
                    {
                        incomingMessage = incomingMessage.Remove(0, 7);
                        logs.AppendText(incomingMessage);
                    }

                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string clientname = textBox_Username.Text;
            string message = textBox_message.Text;
            if(message != "" && message.Length <= 64)
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);
                logs.AppendText(clientname+ ":" + message + "   |"+DateTime.Now+"\n");
            }

        }

        private void textBox_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            // Disconnect from server
            connected = false;
            terminating = true;
            clientSocket.Close();
            logs.AppendText("Disconnected\n");
            button_connect.Enabled = true;
            button_blckuser.Enabled = false;
            button_getusernames.Enabled = false;
            button_disconnect.Enabled = false;
            button_send.Enabled = false;
            textBox_message.Enabled = false;
            button_follow.Enabled = false;
        }
        
        private void button_getusernames_Click(object sender, EventArgs e)
        {
            
        }
        private void button_getSweets_Click(object sender, EventArgs e)
        {
            
        }

        private void button_getusernames_Click_1(object sender, EventArgs e)
        {
            try
            {
                //------------------SEND THE COMMAND RTRV----------------------//
                Byte[] requestBuffer = Encoding.Default.GetBytes("Rtrv");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the list of usernames!\n");
            }
        }

        private void button_follow_Click(object sender, EventArgs e)
        {
            //logs.AppendText("follow clicked");
            string usfollow = textBox_userfollow.Text;
            string requesttype = "FlwUser";

            if (usfollow != "" && usfollow.Length <= 64)
            {
                //requesttype = requesttype + usfollow;
                Byte[] requestBuffer = Encoding.Default.GetBytes(requesttype+usfollow);
                clientSocket.Send(requestBuffer);
            }

        }

        private void button_getSweets_Click_1(object sender, EventArgs e)
        {
            try
            {
                //------------------SEND THE COMMAND GTSWT----------------------//
                Byte[] requestBuffer = Encoding.Default.GetBytes("gtswt");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the list of sweets!\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------SEND THE COMMAND GTSWT----------------------//
                Byte[] requestBuffer = Encoding.Default.GetBytes("gtfswt");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the list of sweets!\n");
            }
        }

        private void button_blckuser_Click(object sender, EventArgs e)
        {
            //logs.AppendText("follow clicked");
            string usblck = textBox_blckuser.Text;
            string requesttype = "BlckUser";

            if (usblck != "" && usblck.Length <= 64)
            {
                //requesttype = requesttype + usfollow;
                Byte[] requestBuffer = Encoding.Default.GetBytes(requesttype + usblck);
                clientSocket.Send(requestBuffer);
            }
        }

        private void button_seefollowed_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------SEND THE COMMAND SEE FOLLOWED----------------------//
                Byte[] requestBuffer = Encoding.Default.GetBytes("seflwd");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the list of followed!\n");
            }
        }

        private void button_seefollowers_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------SEND THE COMMAND GTSWT----------------------//
                Byte[] requestBuffer = Encoding.Default.GetBytes("seflwrs");
                clientSocket.Send(requestBuffer);
            }
            catch
            {
                logs.AppendText("Something went wrong while retrieving the list of followers!\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sweetID = textBox1.Text;
            string requesttype = "SwtDlt";


            Byte[] requestBuffer = Encoding.Default.GetBytes(requesttype + sweetID);
            clientSocket.Send(requestBuffer);
        }
    }
}
