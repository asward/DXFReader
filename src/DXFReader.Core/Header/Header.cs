using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DXFReader.Core.Header
{
    public class DXFHeader
    {
        public List<HeaderVariable> HeaderVariables = new List<HeaderVariable>();
        public DXFHeader()
        {

        }

        public void Load(List<DXFCodeValue> documentCodeValuesTree)
        {
            var headerSection = documentCodeValuesTree
               .Where(d => d.Code == 0 || d.String == "SECTION")
               .Where(d => d.CodeValues.Any(dd => d.Code == 2 && d.String == "HEADER"))
               .SingleOrDefault();

            if (null != headerSection)
            {
                foreach (var headerVar in headerSection.CodeValues)
                {
                    if (headerVar.Code == 9)
                    {
                        HeaderVariables.Add(new HeaderVariable(headerVar.String, headerVar.CodeValues.First().Value));
                    }
                }
            }
        }
    }
    public class HeaderVariable
    {
        public string Name {get;set;}
        public object Value { get; set; }
        public HeaderVariable(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
