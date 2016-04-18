using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonotWasteTime
{
    /// <summary>
    /// 描述需要隔离的进程的相关设置
    /// </summary>
    public class DagerousProcessModel
    {
        public string FullName { get; private set; }

        public string ProcessName { get; private set; }

        public DagerousProcessModel(string fullName, string processName)
        {
            this.FullName = fullName;
            this.ProcessName = processName;
        }
    }
}
