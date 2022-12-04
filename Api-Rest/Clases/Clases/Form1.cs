using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clases
{
    public partial class Form1 : Form
    {
        DBApi dBApi = new DBApi();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPrueba_Click(object sender, EventArgs e)
        {
            dynamic respuesta = dBApi.Get("http://localhost:8000/Proyecto_redis/get/00d4ecd0-b83b-40a9-9367-8578ff3f8d21");
            txtID.Text = respuesta.id.ToString();
            txtMarca.Text = respuesta.marca.ToString();
            txtModelo.Text = respuesta.modelo.ToString();
            txtPrecio.Text = respuesta.precio.ToString();
            txtAlmacenamiento.Text = respuesta.almacenamiento.ToString();
            textColor.Text = respuesta.color.ToString();
            textDate.Text = respuesta.date.ToString();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona
            {
                job = txtMarca.Text,
                name = txtID.Text
            };

            string json = JsonConvert.SerializeObject(persona);

            dynamic respuesta = dBApi.Post("https://reqres.in/api/users",json);

            MessageBox.Show(respuesta.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Persona
    {
        public string name { get; set; }
        public string job { get; set; }
    }
}

