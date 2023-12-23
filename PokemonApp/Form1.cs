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
    public partial class frmPokemons : Form
    {
        private List<Pokemon> listaPokemon;
        private void cargarImagen(string UrlImagen)
        {
            try
            {
                pb1.Load(UrlImagen);
            }
            catch (Exception)
            {
                pb1.Load("https://www.shutterstock.com/image-vector/default-image-icon-vector-missing-260nw-2086941550.jpg");
            }
        }
        public frmPokemons()
        {
            InitializeComponent();
        }

        private void frmPokemons_Load(object sender, EventArgs e)
        {
            cargar();

        }

        private void cargar()
        {
            try
            {
                PokemonNegocio pokemonNegocio = new PokemonNegocio();
                listaPokemon = pokemonNegocio.Listar();
                dgvPokemons.DataSource = listaPokemon;
                dgvPokemons.Columns["Id"].Visible = false;
                dgvPokemons.Columns["UrlImagen"].Visible = false;
                cargarImagen(listaPokemon[0].UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon pokemonSeleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            cargarImagen(pokemonSeleccionado.UrlImagen);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmNuevoPokemon frmNuevoPokemon = new frmNuevoPokemon();
            frmNuevoPokemon.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            
            frmNuevoPokemon frmModificar = new frmNuevoPokemon(seleccionado);
            frmModificar.ShowDialog();
            cargar();
        }
    }
}
