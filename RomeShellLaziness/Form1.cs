using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Input;
using SlimDX.DirectInput;
using WindowsInput.Native;
using WindowsInput;




namespace RomeShellLaziness
{
    public partial class Form1 : Form
    {
        #region ChangeWindow
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);
        static String ProcWindow = "RomeTW";
        private static void RomeTWWindowSwitch()
        {
            Process[] procs = Process.GetProcessesByName(ProcWindow);
            foreach (Process proc in procs)
            {
                //switch to process by name
                SwitchToThisWindow(proc.MainWindowHandle, true);
            }
        }
        #endregion

        public InputSimulator sim = new InputSimulator();
        
       

        public Form1()
        {
            InitializeComponent();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
           
           
            string command = "give_trait";

            string Name = cmbName.Text + " ";
            string SurName = cmbSurName.Text;
            string traitWhole = cmbTrait.Text;
            string TotalName = "\"" + string.Concat(Name, SurName) + "\"" ;
          
            string[] traitsplit = traitWhole.Split(' ');


            string traitName = "\"" + traitsplit[0] + "\"";
            string traitLevel = traitsplit[1];


            //string fullCommand = command + " " + "\"" + TotalName + "\""  + " " + traitName + " " + traitLevel  ;
            RomeTWWindowSwitch();

            sim.Keyboard.TextEntry(command + " " + TotalName + " " + traitName + " " + traitLevel);
            
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
