using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    
    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> clientNames = new List<string>(); //LIST FOR THE CLIENT NAMES TO GUARANTEE UNIQUENESS
        List<string> registerednames = File.ReadAllLines(@"D:\C KOPYA\Desktop\Lab1\server\server\user-db.txt").ToList();
        List<string> sweets = new List<string>();
        List<string> operations = new List<string>();
        List<string> followedUsers = new List<string>();
        int id = 0;


        bool terminating = false;
        bool listening = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;
            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;
                textBox_message.Enabled = true;
                button_send.Enabled = true;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while(listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    Byte[] name_buffer = new Byte[1024];
                    newClient.Receive(name_buffer); //RECEIVES THE NAME OF THE CLIENT 
                    

                    string receivedName = Encoding.Default.GetString(name_buffer);
                    receivedName = receivedName.Substring(0, receivedName.IndexOf("\0"));

                    //-----MESSAGE FOR THE VALIDITY OF THE USERNAME------------//
                    Byte[] usernameAckBuffer = new Byte[4];
                    string usernameAckMessage = "";

                    if (clientNames.Contains(receivedName))
                    {
                        logs.AppendText("Client <" + receivedName + "> is already connected!\n");
                        usernameAckMessage = "0";
                        usernameAckBuffer = Encoding.Default.GetBytes(usernameAckMessage);
                        newClient.Send(usernameAckBuffer);

                        newClient.Close();
                    }
                    else if (registerednames.Contains(receivedName)==false)
                    {
                        logs.AppendText("Client <" + receivedName + "> is not a valid username!\n");
                        usernameAckMessage = "2";
                        usernameAckBuffer = Encoding.Default.GetBytes(usernameAckMessage);
                        newClient.Send(usernameAckBuffer);

                        newClient.Close();
                    }
                    else
                    {
                        clientSockets.Add(newClient);
                        clientNames.Add(receivedName);
                        logs.AppendText("Client <" + receivedName + "> is connected now!\n");
                        usernameAckMessage = "1";
                        usernameAckBuffer = Encoding.Default.GetBytes(usernameAckMessage);
                        newClient.Send(usernameAckBuffer);
                        Thread receiveThread = new Thread(() => Receive(newClient, receivedName)); // updated
                        receiveThread.Start();
                        
                    }
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient, string clientName) // updated ---- HER CLIENT A FARKLI VERİ GÖNDERMEK İSTİYORSAK BURADAN SONRA EKLEMELİYİZ.
        {
            bool connected = true;
            List<string> copyuserlist = new List<string>(registerednames); // DB Yİ KOPYALIYORUZ, HER CLIENT A FARKLI BİR KOPYA GÖNDERMEMİZE YARIYOR.
            List<string> copyfolloweduser = new List<string>(followedUsers);
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0")); //decision of what client wants to do
                    //logs.AppendText(incomingMessage);
                    if (incomingMessage == "Rtrv") //THIS IS THE COMMAND FOR RETRIEVING THE LIST OF USERNAMES THAT ARE UPLOADED BY THE REQUESTOR CLIENT
                    {
                        RetrieveUserList(thisClient, clientName);
                    }
                    else if (incomingMessage.StartsWith("FlwUser")) //THIS IS THE COMMAND TO FOLLOW
                    {
                        //logs.AppendText("wehere");
                        FollowUser(thisClient, clientName, incomingMessage, copyuserlist,copyfolloweduser);
                    }
                    else if (incomingMessage == "gtswt") //THIS IS THE COMMAND TO GETSWEETS
                    {
                        GetSweets(thisClient, clientName);
                    }
                    else if (incomingMessage == "gtfswt") //THIS IS THE COMMAND TO GETSWEETS
                    {
                        GetSweetsFollowed(thisClient, clientName, copyfolloweduser);
                    }
                    else if (incomingMessage.StartsWith("BlckUser")) //THIS IS THE COMMAND TO BLOCK USER
                    {
                        BlockUser(thisClient, clientName, incomingMessage, copyuserlist, copyfolloweduser);
                    }
                    else if (incomingMessage.StartsWith("seflwd")) //THIS IS THE COMMAND TO SEE FOLLOWED USERS
                    {
                        SeeFollowed(thisClient, clientName, copyfolloweduser);
                    }
                    else if (incomingMessage.StartsWith("seflwrs")) //THIS IS THE COMMAND TO SEE FOLLOWED USERS
                    {
                        SeeFollowers(thisClient, clientName, copyfolloweduser);
                    }
                    else if (incomingMessage.StartsWith("SwtDlt"))
                    {
                        SweetDelete(thisClient, incomingMessage);
                    }
                    else
                    {
                        logs.AppendText("ID: " + id + clientName + ": " + incomingMessage + " |   " + DateTime.Now + "\n");
                        sweets.Add("ID: "+id+" | "+clientName+": "+incomingMessage+" |   "+DateTime.Now + "\n");
                        id = id + 1;
                    }
                }
                catch
                {
                    if(!terminating)
                    {
                        logs.AppendText("Client < " + clientName + " > has disconnected.\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    clientNames.Remove(clientName);
                    connected = false;
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string message = textBox_message.Text;
            string msgprefix = "msg";
            if(message != "" && message.Length <= 64)
            {
                Byte[] buffer = Encoding.Default.GetBytes(msgprefix+message);
                foreach (Socket client in clientSockets)
                {
                    try
                    {
                        client.Send(buffer);
                        
                    }
                    catch
                    {
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                        textBox_port.Enabled = true;
                        button_listen.Enabled = true;
                        serverSocket.Close();
                    }

                }
            }
        }

        private void SweetDelete(Socket thisClient, string message)
        {
            string sweetID = message.Remove(0, 6);
            bool sweetDeleted = false;
            string infoToBeSent = "blcInfo";
            for (var i = 0; i < sweets.Count; i++)
            {
                var idEndPos = sweets[i].IndexOf("|") - 1;
                var dummyID = sweets[i].Substring(4, idEndPos - 4);
                if (sweets[i].Substring(4, idEndPos - 4) == sweetID)
                {
                    sweetDeleted = true;
                    sweets.RemoveAt(i);
                }
            }

            if (sweetDeleted == false)
            {
                infoToBeSent += "No sweet found with " + sweetID + "";
                logs.AppendText(" Sweet ID: " + sweetID + " is not in database.\n");

            }
            else
            {
                infoToBeSent += "Sweet with " + sweetID + " deleted";
                logs.AppendText(" Sweet ID: " + sweetID + " deleted.\n");
            }

            Byte[] retrieveBuffer = Encoding.Default.GetBytes(infoToBeSent);
            thisClient.Send(retrieveBuffer);


        }

        private void logs_TextChanged(object sender, EventArgs e)
        {

        }
        private void RetrieveUserList(Socket thisClient, string clientName)
        {
            if (!terminating)
            {
                try
                {
                    //----------NECESSARY INITIALIZATIONS TO READ THE DATABASE LINE BY LINE---------------//
                    var lines = File.ReadLines(@"D:\C KOPYA\Desktop\Lab1\server\server\user-db.txt");
                    string infoToBeSent = "usrlist";
                    foreach (var line in lines)
                    {
                        infoToBeSent += line + "\n";
                    }



                        //-------------SEND THE LIST OF USERNAMES UPLOADED BY THE REQUESTOR CLIENT----------------//
                    Byte[] retrieveBuffer = Encoding.Default.GetBytes(infoToBeSent);
                    thisClient.Send(retrieveBuffer);
                }
                catch (Exception e)
                {
                    logs.AppendText(DateTime.Now + " | Something went wrong while sending the list of usernames!\n");
                }
            }
        }

        private void FollowUser(Socket thisClient, string clientName, string message, List<string> userlist, List<string> copyfollow)
        {
            //logs.AppendText("here");
            if (!terminating)
            {
                try
                {
                    bool blckd = false;
                    string infoToBeSent = "flwrq";
                    message = message.Remove(0, 7);
                    foreach (var line in operations)
                    {
                        if(line.Contains(message + " blocked " + clientName))
                        {
                            blckd = true;
                        }
                    }
                    //logs.AppendText("Sent username:"+message);
                    if (userlist.Contains(message) & message != clientName & blckd == false)
                    {
                        infoToBeSent += message + " followed \n";
                        logs.AppendText(clientName + " followed " + message+ "\n");
                        operations.Add(clientName + " followed " + message + "\n");
                        userlist.Remove(message);
                        copyfollow.Add(message);
                    }
                    else if (blckd == true)
                    {
                        infoToBeSent += message + " is blocked you so you can not follow! \n";
                    }
                    else
                    {
                        infoToBeSent += message + " can not be followed. \n";
                        //logs.AppendText("Sent but -");
                    }

                    Byte[] retrieveBuffer = Encoding.Default.GetBytes(infoToBeSent);
                    thisClient.Send(retrieveBuffer);
                }
                catch (Exception e)
                {
                    logs.AppendText(DateTime.Now + " | Something went wrong while following!\n");
                }
            }
        }
        private void BlockUser(Socket thisClient, string clientName, string message, List<string> userlist, List<string> copyblock)
        {
            //logs.AppendText("here");
            if (!terminating)
            {
                try
                {
                    string infoToBeSent = "blckrq";
                    message = message.Remove(0, 8);
                    //logs.AppendText("Sent username:"+message);
                    if (userlist.Contains(message) & message != clientName)
                    {
                        infoToBeSent += message + " blocked \n";
                        logs.AppendText(clientName + " blocked " + message + "\n");
                        operations.Add(clientName + " blocked " + message + "\n");
                        userlist.Remove(message);
                        copyblock.Add(message);
                    }
                    else
                    {
                        infoToBeSent += message + " can not be blocked. \n";
                        //logs.AppendText("Sent but -");
                    }

                    Byte[] retrieveBuffer = Encoding.Default.GetBytes(infoToBeSent);
                    thisClient.Send(retrieveBuffer);
                }
                catch (Exception e)
                {
                    logs.AppendText(DateTime.Now + " | Something went wrong while blocking!\n");
                }
            }
        }

        private void GetSweets(Socket thisClient, string clientName)
        {
            if (!terminating)
            {
                try
                {
                    string infoToBeSent = "swts";
                    foreach (var line in sweets)
                    {
                        infoToBeSent += line + "\n";
                    }



                    //-------------SEND THE LIST OF SWEETS----------------//
                    Byte[] retrieveBuffer = Encoding.Default.GetBytes(infoToBeSent);
                    thisClient.Send(retrieveBuffer);
                }
                catch (Exception e)
                {
                    logs.AppendText(DateTime.Now + " | Something went wrong while sending the list of usernames!\n");
                }
            }
        }

        private void GetSweetsFollowed(Socket thisClient, string clientName, List<string> flwuser)
        {
            if (!terminating)
            {
                try
                {
                    string infoToBeSent = "flwswts";
                    foreach (var z in operations)
                    {
                        if (z.Contains(" blocked " + clientName))
                        {
                            string user = z.Replace(" blocked " + clientName + "\n", "");
                            flwuser.Remove(user);
                        }
                    }
                    foreach (var line in sweets)
                    {
                        if (flwuser.Any(line.Contains))
                        {
                            infoToBeSent += line + "\n";
                        }
                    }



                    //-------------SEND THE LIST OF SWEETS----------------//
                    Byte[] retrieveBuffer = Encoding.Default.GetBytes(infoToBeSent);
                    thisClient.Send(retrieveBuffer);
                }
                catch (Exception e)
                {
                    logs.AppendText(DateTime.Now + " | Something went wrong while sending the list of usernames!\n");
                }
            }
        }
        private void SeeFollowed(Socket thisClient, string clientName, List<string> flwuser)
        {
            if (!terminating)
            {
                try
                {
                    string infoToBeSent = "seflwd";
                    foreach (var z in operations)
                    {
                        if (z.Contains(" blocked " + clientName))
                        {
                            string user = z.Replace(" blocked " + clientName +"\n", "");
                            flwuser.Remove(user);
                        }
                    }
                    foreach (var line in flwuser)
                    {
                        infoToBeSent += line + "\n";
                    }



                    //-------------SEND THE LIST OF SWEETS----------------//
                    Byte[] retrieveBuffer = Encoding.Default.GetBytes(infoToBeSent);
                    thisClient.Send(retrieveBuffer);
                }
                catch (Exception e)
                {
                    logs.AppendText(DateTime.Now + " | Something went wrong while sending the list of usernames!\n");
                }
            }
        }
        private void SeeFollowers(Socket thisClient, string clientName, List<string> flwuser)
        {
            if (!terminating)
            {
                try
                {
                    string infoToBeSent = "seflwrs";
                    foreach (var z in operations)
                    {
                        if (z.Contains(" followed " + clientName))
                        {
                            string user = z.Replace(" followed " + clientName + "\n", "");
                            flwuser.Add(user);
                        }
                    }
                    foreach (var line in flwuser)
                    {
                        infoToBeSent += line + "\n";
                    }
                    flwuser.Clear();


                    //-------------SEND THE LIST OF SWEETS----------------//
                    Byte[] retrieveBuffer = Encoding.Default.GetBytes(infoToBeSent);
                    thisClient.Send(retrieveBuffer);
                }
                catch (Exception e)
                {
                    logs.AppendText(DateTime.Now + " | Something went wrong while sending the list of usernames!\n");
                }
            }
        }


        private void textBox_port_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
