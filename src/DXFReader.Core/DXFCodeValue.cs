using DXFReader.Core.Read;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DXFReader.Core
{
    public static class DXFCodeValueLinq
    {
        public static string String(this List<DXFCodeValue> list, int code, int nth = 0, string def = "" )
        {
            return list.Where(cv => cv.Code == code).Skip(nth).FirstOrDefault()?.String ?? def;
        }
        public static bool Boolean(this List<DXFCodeValue> list, int code, int nth = 0, bool def = false)
        {
            return list.Where(cv => cv.Code == code).Skip(nth).FirstOrDefault()?.Bool ?? def;
        }
        public static double? Double(this List<DXFCodeValue> list, int code, int start_line = 0)
        {
            
            return list.Where(cv => cv.Code == code).Skip(start_line).FirstOrDefault()?.Double;
        }
        public static short Int16(this List<DXFCodeValue> list, int code, int nth = 0, short def = 0)
        {
            return list.Where(cv => cv.Code == code).Skip(nth).FirstOrDefault()?.Int16 ?? def;
        }
        public static int Int32(this List<DXFCodeValue> list, int code, int nth = 0, int def = 0)
        {
            return list.Where(cv => cv.Code == code).Skip(nth).FirstOrDefault()?.Int32 ?? def;
        }
        public static long Int64(this List<DXFCodeValue> list, int code, int nth = 0, long def = 0)
        {
            return list.Where(cv => cv.Code == code).Skip(nth).FirstOrDefault()?.Int64 ?? def;
        }

        public static List<DXFCodeValue> WhereContainsCodeValue(this List<DXFCodeValue> list, int code, string value,int nth=0)
        {

            //myCVList(0,"ENTITIES")
            return list
                .Where(cv => cv.CodeValues.Any(sub_cv => sub_cv.Code == code && sub_cv.String == value))
                .ToList() ;

        }

        public static List<DXFCodeValue> WhereCodeValue(this List<DXFCodeValue> list, int code, string value, int nth = 0)
        {

            //myCVList(0,"ENTITIES")
            return list
                .Where(cv => cv.Code == code && cv.String == value).ToList();

        }

        /// <summary>
        /// Split list of groups by starting code. All Codevalues found until next 'code' will be grouped.
        /// </summary>
        /// <param name="list">List of DXFCodeValues</param>
        /// <param name="code">The code to split on</param>
        /// <returns>List of List of Code vaues - each with first element having the specified code</returns>
        public static List<List<DXFCodeValue>> GroupsStartingWithCode(this List<DXFCodeValue> list, int code)
        {
            

            List<List<DXFCodeValue>> GroupedLists = new List<List<DXFCodeValue>>();

            int line_start = 0;
            int line_end = 0;
            bool eol = false;
            bool first_code = true; //Skip items before the first found item
            while (!eol)
            {
                //Loop to find a 10
                if(list[line_end].Code == code)
                {
                    if (first_code){
                        first_code = false;
                    } else {
                        GroupedLists.Add(list.GetRange(line_start, line_end - line_start));
                    }
                    
                    line_start = line_end;
                }
                line_end++;
                eol = line_end >= list.Count() ;
            }
            GroupedLists.Add(list.GetRange(line_start, line_end - line_start));

            return GroupedLists;
        }

    }
    public class DXFCodeValue
    {
        private static List<string> EntityList = new List<string>
        {
            {"3DFACE"},
            {"3DSOLID"},
            {"ACAD_PROXY_ENTITIY"},
            {"ARC"},
            {"ATTDEF"},
            {"ATTRIM"},
            {"BODY"},
            {"CIRCLE"},
            {"DIMENSION"},
            {"ELLIPSE"},
            {"HATCH"},
            {"HELIX"},
            {"IMAGE"},
            {"INSERT"},
            {"LEADER"},
            {"LIGHT"},
            {"LINE"},
            {"LWPOLYLINE"},
            {"MESH"},
            {"MLINE"},
            {"MLEADERSTYLE"},
            {"MLEADER"},
            {"MTEXT"},
            {"OLEFRAME"},
            {"OLE2FRAME"},
            {"POINT"},
            {"POLYLINE"},
            {"RAY"},
            {"REGION"},
            {"SECTION"},
            {"SEQEND"},
            {"SHAPE"},
            {"SOLID"},
            {"SPLINE"},
            {"SUN"},
            {"SURFACE"},
            {"TABLE"},
            {"TEXT"},
            {"TOLERANCE"},
            {"TRACE"},
            {"UNDERLAY"},
            {"VERTEX"},
            {"VIEWPORT"},
            {"WIPEOUT"},
            {"XLINE"},
        };
        private static List<string> ClosingValues = new List<string>
        {
            {"ENDSEC"},
            {"ENDTAB"},
            {"ENDBLK"}
        };
        public int Code { get; set; }
        public object Value { get; set; }
        //DXFCodeValue Parent { get; set; }
        public List<DXFCodeValue> CodeValues { get; set; } = new List<DXFCodeValue>();
        public DXFCodeValue(int code, object value)
        {
            Code = code;
            Value = value;
        }
        public DXFCodeValue AddChildren(DXFReaderBase reader)
        {
            var Next = reader.Next();
            while (this.IsParent(Next))
            {
                CodeValues.Add(Next);
                Next = Next.AddChildren(reader);
            }
            return Next;


        }
        private bool HasChild()
        {
            //ENDSEC, ENDTAB, ENDBLK
            if (this.Code == 0 && !DXFCodeValue.ClosingValues.Any(e => e == this.String))
            {
                return true;
            }
            else if (this.Code == 9)
            {
                return true;
            }
            return false;
        }
        private bool IsParent(DXFCodeValue Child)
        {
            if (!this.HasChild()) //Cant be a parent if you never have children
                return false;

            //Check Child Exceptions, everything else is a child
            switch (Child.Code)
            {
                case 0: //NEW ENTITIES
                    if (Child.String == "ENDSEC" && this.String != "SECTION")
                    {
                        //ENDSEC SHOULD ONLY BE CHILD OF SECTION
                        return false;
                    }
                    else if (Child.String == "ENDTAB" && this.String != "TABLE")
                    {
                        //ENDTAB SHOULD ONLY BE CHILD OF TABLE
                        return false;
                    }
                    else if (Child.String == "ENDBLK" && this.String != "BLOCK")
                    {
                        //ENDBLK SHOULD BE CHILD OF BLOCK
                        return false;
                    }
                    else if (Child.String == this.String)
                    {
                        //REPEAT ENTITIES SHOULD NOT BE CHILD OF THEMSELVES (LIKE TABLES, AND DICTIONARY)
                        return false;
                    }
                    else if (DXFCodeValue.EntityList.Any(e => e == Child.String) && this.String != "SECTION")
                    {
                        //ENTITIES LISTED ONLY CHILDS OF SECTION
                        return false;
                    }
                    else if (Child.String == "SECTION")
                    {
                        //SECTION NEVER A CHILD 
                        return false;
                    }
                    else if (Child.String == "EOF")
                    {
                        //SECTION NEVER A CHILD 
                        return false;
                    }
                    return true;
                case 9:
                    if (this.Code == 9 && Child.Code == 9)
                    {
                        //HEADER VARS NOT CHILDS OF THEMSELVES
                        return false;
                    }
                    return true;
                default:
                    return true;
            }
        }
        public string String
        {
            get
            {
                //If byte convert to string
                if (this.Value is byte || this.Value is byte[])
                {
                    return "";
                }
                else
                {
                    return ((string)Value).Trim();
                }
                //If not return as string

            }
        }

        public double Double
        {

            get
            {
                //If byte convert to string
                if (this.Value is byte || this.Value is byte[])
                {
                    return 0.0;
                }
                else
                {
                    if (Double.TryParse(this.String, out double res))
                    {
                        return res;
                    };
                    return 0.0;
                }
                //If not return as string

            }
        }

        public bool Bool
        {

            get
            {
                //If byte convert to string
                if (this.Value is byte || this.Value is byte[])
                {
                    return false;
                }
                else
                {
                    if (Boolean.TryParse(this.String, out bool res))
                    {
                        return res;
                    };
                    return false;
                }
                //If not return as string

            }
        }
        public short Int16
        {

            get
            {
                //If byte convert to string
                if (this.Value is byte || this.Value is byte[])
                {
                    return 0;
                }
                else
                {
                    if (Int16.TryParse(this.String, out Int16 res))
                    {
                        return res;
                    };
                    return 0;
                }
                //If not return as string

            }
        }

        public int Int32
        {

            get
            {
                //If byte convert to string
                if (this.Value is byte || this.Value is byte[])
                {
                    return 0;
                }
                else
                {
                    if (Int32.TryParse(this.String, out Int32 res))
                    {
                        return res;
                    };
                    return 0;
                }
                //If not return as string

            }
        }
        public long Int64
        {

            get
            {
                //If byte convert to string
                if (this.Value is byte || this.Value is byte[])
                {
                    return 0;
                }
                else
                {
                    if (Int64.TryParse(this.String, out Int64 res))
                    {
                        return res;
                    };
                    return 0;
                }
                //If not return as string

            }
        }

    }
}
