using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace DonotWasteTime
{
    public partial class Form1 : Form
    {
        public System.Management.ManagementEventWatcher mgmtWtch;
        public Dictionary<string,DagerousProcessModel> blackDict;


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

            if (this.blackDict == null)
            {
                this.LoadWasteTimeProcess();
            }
            
            if (this.blackDict.ContainsKey(prcessName))
            {
                this.ChooseByYourSelf(KillProcessByName,ShowMessages.是否要关闭这个邪恶的进程, blackDict[prcessName].ProcessName);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mgmtWtch.Stop();
        }


        /// <summary>
        /// 重新加载黑名单列表
        /// </summary>
        public void LoadWasteTimeProcess()
        {
            MessageBox.Show("加载黑名单列表");
            this.blackDict = new Dictionary<string,DagerousProcessModel>();
            this.blackDict.Add("11Game.exe",new DagerousProcessModel("11Game.exe","11Game"));
        }

        /// <summary>
        /// 根据进程名称关闭进程
        /// </summary>
        /// <param name="processName">进程名</param>
        private void KillProcessByName(string processName)
        {
            var processes = Process.GetProcessesByName(processName);

            if(processes != null)
            {
                foreach ( var process in processes)
                {
                    process.Kill();
                }
            }
        }

        /// <summary>
        /// 关或者不关，选择权就在那儿！
        /// </summary>
        /// <param name="abortAction">中止行为操作</param>
        /// <param name="showMessage">显示话术</param>
        /// <param name="processName">进程名称</param>
        private void ChooseByYourSelf(Action<string> abortAction,ShowMessages showMessage, string processName)
        {
            DialogResult dialogResult = MessageBox.Show(showMessage.ToString(), "战胜自己！你可以的！", MessageBoxButtons.AbortRetryIgnore);
            if (dialogResult == DialogResult.Abort)
            {
                abortAction(processName);
                MessageBox.Show(ShowMessages.成功的抵制了诱惑.ToString() + "!");
            }
            else if (dialogResult == DialogResult.Ignore)
            {
                if( (int)showMessage < 2)
                {
                    ChooseByYourSelf(abortAction, (ShowMessages)((int)showMessage + 1), processName);
                }
                else
                {
                    MessageBox.Show(ShowMessages.你太让我失望了.ToString());
                }
            }
            else if ( dialogResult == DialogResult.Retry)
            {
                ChooseByYourSelf(abortAction, showMessage, processName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ChooseByYourSelf(KillProcessByName, ShowMessages.是否要关闭这个邪恶的进程, "11Game.exe");
        }
    }
}
