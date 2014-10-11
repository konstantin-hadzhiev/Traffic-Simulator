using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace TrafficSimulation
{
    [Serializable]
    public class TrafficLight
    {
        private int duration;
        private bool isGreen;
        private Point location;
        private LaneIn laneIn;

        public TrafficLight(Point location)
        {
            this.duration = 5;
            this.isGreen = false;
            this.location = location;
        }

        public LaneIn LaneIn
        {
            get { return laneIn; }
            set { laneIn = value; }
        }

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public bool IsGreen
        {
            get { return isGreen; }
            set { isGreen = value; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Draws the traffic lights in the crossing
        /// </summary>
        /// <param name="gr">The graphics where the trafficlight will be drawn</param>
        public void Draw(Graphics gr)
        {
            if (isGreen)
            {
                SolidBrush myBrush = new SolidBrush(Color.Green);
                gr.FillEllipse(myBrush, location.X, location.Y, 8, 8);
            }
            else
            {
                SolidBrush myBrush = new SolidBrush(Color.Red);
                gr.FillEllipse(myBrush, location.X, location.Y, 8, 8);
            }
        }
    }
}