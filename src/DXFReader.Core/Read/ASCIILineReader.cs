using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DXFReader.Core
{
    public class ASCIILineReader : StreamReader
    {
        public int Line = 0;
        public ASCIILineReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) :base(stream, encoding, detectEncodingFromByteOrderMarks)
        {

        }

        //public override string ReadLine()
        //{
        //    this.Line++;
        //    return (string)base.ReadLine();
        //}
        public override Task<string> ReadLineAsync()
        {
            this.Line++;
            return base.ReadLineAsync();
        }
    }
}
