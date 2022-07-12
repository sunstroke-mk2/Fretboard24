using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fret24
{
    public partial class Form2 : Form
    {
        Form1 mainForm;
        public Form2()
        {
            
            InitializeComponent();
        }

  
        public void init(Form1 _mainForm)
        {
            mainForm = _mainForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.getConfig((int)numericUpDown2.Value, (int)numericUpDown1.Value, textBox2.Text.Split(','), textBox1.Text.Split(','));
        }
   
    }

}
