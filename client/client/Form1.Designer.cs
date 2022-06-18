namespace client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_send = new System.Windows.Forms.Button();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.button_getSweets = new System.Windows.Forms.Button();
            this.button_getusernames = new System.Windows.Forms.Button();
            this.textBox_userfollow = new System.Windows.Forms.TextBox();
            this.button_follow = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button_blckuser = new System.Windows.Forms.Button();
            this.textBox_blckuser = new System.Windows.Forms.TextBox();
            this.button_seefollowers = new System.Windows.Forms.Button();
            this.button_seefollowed = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(97, 15);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(116, 22);
            this.textBox_ip.TabIndex = 2;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(97, 50);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(116, 22);
            this.textBox_port.TabIndex = 3;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(23, 134);
            this.button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(117, 27);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(325, 64);
            this.logs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(397, 320);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            // 
            // textBox_message
            // 
            this.textBox_message.Enabled = false;
            this.textBox_message.Location = new System.Drawing.Point(84, 394);
            this.textBox_message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(129, 22);
            this.textBox_message.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Message:";
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(220, 389);
            this.button_send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(87, 32);
            this.button_send.TabIndex = 8;
            this.button_send.Text = "send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(97, 91);
            this.textBox_Username.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(116, 22);
            this.textBox_Username.TabIndex = 9;
            this.textBox_Username.TextChanged += new System.EventHandler(this.textBox_username_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Username:";
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(175, 134);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(117, 27);
            this.button_disconnect.TabIndex = 11;
            this.button_disconnect.Text = "disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // button_getSweets
            // 
            this.button_getSweets.Enabled = false;
            this.button_getSweets.Location = new System.Drawing.Point(23, 166);
            this.button_getSweets.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_getSweets.Name = "button_getSweets";
            this.button_getSweets.Size = new System.Drawing.Size(117, 27);
            this.button_getSweets.TabIndex = 12;
            this.button_getSweets.Text = "Get Sweets";
            this.button_getSweets.UseVisualStyleBackColor = true;
            this.button_getSweets.Click += new System.EventHandler(this.button_getSweets_Click_1);
            // 
            // button_getusernames
            // 
            this.button_getusernames.Enabled = false;
            this.button_getusernames.Location = new System.Drawing.Point(175, 166);
            this.button_getusernames.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_getusernames.Name = "button_getusernames";
            this.button_getusernames.Size = new System.Drawing.Size(117, 27);
            this.button_getusernames.TabIndex = 13;
            this.button_getusernames.Text = "Get Usernames";
            this.button_getusernames.UseVisualStyleBackColor = true;
            this.button_getusernames.Click += new System.EventHandler(this.button_getusernames_Click_1);
            // 
            // textBox_userfollow
            // 
            this.textBox_userfollow.Location = new System.Drawing.Point(37, 342);
            this.textBox_userfollow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_userfollow.Name = "textBox_userfollow";
            this.textBox_userfollow.Size = new System.Drawing.Size(131, 22);
            this.textBox_userfollow.TabIndex = 14;
            // 
            // button_follow
            // 
            this.button_follow.Enabled = false;
            this.button_follow.Location = new System.Drawing.Point(175, 342);
            this.button_follow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_follow.Name = "button_follow";
            this.button_follow.Size = new System.Drawing.Size(117, 27);
            this.button_follow.TabIndex = 15;
            this.button_follow.Text = "Follow";
            this.button_follow.UseVisualStyleBackColor = true;
            this.button_follow.Click += new System.EventHandler(this.button_follow_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(71, 230);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 27);
            this.button1.TabIndex = 16;
            this.button1.Text = "Get Followed Sweets";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_blckuser
            // 
            this.button_blckuser.Enabled = false;
            this.button_blckuser.Location = new System.Drawing.Point(175, 310);
            this.button_blckuser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_blckuser.Name = "button_blckuser";
            this.button_blckuser.Size = new System.Drawing.Size(117, 27);
            this.button_blckuser.TabIndex = 17;
            this.button_blckuser.Text = "Block User";
            this.button_blckuser.UseVisualStyleBackColor = true;
            this.button_blckuser.Click += new System.EventHandler(this.button_blckuser_Click);
            // 
            // textBox_blckuser
            // 
            this.textBox_blckuser.Location = new System.Drawing.Point(37, 313);
            this.textBox_blckuser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_blckuser.Name = "textBox_blckuser";
            this.textBox_blckuser.Size = new System.Drawing.Size(131, 22);
            this.textBox_blckuser.TabIndex = 18;
            // 
            // button_seefollowers
            // 
            this.button_seefollowers.Enabled = false;
            this.button_seefollowers.Location = new System.Drawing.Point(23, 198);
            this.button_seefollowers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_seefollowers.Name = "button_seefollowers";
            this.button_seefollowers.Size = new System.Drawing.Size(117, 27);
            this.button_seefollowers.TabIndex = 19;
            this.button_seefollowers.Text = "See Followers";
            this.button_seefollowers.UseVisualStyleBackColor = true;
            this.button_seefollowers.Click += new System.EventHandler(this.button_seefollowers_Click);
            // 
            // button_seefollowed
            // 
            this.button_seefollowed.Enabled = false;
            this.button_seefollowed.Location = new System.Drawing.Point(175, 198);
            this.button_seefollowed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_seefollowed.Name = "button_seefollowed";
            this.button_seefollowed.Size = new System.Drawing.Size(117, 27);
            this.button_seefollowed.TabIndex = 20;
            this.button_seefollowed.Text = "See Followed";
            this.button_seefollowed.UseVisualStyleBackColor = true;
            this.button_seefollowed.Click += new System.EventHandler(this.button_seefollowed_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(37, 283);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(131, 22);
            this.textBox1.TabIndex = 21;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(175, 281);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 27);
            this.button3.TabIndex = 22;
            this.button3.Text = "Delete Sweet";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 441);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_seefollowed);
            this.Controls.Add(this.button_seefollowers);
            this.Controls.Add(this.textBox_blckuser);
            this.Controls.Add(this.button_blckuser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_follow);
            this.Controls.Add(this.textBox_userfollow);
            this.Controls.Add(this.button_getusernames);
            this.Controls.Add(this.button_getSweets);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_getSweets;
        private System.Windows.Forms.Button button_getusernames;
        private System.Windows.Forms.TextBox textBox_userfollow;
        private System.Windows.Forms.Button button_follow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_blckuser;
        private System.Windows.Forms.TextBox textBox_blckuser;
        private System.Windows.Forms.Button button_seefollowers;
        private System.Windows.Forms.Button button_seefollowed;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
    }
}

