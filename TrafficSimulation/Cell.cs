using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrafficSimulation
{
    [Serializable]
    public class Cell
    {
        private Point location;
        private bool taken;
        private Crossing crossing;

        public Cell(Point location)
        {
            this.location = location;
            this.taken = false;
        }

        public Point Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        public Crossing Crossing
        {
            get { return this.crossing; }
            set { this.crossing = value; }
        }

        public bool Taken
        {
            get { return this.taken; }
            set { this.taken = value; }
        }
    }
}
