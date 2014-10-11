using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficSimulation
{
    [Serializable()]
    class CrossingT1 : Crossing
    {
        public CrossingT1(Point location, PictureBox pb_background, PictureBox pb_transparrent)
            : base(location, pb_background, pb_transparrent)
        {
            //group 1
            TrafficLight tl1 = new TrafficLight(new Point(122, 161));
            TrafficLight tl2 = new TrafficLight(new Point(175, 131));

            //group 2
            TrafficLight tl3 = new TrafficLight(new Point(122, 147));
            TrafficLight tl4 = new TrafficLight(new Point(175, 146));

            //group 3
            TrafficLight tl5 = new TrafficLight(new Point(161, 175));
            TrafficLight tl6 = new TrafficLight(new Point(137, 118));

            //StopPoint - Length - 10
            //Left 
            LaneIn laneIn1 = new LaneIn(new Point(0, 144), 127, 15, "east", tl3, 110);
           
            //Right and straight
            LaneIn laneIn2 = new LaneIn(new Point(0, 159), 127, 15, "east", tl1, 110);
            
            //StopPoint - Location.X + 10
            //Left 
            LaneIn laneIn3 = new LaneIn(new Point(173, 128), 135, 15, "west", tl2, 186);

            //Right and straight
            LaneIn laneIn4 = new LaneIn(new Point(173, 142), 135, 15, "west", tl4, 186);

            //Lenght - 30
            //ONLY Straight
            LaneIn laneIn5 = new LaneIn(new Point(128, 0), 24, 127, "south", tl6, 93);
                  
            //Location.Y + 30
            //Only Straight
            LaneIn laneIn6 = new LaneIn(new Point(151, 173), 25, 127, "north", tl5, 203);

            this.LaneOutList.Add(new LaneOut(new Point(177, 157), 123, 16, "east", 295));
            this.LaneOutList.Add(new LaneOut(new Point(0, 128), 128, 15, "west", 1));
            this.LaneOutList.Add(new LaneOut(new Point(129, 173), 22, 128, "south", 292));
            this.LaneOutList.Add(new LaneOut(new Point(152, 0), 22, 127, "north", 1));

            //Adding LaneIn's in the LaneInList
            this.LaneInList.Add(laneIn1);
            this.LaneInList.Add(laneIn2);
            this.LaneInList.Add(laneIn3);
            this.LaneInList.Add(laneIn4);
            this.LaneInList.Add(laneIn5);
            this.LaneInList.Add(laneIn6);

            //adding groups
            Groups.AddToGroup1(tl1);
            Groups.AddToGroup1(tl2);
            Groups.AddToGroup2(tl3);
            Groups.AddToGroup2(tl4);
            Groups.AddToGroup3(tl5);
            Groups.AddToGroup3(tl6);
            //sets grp1 to green
            Groups.SetLight(Groups.GetGroup1(), true);

            //Setting the laneIn to the traffic lights
            tl1.LaneIn = laneIn2;
            tl2.LaneIn = laneIn3;
            tl3.LaneIn = laneIn1;
            tl4.LaneIn = laneIn4;
            tl5.LaneIn = laneIn6;
            tl6.LaneIn = laneIn5; 
        }
    }
}