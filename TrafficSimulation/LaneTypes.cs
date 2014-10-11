using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSimulation
{
    public enum LaneTypes
    {
        //TYPE1 - crossing with zebra
        //TYPE2 - the other crossing
        //ToWest1, ToWest2 - both laneIn that are from east to west, they are the same for both types of crossing
        //ToEast1, ToEast2 - both laneIn that are from west to east, they are the same for both types of crossing
        //Type1ToNorth - LaneIn in the crossing type1, with direction from south to north
        //Type1ToSouth - LaneIn in the crossing type1, with direction from north to south 
        //Type2ToSounth1 - LaneIn in the crossing type2, with direction from north to south 
        //Type2ToSounth2 - The second LaneIn in the crossing type2, with direction from north to south 
        //Type2ToNorth1 - The first LaneIn in the crossing type2, with direction from South to North 
        //Type2ToNorth2 - The second LaneIn in the crossing type2, with direction from South to North 
        FromWest1, FromWest2, FromEast1, FromEast2, Type1FromNorth, Type1FromSouth, Type2ToSouth1, Type2ToSouth2, Type2ToNorth1, Type2ToNorth2

        //ToWest2, ToEast2, Type2ToSouth1, Type2ToSouth2, Type2ToNorth1, Type2ToNorth2

    }
}
