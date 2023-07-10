using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bigdata
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        OpenFileDialog op;Thread th;
        private void button1_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (op.ShowDialog()==DialogResult.OK)
            {
                th = new Thread(bsl); th.Start();
            }
        }
        private void bsl()
        {
            using (FileStream fs = File.Open(op.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    GC.Collect();
                    GC.WaitForFullGCApproach();

                    richTextBox1.AppendText(line+"\r");

                    GC.Collect();
                    GC.WaitForFullGCApproach();
                    
                }
            }
        }
    }
}
