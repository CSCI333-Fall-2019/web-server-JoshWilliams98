using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anton
{

    main.listener = new TcpListener(IPAddress.Any, main.port);
    main.listener.Start();

    while (main.IsRunning)
    {
        try
        {
            TcpClient client = main.listener.AcceptTcpClient();
            ThreadPool.QueueUserWorkItem(GetRequestedItem, client);
        }

        catch(Exception ex)
        {
            Console.WriteLine("Error recieved: " + ex.message);
        }
    }

    private bool _isRunning;
    static readonly object _isRunningLock = new Object();
    public bool IsRunning
    {
        get
        {
            lock (_isRunningLock)
            {
                return this._isRunning;
            }
        }
        set
        {
            lock (_isRunningLock)
            {
                this._isRunning = value;
            }
            cmdStart.Enabled = !value;
            cmdStop.Enabled = value;
        } 
    }
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Start";
            button2.Text = "Stop";
                
        }

        private void AntonIntestine(object input)
        {
            Thread.Sleep(5000);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            ThreadPool.QueueUserWorkItem(AntonIntestine, this);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;
        }
    }
}
