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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            //StreamReader sr = new StreamReader(ofd.FileName);
            //LlenarArbol(sr);
            filltree(ofd.FileName);
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

        private void filltree(string filename)
        {
            // Crear un diccionario para almacenar los códigos postales y las colonias correspondientes
            Dictionary<string, List<string>> cpColonias = new Dictionary<string, List<string>>();

            // Leer el archivo de texto que contiene los datos de los códigos postales
            string[] lineas = File.ReadAllLines(filename);

            // Recorrer cada línea del archivo de texto
            foreach (string linea in lineas)
            {
                // Separar los campos de la línea por comas
                string[] campos = linea.Split('|');

                // Obtener el estado, la ciudad, el código postal y la colonia correspondientes
                string estado = campos[4];
                string ciudad = campos[5];
                string cp = campos[0];
                string colonia = campos[1];

                // Verificar si el código postal ya está en el diccionario
                if (!cpColonias.ContainsKey(cp))
                {
                    // Si el código postal no está en el diccionario, agregarlo con una lista vacía de colonias
                    cpColonias.Add(cp, new List<string>());
                }

                // Agregar la colonia correspondiente a la lista de colonias del código postal
                cpColonias[cp].Add(colonia);

                // Verificar si el estado ya está en el árbol
                TreeNode estadoNode = null;
                foreach (TreeNode node in treeView1.Nodes)
                {
                    if (node.Text == estado)
                    {
                        estadoNode = node;
                        break;
                    }
                }
                if (estadoNode == null)
                {
                    // Si el estado no está en el árbol, agregarlo como nodo principal
                    estadoNode = new TreeNode(estado);
                    treeView1.Nodes.Add(estadoNode);
                }

                // Verificar si la ciudad ya está en el árbol
                TreeNode ciudadNode = null;
                foreach (TreeNode node in estadoNode.Nodes)
                {
                    if (node.Text == ciudad)
                    {
                        ciudadNode = node;
                        break;
                    }
                }
                if (ciudadNode == null)
                {
                    // Si la ciudad no está en el árbol, agregarla como nodo secundario del estado
                    ciudadNode = new TreeNode(ciudad);
                    estadoNode.Nodes.Add(ciudadNode);
                }

                // Verificar si el código postal ya está en el árbol
                TreeNode cpNode = null;
                foreach (TreeNode node in ciudadNode.Nodes)
                {
                    if (node.Text == cp)
                    {
                        cpNode = node;
                        break;
                    }
                }
                if (cpNode == null)
                {
                    // Si el código postal no está en el árbol, agregarlo como nodo secundario de la ciudad
                    cpNode = new TreeNode(cp);
                    ciudadNode.Nodes.Add(cpNode);
                }

                // Verificar si la colonia ya está en el árbol
                TreeNode coloniaNode = null;
                foreach (TreeNode node in cpNode.Nodes)
                {
                    if (node.Text == colonia)
                    {
                        coloniaNode = node;
                        break;
                    }
                }
                if (coloniaNode == null)
                {
                    // Si la colonia no está en el árbol, agregarla como nodo secundario del código postal
                    coloniaNode = new TreeNode(colonia);
                    cpNode.Nodes.Add(coloniaNode);
                }
            }
        }
        

    }
}
