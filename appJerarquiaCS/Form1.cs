using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appJerarquiaCS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) { return; }

            StreamReader sr = new StreamReader(ofd.FileName);
            LlenarArbol(sr);

            
        }
        private void LlenarArbol(StreamReader sr)
        {
            string renglon;
            string x = "";
            TreeNode nodo = new TreeNode();

            while ((renglon = sr.ReadLine()) != null)
            {
                string[] datos = renglon.Split('|');
                if (x != datos[0] && x != "")
                {
                    
                    nodo.Text = datos[5];
                    treeView1.Nodes.Add(nodo);
                    nodo = new TreeNode();
                }
                nodo.Nodes.Add(x);
                x = datos[0];

            }
            
        }
    }
}
