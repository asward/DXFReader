using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DXFReader.Core.Entities
{
    
    public class Entity
    {
        public string EntityType { get; set; } //0 - string
        public string Handle { get; set; } //5 - string
        public string LayerName { get; set; } //8 - string

        public Entity(List<DXFCodeValue> entityCodeValues)
        {
            EntityType = entityCodeValues.Where(cv => cv.Code == 0).SingleOrDefault()?.String;
            Handle= entityCodeValues.Where(cv => cv.Code == 5).SingleOrDefault()?.String;
            LayerName = entityCodeValues.Where(cv => cv.Code == 8).SingleOrDefault()?.String;
        }
    }

    
    public class DXFEntities
    {
        public static class TYPE
        {
            public static string ENTITIES { get; set; } = "ENTITIES";
            public static string _3DFace { get; set; } = "3DFACE";
            public static string _3DSolid { get; private set; } = "3DSOLID";
            public static string Arc { get; private set; } = "ARC";
            public static string Circle { get; private set; } = "CIRCLE";
            public static string Ellipse { get; private set; } = "ELLIPSE";
            public static string Line { get; set; } = "LINE";
            public static string LWPolyLine { get; private set; } = "LWPOLYLINE";
            public static string PolyLine { get; private set; } = "POLYLINE";
            public static string Spline { get; private set; } = "SPLINE";
        };
        public List<_3DFace> _3DFaces {get; set; } = new List<_3DFace>();
        public List<_3DSolid> _3DSolids { get; set; } = new List<_3DSolid>();
        public List<Arc> Arcs { get; private set; } = new List<Arc>();
        public List<Circle> Circles { get; private set; } = new List<Circle>();
        public List<Ellipse> Ellipses { get; private set; } = new List<Ellipse>();
        public List<Line> Lines { get; private set; } = new List<Line>();
        public List<LWPolyLine> LWPolyLines { get; private set; } = new List<LWPolyLine>();
        public List<PolyLine> PolyLines { get; private set; } = new List<PolyLine>();
        public List<Spline> Splines { get; private set; } = new List<Spline>();

        public void Load(List<DXFCodeValue> documentCodeValues, double scale = 1)
        {
            var entitySection = documentCodeValues
               .WhereContainsCodeValue(2, "ENTITIES").First().CodeValues ;

            entitySection.WhereCodeValue(0, TYPE._3DFace).ForEach(e => _3DFaces.Add(new _3DFace(e.CodeValues)));
            entitySection.WhereCodeValue(0, TYPE._3DSolid).ForEach(e => _3DSolids.Add(new _3DSolid(e.CodeValues)));
            entitySection.WhereCodeValue(0, TYPE.Arc).ForEach(e => Arcs.Add(new Arc(e.CodeValues)));
            entitySection.WhereCodeValue(0, TYPE.Circle).ForEach(e => Circles.Add(new Circle(e.CodeValues)));
            entitySection.WhereCodeValue(0, TYPE.Ellipse).ForEach(e => Ellipses.Add(new Ellipse(e.CodeValues)));
            entitySection.WhereCodeValue(0, TYPE.Line).ForEach(e => Lines.Add(new Line(e.CodeValues)));
            entitySection.WhereCodeValue(0, TYPE.LWPolyLine).ForEach(e => LWPolyLines.Add(new LWPolyLine(e.CodeValues)));
            entitySection.WhereCodeValue(0, TYPE.PolyLine).ForEach(e => PolyLines.Add(new PolyLine(e.CodeValues)));
            entitySection.WhereCodeValue(0, TYPE.Spline).ForEach(e => Splines.Add(new Spline(e.CodeValues)));

        }
        public List<Entities.Entity> EntityList_2D { 
            get {
                List<Entity> eList = new List<Entity>();
                eList.AddRange(Arcs);
                eList.AddRange(Circles);
                eList.AddRange(Ellipses);
                eList.AddRange(Lines);
                eList.AddRange(LWPolyLines);
                eList.AddRange(PolyLines);
                eList.AddRange(Splines);
                return eList;
            } 
        }

    }
}
