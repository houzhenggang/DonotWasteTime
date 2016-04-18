
using System;
using System.Collections.Generic;
using System.Management;


namespace DonotWasteTime.BLL
{ 
    /// <summary>
    /// 监控是否有浪费时间的进程
    /// </summary>
    public class WasteTimeMonitor
    {
        public ManagementEventWatcher mgmtWtch;

        //public event Warn;

        /// <summary>
        /// 监控
        /// </summary>
        public void Monitoring()
        {
            mgmtWtch = new ManagementEventWatcher("Select * From Win32_ProcessStartTrace");
            mgmtWtch.EventArrived += new EventArrivedEventHandler(mgmtWtch_EventArrived);
            mgmtWtch.Start();
        }

        private void mgmtWtch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine((string)e.NewEvent["ProcessName"]);
        }

        private void CheckingBlackListEvent(object sender, EventArrivedEventArgs e)
        {
            var blackProcessSet = this.GetWasteTimeProcess();
            if (blackProcessSet.Contains((string)e.NewEvent["PrcessName"]))
            {

            }
        }


        /// <summary>
        /// 获得 受限进程列表  
        /// </summary>
        /// <returns></returns>
        private HashSet<string> GetWasteTimeProcess()
        {
            var set = new HashSet<string>();
            set.Add("11Game.exe");
            return set;
        }

    }
}
