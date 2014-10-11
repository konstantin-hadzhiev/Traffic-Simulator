using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSimulation
{
    [Serializable]
    public class Pedestrian
    {
        private Point location;
        private bool moving;
        private int height;
        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public Pedestrian(Point location, int height, int width)
        {
            this.location = location;
            this.height = height;
            this.width = width;
        }



        public void Draw(Graphics gr)
        {
            gr.FillRectangle(Brushes.DarkMagenta, this.location.X, this.location.Y, this.width, this.height);
        }

        public void Move()
        {
            this.Location = new Point(location.X + 1, location.Y);
        }

    }
}
