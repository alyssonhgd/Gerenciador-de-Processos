using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Exercicio7._4
{
    public partial class Form2 : Form
    {
        private System.Diagnostics.Process processoManipulado;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(System.Diagnostics.Process processoManipulado)
        {
            InitializeComponent();
            this.processoManipulado = processoManipulado;
            ProcessThreadCollection colecaoThreads = processoManipulado.Threads;
            foreach(ProcessThread pt in colecaoThreads)
            {
            string exibirThread = "Thread ID    " +pt.Id+ "   Prioridade: "+pt.PriorityLevel;
            listBox1.Items.Add(exibirThread);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2.ActiveForm.Close();
        }
    }
}
