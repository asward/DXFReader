using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXFReader.Core.Entities
{

    public class Spline : Entity
    {

        public Location NormalVector { get; set; }
        public struct SPLINE_TYPE
        {
            public bool IS_CLOSED;
            public bool IS_PERIODIC;
            public bool IS_RATIONAL;
            public bool IS_PLANAR;
            public bool IS_LINEAR;
            public SPLINE_TYPE(short bit_code)
            {
                IS_CLOSED = (bit_code & (1 << 0)) != 0; 
                IS_PERIODIC = (bit_code & (1 << 1)) != 0;
                IS_RATIONAL = (bit_code & (1 << 2)) != 0;
                IS_PLANAR = (bit_code & (1 << 3)) != 0;
                IS_LINEAR = (bit_code & (1 << 4)) != 0;
            }
        }
        public SPLINE_TYPE SplineType { get; set; }
        public short Degree { get; set; }
        public short NumKnots { get; set; }
        public short NumControlPoints { get; set; }
        public short NumFitPoints { get; set; }

        public double KnotTolerance { get; set; }
        public double ControlPointTolerance { get; set; }
        public double FitTolerance { get; set; }

        public Location StartTangent { get; set; }
        public Location EndTangent { get; set; }

        public List<double> Knots { get; set; } = new List<double>();
        public List<double> KnotsWeight { get; set; } = new List<double>();

        public List<Location> ControlPoints { get; set; } = new List<Location>();
        public List<Location> FitPoints { get; set; } = new List<Location>();
        public Location ExtrustionDirection { get; set; }
        public Spline(List<DXFCodeValue> entityValues) : base(entityValues)
        {
            NormalVector = new Location(entityValues, 210, 220, 230);
            SplineType = new SPLINE_TYPE(entityValues.Int16(70));

            Degree = entityValues.Int16(71);
            NumKnots = entityValues.Int16(72);
            NumControlPoints = entityValues.Int16(73);
            NumFitPoints = entityValues.Int16(74);


            NumFitPoints = entityValues.Int16(74);
            KnotTolerance = entityValues.Double(42) ?? 0.0;
            ControlPointTolerance = entityValues.Double(43) ?? 0.0;
            FitTolerance = entityValues.Double(44) ?? 0.0;

            StartTangent = new Location(entityValues, 12, 22, 32);
            EndTangent = new Location(entityValues, 13, 23, 33);

            for (int i = 0; i < NumKnots; i++)

            {
                Knots.Add(entityValues.Double(40, i) ?? 0.0);
                KnotsWeight.Add(entityValues.Double(41, i) ?? 0.0);
            }

            for (int i = 0; i < NumControlPoints; i++)

            {
                ControlPoints.Add(new Location(
                    entityValues.Double(10, i) ?? 0.0,
                    entityValues.Double(20, i) ?? 0.0,
                    entityValues.Double(30, i) ?? 0.0
                    ));
            }
            for (int i = 0; i < NumFitPoints; i++)

            {
                FitPoints.Add(new Location(
                    entityValues.Double(11, i) ?? 0.0,
                    entityValues.Double(21, i) ?? 0.0,
                    entityValues.Double(31, i) ?? 0.0
                    ));
            }

            ExtrustionDirection = new Location(entityValues, 210, 220, 230);
        }
    }
}
