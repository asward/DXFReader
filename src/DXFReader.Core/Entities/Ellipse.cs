using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class Ellipse : Entity
    {
        public Location Center { get; set; }
        public Location MajorAxisEndPoint { get; set; }
        public Location ExtrustionDirection { get; set; }
        public double MajorMinorAxisRatio { get; set; }
        public double StartParameter { get; set; }
        public double StopParameter { get; set; }

        public Ellipse(List<DXFCodeValue> entityValues) : base(entityValues)
        {

            Center = new Location(entityValues, 10, 20, 30);
            MajorAxisEndPoint = new Location(entityValues, 11, 21, 31);

            ExtrustionDirection = new Location(entityValues, 210, 220, 230);
            MajorMinorAxisRatio = entityValues.Double(40) ?? 0.0;
            StartParameter = entityValues.Double(41) ?? 0.0;
            StopParameter = entityValues.Double(42) ?? 0.0;

        }
    }
}
