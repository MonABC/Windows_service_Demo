using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService01090822
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteLog("thoi gian bat dau chay: " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(TimeInterval);
            timer.Interval = 10000;
            timer.Enabled = true;
            
        }
        private void TimeInterval(object source, ElapsedEventArgs e)
        {
            WriteLog("thoi gian lap lai cua service: "+ DateTime.Now);
        }

        protected override void OnStop()
        {
            WriteLog("thoi gian dung service: " + DateTime.Now);
        }

        private void WriteLog(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
            if (!System.IO.Directory.Exists(path)) 
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string file = path + DateTime.Now.ToString("dd-MM-yyyy") +".txt";
            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = File.CreateText(file))
                {
                    sw.WriteLine(message);  
                }

            }
            else
            {
                using (StreamWriter sw = File.AppendText(file))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
