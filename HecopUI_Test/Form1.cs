using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using HecopUI_Winforms.Controls.Charts;
using System.Collections.Generic;
using HecopUI_Winforms.Controls.Charts.FunnelChart;
using HecopUI_Winforms.Controls;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using static HecopUI_Winforms.Win32.ShellAPI;
using HecopUI_Winforms.Win32;
using static HecopUI_Winforms.Forms.HMessageBox;
using HecopUI_Winforms.HDialog;

namespace HecopUI_Test
{
    public partial class Form1 : HecopUI_Winforms.Forms.HForm
    {
     
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         

        }

        private void hCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            hCheckBox1.Text = hCheckBox1.Name+" (Checked: "+ hCheckBox1.Checked+")";
        }

        private void hCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            hCheckBox2.Text = hCheckBox2.Name + " (Checked: " + hCheckBox2.Checked + ")";
        }

        private void hCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            hCheckBox3.Text = hCheckBox3.Name + " (Checked: " + hCheckBox3.Checked + ")";
        }
    }
}
