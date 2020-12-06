using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class _3DFace : Entity
    {
        public List<Location> Points { get; set; } = new List<Location>();

        public struct Visibility
        {
            public bool Edge1;
            public bool Edge2;
            public bool Edge3;
            public bool Edge4;
        }
        public Visibility EdgeVisiblity = new Visibility();
        public _3DFace(List<DXFCodeValue> entityValues) : base(entityValues)
        {
            Points.Add(new Location(entityValues, 10, 20, 30));
            Points.Add(new Location(entityValues, 11, 21, 31));
            Points.Add(new Location(entityValues, 12, 22, 32));
            Points.Add(new Location(entityValues, 13, 23, 33));

            var edgeFlags = entityValues.Where(c => c.Code == 70);
            EdgeVisiblity.Edge1 = edgeFlags.Any(ef => ef.Int16 == 1);
            EdgeVisiblity.Edge1 = edgeFlags.Any(ef => ef.Int16 == 2);
            EdgeVisiblity.Edge1 = edgeFlags.Any(ef => ef.Int16 == 3);
            EdgeVisiblity.Edge1 = edgeFlags.Any(ef => ef.Int16 == 4);
        }
    }
}
