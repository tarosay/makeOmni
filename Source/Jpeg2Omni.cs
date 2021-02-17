using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOmni
{
    public class Jpeg2Omni
    {
        public string JpegFilename = "";
        public int Width = 0;
        public int Height = 0;

        private byte[][] MetaBlocks = new byte[5][];

        public Jpeg2Omni()
        {
            setMetaBlocks();
        }

        public Jpeg2Omni(string jpegfilename, int width, int height)
        {
            this.JpegFilename = jpegfilename;
            this.Width = width;
            this.Height = height;

            setMetaBlocks();
        }

        private void setMetaBlocks()
        {
            {
                byte[][] data = new byte[11][];
                data[0] = Encoding.ASCII.GetBytes("http://ns.adobe.com/xap/1.0/");
                data[1] = new byte[] { 0 };
                data[2] = Encoding.ASCII.GetBytes("<?xpacket begin=\"");
                data[3] = new byte[] { 0xef, 0xbb, 0xbf };
                data[4] = Encoding.ASCII.GetBytes("\" id=\"W5M0MpCehiHzreSzNTczkc9d\"?>\n");
                data[5] = Encoding.ASCII.GetBytes("<x:xmpmeta xmlns:x=\"adobe:ns:meta/\" xmptk=\"mana544 Equirectangular Illustration Ver1.10\">\n");
                data[6] = Encoding.ASCII.GetBytes("  <rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">\n");
                data[7] = Encoding.ASCII.GetBytes("    <rdf:Description rdf:about=\"\" xmlns:GPano=\"http://ns.google.com/photos/1.0/panorama/\">\n");
                data[8] = Encoding.ASCII.GetBytes("      <GPano:ProjectionType>equirectangular</GPano:ProjectionType>\n");
                data[9] = Encoding.ASCII.GetBytes("      <GPano:UsePanoramaViewer>True</GPano:UsePanoramaViewer>\n");
                data[10] = Encoding.ASCII.GetBytes("      <GPano:CroppedAreaImageWidthPixels>");
                List<byte> blkData = new List<byte>();
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        blkData.Add(data[i][j]);
                    }
                }
                this.MetaBlocks[0] = blkData.ToArray();
            }
            {
                byte[][] data = new byte[2][];
                data[0] = Encoding.ASCII.GetBytes("</GPano:CroppedAreaImageWidthPixels>\n");
                data[1] = Encoding.ASCII.GetBytes("      <GPano:CroppedAreaImageHeightPixels>");
                List<byte> blkData = new List<byte>();
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        blkData.Add(data[i][j]);
                    }
                }
                this.MetaBlocks[1] = blkData.ToArray();
            }
            {
                byte[][] data = new byte[2][];
                data[0] = Encoding.ASCII.GetBytes("</GPano:CroppedAreaImageHeightPixels>\n");
                data[1] = Encoding.ASCII.GetBytes("      <GPano:FullPanoWidthPixels>");
                List<byte> blkData = new List<byte>();
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        blkData.Add(data[i][j]);
                    }
                }
                this.MetaBlocks[2] = blkData.ToArray();
            }
            {
                byte[][] data = new byte[2][];
                data[0] = Encoding.ASCII.GetBytes("</GPano:FullPanoWidthPixels>\n");
                data[1] = Encoding.ASCII.GetBytes("      <GPano:FullPanoHeightPixels>");
                List<byte> blkData = new List<byte>();
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        blkData.Add(data[i][j]);
                    }
                }
                this.MetaBlocks[3] = blkData.ToArray();
            }
            {
                byte[][] data = new byte[9][];
                data[0] = Encoding.ASCII.GetBytes("</GPano:FullPanoHeightPixels>\n");
                data[1] = Encoding.ASCII.GetBytes("      <GPano:CroppedAreaLeftPixels>0</GPano:CroppedAreaLeftPixels>\n");
                data[2] = Encoding.ASCII.GetBytes("      <GPano:CroppedAreaTopPixels>0</GPano:CroppedAreaTopPixels>\n");
                data[3] = Encoding.ASCII.GetBytes("      <GPano:PosePitchDegrees>0</GPano:PosePitchDegrees>\n");
                data[4] = Encoding.ASCII.GetBytes("      <GPano:PoseRollDegrees>0</GPano:PoseRollDegrees>\n");
                data[5] = Encoding.ASCII.GetBytes("    </rdf:Description>\n");
                data[6] = Encoding.ASCII.GetBytes("  </rdf:RDF>\n");
                data[7] = Encoding.ASCII.GetBytes("</x:xmpmeta>\n");
                data[8] = Encoding.ASCII.GetBytes("<?xpacket end=\"r\"?>\n");
                List<byte> blkData = new List<byte>();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        blkData.Add(data[i][j]);
                    }
                }
                this.MetaBlocks[4] = blkData.ToArray();
            }
        }

        public bool WriteMetaBlock(string jpegfilename, int width, int height)
        {
            this.JpegFilename = jpegfilename;
            this.Width = width;
            this.Height = height;

            return WriteMetaBlock();
        }

        public bool WriteMetaBlock()
        {
            if (this.JpegFilename == "" || !File.Exists(this.JpegFilename))
            {
                return false;
            }

            byte[] binData = File.ReadAllBytes(this.JpegFilename);
            int add = 0;

            for (int i = 0; i < binData.Length - 1; i++)
            {
                if (binData[i] == 0xff)
                {
                    if (binData[i + 1] == 0xe1)
                    {
                        add = i;
                        break;
                    }
                    else if (binData[i + 1] == 0xdb)
                    {
                        add = i;
                        break;
                    }
                }
            }

            if (add == 0) { return false; }

            List<byte> omni = new List<byte>();

            for (int i = 0; i < add; i++)
            {
                omni.Add(binData[i]);
            }

            omni.Add(0xFF);
            omni.Add(0xE1);

            List<byte> ffe1 = new List<byte>();
            byte[] bin;

            for (int i = 0; i < this.MetaBlocks[0].Length; i++)
            {
                ffe1.Add(this.MetaBlocks[0][i]);
            }
            bin = Encoding.ASCII.GetBytes(this.Width.ToString());
            for (int i = 0; i < bin.Length; i++)
            {
                ffe1.Add(bin[i]);
            }

            for (int i = 0; i < this.MetaBlocks[1].Length; i++)
            {
                ffe1.Add(this.MetaBlocks[1][i]);
            }
            bin = Encoding.ASCII.GetBytes(this.Height.ToString());
            for (int i = 0; i < bin.Length; i++)
            {
                ffe1.Add(bin[i]);
            }

            for (int i = 0; i < this.MetaBlocks[2].Length; i++)
            {
                ffe1.Add(this.MetaBlocks[2][i]);
            }
            bin = Encoding.ASCII.GetBytes(this.Width.ToString());
            for (int i = 0; i < bin.Length; i++)
            {
                ffe1.Add(bin[i]);
            }

            for (int i = 0; i < this.MetaBlocks[3].Length; i++)
            {
                ffe1.Add(this.MetaBlocks[3][i]);
            }
            bin = Encoding.ASCII.GetBytes(this.Height.ToString());
            for (int i = 0; i < bin.Length; i++)
            {
                ffe1.Add(bin[i]);
            }

            for (int i = 0; i < this.MetaBlocks[4].Length; i++)
            {
                ffe1.Add(this.MetaBlocks[4][i]);
            }

            byte[] byteFFE1 = ffe1.ToArray();
            int size = byteFFE1.Length + 2;

            omni.Add((byte)(size / 256));
            omni.Add((byte)(size % 256));

            for (int i = 0; i < byteFFE1.Length; i++)
            {
                omni.Add(byteFFE1[i]);
            }

            for (int i = add; i < binData.Length; i++)
            {
                omni.Add(binData[i]);
            }

            bin = omni.ToArray();
            File.WriteAllBytes(this.JpegFilename, bin);

            return true;
        }
    }
}
