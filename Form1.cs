using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Threading;
namespace Exercicio7._4
{
    public partial class Form1 : Form
    {
        Process[] processosEmExecucao;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            atualizarLabelsMemoria();
        }

        //Metodo que retorna o total de memoria que o pc possui
        public ulong memoriaTotalPc()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
        }

        public ulong memoriaLivrelPc()
        {
            return (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory - (new Microsoft.VisualBasic.Devices.ComputerInfo().TotalVirtualMemory));
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            
            listBox1.Items.Clear();
            processosEmExecucao = Process.GetProcesses(".");
            for (int i = 0; i < processosEmExecucao.Length;i++ )
            {
                String propriedadesProcessos = "PID = "+processosEmExecucao[i].Id.ToString()+" |"+"  Nome = "+processosEmExecucao[i].ProcessName;
                listBox1.Items.Add(propriedadesProcessos);
                listBox1.Sorted = true;
            }
            label3.Text = "Processos Ativos: "+processosEmExecucao.Length;
            atualizarLabelsMemoria();
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            label3.Text = "Processos Ativos: ";
            atualizarLabelsMemoria();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string processoSelecionado = listBox1.SelectedItem.ToString();
                string[] splitstring = processoSelecionado.Split('=', '|');

                Process processoMorto = Process.GetProcessById(int.Parse(splitstring[1]));
                processoMorto.Kill();
                listBox1.Items.Clear();
                atualizarLabelsMemoria();
                button1.PerformClick();
            }
            catch(NullReferenceException)
            {
                button1.PerformClick();
                MessageBox.Show("Erro, selecione um dos processos da tela e depois mate-o!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int numPID = int.Parse(textBox1.Text);
                Process processoManipulado;
                processoManipulado = Process.GetProcessById(numPID);
                textBox2.Text = processoManipulado.Threads.Count.ToString();
                Form2 form2 = new Form2(processoManipulado);
                form2.Show();
            }
            catch (FormatException)
            {
                MessageBox.Show("Erro, entre com o numero PID do processo!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Erro, entre com um numero PID do processo válido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                button1.PerformClick();
            }
            atualizarLabelsMemoria();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        public void atualizarLabelsMemoria()
        {
            PerformanceCounter contadorRam = new PerformanceCounter("Memory", "Available Mbytes");
            PerformanceCounter contadorCPU = new PerformanceCounter();
            contadorCPU = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            label4.Text = "Total Mémoria RAM:" + (memoriaTotalPc()) / 1024 / 1024 + "MB";
            label5.Text = "Mémoria Ram Utilizada:" + contadorRam.NextValue() + " MB";
            label6.Text = "Mémoria Ram Livre:" + (memoriaLivrelPc() / 1024) / 1024 + "MB";
            for (int i = 0; i < 9;i++ )
            {
                label7.Text = "Uso da CPU: " + (contadorCPU.NextValue()).ToString("0.0")+"%";
                Thread.Sleep(30);
            }
        }

        

    }

}
