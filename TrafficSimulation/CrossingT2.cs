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
    class CrossingT2 : Crossing
    {
        private List<Lane> laneList = new List<Lane>();

        public CrossingT2(Point location, PictureBox pb_background, PictureBox pb_transparrent)
            : base(location, pb_background, pb_transparrent) 
        {
            //group 1
            //from west Left
            TrafficLight tl1 = new TrafficLight(new Point(121, 147));
            //from east Left
            TrafficLight tl2 = new TrafficLight(new Point(175, 147));
            //from North StraightRIght
            TrafficLight tl3 = new TrafficLight(new Point(133, 119));
            //from South StraightRight
            TrafficLight tl4 = new TrafficLight(new Point(164, 175));

            //group 2
            //from west StraightRight
            TrafficLight tl5 = new TrafficLight(new Point(121, 162));
            //from east StraightRight
            TrafficLight tl6 = new TrafficLight(new Point(175, 132));
            //From north Left
            TrafficLight tl7 = new TrafficLight(new Point(148, 119));
            //from south Left
            TrafficLight tl8 = new TrafficLight(new Point(149, 175));


            //Hardcode 12 lanes
            //To the Straight &right, checking Y for the stopPoint - 10pixels before the TL, Lenght - 10
            LaneIn laneIn1 = new LaneIn(new Point(128, 0), 16, 127, "south", tl3, 110);
            
            //left
            LaneIn laneIn2 = new LaneIn(new Point(145, 0), 17, 127, "south", tl7, 110);
           
            //StopPoint - Location.Y + 10
            //left
            LaneIn laneIn3 = new LaneIn(new Point(143, 172), 16, 133, "north", tl8, 185);
          
            //Straight & Right
            LaneIn laneIn4 = new LaneIn(new Point(158, 172), 17, 135, "north", tl4, 185);
            
            //StopPoint - Length- 10
            //Left
            LaneIn laneIn5 = new LaneIn(new Point(0, 143), 127, 15, "east", tl1, 110);
           
            //Straight & Right
            LaneIn laneIn6 = new LaneIn(new Point(0, 158), 127, 15, "east", tl5, 110);
           
            //StopPoint - Location.X+10
            //Straight & Right
            LaneIn laneIn7 = new LaneIn(new Point(173, 127), 135, 15, "west", tl6, 186);
            
            //Left
            LaneIn laneIn8 = new LaneIn(new Point(173, 144), 135, 15, "west", tl2, 186);
           
            this.LaneOutList.Add(new LaneOut(new Point(173, 159), 130, 14, "east", 295));
            this.LaneOutList.Add(new LaneOut(new Point(0, 128), 128, 15, "west", 1));
            this.LaneOutList.Add(new LaneOut(new Point(129, 173), 14, 127, "south", 292));
            this.LaneOutList.Add(new LaneOut(new Point(160, 0), 14, 128, "north", 1));

            //Adding LaneIn's to the LaneInList            
            this.LaneInList.Add(laneIn1);
            this.LaneInList.Add(laneIn2);      
            this.LaneInList.Add(laneIn3);
            this.LaneInList.Add(laneIn4);
            this.LaneInList.Add(laneIn5);
            this.LaneInList.Add(laneIn6);
            this.LaneInList.Add(laneIn7);
            this.LaneInList.Add(laneIn8);

            //Adding to the groups
            Groups.AddToGroup1(tl1);
            Groups.AddToGroup1(tl2);

            Groups.AddToGroup2(tl3);
            Groups.AddToGroup2(tl4);

            Groups.AddToGroup3(tl5);
            Groups.AddToGroup3(tl6);

            Groups.AddToGroup4(tl7);
            Groups.AddToGroup4(tl8);

            //sets grp1 to green
            Groups.SetLight(Groups.GetGroup1(), true);

            //Setting the LaneIn to the traffic lights
            tl1.LaneIn = laneIn5;
            tl2.LaneIn = laneIn8;
            tl3.LaneIn = laneIn1;
            tl4.LaneIn = laneIn4;
            tl5.LaneIn = laneIn6;
            tl6.LaneIn = laneIn7;
            tl7.LaneIn = laneIn2;
            tl8.LaneIn = laneIn3;
        }
    }
}