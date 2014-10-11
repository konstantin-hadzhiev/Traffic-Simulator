using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSimulation
{
    [Serializable]
    public class LaneOut : Lane
    {
        public LaneOut(Point location, int width, int height, string directionLane, int stopPoint) 
            : base(location, width, height, directionLane, stopPoint) 
        {
            this.StopPoint = -1;
        }
    }
}
