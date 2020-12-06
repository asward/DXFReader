using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class PolyLine : Entity
    {
        //public int NumPoints { get; set; }
        //public short PolylineFlag { get; set; }
        //public List<Location> Vertices = new List<Location>();
        //public List<double> Buldge = new List<double>();
        //public Location ExtrustionDirection { get; set; }

        public PolyLine(List<DXFCodeValue> entityValues) : base(entityValues)
        {
            //var entityValues = entityCodeValues.First().CodeValues;
            //NumPoints = entityValues.Int32(90); 
            //PolylineFlag = entityValues.Int16(70);
            //for (int i = 0; i < NumPoints; i++)
            //{
                
            //    Vertices.Add(new Location(
            //        entityValues.Double(10,i),
            //        entityValues.Double(20,i),
            //        entityValues.Double(30,i)
            //        ));
            //    Buldge.Add(entityValues.Double(42, i));
            //}


            //ExtrustionDirection = new Location(entityValues, 210, 220, 230);
        }
    }
}
