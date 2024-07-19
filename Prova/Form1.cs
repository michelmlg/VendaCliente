using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prova
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BLL.conecta();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BLL.desconecta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente.setCNPJ(textBox1.Text);

            BLL.validaCNPJ();

            if(Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
            {
                textBox2.Text = Cliente.getNome();

                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();

                double somaToneladas = 0;
                double somaValores = 0;

                BLL.getProximaVenda(); 
                while (!Erro.getErro())
                {
                    string toneladasStr = VendaCliente.getToneladas();
                    double toneladasDbl = Double.Parse(toneladasStr);
                    somaToneladas += toneladasDbl;

                    string valoresStr = VendaCliente.getValor();
                    double valoresDbl = Double.Parse(valoresStr);
                    somaValores += valoresDbl;

                    listBox1.Items.Add(VendaCliente.getData());
                    listBox2.Items.Add(toneladasStr);
                    listBox3.Items.Add(valoresStr);

                    BLL.getProximaVenda();
                }

                textBox3.Text = somaToneladas.ToString();
                textBox4.Text = somaValores.ToString();
            }
        }
    }
}
