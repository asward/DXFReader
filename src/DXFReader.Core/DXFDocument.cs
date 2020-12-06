using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DXFReader.Core.Read;

using DXFReader.Core.Header;
using DXFReader.Core.Classes;
using DXFReader.Core.Tables;
using DXFReader.Core.Blocks;
using DXFReader.Core.Entities;
using DXFReader.Core.Objects;
using DXFReader.Core.Thumbnailimage;
using System.Threading.Tasks;

namespace DXFReader.Core
{
    public class DXFDocument
    {
        string Filename { get; set; }
        public DXFReaderBase DXFReader { get; set; }
        List<DXFCodeValue> CodeValues { get; set; } = new List<DXFCodeValue>();


        public DXFHeader Header { get; set; } = new DXFHeader();
        public DXFClasses Classes { get; set; } = new DXFClasses();
        public DXFTables Tables { get; set; } = new DXFTables();
        public DXFBlocks Blocks { get; set; } = new DXFBlocks();
        public DXFEntities Entities { get; set; } = new DXFEntities();
        public DXFObjects Objects { get; set; } = new DXFObjects();
        public DXFThumbnailimage Thumbnailimage { get; set; } = new DXFThumbnailimage();

        public async Task<bool> IsBinaryAsync(Stream stream)
        {
            return false; //Todo - can't read binary stream async...
        }
        public static bool IsBinary(Stream stream)
        {
            //Check in DXF is stored as binar
            //Autodesk. (2011). DXF Reference [PDF file]. Retrieved from https://images.autodesk.com/adsk/files/autocad_2012_pdf_dxf-reference_enu.pdf

            BinaryReader reader = new BinaryReader(stream);
            //Sentinel excluding <CR><LF><SUB><NULL>
            byte[] sentinel = reader.ReadBytes(22);
            return Encoding.UTF8.GetString(sentinel, 0, sentinel.Length - 4) == "AutoCAD Binary DXF";

        }
        public bool Read()
        {
            if (DXFReader == null)
                throw new Exception("No Reader Specified");


            var Next = DXFReader.Next();
            while (Next.String != "EOF")
            {
                this.CodeValues.Add(Next);
                Next = Next.AddChildren(DXFReader);
            }

            return true;
        }
        public async Task<bool> ReadAsync()
        {
            if (DXFReader == null)
                throw new Exception("No Reader Specified");


            var Next = await DXFReader.NextAsync();
            while (Next.String != "EOF")
            {
                this.CodeValues.Add(Next);
                Next = Next.AddChildren(DXFReader);
            }

            return true;
        }
        public bool LoadFile(string filename, double scale = 1)
        {
            using (Stream stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return LoadFileStream(stream, scale);
            }
        }
        public bool LoadFile(Stream stream, double scale = 1)
        {
            return LoadFileStream(stream, scale);
        }
        public async Task<bool> LoadFileAsync(Stream stream, double scale = 1)
        {
            return await LoadFileStreamAsync(stream, scale);
        }
        private async Task<bool> LoadFileStreamAsync(Stream stream, double scale = 1)
        {

            //Open stream to file and start reading!
            try
            {
                long startingLocation = stream.Position;
                if (await IsBinaryAsync(stream))
                {
                    stream.Position = startingLocation;
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        DXFReader = new DXFBinaryReader(reader);
                        await ReadAsync();
                    }
                }
                else
                {
                    stream.Position = startingLocation;
                    using (ASCIILineReader reader = new ASCIILineReader(stream, Encoding.UTF8, false))
                    {
                        DXFReader = new DXFASCIIReader(reader);
                        await ReadAsync();
                    }
                }

            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("File Not Found: " + fnfe.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unhandeled Error: " + e.Message);
                return false;
            }

            LoadElements();

            return true;

        }
        private bool LoadFileStream(Stream stream, double scale = 1)
        {
            //Open stream to file and start reading!
            try
            {
                long startingLocation = stream.Position;
                if (IsBinary(stream))
                {
                    stream.Position = startingLocation;
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        DXFReader = new DXFBinaryReader(reader);
                        Read();
                    }
                }
                else
                {
                    stream.Position = startingLocation;
                    using (ASCIILineReader reader = new ASCIILineReader(stream, Encoding.UTF8, false))
                    {
                        DXFReader = new DXFASCIIReader(reader);
                        Read();
                    }
                }

            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("File Not Found: " + fnfe.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unhandeled Error: " + e.Message);
                return false;
            }

            LoadElements();

            return true;

        }
        private void LoadElements()
        {

            Header.Load(CodeValues);
            Classes.Load(CodeValues);
            Tables.Load(CodeValues);
            Blocks.Load(CodeValues);
            Entities.Load(CodeValues);
            Objects.Load(CodeValues);
            Thumbnailimage.Load(CodeValues);

        }

    }
}
