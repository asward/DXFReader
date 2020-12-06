using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class Line : Entity
    {
        public Location Start { get; set; }
        public Location End { get; set; }
        public Location ExtrustionDirection { get; set; }

        public Line(List<DXFCodeValue> entityValues) : base(entityValues)
        {

            Start = new Location(entityValues, 10, 20, 30);
            End = new Location(entityValues, 11, 21, 31);

            ExtrustionDirection = new Location(entityValues, 210, 220, 230);
        }
    }
}
