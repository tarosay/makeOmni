using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeOmni
{
    public partial class MakeOmni_Form : Form
    {
        public byte[][] MetaBlocks = new byte[5][];

        public MakeOmni_Form()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void MakeOmni_Form_Load(object sender, EventArgs e)
        {

        }

        private void MakeOmni_Form_Shown(object sender, EventArgs e)
        {

        }

        private void MakeOmni_Form_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void MakeOmni_Form_DragDrop(object sender, DragEventArgs e)
        {
            DragDropMethod(e);
        }

        private void MakeOmni_Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) { e.Effect = DragDropEffects.Copy; }
        }
        private void DragDropMethod(DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (filenames.Length == 0 || !File.Exists(filenames[0]))
            {
                return;
            }

            Image image = null;
            string filename = "";
            int width = 0;
            int height = 0;

            try
            {
                filename = filenames[0];
                image = Bitmap.FromFile(filename);

                Image imgOld = pbxData.Image;
                pbxData.Image = (Image)image.Clone();

                if (imgOld != null)
                {
                    imgOld.Dispose();
                    imgOld = null;
                }

                width = image.Width;
                height = image.Height;

                string jpgFilename = Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + "_Omni.jpg";

                image.Save(jpgFilename, System.Drawing.Imaging.ImageFormat.Jpeg);

                Jpeg2Omni jpg2omni = new Jpeg2Omni(jpgFilename, width, height);

                bgWorker.RunWorkerAsync(jpg2omni);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return;
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Jpeg2Omni jpg2omni = (Jpeg2Omni)e.Argument;

            jpg2omni.WriteMetaBlock();
        }
    }
}
