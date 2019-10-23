using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace ScriptTest
{
    class Program
    {

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);
        static String ProcWindow = "RomeTW";
        private static void switchToWechart()
        {
            Process[] procs = Process.GetProcessesByName(ProcWindow);
            foreach (Process proc in procs)
            {
                //switch to process by name
                SwitchToThisWindow(proc.MainWindowHandle, true);
            }
        }

        static void Main(string[] args)
        {
            InputSimulator sim = new InputSimulator();
            
            switchToWechart();
            Thread.Sleep(200);
            
            
            



        }
    }
}
