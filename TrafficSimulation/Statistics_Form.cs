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
    public partial class Statistics_Form : Form
    {
        Grid grid;
        List<Label> labels;

        public Statistics_Form(Grid grid)
        {
            this.grid = grid;
            this.labels = new List<Label>();
            InitializeComponent();
            timer1.Start();
            InitializeLabels();
            tb_junction.KeyUp += CheckTextbox;
        }

        public void InitializeLabels()
        {
            labels.Add(label1);
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);
            labels.Add(label6);
            labels.Add(label7);
            labels.Add(label8);
            labels.Add(label9);
            labels.Add(label10);
            labels.Add(label11);
            labels.Add(label12);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < grid.Cells.Count; i++)
            {
                int nrOfCars = 0;
                if (grid.Cells[i].Taken)
                {
                    Crossing cr = grid.Cells[i].Crossing;
                    foreach (Lane lane in cr.LaneInList)
                    {
                        nrOfCars += lane.CarList.Count;
                    }
                    foreach (Lane lane in cr.LaneOutList)
                    {
                        nrOfCars += lane.CarList.Count;
                    }
                    labels[i].Text = "Cars: " + nrOfCars;
                    StartChecking(labels[i]);
                    nrOfCars = 0;
                }
                else
                {
                    labels[i].Text = "";
                }
            }
        }

        //sets the junction number for a simulation
        private void btn_save_Click(object sender, EventArgs e)
        {
            grid.Junction = Convert.ToInt32(tb_junction.Text);
        }

        public static bool IsKeyADigit(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9);
        }

        public void CheckTextbox(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (IsKeyADigit(e.KeyCode))
            {
                int number = Convert.ToInt32(tb.Text);              
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
        /// Checks if the number of cars on a crossing is bigger or equal to the given number for junction, 
        /// when it'true the label of the corresponding crossing turns to be red
        /// </summary>
        /// <param name="label"></param>
        public void StartChecking(Label label)
        {
            int currentCars = Convert.ToInt32(label.Text.Remove(0, 6));
            if (currentCars > grid.Junction)
            {
                label.ForeColor = Color.Red;
            }
            else if (currentCars <= grid.Junction)
            {
                label.ForeColor = Color.Black;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
