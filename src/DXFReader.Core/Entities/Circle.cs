using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class Circle : Entity
    {
        public Location Center { get; set; }
        public double Radius { get; set; }
        public Location ExtrustionDirection { get; set; }
        public Circle(List<DXFCodeValue> entityValues) : base(entityValues)
        {
            Center = new Location(entityValues, 10, 20, 30);
            Radius = entityValues.Double(40) ?? 0.0;
            ExtrustionDirection = new Location(entityValues, 210, 220, 230);
        }
    }
}
