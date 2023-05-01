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
    public partial class DataGridViewTests : Form
    {
        public DataGridViewTests()
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rowIndex = int.Parse(textBox1.Text);
            dataGridView1.ClearSelection();
            dataGridView1.Rows[rowIndex].Selected = true;
        }
    }
}
