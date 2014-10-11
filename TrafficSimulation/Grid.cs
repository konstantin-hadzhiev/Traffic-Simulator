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
    public class Grid
    {
        private List<Cell> cells;
        private List<Crossing> crossings;
        [NonSerialized]
        private Timer car_timer;
        private long time;
        private int junctionDefinition;

        public Grid()
        {
            this.cells = new List<Cell>();
            this.crossings = new List<Crossing>();
            this.junctionDefinition = 40;
            this.InitializeCarTimer();
            this.AddCells();
        }

        public List<Cell> Cells
        {
            get
            {
                return cells;
            }
        }

        public int Junction
        {
            get { return junctionDefinition; }
            set { junctionDefinition = value; }
        }

        public List<Crossing> Crossings
        {
            get { return crossings; }
        }

        public void InitializeCarTimer()
        {
            car_timer = new Timer();
            car_timer.Interval = 40;
            car_timer.Tick += car_timer_Tick;
        }

        public void InitializeGrid()
        {
            car_timer = new Timer();
            car_timer.Interval = 40;
            car_timer.Tick += car_timer_Tick;
            foreach (Crossing cr in crossings)
            {
                cr.InitializeCrossing();
            }
        }

        /// <summary>
        /// Drawing all the cars on the crossing and moving them accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void car_timer_Tick(object sender, EventArgs e)
        {
            foreach (Crossing cr in Crossings)
            {
                cr.Move();
                for (int i = 0; i < cr.CarFrequency.Count(); i++)
                {
                    if (time % cr.CarFrequency[i] == 0)
                    {
                        cr.AddCarToLane(cr, cr.IncomingStreams[i]/*, new Car(new Point(0, 0), "")*/);
                    }
                }
                cr.PlaceCarsOnCrossingPb();
                cr.PlaceTrafficLightsOnCrossingPb();
            }
            time += 40;
        }

        /// <summary>
        /// Starting the car timer and all the traffic light timers
        /// </summary>
        public void StartTimers()
        {
            car_timer.Start(); 
            foreach (Crossing cr in this.Crossings)
            {
                cr.StartTLTimer();
            }
        }

        /// <summary>
        /// Pausing the car timer and stopping all the traffic light timers
        /// </summary>
        public void PauseTimers()
        {
            car_timer.Stop(); 
            foreach (Crossing cr in this.Crossings)
            {
                cr.StopTLTimer();
            }
        }

        /// <summary>
        /// Stopping all the timers and removing the cars from all the lists of the lanes
        /// </summary>
        public void StopSimulation()
        {
            car_timer.Stop();
            time = 0;
            foreach (Crossing cr in this.Crossings)
            {
                cr.RemoveCarsFromLanes();
                cr.StopTLTimer();
            }
        }

        public void AddCrossing(Crossing cr)
        {
            crossings.Add(cr);
        }

        public Crossing GetCrossing(Point location)
        {
            foreach (Crossing cr in crossings)
            {
                if (cr.Location!=null)
                {
                    return cr;
                }
            }
            return null;
        }

        /// <summary>
        /// Removes crossing, visual representation + object
        /// </summary>
        /// <param name="selectedCell">cell with position of the crossing</param>
        public void RemoveCrossing(int selectedCell)
        {
            Crossing selectedCrossing = this.cells[selectedCell - 1].Crossing;
            selectedCrossing.Pb_Background.Dispose();
            selectedCrossing.Pb_Transparent.Dispose();
            crossings.Remove(selectedCrossing);
            cells[selectedCell - 1].Taken = false;
            cells[selectedCell - 1].Crossing = null;
        }

        /// <summary>
        /// Defining the cells on the grid
        /// </summary>
        /// <param name="panel"></param>
        public void AddCells()
        {
            int rows = 3;
            int columns = 4;

            int cellWidth = 300;
            int cellHeight = 300;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.Cells.Add(new Cell(new Point(0 + j * cellWidth, 0 + i * cellHeight)));
                }
            }
        }

        /// <summary>
        /// checks the west neighbour of the crossing on a given cell
        /// </summary>
        /// <param name="cellNr">the cell Nr of the crossing whose neighbour its checked</param>
        public void CheckWest(int cellNr)
        {
            //crossing whose neighbour we check
            Crossing cr = Cells[cellNr].Crossing;

            //check whether there is a west neighbour
            //If there is - cr.LaneIn with direction east is NOT endLane
            if (cells[cellNr - 1].Taken)
            {
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "east")
                    {
                        laneIn.EndLane = false;
                    }
                }
                //car should NOT be spawned && LaneOut with directin West is NOT endLane
                cr.IncomingStreams[1] = "";
                cr.LaneOutList[1].EndLane = false;

                //LaneIn 'west' of the west neighbour is NOT endLane
                foreach (LaneIn laneIn in cells[cellNr - 1].Crossing.LaneInList)
                {
                    if (laneIn.Direction == "west")
                    {
                        laneIn.EndLane = false;
                    }
                }
                //no spawned car in the neighbour from the east laneIns and LaneOut 'east' is not endlane
                cells[cellNr - 1].Crossing.IncomingStreams[3] = "";
                cells[cellNr - 1].Crossing.LaneOutList[0].EndLane = false;

                cr.Neighbours[3] = cells[cellNr - 1].Crossing;
                cells[cellNr - 1].Crossing.Neighbours[1] = cr;
            }
                //if there is NO neighbour on west side, laneIns is endlane(spawning point)
                //laneout is endlane, cars are erased after leaving it
            else
            {
                cr.Neighbours[3] = null;
                cr.IncomingStreams[1] = "east";
                cr.LaneOutList[1].EndLane = true;
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "east")
                    {
                        laneIn.EndLane = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// checks the east neighbour of the crossing on a given cell
        /// </summary>
        /// <param name="cellNr">the cell Nr of the crossing whose neighbour its checked</param>
        public void CheckEast(int cellNr)
        {
            Crossing cr = Cells[cellNr].Crossing;

            //If there is east neighbour - laneIns of the current crossing with direction east are NOT endLane
            if (cells[cellNr + 1].Taken)
            {
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "west")
                    {
                        laneIn.EndLane = false;
                    }
                }

                //car should NOT be spawned from east && LaneOut of current crossing with direction east is NOT endLane
                cr.IncomingStreams[3] = "";
                cr.LaneOutList[0].EndLane = false;

                //LaneIn 'east' of the east neighbour is NOT endLane
                foreach (LaneIn laneIn in cells[cellNr + 1].Crossing.LaneInList)
                {
                    if (laneIn.Direction == "east")
                    {
                        laneIn.EndLane = false;
                    }
                }
                //no spawned car in the neighbour from the east laneIns and LaneOut 'west' is not endlane
                cells[cellNr + 1].Crossing.IncomingStreams[1] = "";
                cells[cellNr + 1].Crossing.LaneOutList[1].EndLane = false;
                cr.Neighbours[1] = cells[cellNr + 1].Crossing;
                cells[cellNr + 1].Crossing.Neighbours[3] = cr;
            }
            //if there is NO neighbour on east side, laneIns of the current crossing are endlane(spawning point)
            //laneout is endlane, cars are erased after leaving it
            else
            {
                cr.Neighbours[1] = null;
                cr.IncomingStreams[3] = "west";
                cr.LaneOutList[0].EndLane = true;
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "west")
                    {
                        laneIn.EndLane = true;
                    }
                }
            }
        }

        /// <summary>
        /// checks the north neighbour of the crossing on a given cell
        /// </summary>
        /// <param name="cellNr">the cell Nr of the crossing whose neighbour its checked</param>
        public void CheckNorth(int cellNr)
        {
            Crossing cr = Cells[cellNr].Crossing;
            //If there is north neighbour - laneIns 'south' of the current crossing are NOT endLane
            if (cells[cellNr - 4].Taken)
            {
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "south")
                    {
                        laneIn.EndLane = false;
                    }
                }

                //car should NOT be spawned from north && LaneOut of current crossing with direction north is NOT endLane
                cr.IncomingStreams[2] = "";
                cr.LaneOutList[3].EndLane = false;

                //LaneIn 'north' of the north neighbour is NOT endLane
                foreach (LaneIn laneIn in cells[cellNr - 4].Crossing.LaneInList)
                {
                    if (laneIn.Direction == "north")
                    {
                        laneIn.EndLane = false;
                    }
                }
                //no spawned car in the neighbour from the north laneIns and LaneOut 'south' is not endlane
                cells[cellNr - 4].Crossing.IncomingStreams[0] = "";
                cells[cellNr - 4].Crossing.LaneOutList[2].EndLane = false;
                cr.Neighbours[0] = cells[cellNr - 4].Crossing;
                cells[cellNr - 4].Crossing.Neighbours[2] = cr;
            }
            //if there is NO neighbour on north side, laneIns of the current crossing are endlane(spawning point)
            //laneout is endlane, cars are erased after leaving it
            else
            {
                cr.Neighbours[0] = null;
                cr.IncomingStreams[2] = "south";
                cr.LaneOutList[3].EndLane = true;
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "south")
                    {
                        laneIn.EndLane = true;
                    }
                }
            }
        }

        /// <summary>
        ///  checks the south neighbour of the crossing on a given cell
        /// </summary>
        /// <param name="cellNr">the cell Nr of the crossing whose neighbour its checked</param>
        public void CheckSouth(int cellNr)
        {
            Crossing cr = Cells[cellNr].Crossing;
            //If there is south neighbour - laneIns from the south of the current crossing are NOT endLane
            if (cells[cellNr + 4].Taken)
            {
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "north")
                    {
                        laneIn.EndLane = false;
                    }
                }

                //car should NOT be spawned from south direction
                //LaneOut of current crossing with direction to the north is NOT endLane
                cr.IncomingStreams[0] = "";
                cr.LaneOutList[2].EndLane = false;


                //LaneIn 'south' of the south neighbour is NOT endLane
                foreach (LaneIn laneIn in cells[cellNr + 4].Crossing.LaneInList)
                {
                    if (laneIn.Direction == "south")
                    {
                        laneIn.EndLane = false;
                    }
                }
                //no spawned car in the neighbour from the south laneIns and LaneOut 'north' is not endlane
                cells[cellNr + 4].Crossing.IncomingStreams[2] = "";
                cells[cellNr + 4].Crossing.LaneOutList[3].EndLane = false;
                cr.Neighbours[2] = cells[cellNr + 4].Crossing;
                cells[cellNr + 4].Crossing.Neighbours[0] = cr;
            }
            //if there is NO neighbour on south side, laneIns of the current crossing are endlane(spawning point)
            //laneout is endlane, cars are erased after leaving it
            else
            {
                cr.Neighbours[2] = null;
                cr.IncomingStreams[0] = "north";
                cr.LaneOutList[2].EndLane = true;
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "north")
                    {
                        laneIn.EndLane = true;
                    }
                }
            }
        }

        /// <summary>
        /// enables the stream of cars from a particular side of the crossing
        /// </summary>
        /// <param name="direction">side of the crossing where the streams of cars should be enabled to</param>
        /// <param name="cellNr">cell number of the crossing being checked</param>
        public void EnableStream(string direction, int cellNr)
        {
            Crossing cr = cells[cellNr].Crossing;
            //if direction = 'north', laneIn to north are ednLane and laneOut to south is endlane, No south neighbour
            if (direction == "north")
            {
                cr.IncomingStreams[0] = "north";
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "north")
                    {
                        laneIn.EndLane = true;
                    }
                }
                cr.LaneOutList[2].EndLane = true;
                cr.Neighbours[2] = null;
            }

            //if direction = 'east', laneIn to east are ednLane and laneOut to west is endlane, No west neighbour
            else if (direction == "east")
            {
                cr.IncomingStreams[1] = "east";
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "east")
                    {
                        laneIn.EndLane = true;
                    }
                }
                cr.LaneOutList[1].EndLane = true;
                cr.Neighbours[3] = null;
            }

            //laneIn to sout are ednLane and laneOut to north is endlane, No north neighbour
            else if (direction == "south")
            {
                cr.IncomingStreams[2] = "south";
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "south")
                    {
                        laneIn.EndLane = true;
                    }
                }
                cr.LaneOutList[3].EndLane = true;
                cr.Neighbours[0] = null;
            }

            //laneIn to west are ednLane and laneOut to east is endlane, No east neighbour
            else if (direction == "west")
            {
                cr.IncomingStreams[3] = "west";
                foreach (LaneIn laneIn in cr.LaneInList)
                {
                    if (laneIn.Direction == "west")
                    {
                        laneIn.EndLane = true;
                    }
                }
                cr.LaneOutList[0].EndLane = true;
                cr.Neighbours[1] = null;
            }
        }

        /// <summary>
        /// Checking whether there are neighbours according to the crossing position on the grid 
        /// enabling the streams of cars when the cell is on the borders of the grid
        /// </summary>
        /// <param name="cellNr">location on the grid - cellNumber</param>
        public void CheckNeighbours(int cellNr)
        {
            if (cellNr == 0)
            {
                CheckEast(cellNr);
                CheckSouth(cellNr);
                //south and east side of cell[0] never have neighbour 
                EnableStream("south", cellNr);
                EnableStream("east", cellNr);
            }
            else if (cellNr == 1 || cellNr == 2)
            {
                //south part is always spawning point - grid boundaries from south side
                CheckWest(cellNr);
                CheckEast(cellNr);
                CheckSouth(cellNr);
                EnableStream("south", cellNr);
            }
            else if (cellNr == 3)
            {
                CheckWest(cellNr);
                CheckSouth(cellNr);
                //no neighbour from west and south side
                EnableStream("south", cellNr);
                EnableStream("west", cellNr);
            }
            else if (cellNr == 4)
            {
                CheckEast(cellNr);
                CheckSouth(cellNr);
                CheckNorth(cellNr);
                //grid borders on east
                EnableStream("east", cellNr);
            }
            else if (cellNr == 5 || cellNr == 6)
            {
                CheckWest(cellNr);
                CheckEast(cellNr);
                CheckNorth(cellNr);
                CheckSouth(cellNr);
            }
            else if (cellNr == 7)
            {
                CheckWest(cellNr);
                CheckNorth(cellNr);
                CheckSouth(cellNr);
                //grid borders on west side
                EnableStream("west", cellNr);
            }
            else if (cellNr == 8)
            {
                CheckEast(cellNr);
                CheckNorth(cellNr);
                //no neighbour from north and east side
                EnableStream("north", cellNr);
                EnableStream("east", cellNr);
            }
            else if (cellNr == 9 || cellNr == 10)
            {
                CheckWest(cellNr);
                CheckEast(cellNr);
                CheckNorth(cellNr);
                //never a neighbour on north side
                EnableStream("north", cellNr);
            }
            else if (cellNr == 11)
            {
                CheckWest(cellNr);
                CheckNorth(cellNr);
                //no neighbour from north and west side
                EnableStream("west", cellNr);
                EnableStream("north", cellNr);
            }
        } 
    }
}