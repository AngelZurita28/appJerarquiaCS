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
            //string y = "";
            TreeNode ciudad = new TreeNode();
            TreeNode estado = new TreeNode();
            TreeNode codigoPostal = new TreeNode();
            TreeNode colonia = new TreeNode();
            string cd = "";
            string cp = "";
            //while ((renglon = sr.ReadLine()) != null)
            //{
            //    string[] datos = renglon.Split('|');
            //    if (x != datos[0] && x != "")
            //    {                    
            //        if (nodo.Text != datos[5] && nodo.Text != "")
            //        {
            //            treeView1.Nodes.Add(nodo);
            //            nodo = new TreeNode();
            //        }
            //    }
            //    nodo.Text = datos[5];
            //    nodo.Nodes.Add(x);
            //    x = datos[0];
            //}
            //treeView1.Nodes.Add(nodo);

            while ((renglon = sr.ReadLine()) != null)
            {
                string[] datos = renglon.Split('|');
                if (colonia.Text != datos[1] && x != "")
                {
                    if (codigoPostal.Text != datos[0] && ciudad.Text != "")
                    {
                        if (ciudad.Text != datos[5] && ciudad.Text != "")
                        {
                            if (estado.Text != datos[4] && estado.Text != "")
                            {
                                treeView1.Nodes.Add(estado.Text);
                                estado = new TreeNode();

                            }
                        }
                        estado.Text = datos[5];
                        estado.Nodes.Add(ciudad);
                        ciudad = new TreeNode();
                    }
                    ciudad.Text = datos[1];
                    codigoPostal.Nodes.Add(ciudad);
                    codigoPostal = new TreeNode();
                }
                codigoPostal.Text = datos[5];
                codigoPostal.Nodes.Add(codigoPostal);
                colonia.Text = datos[0];

            }
        }
    }
}
