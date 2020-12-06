using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class LWPolyLine : Entity
    {
        public int NumPoints { get; set; }
        public short PolylineFlag { get; set; }
        public List<Location> Vertices = new List<Location>();
        public List<double> Buldge = new List<double>();
        public Location ExtrustionDirection { get; set; }

        public LWPolyLine(List<DXFCodeValue> entityValues) : base(entityValues)
        {
            NumPoints = entityValues.Int32(90); 
            PolylineFlag = entityValues.Int16(70);

            var Groups = entityValues.GroupsStartingWithCode(10);

            foreach(var group in Groups)
            {
                Vertices.Add(new Location(
                    group.Double(10) ?? 0.0,
                    group.Double(20) ?? 0.0,
                    0
                    ));
                Buldge.Add(group.Double(42) ?? 0.0);
            }
            //If closed add another vertex as copy of first vertex


            ExtrustionDirection = new Location(entityValues, 210, 220, 230);
        }
    }
}
