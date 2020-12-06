using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFReader.Core.Read
{
    public abstract class DXFReaderBase
    {
        public DXFCodeValue Current {get;set;}
        public abstract DXFCodeValue Next();
        public abstract Task<DXFCodeValue> NextAsync();

    }
    public class DXFBinaryReader : DXFReaderBase
    {
        BinaryReader Reader { get; set; }
        public DXFBinaryReader(BinaryReader reader)
        {
            Reader = reader;
        }
        Stream stream { get; set; }
        public override DXFCodeValue Next()
        {
            int code = Reader.ReadInt32();
            byte value= Reader.ReadByte(); //TODO Determine how far to read based on 'code' value - then read that far.
            Current = new DXFCodeValue(code, value);
            return Current;
        }

        public override Task<DXFCodeValue> NextAsync()
        {
            throw new NotImplementedException();
        }
    }
    public class DXFASCIIReader : DXFReaderBase
    {
        ASCIILineReader Reader { get; set; }
        public DXFASCIIReader(ASCIILineReader reader)
        {
            Reader = reader;
        }
        Stream stream { get; set; }
        public override DXFCodeValue Next()
        {

            int code = Int32.Parse(Reader.ReadLine().Trim());
            string value = Reader.ReadLine(); 

            Current = new DXFCodeValue(code, value);
            return Current;

        }
        public async override Task<DXFCodeValue> NextAsync()
        {

            string codeString = await Reader.ReadLineAsync();
            int code = Int32.Parse(codeString.Trim());
            string value = await Reader.ReadLineAsync();

            Current = new DXFCodeValue(code, value);
            return Current;

        }
    }

}
