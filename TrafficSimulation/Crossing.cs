using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficSimulation
{
    [Serializable]
    public class Crossing
    {
        private Point location;
        [NonSerialized]
        private PictureBox pb_back;
        [NonSerialized]
        private PictureBox pb_trans;
        private List<LaneIn> laneInList;
        private List<LaneOut> laneOutList;
        private TrafficLightGroups groups;
        private Crossing[] neighbours;
        [NonSerialized]
        Graphics gr;
        List<Point> spawns;
        [NonSerialized]
        Timer tlTimer;
        private int[,] percentages;
        private int[] carFrequency;
        private string[] incomingStreams;
        private Random random;
        private int nrOfCars;

        public Crossing(Point location, PictureBox pb_background, PictureBox pb_transparrent)
        {
            this.location = location;
            this.pb_back = pb_background;
            this.pb_trans = pb_transparrent;
            this.neighbours = new Crossing[4];
            random = new Random();
            percentages = new int[4, 3] { { 30, 30, 40 }, { 30, 30, 40 }, { 30, 30, 40 }, { 30, 30, 40 } };
            carFrequency = new int[4] { 5000, 5000, 5000, 5000 };
            incomingStreams = new string[4] { "north", "east", "south", "west" };
            laneInList = new List<LaneIn>();
            laneOutList = new List<LaneOut>();
            groups = new TrafficLightGroups();
            gr = pb_trans.CreateGraphics();
            spawns = new List<Point>();
            tlTimer = new Timer();
            tlTimer.Interval = 1000;
            tlTimer.Tick += tlTimer_tick;
        }

        /// <summary>
        /// Shows a list of the neighbours of the crossing
        /// </summary>
        public Crossing[] Neighbours
        {
            get { return this.neighbours; }
            set { this.neighbours = value; }
        }

        /// <summary>
        /// Shows the time (in milliseconds) between each new spawned car on the laneIn
        /// </summary>
        public int[] CarFrequency
        {
            get { return this.carFrequency; }
            set { this.carFrequency = value; }
        }
        
        /// <summary>
        /// Counts the number of cars in the crossing
        /// </summary>
        public int NrOfCars
        {
            get { return this.nrOfCars; }
            set { this.nrOfCars = value; }
        }

        public string[] IncomingStreams
        {
            get { return this.incomingStreams; }
            set { this.incomingStreams = value; }
        }


        /// <summary>
        /// Shows the percentage of cars in every lane for each direction(left, straight, right) in the crossing
        /// </summary>
        public int[,] Percentages
        {
            get { return this.percentages; }
            set { this.percentages = value; }
        }

        public TrafficLightGroups Groups
        {
            get { return groups; }
            set { groups = value; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public PictureBox Pb_Background
        {
            get { return pb_back; }
            set { pb_back = value; }
        }

        public PictureBox Pb_Transparent
        {
            get { return pb_trans; }
            set { pb_trans = value; }
        }

        /// <summary>
        /// Gets the LaneIn list of the crossing.
        /// </summary>
        public List<LaneIn> LaneInList
        {
            get { return laneInList; }   
        }

        /// <summary>
        /// Gets the LaneOut list of the crossing.
        /// </summary>
        public List<LaneOut> LaneOutList
        {
            get { return laneOutList; }
        }

        /// <summary>
        /// Starts the traffic light timer.
        /// </summary>
        public void StartTLTimer()
        {
            tlTimer.Start();
        }

        /// <summary>
        /// Stops the traffic light timer.
        /// </summary>
        public void StopTLTimer()
        {
            tlTimer.Stop();
        }

        /// <summary>
        /// Initializes the non-serialized fields of the crossing.
        /// </summary>
        public void InitializeCrossing()
        {
            tlTimer = new Timer();
            tlTimer.Interval = 1000;
            tlTimer.Tick += tlTimer_tick;
            gr = pb_trans.CreateGraphics();
        }

        /// <summary>
        /// Resets the stop point of a neighbour's lane out.
        /// </summary>
        /// <param name="cr">The crossing that will have its neighbour's laneout reset.</param>
        /// <param name="laneOutIndex">The index of the lane out in the neighbour's laneOutList.</param>
        public void ResetNeighbourLaneOut(Crossing cr, int laneOutIndex)
        {
            if (cr != null)
            {
                cr.laneOutList[laneOutIndex].StopPoint = -1;
                foreach (Car car in cr.laneOutList[laneOutIndex].CarList)
                {
                    car.Moving = true;
                }
            }
        }

        /// <summary>
        /// Reseting the stop point of the laneIn and laneOut after the belonging traffic light to this laneIn gets green
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tlTimer_tick(object sender, EventArgs e)
        {
            if (groups.Count())
            {
                if (groups.Phase == "1")
                {
                    foreach (TrafficLight tl in groups.GetGroup1())
                    {
                        if (tl.LaneIn != null)
                        {   
                            tl.LaneIn.ResetCars();
                            tl.LaneIn.ResetStopPoint();
                        }
                    }
                    if (this is CrossingT1)
                    {
                        ResetNeighbourLaneOut(neighbours[1], 0);
                        ResetNeighbourLaneOut(neighbours[3], 1);
                    }
                }
                else if (groups.Phase == "2")
                {
                    foreach (TrafficLight tl in groups.GetGroup2())
                    {
                        if (tl.LaneIn != null)
                        {
                            tl.LaneIn.ResetCars();
                            tl.LaneIn.ResetStopPoint();
                        }
                    } 
                    if (this is CrossingT2)
                    {
                        ResetNeighbourLaneOut(neighbours[0], 2);
                        ResetNeighbourLaneOut(neighbours[2], 3);
                    }
                }
                else if (groups.Phase == "3")
                {
                    foreach (TrafficLight tl in groups.GetGroup3())
                    {
                        if (tl.LaneIn != null)
                        {
                            tl.LaneIn.ResetCars();
                            tl.LaneIn.ResetStopPoint();
                        }
                    } 
                    if (this is CrossingT1)
                    {
                        ResetNeighbourLaneOut(neighbours[0], 2);
                        ResetNeighbourLaneOut(neighbours[2], 3);
                    } 
                    if (this is CrossingT2)
                    {
                        ResetNeighbourLaneOut(neighbours[1], 0);
                        ResetNeighbourLaneOut(neighbours[3], 1);
                    }
                }
                else if (groups.Phase == "4")
                {
                    foreach (TrafficLight tl in groups.GetGroup4())
                    {
                        if (tl.LaneIn != null)
                        {
                            tl.LaneIn.ResetCars();
                            tl.LaneIn.ResetStopPoint();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draws all the cars of the crossing on its transparent picture box.
        /// </summary>
        public void PlaceCarsOnCrossingPb()
        {
            foreach (Lane lane in LaneInList)
            {
                lane.PlaceCars(gr);
            }
            foreach (Lane lane in LaneOutList)
            {
                lane.PlaceCars(gr);
            }
        }

        /// <summary>
        /// Draws all the cars of the crossing on its transparent picture box.
        /// </summary>
        public void PlaceTrafficLightsOnCrossingPb()
        {
            foreach (TrafficLight tl in Groups.GetGroup1())
            {
                tl.Draw(gr);
            }
            foreach (TrafficLight tl in Groups.GetGroup2())
            {
                tl.Draw(gr);
            }
            foreach (TrafficLight tl in Groups.GetGroup3())
            {
                tl.Draw(gr);
            }
            foreach (TrafficLight tl in Groups.GetGroup4())
            {
                tl.Draw(gr);
            }
        }

        /// <summary>
        /// Makes a car take a right turn when it gets out of the boundaries of the lane.
        /// </summary>
        /// <param name="lane">The lane that the car belongs to.</param>
        /// <param name="car">The car that will take the turn.</param>
        public void TurnRightCar(LaneIn lane, Car car)
        {
            //laneIn with direction 'north' with right turn ends on LaneOut 'east' (equal laneOutList[0]
            if (lane.Direction == "north")
            {
                //while the car is not yet in the boundary of laneOut, turn should be turning
                if (!laneOutList[0].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X + 1, car.Location.Y - 1);
                }
                //when the car enters in the boundary of the LaneOut, add it in the laneOut and remove it from the laneIn
                else
                {
                    laneOutList[0].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }

             //laneIn with direction 'south' with right turn ends on LaneOut 'west' = laneOutList[1]
            else if (lane.Direction == "south")
            {
                //turn right while car is not in the laneOut yet
                if (!laneOutList[1].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X - 1, car.Location.Y + 1);
                }
                else
                {
                    laneOutList[1].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }

            //laneIn with direction 'west' with right turn ends on LaneOut 'north' = laneOutList[3]
            else if (lane.Direction == "west")
            {
                if (!laneOutList[3].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X - 1, car.Location.Y - 1);
                }
                else
                {
                    laneOutList[3].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }

            //laneIn with direction 'east' with right turn ends on LaneOut 'south' = laneOutList[2]
            else if (lane.Direction == "east")
            {
                if (!laneOutList[2].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X + 1, car.Location.Y + 1);
                }
                else
                {
                    laneOutList[2].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }
        }

        /// <summary>
        /// Makes a car take a left turn when it gets out of the boundaries of the lane.
        /// </summary>
        /// <param name="lane">The lane that the car belongs to.</param>
        /// <param name="car">The car that will take the turn.</param>
        public void TurnLeftCar(LaneIn lane, Car car)
        {
            //laneIn with direction 'north' with left turn ends on LaneOut 'west' = laneOutList[1]
            if (lane.Direction == "north")
            {
                if (!laneOutList[1].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X - 1, car.Location.Y - 1);
                }
                else
                {
                    laneOutList[1].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }

            //Entering in LaneOut "East"
            else if (lane.Direction == "south")
            {
                if (!laneOutList[0].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X + 1, car.Location.Y + 1);
                }
                else
                {
                    laneOutList[0].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }
            else if (lane.Direction == "west")
            {
                //Entering in laneOut "South"
                if (!laneOutList[2].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X - 1, car.Location.Y + 1);
                }
                else
                {
                    laneOutList[2].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }
                //Entering in laneout 'north'
            else if (lane.Direction == "east")
            {
                if (!laneOutList[3].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X + 1, car.Location.Y - 1);
                }
                else
                {
                    laneOutList[3].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }
        }

        /// <summary>
        /// Makes a car go straight when it gets out of the boundaries of the lane.
        /// </summary>
        /// <param name="lane">The lane that the car belongs to.</param>
        /// <param name="car">The car that will go straight.</param>
        public void GoStraightCar(LaneIn lane, Car car)
        {
            //Enters in the laneOut 'north'
            if (lane.Direction == "north")
            {
                if (!laneOutList[3].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X, car.Location.Y - 1);
                }
                else
                {
                    laneOutList[3].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }

            // enters in the laneOut 'south' = laneOutList[2]
            else if (lane.Direction == "south")
            {
                if (!laneOutList[2].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X, car.Location.Y + 1);
                }
                else
                {
                    laneOutList[2].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }

            //  enters in the laneOut 'west' = laneOutList[1]
            else if (lane.Direction == "west")
            {
                if (!laneOutList[1].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X - 1, car.Location.Y);
                }
                else
                {
                    laneOutList[1].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }
            //  enters in the laneOut 'east' = laneOutList[0]
            else if (lane.Direction == "east")
            {
                if (!laneOutList[0].CarWithinBoundaries(car))
                {
                    car.Location = new Point(car.Location.X + 1, car.Location.Y);
                }
                else
                {
                    laneOutList[0].CarList.Add(car);
                    car.ToBeRemoved = true;
                }
            }
        }

        /// <summary>
        /// Makes the cars of the crossing move and change lanes or crossing if necessary.
        /// </summary>
        public void Move()
        {
            pb_trans.Refresh();
            foreach (LaneIn lane in this.LaneInList)
            {
                foreach (Car car in lane.CarList)
                {
                    if (car.Moving)
                    {
                        //Move the car along the lane In if its within the boundaries of the LaneIn
                        if (lane.CarWithinBoundaries(car))
                        {
                            car.ToBeRemoved = false;
                            lane.MoveCarAlongLaneIn(car);
                        }
                        //if the car goes out of the boundaries of the laneIn, move the car accroding to its destination
                        else
                        {
                            if (car.Destination == "right")
                            {
                                TurnRightCar(lane, car);
                            }
                            else if (car.Destination == "left")
                            {
                                TurnLeftCar(lane, car);
                            }
                            else if (car.Destination == "straight")
                            {
                                GoStraightCar(lane, car);
                            }
                        }
                    }
                }
            }
            //Remove all the cars of LaneIn list that are marked ToBeRemoved
            foreach (LaneIn lane in laneInList)
            {
                for (int i = 0; i < lane.CarList.Count; i++)
                {
                    if (lane.CarList[i].ToBeRemoved)
                    {
                        lane.CarList[i].ToBeRemoved = false;
                        lane.CarList.Remove(lane.CarList[i]);
                    }
                }
            }

            foreach (LaneOut lane in this.laneOutList)
            {
                foreach (Car car in lane.CarList)
                {
                    if (car.Moving)
                    {
                        //If car is within the LaneOut, move the car along it 
                        if (lane.CarWithinBoundaries(car))
                        {
                            lane.MoveCarAlong(car);
                        }
                        else
                        {
                            if (!lane.EndLane)
                            {
                                nrOfCars--;

                                //if the north laneOut is not endLane, car enters in its north neighbour = neighbour[0]
                                if (lane.Direction == "north")
                                {
                                    neighbours[0].NrOfCars++;

                                    //if the north neighbour is crossingT1, the car destination must be straight
                                    //car added in the LaneIn 'north' = laneInList[5] if the laneIn is not full
                                    if (neighbours[0] is CrossingT1)
                                    {
                                        car.Destination = "straight";
                                        car.Location = new Point(neighbours[0].laneInList[5].Location.X + 7, 299);
                                        if (!neighbours[0].laneInList[5].CheckFullLane())
                                        {
                                            neighbours[0].laneInList[5].CarList.Add(car);
                                        }
                                        else
                                        {
                                            lane.StopPoint = lane.InitialStopPoint;
                                        }
                                    }
                                     
                                        //if the north neighbour is CrossingT2 
                                    else
                                    {
                                        car.Destination = SetCarDestination(0, 2);
                                        if (car.Destination == "straight" || car.Destination == "right")
                                        {
                                            //if the car is destination = straight || right, enters in laneIn 'East'
                                            //If the laneIn is not full, car enters, else stops in the leaving laneOut 
                                            car.Location = new Point(neighbours[0].laneInList[3].Location.X + 7, 299);
                                            if (!neighbours[0].laneInList[3].CheckFullLane())
                                            {
                                                neighbours[0].laneInList[3].CarList.Add(car); 
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                        else
                                        {
                                            // if car destination=Left, enters in the laneIn 'west' that turns to the left
                                            car.Location = new Point(neighbours[0].laneInList[2].Location.X + 7, 299);
                                            if (!neighbours[0].laneInList[3].CheckFullLane())
                                            {
                                                neighbours[0].laneInList[2].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                    }
                                }
                                else if (lane.Direction == "east")
                                {
                                    neighbours[1].NrOfCars++;
                                    car.Destination = SetCarDestination(1, 3);
                                    if (neighbours[1] is CrossingT1)
                                    {
                                        if (car.Destination == "straight" || car.Destination == "right")
                                        {
                                            car.Location = new Point(1, neighbours[1].laneInList[1].Location.Y + 4);
                                            if (!neighbours[1].laneInList[1].CheckFullLane())
                                            {
                                                neighbours[1].laneInList[1].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                        else
                                        {
                                            car.Location = new Point(1, neighbours[1].laneInList[0].Location.Y + 4); 
                                            if (!neighbours[1].laneInList[1].CheckFullLane())
                                            {
                                                neighbours[1].laneInList[0].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (car.Destination == "straight" || car.Destination == "right")
                                        {
                                            car.Location = new Point(1, neighbours[1].laneInList[5].Location.Y + 4);
                                            if (!neighbours[1].laneInList[5].CheckFullLane())
                                            {
                                                neighbours[1].laneInList[5].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                        else
                                        {
                                            car.Location = new Point(1, neighbours[1].laneInList[4].Location.Y + 4);
                                            if (!neighbours[1].laneInList[5].CheckFullLane())
                                            {
                                                neighbours[1].laneInList[4].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                    }
                                }
                                else if (lane.Direction == "south")
                                {
                                    neighbours[2].NrOfCars++;
                                    if (neighbours[2] is CrossingT1)
                                    {
                                        car.Destination = "straight";
                                        car.Location = new Point(neighbours[2].laneInList[4].Location.X + 7, 1);
                                        if (!neighbours[2].laneInList[4].CheckFullLane())
                                        {
                                            neighbours[2].laneInList[4].CarList.Add(car);
                                        }
                                        else
                                        {
                                            lane.StopPoint = lane.InitialStopPoint;
                                        }
                                    }
                                    else
                                    {
                                        car.Destination = SetCarDestination(2, 0);
                                        if (car.Destination == "straight" || car.Destination == "right")
                                        {
                                            car.Location = new Point(neighbours[2].laneInList[0].Location.X + 7, 1);
                                            if (!neighbours[2].laneInList[0].CheckFullLane())
                                            {
                                                neighbours[2].laneInList[0].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                        else
                                        {
                                            car.Location = new Point(neighbours[2].laneInList[1].Location.X + 7, 1);
                                            if (!neighbours[2].laneInList[0].CheckFullLane())
                                            {
                                                neighbours[2].laneInList[1].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                    }
                                }
                                else if (lane.Direction == "west")
                                {
                                    neighbours[3].NrOfCars++;
                                    car.Destination = SetCarDestination(3, 1);
                                    if (neighbours[3] is CrossingT1)
                                    {
                                        if (car.Destination == "straight" || car.Destination == "right")
                                        {
                                            car.Location = new Point(300, neighbours[3].laneInList[2].Location.Y + 4);
                                            if (!neighbours[3].laneInList[2].CheckFullLane())
                                            {
                                                neighbours[3].laneInList[2].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                        else
                                        {
                                            car.Location = new Point(300, neighbours[3].laneInList[3].Location.Y + 4);
                                            if (!neighbours[3].laneInList[2].CheckFullLane())
                                            {
                                                neighbours[3].laneInList[3].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (car.Destination == "straight" || car.Destination == "right")
                                        {
                                            car.Location = new Point(298, neighbours[3].laneInList[6].Location.Y + 4);
                                            if (!neighbours[3].laneInList[6].CheckFullLane())
                                            {
                                                neighbours[3].laneInList[6].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                        else
                                        {
                                            car.Location = new Point(298, neighbours[3].laneInList[7].Location.Y + 4);
                                            if (!neighbours[3].laneInList[6].CheckFullLane())
                                            {
                                                neighbours[3].laneInList[7].CarList.Add(car);
                                            }
                                            else
                                            {
                                                lane.StopPoint = lane.InitialStopPoint;
                                            }
                                        }
                                    }
                                }
                            }
                            car.ToBeRemoved = true;
                        }
                    }
                }
            }

            foreach (LaneOut lane in laneOutList)
            {
                for (int i = 0; i < lane.CarList.Count; i++)
                {
                    if (lane.CarList[i].ToBeRemoved)
                    {
                        //nrOfCars--;
                        lane.CarList[i].ToBeRemoved = false;
                        lane.CarList.Remove(lane.CarList[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a destination as a string, according to the crossing's percentage settings.
        /// Only used when the car is being transfered to another crossing.
        /// </summary>
        /// <param name="neighbour">Neighbour's index in the neighbours list.</param>
        /// <param name="row">The index of the row, which corresponds to the direction that the car will spawn from.</param>
        /// <returns></returns>
        public string SetCarDestination(int neighbour, int row)
        {
            int i = random.Next(1, 101);
            int[,] perc = neighbours[neighbour].Percentages;

            if (i < perc[row, 0] + 1)
            {
                return "right";
            }
            else if (i < perc[row, 1] + perc[row, 0] + 1)
            {
                return "straight";
            }
            else
            {
                return "left";
            }
        }

        /// <summary>
        /// Returns a destination as a string, according to the crossing's percentage settings.
        /// </summary>
        /// <param name="row">The index of the row, which corresponds to the direction that the car will spawn from.</param>
        /// <returns></returns>
        public string SetCarDestination(int row)
        {
            int i = random.Next(1, 101);
            if (i < percentages[row, 0] + 1)
            {
                return "right";
            }
            else if (i < percentages[row, 1] + percentages[row, 0] + 1)
            {
                return "straight";
            }
            else
            {
                return "left";
            }
        }

        /// <summary>
        /// Adds a car to a crossing from a direction.
        /// </summary>
        /// <param name="cr">The crossing that the car will be added to.</param>
        /// <param name="direction">The direction.</param>
        public void AddCarToLane(Crossing cr, string direction)
        {
            Car car = new Car(new Point(0, 0), "");
            if (direction == "north")
            {
                nrOfCars++;
                if (cr is CrossingT1)
                {
                    if (!cr.laneInList[5].CheckFullLane())
                    {
                        car.Destination = "straight";
                        car.Location = new Point(cr.laneInList[5].Location.X + 7, 299);
                        cr.laneInList[5].CarList.Add(car);
                    }
                }
                else
                {
                    car.Destination = SetCarDestination(2);
                    if (car.Destination == "straight" || car.Destination == "right")
                    {
                        if (!cr.laneInList[3].CheckFullLane())
                        {
                            car.Location = new Point(cr.laneInList[3].Location.X + 7, 299);
                            cr.laneInList[3].CarList.Add(car);
                        }
                    }
                    else
                    {
                        if (!cr.laneInList[2].CheckFullLane())
                        {
                            car.Location = new Point(cr.laneInList[2].Location.X + 7, 299);
                            cr.laneInList[2].CarList.Add(car);
                        }
                    }
                }
            }
            else if (direction == "east")
            {
                nrOfCars++;
                car.Destination = SetCarDestination(3);
                if (cr is CrossingT1)
                {
                    if (car.Destination == "straight" || car.Destination == "right")
                    {
                        if (!cr.laneInList[1].CheckFullLane())
                        {
                            car.Location = new Point(1, cr.laneInList[1].Location.Y + 4);
                            cr.laneInList[1].CarList.Add(car);
                        }
                    }
                    else
                    {
                        if (!cr.laneInList[0].CheckFullLane())
                        {
                            car.Location = new Point(1, cr.laneInList[0].Location.Y + 4);
                            cr.laneInList[0].CarList.Add(car);
                        }
                    }
                }
                else
                {
                    if (car.Destination == "straight" || car.Destination == "right")
                    {
                        if (!cr.laneInList[5].CheckFullLane())
                        {
                            car.Location = new Point(1, cr.laneInList[5].Location.Y + 4);
                            cr.laneInList[5].CarList.Add(car);
                        }
                    }
                    else
                    {
                        if (!cr.laneInList[4].CheckFullLane())
                        {
                            car.Location = new Point(1, cr.laneInList[4].Location.Y + 4);
                            cr.laneInList[4].CarList.Add(car);
                        }
                    }
                }
            }
            else if (direction == "south")
            {
                nrOfCars++;
                if (cr is CrossingT1)
                {
                    if (!cr.laneInList[4].CheckFullLane())
                    {
                        car.Destination = "straight";
                        car.Location = new Point(cr.laneInList[4].Location.X + 7, 1);
                        cr.laneInList[4].CarList.Add(car);
                    }
                }
                else
                {
                    car.Destination = SetCarDestination(0);
                    if (car.Destination == "straight" || car.Destination == "right")
                    {
                        if (!cr.laneInList[0].CheckFullLane())
                        {
                            car.Location = new Point(cr.laneInList[0].Location.X + 7, 1);
                            cr.laneInList[0].CarList.Add(car);
                        }
                    }
                    else
                    {
                        if (!cr.laneInList[1].CheckFullLane())
                        {
                            car.Location = new Point(cr.laneInList[1].Location.X + 7, 1);
                            cr.laneInList[1].CarList.Add(car);
                        }
                    }
                }
            }
            else if (direction == "west")
            {
                nrOfCars++;
                car.Destination = SetCarDestination(1);
                if (cr is CrossingT1)
                {
                    if (car.Destination == "straight" || car.Destination == "right")
                    {
                        if (!cr.laneInList[2].CheckFullLane())
                        {
                            car.Location = new Point(300, cr.laneInList[2].Location.Y + 4);
                            cr.laneInList[2].CarList.Add(car);
                        }
                    }
                    else
                    {
                        if (!cr.laneInList[3].CheckFullLane())
                        {
                            car.Location = new Point(300, cr.laneInList[3].Location.Y + 4);
                            cr.laneInList[3].CarList.Add(car);
                        }
                    }
                }
                else
                {
                    if (car.Destination == "straight" || car.Destination == "right")
                    {
                        if (!cr.laneInList[6].CheckFullLane())
                        {
                            car.Location = new Point(298, cr.laneInList[6].Location.Y + 4);
                            cr.laneInList[6].CarList.Add(car);
                        }
                    }
                    else
                    {
                        if (!cr.laneInList[7].CheckFullLane())
                        {
                            car.Location = new Point(298, cr.laneInList[7].Location.Y + 4);
                            cr.laneInList[7].CarList.Add(car);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes all the cars from all the lane lists and refresh the transparent layer of picture box
        /// </summary>
        public void RemoveCarsFromLanes()
        {
            foreach (LaneIn laneIn in LaneInList)
            {
                laneIn.RemoveCars();
            }
            foreach (LaneOut laneOut in LaneOutList)
            {
                laneOut.RemoveCars();   
            }
            pb_trans.Refresh();
        }
    }
}