using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using WindowsInput.Native;
using WindowsInput;
using System.IO;

namespace RomeShellLaziness
{
    public partial class Form1 : Form
    {
        #region ChangeWindow
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);
        static String ProcWindow = "RomeTW";
        private static bool RomeTWWindowSwitch()
        {
            Process[] procs = Process.GetProcessesByName(ProcWindow);
            foreach (Process proc in procs)
            {
                //switch to process by name
                SwitchToThisWindow(proc.MainWindowHandle, true);
                return true;
            }
            return false;
        }
        #endregion
        public InputSimulator sim = new InputSimulator();
        public StreamReader reader;
        public StreamWriter writer;
        
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
            if (RomeTWWindowSwitch())
            {
                sim.Keyboard.TextEntry(command + " " + TotalName + " " + traitName + " " + traitLevel);
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            }
            else
            {
                lblLog.Text = "Rome total war not detected";
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string command = "give_trait";

            string Name = cmbName.Text;
            string SurName = " " + cmbSurName.Text;
            string[] GoodTraits = {"GoodCommander 5", "NaturalMilitarySkill 4", "RhetoricSkill 3"
                                 , "NaturalPhilosophy 3","PlainRomanVirtue 3","PoliticsSkill 3","PhlegmHumour 3"
                                 , "RacesHaterRomanVice 3", "Intelligent 3", "HighPersonalSecurity 3", "GoodSiegeAttacker 5"
                                 , "GoodSiegeDefender 5", "Austere 3", "CounterSpy 4", "WellConnectedWife 3", "GoodAdministrator 5", "Handsome 3"
                                 , "AssassinMaster 4", "SpyMaster 5", "HaleAndHearty 3"};
            for (int i = 0; i < GoodTraits.Length; i++)
            {
                giveTrait(command, Name, SurName, GoodTraits[i]);
            }

            

        }

        void giveTrait(string command, string Name, string SurName, string traitWhole)
        {
            if (SurName == " ")
                SurName = "";
            

            string TotalName = "";
            try
            {
                 TotalName = "\"" + string.Concat(Name, SurName) + "\"";
            }
            catch (Exception)
            {

                 TotalName = "\"" + Name + "\"";
            }


            string[] traitsplit = traitWhole.Split(' ');

            string traitName = "\"" + traitsplit[0] + "\"";
            string traitLevel = traitsplit[1];


            if (RomeTWWindowSwitch())
            {
                sim.Keyboard.KeyPress(VirtualKeyCode.OEM_3);
                sim.Keyboard.TextEntry(command + " " + TotalName + " " + traitName + " " + traitLevel);

                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            }
            else 
            {
                MessageBox.Show("Rome total war not detected");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string command = "give_trait";

            string Name = cmbName.Text;
            string SurName =" " + cmbSurName.Text;
            string[] BadTraits = { "BadAdministrator 4", "BadAttacker 5", "BadBuilder 3", "Cuckold 4", "LostEagle 2"
                                 , "Drink 6", "ExpensiveTastes 3", "Hypochondriac 3", "Inbred 6", "IanR 4"
                                 , "Perverted 6", "PublicAtheism 6", "Xenophilia 4" };
            for (int i = 0; i < BadTraits.Length; i++)
            {
                giveTrait(command, Name, SurName, BadTraits[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string command = "give_trait";

            string Name = cmbName.Text + " ";
            string SurName = cmbSurName.Text;
            string[] BadTraits = {"GoodAssassin 5", "GoodSpy 5" };
            for (int i = 0; i < BadTraits.Length; i++)
            {
                giveTrait(command, Name, SurName, BadTraits[i]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RomeTWWindowSwitch())
            {
                for (int i = 0; i < 500; i++)
                {
                    sim.Keyboard.TextEntry("add_money 40000");

                    sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                    Thread.Sleep(2);
                }
            }
            else
            {
                MessageBox.Show("Rome total war not detected");
            }
        }
        void giveUnit(string cityNaam, string unit)
        {
            string unitF = "\"" + unit + "\"";
            string cityF = "\"" + cityNaam + "\"";
            sim.Keyboard.TextEntry("create_unit " + cityF + " " + unitF + " 1 8 8 8");
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }
        void giveUnit(string FirstName, string LastName, string unit)
        {
            if (LastName == " ")
            {
                LastName = "";
            }

            string TotalName = "";
            try
            {
                TotalName = "\"" + string.Concat(FirstName, LastName) + "\"";
            }
            catch (Exception)
            {

                TotalName = "\"" + FirstName + "\"";
            }
            string unitF = "\"" + unit + "\"";
            sim.Keyboard.TextEntry("create_unit " + TotalName + " " + unitF + " 1 8 8 8");
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }

        private void btnCreateUnits_Click_1(object sender, EventArgs e)
        {
            if (RomeTWWindowSwitch())
            {
                string cityName, Fname, Lname, unit;
                int amount = int.Parse(txtAmount.Text);

                Fname = cmbFirstName.Text + " ";
                Lname = cmbLname.Text;

                if (Lname == "")
                {
                    Fname = cmbFirstName.Text;
                }

                cityName = cmbCity.Text;
                unit = cmbUnit.Text;

                if (cityName != "")
                {
                    for (int i = 0; i < amount; i++)
                    {
                        giveUnit(cityName, unit);
                    }
                }
                else
                {
                    for (int i = 0; i < amount; i++)
                    {
                        giveUnit(Fname, Lname, unit);
                    }
                }
            }
            else 
            {
                MessageBox.Show("Rome total war not detected");
            }
        }

        private void TempTemplateSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void TempLoadTemplate_Click(object sender, EventArgs e)
        {
            
            try
            {
                TempUnitRawInput.Text = "";
                string bestand = TempTemplateSelect.Text;
                reader = new StreamReader(bestand);
                while (!reader.EndOfStream)
                {
                    TempUnitRawInput.Text += reader.ReadLine() + "\n\r";
                }
                lblLog.ForeColor = Color.Green;
                lblLog.Text = "Loaded " + bestand;
                reader.Close();
            }
            catch (Exception)
            {
                lblLog.ForeColor = Color.Red;
                lblLog.Text = "Error, could not load selected file";
                UpdateList();
            }
            
        }

        private void TempSaveTemplate_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("RomeUnitTemplates"))
            {
                Directory.CreateDirectory("RomeUnitTemplates");
            }

            string file = @"RomeUnitTemplates\" + TempTemplateSelect.Text + ".txt";
            writer = new StreamWriter(file);
            
            foreach ( string line in TempUnitRawInput.Text.Split('\n'))
            {
                writer.WriteLine(line);
            }
            writer.Close();
            UpdateList();
            lblLog.ForeColor = Color.Green;
            lblLog.Text = "Saved to " + file;
        }

        private void TempInsertUnit_Click(object sender, EventArgs e)
        {
            try
            {
                string unit = TempUnitSelect.Text;
                int amount = int.Parse(TempUnitAmount.Text);
                for (int i = 0; i < amount; i++)
                {
                    TempUnitRawInput.Text += "create_unit " + "\"" + unit + "\"" + " 1 8 8 8" + "\r\n";
                }
            }
            catch (Exception)
            {
                lblLog.ForeColor = Color.Red;
                lblLog.Text = "Error, invalid input";
            } 
        }
        void UpdateList()
        {
            try
            {
                CreateLoadTemplate.Items.Clear();
                CreateLoadTemplate.Items.AddRange(Directory.GetFiles("RomeUnitTemplates"));

                TempTemplateSelect.Items.Clear();
                TempTemplateSelect.Items.AddRange(Directory.GetFiles("RomeUnitTemplates"));
            }
            catch 
            {
            } 
        }

        private void TemplateClear_Click(object sender, EventArgs e)
        {
            TempUnitRawInput.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string cityName, FirstName, LastName, file;
            file = CreateLoadTemplate.Text;
            FirstName = cmbFirstName.Text + " ";
            LastName = cmbLname.Text;
            cityName = cmbCity.Text;

            if (LastName == "")
            {
                FirstName = cmbFirstName.Text;
            }

            if (LastName == " ")
            {
                LastName = "";
            }

            string TotalName = "";
            try
            {
                TotalName = "\"" + string.Concat(FirstName, LastName) + "\"";
            }
            catch (Exception)
            {
                TotalName = "\"" + FirstName + "\"";
            }
            if (RomeTWWindowSwitch())
            {
                reader = new StreamReader(file);

                if (cityName != "")
                {
                    while (!reader.EndOfStream)
                    {
                        string temp = reader.ReadLine();
                        try
                        {
                            string command = temp.Substring(0, 12) + "\"" + cityName + "\"" + temp.Substring(11);
                            sim.Keyboard.TextEntry(command);
                            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            string temp = reader.ReadLine();
                            string command = temp.Substring(0, 12) + TotalName + temp.Substring(11);
                            sim.Keyboard.TextEntry(command);
                            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                        }
                        catch
                        {
                        }
                    }
                }

                reader.Close();
            }
            else
            {
                MessageBox.Show("Rome total war not detected");
            }
        }
    }
}
