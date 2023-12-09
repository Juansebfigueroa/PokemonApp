
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;


namespace PokemonApp
{
    public partial class frmNuevoPokemon : Form
    {
        public frmNuevoPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Pokemon nuevo = new Pokemon();
            PokemonNegocio nuevopokemonNegocio = new PokemonNegocio();
            try
            {
                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Tipo = (Elemento)cbTipo.SelectedItem;
                nuevo.Debilidad = (Elemento)cbDebilidad.SelectedItem;
                nuevopokemonNegocio.agregar(nuevo);
                MessageBox.Show("Se ha agregado exitosamente el pokemon");
                Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }

        private void frmNuevoPokemon_Load(object sender, EventArgs e)
        {
            try
            {
                ElementoNegocio elementoNegocio = new ElementoNegocio();
                cbTipo.DataSource = elementoNegocio.listar();
                cbDebilidad.DataSource = elementoNegocio.listar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
