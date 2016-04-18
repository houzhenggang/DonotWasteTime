using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DonotWasteTime
{
    public partial class Form1 : Form
    {
        public System.Management.ManagementEventWatcher mgmtWtch;
        public HashSet<string> blackSet;


        public Form1()
        {
            InitializeComponent();
            mgmtWtch = new System.Management.ManagementEventWatcher("Select * From Win32_ProcessStartTrace");
            mgmtWtch.EventArrived += new System.Management.EventArrivedEventHandler(CheckingBlackListEvent);
            mgmtWtch.Start();
        }

        void mgmtWtch_EventArrived(object sender, System.Management.EventArrivedEventArgs e)
        {
            MessageBox.Show((string)e.NewEvent["ProcessName"]);
        }

        private void CheckingBlackListEvent(object sender, System.Management.EventArrivedEventArgs e)
        {
            var prcessName = (string)e.NewEvent["ProcessName"];
            MessageBox.Show(prcessName);
            if (this.blackSet == null)
            {
                this.LoadWasteTimeProcess();
            }
            
            if (this.blackSet.Contains(prcessName))
            {
                MessageBox.Show("你能通过游戏赚钱吗！不能还玩什么！");
            }
            else
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mgmtWtch.Stop();
        }


        /// <summary>
        /// 获得 受限进程列表  
        /// </summary>
        /// <returns></returns>
        public void LoadWasteTimeProcess()
        {
            MessageBox.Show("加载黑名单列表");
            this.blackSet = new HashSet<string>();
            this.blackSet.Add("11Game.exe");
          
        }
    }
}
