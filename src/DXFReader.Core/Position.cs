using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace DXFReader.Core
{
    public struct Location
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; } 
        public Location(List<DXFCodeValue> codeValues, int X_Code, int Y_Code, int? Z_Code = null)
        {
            X = codeValues.Double(X_Code) ?? 0.0;
            Y = codeValues.Double(Y_Code) ?? 0.0;
            Z = Z_Code == null ? 0.0: codeValues.Double((int) Z_Code) ?? 0.0;
        }
        public Location(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0;
        }
        public Location(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
