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
    public partial class TrafficSimulator : Form
    {
        Grid grid;
        Point mouseDown;
        bool dragTypeOne;
        int selectedCell = 0;
        int firstSelection = 0;
        bool runningSimulation;

        public TrafficSimulator()
        {
            InitializeComponent();
            runningSimulation = false;
            grid = new Grid();
        }

        //determins which cell the crossing has been dragged to 
        private void determineCell(DragEventArgs e)
        {
            Point cursor = PointToClient(Cursor.Position);
            int column = ((cursor.X - panel1.Left) / 300) + 1;
            int row = (cursor.Y - panel1.Top) / 300;
            selectedCell = row * 4 + column;
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            Point cursor = PointToClient(Cursor.Position);
            Point drawPoint = new Point();

            //Finding out which cell the crossing is dropped on
            determineCell(e);
            drawPoint = grid.Cells[selectedCell - 1].Location;

            //Moving the crossing
            if (e.AllowedEffect == DragDropEffects.Move)
            {
                foreach (Crossing cr in grid.Crossings)
                {
                    if (cr.Location == mouseDown && !grid.Cells[selectedCell - 1].Taken)
                    {
                        cr.Location = drawPoint;
                        // cr.Pb_Background.Location = drawPoint;
                        cr.Pb_Transparent.Location = drawPoint;
                        grid.Cells[selectedCell - 1].Taken = true;
                        grid.Cells[selectedCell - 1].Crossing = grid.Cells[firstSelection - 1].Crossing;
                        grid.Cells[firstSelection - 1].Taken = false;
                        grid.Cells[firstSelection - 1].Crossing = null;
                        //grid.CheckNeighbours(selectedCell - 1);
                        for (int i = 0; i < grid.Cells.Count; i++)
                        {
                            if (grid.Cells[i].Taken)
                            {
                                grid.CheckNeighbours(i);
                            }
                        }
                    }
                }
            }
            else if (!grid.Cells[selectedCell - 1].Taken)
            {
                grid.Cells[selectedCell - 1].Taken = true;
                PictureBox pb_b = new PictureBox();
                pb_b.Height = panel1.Height / 3;
                pb_b.Width = panel1.Width / 4;
                pb_b.SizeMode = PictureBoxSizeMode.StretchImage;
                pb_b.BackColor = Color.Transparent;
                pb_b.MouseDown += new MouseEventHandler(Mouse_Down);
                //  pb_b.Image = (Image)e.Data.GetData(DataFormats.Bitmap);

                //Added transparent layer
                PictureBox pb_t = new PictureBox();
                pb_t.Size = pb_b.Size;
                pb_t.MouseDown += new MouseEventHandler(Mouse_Down);

                //Which crossing will be dragged
                if (dragTypeOne)
                {
                    System.Drawing.Graphics formGraphics = this.CreateGraphics();
                    CrossingT1 cr = new CrossingT1(drawPoint, pb_b, pb_t);
                  //  pb_b.Location = cr.Location;
                    pb_b.Tag = cr;
                    pb_t.Tag = cr;
                    grid.AddCrossing(cr);
                    grid.Cells[selectedCell - 1].Crossing = cr;

                    pb_b.Image = TrafficSimulation.Properties.Resources.type1;
                }
                else
                {
                    CrossingT2 cr = new CrossingT2(drawPoint, pb_b, pb_t);
                   // pb_b.Location = cr.Location;
                    pb_b.Tag = cr;
                    pb_t.Tag = cr;
                    grid.AddCrossing(cr);
                    grid.Cells[selectedCell - 1].Crossing = cr;

                    pb_b.Image = TrafficSimulation.Properties.Resources.CcrossingType2;
                }
                this.Controls.Add(pb_b);
                this.Controls.Add(pb_t);

               
                pb_t.BringToFront();

                
                pb_b.Location = drawPoint;

                pb_b.Parent = panel1;
                pb_t.Parent = pb_b;
                grid.CheckNeighbours(selectedCell - 1);
            }
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            Point cursor = PointToClient(Cursor.Position); 
            int column = ((cursor.X - panel1.Left) / 300) + 1;
            int row = (cursor.Y - panel1.Top) / 300;
            selectedCell = row * 4 + column;
            mouseDown = grid.Cells[selectedCell - 1].Location;
            firstSelection = selectedCell;

            if (!runningSimulation)
            {
                PictureBox pb;
                pb = (PictureBox)sender;
                pb.DoDragDrop(((Crossing)(pb.Tag)).Pb_Background.Image, DragDropEffects.Move);
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, cursor.X, cursor.Y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            runningSimulation = true;
            grid.StartTimers();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            grid.PauseTimers();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!runningSimulation)
            {
                dragTypeOne = true;
                pictureBox1.DoDragDrop(pictureBox1.Image, DragDropEffects.Copy);
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (!runningSimulation)
            {
                dragTypeOne = false;
                pictureBox2.DoDragDrop(pictureBox2.Image, DragDropEffects.Copy);
            }
        }

        private void CrossingOptions_Click(object sender, System.EventArgs e)
        {
            CrossingOptionsForm crOptions = new CrossingOptionsForm(grid.Cells[selectedCell - 1].Crossing);
            crOptions.ShowDialog();
        }

        private void removeCrossingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this crossing?", "Remove Crossing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                grid.RemoveCrossing(selectedCell);
                for (int i = 0; i < grid.Cells.Count; i++)
                {
                    if (grid.Cells[i].Taken)
                    {
                        grid.CheckNeighbours(i);
                    }
                }
            }
        }

        private void btn_stat_Click(object sender, EventArgs e)
        {
            Statistics_Form statistic_Form = new Statistics_Form(grid);
            statistic_Form.Show();
        }

        private void btn_StopSimulation_Click(object sender, EventArgs e)
        {
            runningSimulation = false;
            grid.StopSimulation();
        }

        /// <summary>
        /// Serializes an object to be saved
        /// </summary>
        /// <param name="filename">The name of the file to be saved</param>
        public void Save(string filename)
        {
            Serialize serialize = new Serialize();
            serialize.SerializeObject(filename, grid);
        }

        /// <summary>
        /// Saves a file in a chosen directory from the user in a .sim format
        /// </summary>
        public void SaveAs()
        {
            SaveFileDialog saveas = new SaveFileDialog();
            saveas.FileName = "My simulaiton";
            saveas.Filter = "Simulation file|*.sim";

            if (saveas.ShowDialog() == DialogResult.OK)
            {
                Save(saveas.FileName);
                MessageBox.Show("Saved");
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            loadfile();
        }

        /// <summary>
        /// Loads a saved file from a chosen directory in a .sim format
        /// </summary>
        public void loadfile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = "My simulaiton";
            openFile.Filter = "Simulation file|*.sim";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("open");
                panel1.Controls.Clear();
                loadFile(openFile.FileName);
            }
        }

        /// <summary>
        /// Deserializes an object to be loaded
        /// </summary>
        /// <param name="filename">The name of the file to be loaded</param>
        public void loadFile(string filename)
        {
            Serialize serialize = new Serialize();
            this.grid = (Grid)serialize.DeSerializeObject(filename);
            foreach (Crossing cr in grid.Crossings)
            {
                if (cr != null)
                {
                    PictureBox pb_b = new PictureBox();
                    pb_b.Height = panel1.Height / 3;
                    pb_b.Width = panel1.Width / 4;
                    pb_b.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb_b.BackColor = Color.Transparent;
                    pb_b.MouseDown += new MouseEventHandler(Mouse_Down);

                    //Added transparent layer
                    PictureBox pb_t = new PictureBox();
                    pb_t.Size = pb_b.Size;
                    pb_t.MouseDown += new MouseEventHandler(Mouse_Down);
                    pb_t.Location = new System.Drawing.Point(0, 0);
                    if (cr is CrossingT1)
                    {
                        pb_b.Tag = cr;
                        pb_t.Tag = cr;
                        pb_b.Image = TrafficSimulation.Properties.Resources.type1;
                    }
                    else
                    {
                        pb_b.Tag = cr;
                        pb_t.Tag = cr;
                        pb_b.Image = TrafficSimulation.Properties.Resources.CcrossingType2;
                    }
                    this.Controls.Add(pb_b);
                    this.Controls.Add(pb_t);

                    pb_b.BringToFront();
                    pb_t.BringToFront();

                    pb_t.Location = new System.Drawing.Point(0, 0);
                    pb_b.Location = cr.Location;

                    pb_b.Parent = panel1;
                    pb_t.Parent = pb_b;

                    cr.Pb_Background = pb_b;
                    cr.Pb_Transparent = pb_t;
                }
            }
            grid.InitializeGrid();
        }
    }
}
