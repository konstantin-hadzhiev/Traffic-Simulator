using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficSimulation
{
    public partial class CrossingOptionsForm : Form
    {
        private Crossing cr;
        private List<TextBox> TBGroup1;
        private List<TextBox> TBGroup2;
        private List<TextBox> TBGroup3;
        private List<TextBox> TBGroup4;
        private List<TextBox> CarFrequencyTextboxes;

        public CrossingOptionsForm(Crossing cr)
        {
            InitializeComponent();
            label25.Hide();
            TBGroup1 = new List<TextBox>();
            TBGroup2 = new List<TextBox>();
            TBGroup3 = new List<TextBox>();
            TBGroup4 = new List<TextBox>();
            this.cr = cr;
            pictureBox1.Image = cr.Pb_Background.Image;

            this.LoadCarsPerMinute();
            this.LoadPercentages();
            this.DisableTextBox();
            this.GetTrafficLightGroupsDuration();
            this.InitializeTBGroups();

            TBgroup1.Leave += TBgroup_Leave;
            TBgroup2.Leave += TBgroup_Leave;
            TBgroup3.Leave += TBgroup_Leave;
            TBgroup4.Leave += TBgroup_Leave;

            if(cr is CrossingT1)
            {
                HideCT1();
            }
        }

        /// <summary>
        /// Initializes the CarPerMinute buttons
        /// </summary>
        private void InitializeCPMButtons()
        {
            TBcarsToEast.KeyUp += cpmValues;
            TBcarsToNorth.KeyUp += cpmValues;
            TBcarsToSouth.KeyUp += cpmValues;
            TBcarsToWest.KeyUp += cpmValues;
            TBcarsToEast.Leave += tb_Leave;
            TBcarsToNorth.Leave += tb_Leave;
            TBcarsToSouth.Leave += tb_Leave;
            TBcarsToWest.Leave += tb_Leave;
        }

        private void InitializeTBGroups()
        {
            TBGroup1.Add(TBToSouthRight);
            TBGroup1.Add(TBToSouthStraight);
            TBGroup1.Add(TBToSouthLeft);
            TBGroup2.Add(TBToWestRight);
            TBGroup2.Add(TBToWestStraight);
            TBGroup2.Add(TBToWestLeft);
            TBGroup3.Add(TBToNorthRight);
            TBGroup3.Add(TBToNorthStraight);
            TBGroup3.Add(TBToNorthLeft);
            TBGroup4.Add(TBToEastRight);
            TBGroup4.Add(TBToEastStraight);
            TBGroup4.Add(TBToEastLeft);

            foreach (TextBox tb in TBGroup1)
            {
                tb.KeyUp += tb_KeyUp;
                tb.Leave += tb_Leave;
            } 
            foreach (TextBox tb in TBGroup2)
            {
                tb.KeyUp += tb_KeyUp;
                tb.Leave += tb_Leave;
            } 
            foreach (TextBox tb in TBGroup3)
            {
                tb.KeyUp += tb_KeyUp;
                tb.Leave += tb_Leave;
            } 
            foreach (TextBox tb in TBGroup4)
            {
                tb.KeyUp += tb_KeyUp;
                tb.Leave += tb_Leave;
            }
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "0";
            }
            label25.Hide();
        }

        /// <summary>
        /// Checks if a pressed key on the keyboard is a digit
        /// </summary>
        /// <param name="key">The key to be checked</param>
        /// <returns></returns>
        public static bool IsKeyADigit(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9);
        }

        private void tb_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = ((TextBox)sender);

            if (IsKeyADigit(e.KeyCode))
            {
                int tag = Convert.ToInt32(tb.Tag);

                if (tag == 0)
                {
                    CheckSumOfGroupmates(tb, TBGroup1);
                }
                else if (tag == 1)
                {
                    CheckSumOfGroupmates(tb, TBGroup2);
                }
                else if (tag == 2)
                {
                    CheckSumOfGroupmates(tb, TBGroup3);
                }
                else if (tag == 3)
                {
                    CheckSumOfGroupmates(tb, TBGroup4);
                }
            }
            else
            {
             string text = tb.Text;
                tb.Text = "";
                for (int i = 0; i < text.Length - 1; i++)
                {
                    tb.Text += text[i];
                }
            }
            tb.Select(tb.TextLength, 0);
        }

        /// <summary>
        /// Check the values of the 3 texboxes for each lane  if the sum is 100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="TBGroup"></param>
        private void CheckSumOfGroupmates(TextBox sender, List<TextBox> TBGroup)
        {
            int sum = 0;
            int subSum = 0;
            foreach (TextBox tBox in TBGroup)
            {
                sum += Convert.ToInt32(tBox.Text);
            } 
            if (sum > 100)
            {
                foreach (TextBox tbb in TBGroup)
                {
                    if (tbb != sender)
                    {
                        subSum += Convert.ToInt32(tbb.Text);
                    }
                }
                sender.Text = (100 - subSum).ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckTBGRoupsSum())
            {
                SetCarsPerMinute();
                SetPercentages();
                SetTrafficLightGroupsDuration();

                this.Close();
            }
            else
            {
                label25.Show();
            }
        }

        private bool CheckTBGRoupsSum()
        {
            if (CheckTBGroupSum(TBGroup2))
            {
                if (CheckTBGroupSum(TBGroup4))
                {
                    if (cr is CrossingT1)
                    {
                        return true;
                    }
                    else
                    {
                        if (CheckTBGroupSum(TBGroup1))
                        {
                            if (CheckTBGroupSum(TBGroup3))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CheckTBGroupSum(List<TextBox> TBGroup)
        {
            int sum = 0;
            foreach (TextBox tb in TBGroup)
            {
                sum += Convert.ToInt32(tb.Text);
            }
            if (sum == 100)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the percentages of incoming cars for each direction(left, straight, right)
        /// </summary>
        public void SetPercentages()
        {
            int[,] perc = cr.Percentages;
            perc[0, 0] = Convert.ToInt32(TBToSouthRight.Text);
            perc[0, 1] = Convert.ToInt32(TBToSouthStraight.Text);
            perc[0, 2] = Convert.ToInt32(TBToSouthLeft.Text);
            perc[1, 0] = Convert.ToInt32(TBToWestRight.Text);
            perc[1, 1] = Convert.ToInt32(TBToWestStraight.Text);
            perc[1, 2] = Convert.ToInt32(TBToWestLeft.Text);
            perc[2, 0] = Convert.ToInt32(TBToNorthRight.Text);
            perc[2, 1] = Convert.ToInt32(TBToNorthStraight.Text);
            perc[2, 2] = Convert.ToInt32(TBToNorthLeft.Text);
            perc[3, 0] = Convert.ToInt32(TBToEastRight.Text);
            perc[3, 1] = Convert.ToInt32(TBToEastStraight.Text);
            perc[3, 2] = Convert.ToInt32(TBToEastLeft.Text);           
        }

        public void LoadPercentages()
        {
            int[,] perc = cr.Percentages;

            TBToSouthRight.Text = perc[0, 0].ToString();
            TBToSouthStraight.Text = perc[0, 1].ToString();
            TBToSouthLeft.Text = perc[0, 2].ToString();

            TBToWestRight.Text = perc[1, 0].ToString();
            TBToWestStraight.Text = perc[1, 1].ToString();
            TBToWestLeft.Text = perc[1, 2].ToString();

            TBToNorthRight.Text = perc[2, 0].ToString();
            TBToNorthStraight.Text = perc[2, 1].ToString();
            TBToNorthLeft.Text = perc[2, 2].ToString();

            TBToEastRight.Text = perc[3, 0].ToString();
            TBToEastStraight.Text = perc[3, 1].ToString();
            TBToEastLeft.Text = perc[3, 2].ToString();
        }

        /// <summary>
        /// Sets the amound of incoming cars in one crossing per minute
        /// </summary>
        public void SetCarsPerMinute()
        {
            CarFrequencyTextboxes = new List<TextBox>();
            CarFrequencyTextboxes.Add(TBcarsToNorth);
            CarFrequencyTextboxes.Add(TBcarsToEast);
            CarFrequencyTextboxes.Add(TBcarsToSouth);
            CarFrequencyTextboxes.Add(TBcarsToWest);

            for (int i = 0; i < CarFrequencyTextboxes.Count; i++)
            {
                int num;
                bool isNum = int.TryParse(CarFrequencyTextboxes[i].Text.Trim(), out num);
                if (isNum)
                {
                    cr.CarFrequency[i] = 60 / Convert.ToInt32(CarFrequencyTextboxes[i].Text) * 1000;
                }
            }
        }

        public void LoadCarsPerMinute()
        {
            TBcarsToNorth.Text = (60 / (cr.CarFrequency[0] / 1000)).ToString();
            TBcarsToEast.Text = (60 / (cr.CarFrequency[1] / 1000)).ToString();
            TBcarsToSouth.Text = (60 / (cr.CarFrequency[2] / 1000)).ToString();
            TBcarsToWest.Text = (60 / (cr.CarFrequency[3] / 1000)).ToString();
        }

        public void HideCT1()
        {
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();

            TBToSouthRight.Hide();
            TBToSouthStraight.Hide();
            TBToSouthLeft.Hide();
            TBToNorthRight.Hide();
            TBToNorthLeft.Hide();
            TBToNorthStraight.Hide();

            label21.Hide();
            TBgroup4.Hide();
        }

        public void DisableTextBox()
        {
            if (cr.IncomingStreams[0] == "")
            {
                TBcarsToNorth.Text = "Not endlane";
                TBcarsToNorth.Enabled = false;
            }
            if (cr.IncomingStreams[1] == "")
            {
                TBcarsToEast.Text = "Not endlane";
                TBcarsToEast.Enabled = false;
            }
            if (cr.IncomingStreams[2] == "")
            {
                TBcarsToSouth.Text = "Not endlane";
                TBcarsToSouth.Enabled = false;
            }
            if (cr.IncomingStreams[3] == "")
            {
                TBcarsToWest.Text = "Not endlane";
                TBcarsToWest.Enabled = false;
            }
        }

        public void GetTrafficLightGroupsDuration()
        {
            TBgroup1.Text = (cr.Groups.DurationGroup1 / 1000).ToString();
            TBgroup2.Text = (cr.Groups.DurationGroup2 / 1000).ToString();
            TBgroup3.Text = (cr.Groups.DurationGroup3 / 1000).ToString();
            TBgroup4.Text = (cr.Groups.DurationGroup4 / 1000).ToString();
        }

        public void SetTrafficLightGroupsDuration()
        {
            cr.Groups.DurationGroup1 = Convert.ToInt32(TBgroup1.Text) * 1000;
            cr.Groups.DurationGroup2 = Convert.ToInt32(TBgroup2.Text) * 1000;
            cr.Groups.DurationGroup3 = Convert.ToInt32(TBgroup3.Text) * 1000;
            cr.Groups.DurationGroup4 = Convert.ToInt32(TBgroup4.Text) * 1000;
        }

        private void TBgroup1_Enter(object sender, EventArgs e)
        {
            if (cr is CrossingT1)
            {
                pictureBox1.Image = TrafficSimulation.Properties.Resources.Crossing1group1;
            }
            else
            {
                pictureBox1.Image = TrafficSimulation.Properties.Resources.Crossing2group1;
            }
        }

        private void TBgroup2_Enter(object sender, EventArgs e)
        {
            if (cr is CrossingT1)
            {
                pictureBox1.Image = TrafficSimulation.Properties.Resources.Crossing1group2;
            }
            else
            {
                pictureBox1.Image = TrafficSimulation.Properties.Resources.Crossing2group2;
            }
        }

        private void TBgroup3_Enter(object sender, EventArgs e)
        {
            if (cr is CrossingT1)
            {
                pictureBox1.Image = TrafficSimulation.Properties.Resources.Crossing1group3;
            }
            else
            {
                pictureBox1.Image = TrafficSimulation.Properties.Resources.Crossing2group3;
            }
        }

        private void TBgroup4_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = TrafficSimulation.Properties.Resources.Crossing2group4;
        }

        private void TBgroup_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = cr.Pb_Background.Image;
        }

        private void cpmValues(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (IsKeyADigit(e.KeyCode))
            {
               
                int number = Convert.ToInt32(tb.Text);
                if (number > 30)
                {
                    number = 30;
                    tb.Text = number.ToString();
                }
                
            }
            else
            {
                string text = tb.Text;
                tb.Text = "";
                for (int i = 0; i < text.Length - 1; i++)
                {
                    tb.Text += text[i];
                }
            }
            tb.Select(tb.TextLength, 0);
        }
    }
}
