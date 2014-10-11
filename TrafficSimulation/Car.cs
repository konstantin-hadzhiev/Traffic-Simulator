using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficSimulation
{
    [Serializable]
    public class Car
    {
        private Point location; //location on the crossing, upper left corner of the drawing
        private bool moving;
        private int width; //width (in pixels) of the visual representation of a car, X
        private int height; //height (in pixels) of the visual representation of a car, Y
        private string destination; //destination of the car - left, straight, right
        private bool inLane; 
        private bool toBeRemoved;

        public Car(Point loc, string destination)
        {
            this.location = loc;
            this.width = 8;
            this.height = 8;
            this.destination = destination;
            this.inLane = true;
            this.moving = true;
            this.toBeRemoved = false;
        }

        public bool Moving
        {
            get { return this.moving; }
            set { this.moving = value; }
        }
        
        /// <summary>
        ///Boolean, showing whether the car object needs to be removed from the leaving LaneIn/OutList 
        /// </summary>
        public bool ToBeRemoved
        {
            get { return this.toBeRemoved; }
            set { this.toBeRemoved = value; }
        }

        public bool InLane
        {
            get { return this.inLane; }
            set { this.inLane = value; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public String Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        /// <summary>
        /// Drawing a car as a rectangle on the location of car with sizes, given for the car
        /// </summary>
        /// <param name="gr">The graphics where the car will be drawn</param>
        public void Draw(ref Graphics gr)
        {
            gr.FillRectangle(Brushes.Cyan, this.location.X, this.location.Y, this.width, this.height);
        }
    }
}
