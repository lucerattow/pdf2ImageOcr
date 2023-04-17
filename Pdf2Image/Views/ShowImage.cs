using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pdf2Image.Views
{
    public partial class ShowImage : Form
    {
        public ShowImage()
        {
            InitializeComponent();
            button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static void OpenImage(Image image)
        {
            //muestro la imagen
            var showImage = new ShowImage();
            showImage.Image.Image = image;
            showImage.ShowDialog();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Image.Top = -e.NewValue * 20;
        }
    }
}
