
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
        private Pokemon pokemonx = null;
        
        public frmNuevoPokemon()
        {
            InitializeComponent();
        }
        public frmNuevoPokemon(Pokemon seleccionado)
        {
            InitializeComponent();
            pokemonx = seleccionado;
            Text = "Modificar pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            PokemonNegocio nuevopokemonNegocio = new PokemonNegocio();
            try
            {
                if (pokemonx == null)
                    pokemonx = new Pokemon();
                pokemonx.Numero = int.Parse(txtNumero.Text);
                pokemonx.Nombre = txtNombre.Text;
                pokemonx.Descripcion = txtDescripcion.Text;
                pokemonx.UrlImagen = txtUrlImagen.Text;
                pokemonx.Tipo = (Elemento)cbTipo.SelectedItem;
                pokemonx.Debilidad = (Elemento)cbDebilidad.SelectedItem;
                

                if (pokemonx.Id == 0)
                {
                    nuevopokemonNegocio.agregar(pokemonx);
                    MessageBox.Show("Se ha agregado exitosamente el pokemon");
                } else
                {
                    nuevopokemonNegocio.modificar(pokemonx);
                    MessageBox.Show("Se ha modificado exitosamente los datos del pokemon");
                }
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
                cbTipo.ValueMember = "Id";
                cbTipo.DisplayMember = "Descripcion";
                cbDebilidad.DataSource = elementoNegocio.listar();
                cbDebilidad.ValueMember = "Id";
                cbDebilidad.DisplayMember = "Descripcion";

                if (pokemonx != null)
                {
                    txtNumero.Text = pokemonx.Numero.ToString();
                    txtNombre.Text = pokemonx.Nombre;
                    txtDescripcion.Text = pokemonx.Descripcion;
                    txtUrlImagen.Text = pokemonx.UrlImagen;
                    CargarImagen(pokemonx.UrlImagen);
                    cbTipo.SelectedValue = pokemonx.Tipo.Id;
                    cbDebilidad.SelectedValue = pokemonx.Debilidad.Id;

                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtUrlImagen.Text);
        }
        private void CargarImagen(string urlImagen)
        {
            try
            {
                pbNuevoPokemon.Load(urlImagen);
            }
            catch (Exception)
            {
                pbNuevoPokemon.Load("https://www.shutterstock.com/image-vector/default-image-icon-vector-missing-260nw-2086941550.jpg");
            }
        }
    }
}
