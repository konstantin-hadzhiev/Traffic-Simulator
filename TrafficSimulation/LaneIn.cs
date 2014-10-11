using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSimulation
{
    [Serializable]
    public class LaneIn : Lane
    {
        private Random random;
        private TrafficLight trafficLight;
        private int percentage;

        public LaneIn(Point location, int width, int height, string directionLane, TrafficLight tl, int stopPoint)
            : base(location, width, height, directionLane, stopPoint)
        {
            this.trafficLight = tl;
            this.random = new Random();
        }

        public TrafficLight TrafficLight
        {
            get { return trafficLight; }
            set { trafficLight = value; }
        }

        /// <summary>
        /// Percentage of cars for each direction(left, straight, right) in the lane
        /// </summary>
        public int Percentage
        {
            get { return percentage; }
            set { percentage = value; }
        }      

        /// <summary>
        /// Sets all the cars in the list to move
        /// </summary>
        public void ResetCars()
        {
            if (CarList.Count != 0)
            {
                foreach (Car c in this.CarList)
                {
                    if (!c.Moving)
                    {
                        c.Moving = true;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the lane is full
        /// </summary>
        /// <returns></returns>
        public bool CheckFullLane()
        {
            if (this.Direction == "south" || this.Direction == "east")
            {
                if (this.StopPoint < 1)
                {
                    return true;
                }
            }
            else if (this.Direction == "north" || this.Direction == "west")
            {
                if (this.StopPoint > 300)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Moves the car along its lane in
        /// </summary>
        /// <param name="car">The car to be moved</param>
        public void MoveCarAlongLaneIn(Car car)
        {
            if (this.Direction == "north")
            {
                if (this.StopPoint == car.Location.Y && !trafficLight.IsGreen)
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
                if (this.StopPoint == car.Location.Y && !trafficLight.IsGreen)
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
                if (this.StopPoint == car.Location.X && !trafficLight.IsGreen)
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
                if (this.StopPoint == car.Location.X && !trafficLight.IsGreen)
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
        
        /// <summary>
        /// Adds cars to the lane of crossing type1
        /// </summary>
        /// <param name="car">The car to be added</param>
        public void AddCarsToLaneCrossing1(Car car)
        {
            int i = 0;
            string destination = "";

            //For North direction
            if (this.Direction == "north")
            {
                destination = "straight";
                this.CarList.Add(new Car(new Point(this.Location.X + 7, 299), destination));
            }

            //For South direction
            else if (this.Direction == "south")
            {
                destination = "straight";
                this.CarList.Add(new Car(new Point(this.Location.X + 7, 1), destination));
            }

            //For East direction
            else if (this.Direction == "east")
            {
                if (this.Location.Y == 144)
                {
                    destination = "left";
                }
                else
                {
                    i = random.Next(1, percentage + 1);
                    if (i == 1)
                    {
                        destination = "straight";
                    }
                    else
                    {
                        destination = "right";
                    }
                }
                this.CarList.Add(new Car(new Point(1, this.Location.Y + 5), destination));
            }

            //For west direction lanes
            else if (this.Direction == "west")
            {
                if (this.Location.Y == 128)
                {
                    i = random.Next(1, 3);
                    if (i == 1)
                    {
                        destination = "straight";
                    }
                    else
                    {
                        destination = "right";
                    }
                }

                else if (this.Location.Y == 142)
                {
                    destination = "left";
                }
                this.CarList.Add(new Car(new Point(298, this.Location.Y + 5), destination));
            }
        }

        /// <summary>
        /// Adds cars to the lane of crossing type 2
        /// </summary>
        /// <param name="car">The car to be added</param>
        public void AddCarsToLaneCrossing2(Car car)
        {
            int i = 0;
            string destination = "";

            if (this.Direction == "north")
            {
                //LaneIn.Location.X == 143, LaneIn To the left
                if (this.Location.X == 143)
                {
                    destination = "left";
                }

                //LaneIn To the right & straight
                else
                {
                    i = random.Next(1, 3);
                    if (i == 1)
                    {
                        destination = "straight";
                    }
                    else
                    {
                        destination = "right";
                    }
                }
                car.Location = new Point(this.Location.X + 7, 299);
            }

            else if (this.Direction == "south")
            {
                if (this.Location.X == 128)
                {
                    i = random.Next(1, 3);
                    if (i == 1)
                    {
                        destination = "straight";
                    }
                    else
                    {
                        destination = "right";
                    }
                }
                //LaneIn to the left
                else
                {
                    destination = "left";
                }
                car.Location = new Point(this.Location.X + 7, 1);
            }

            else if (this.Direction == "west")
            {
                //LaneIn straight&& right
                if (this.Location.Y == 127)
                {
                    i = random.Next(1, 3);
                    if (i == 1)
                    {
                        destination = "straight";
                    }
                    else
                    {
                        destination = "right";
                    }
                }
                else
                {
                    destination = "left";
                }
                car.Location = new Point(298, this.Location.Y + 4);
            }

            else if (this.Direction == "east")
            {
                if (this.Location.Y == 143)
                {
                    destination = "left";
                }
                else
                {
                    i = random.Next(1, 3);
                    if (i == 1)
                    {
                        destination = "straight";
                    }
                    else
                    {
                        destination = "right";
                    }
                }
                car.Location = new Point(1, this.Location.Y + 4);
            }
            car.Destination = destination;
            this.CarList.Add(car);
        }
    }
}