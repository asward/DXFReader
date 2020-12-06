using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class Arc : Entity
    {
        public Location Center { get; set; }
        public double Radius { get; set; }
        public double StartAngle { get; set; }
        public double EndAngle { get; set; }
        public Location ExtrustionDirection { get; set; }
        public Arc(List<DXFCodeValue> entityValues) : base(entityValues)
        {

            Center = new Location(entityValues, 10, 20, 30);
            Radius = entityValues.Double(40) ?? 0.0;
            StartAngle = entityValues.Double(50) ?? 0.0;
            EndAngle = entityValues.Double(51) ?? 0.0;
            ExtrustionDirection = new Location(entityValues, 210, 220, 230);
        }
    }
}
