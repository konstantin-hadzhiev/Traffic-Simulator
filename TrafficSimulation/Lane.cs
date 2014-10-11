using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSimulation
{
    [Serializable]
    public class Lane
    {
        private List<Car> carList;
        private string direction;
        private int height; //height (in pixels) of the visual representation of a lane, Y
        private int width; //width (in pixels) of the visual representation of a lane, X
        private Point location; //Initial location according to the form ex: upper left corner
        private Rectangle boundaries;//The boundaries of the lane
        private bool endLane;
        private int stopPoint;
        private int initialStopPoint;

        public Lane(Point loc, int widthLn, int heigthLn, string directionLane, int StopPointXY)
        {
            this.carList = new List<Car>();

            this.location = loc;
            this.height = heigthLn;
            this.width = widthLn;
            this.direction = directionLane;
            this.boundaries = new Rectangle(loc, new Size(width, height));
            this.endLane = true;
            this.stopPoint = StopPointXY;
            this.initialStopPoint = StopPointXY;
        }

        /// <summary>
        /// List of cars belonging to the lane
        /// </summary>
        public List<Car> CarList
        {
            get { return carList; }
            set { carList = value; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public bool EndLane
        {
            get { return this.endLane; }
            set { this.endLane = value; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        { 
            get { return width; } 
        }

        public string Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Rectangle Boundaries
        {
            get { return boundaries; }
        }

        public int StopPoint
        {
            get { return stopPoint; }
            set { stopPoint = value; }
        }

        public int InitialStopPoint
        {
            get { return initialStopPoint; }
            set { initialStopPoint = value; }
        }

        public void ResetStopPoint()
        {
            if (this is LaneIn)
            {
                stopPoint = initialStopPoint;
            }
            else
            {
                stopPoint = -1;
            }
        }
        /// <summary>
        /// Placing cars on the crossing
        /// </summary>
        /// <param name="brush">Brush, used for drawing the cars</param>
        /// <param name="gr">Graphics, used for drawing the cars</param>
        public void PlaceCars(Graphics gr)
        {   
            foreach (Car car in carList)
            {
                car.Draw(ref gr);
            }
        }

        //checks if the car is within boundaries of the lane
        public bool CarWithinBoundaries(Car car)
        {
            if (this.Boundaries.Contains(new Rectangle(car.Location, new Size(car.Width, car.Height))))
            {
                car.InLane = true;
                return true;  
            }
            else
            {
                car.InLane = false;
                return false;
            }
        }

        /// <summary>
        /// Moving all the cars along the laneIn/laneOut they belong to
        /// </summary>
        public void MoveCarAlong(Car car)
        {
            if (this.Direction == "north")
            {
                if (this.StopPoint == car.Location.Y)
                {
                    car.Moving = false;
                    this.StopPoint += 11;
                }
                else
                {
                    car.Location = new Point(car.Location.X, car.Location.Y - 1);
                }
            }
            else if (this.Direction == "south")
            {
                if (this.StopPoint == car.Location.Y)
                {
                    car.Moving = false;
                    this.StopPoint -= 11;
                }
                else
                {
                    car.Location = new Point(car.Location.X, car.Location.Y + 1);
                }
            }
            else if (this.Direction == "west")
            {
                if (this.StopPoint == car.Location.X)
                {
                    car.Moving = false;
                    this.StopPoint += 11;
                }
                else
                {
                    car.Location = new Point(car.Location.X - 1, car.Location.Y);
                }
            }
            else if (this.Direction == "east")
            {
                if (this.StopPoint == car.Location.X)
                {
                    car.Moving = false;
                    this.StopPoint -= 11;
                }
                else
                {
                    car.Location = new Point(car.Location.X + 1, car.Location.Y);
                }
            }
        }

        //removes all files
        public void RemoveCars()
        {
            int count = CarList.Count;
            for (int i = 0; i < count; i++)
            {
                CarList.RemoveAt(0);
            }
        }
    }
}