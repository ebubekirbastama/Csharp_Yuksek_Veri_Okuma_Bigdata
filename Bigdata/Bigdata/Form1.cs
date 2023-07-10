using BekraRamclear;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bigdata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        Thread th;
        OpenFileDialog op;
        private void button1_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (op.ShowDialog()==DialogResult.OK)
            {
                th = new Thread(oku); th.Start();
            }
        }
        StringBuilder s = new StringBuilder();
        private void oku()
        {
            int syc = 1;
            using (FileStream fs = File.Open(op.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                //BekraRamClearr.temizle();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    GC.Collect();
                    GC.WaitForFullGCApproach();
                    s.AppendLine(line + ",");
                    syc++;
                    label1.Text = syc.ToString();
                    GC.Collect();
                    GC.WaitForFullGCApproach();
                    //richTextBox1.AppendText(line);
                }
                BekraRamClearr.temizle();
            }
        }
    }
}
