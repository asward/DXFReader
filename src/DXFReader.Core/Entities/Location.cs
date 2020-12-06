using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class Point : Entity
    {
        public Location Location { get; set; }

        public double X { get { return Location.X; } }
        public double Y { get { return Location.Y; } }
        public double Z { get { return Location.Z; } }
        public Location ExtrustionDirection { get; set; }

        public Point(List<DXFCodeValue> entityValues) : base(entityValues)
        {
            Location = new Location(entityValues, 10, 20, 30);

            ExtrustionDirection = new Location(entityValues, 210, 220, 230);
        }
    }
}
