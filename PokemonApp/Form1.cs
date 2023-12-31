﻿using System;
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
            cbCampo.Items.Add("Numero");
            cbCampo.Items.Add("Nombre"); 
            cbCampo.Items.Add("Tipo");
        }

        private void cargar()
        {
            try
            {
                PokemonNegocio pokemonNegocio = new PokemonNegocio();
                listaPokemon = pokemonNegocio.Listar();
                dgvPokemons.DataSource = listaPokemon;
                ajustarColumnas();
                cargarImagen(listaPokemon[0].UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void ajustarColumnas()
        {
            dgvPokemons.Columns["Id"].Visible = false;
            dgvPokemons.Columns["UrlImagen"].Visible = false;
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPokemons.CurrentRow != null)
            {
                Pokemon pokemonSeleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                cargarImagen(pokemonSeleccionado.UrlImagen);
            }
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

        private void btnBorrarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }
        private void btnBorrarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar(bool logico = false)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccionado;
            try
            {
                DialogResult result = MessageBox.Show("Realmente lo queres eliminar?", "Eliminar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                    if (logico)
                        negocio.eliminarLogico(seleccionado.Id);
                    else
                        negocio.eliminarFisico(seleccionado.Id);
                    cargar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> listaFiltrada;
            if(txtFiltroRapido.Text.Length >= 2)
            {
                listaFiltrada = listaPokemon.FindAll(x => x.Nombre.ToLower().Contains(txtFiltroRapido.Text.ToLower()) || x.Tipo.Descripcion.ToLower().Contains(txtFiltroRapido.Text.ToLower()));
            }
            else
            {
                listaFiltrada = listaPokemon;
            }
            dgvPokemons.DataSource = null;
            dgvPokemons.DataSource = listaFiltrada;
            ajustarColumnas();

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            string campo = cbCampo.SelectedItem.ToString();
            string criterio = cbCriterio.SelectedItem.ToString();
            string filtro = txtFiltro.Text;
            dgvPokemons.DataSource = negocio.filtrar(campo, criterio, filtro);
           
        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string campo = cbCampo.SelectedItem.ToString();
            switch (campo)
            {
                case "Numero":
                    cbCriterio.Items.Clear();
                    cbCriterio.Items.Add("Igual a");
                    cbCriterio.Items.Add("Mayor a");
                    cbCriterio.Items.Add("Menor a");
                    break;
                case "Nombre":
                    cbCriterio.Items.Clear();
                    cbCriterio.Items.Add("Contiene");
                    cbCriterio.Items.Add("Empieza con");
                    cbCriterio.Items.Add("Termina con");
                    break;
                case "Tipo":
                    cbCriterio.Items.Clear();
                    cbCriterio.Items.Add("Fuego");
                    cbCriterio.Items.Add("Planta");
                    cbCriterio.Items.Add("Agua");
                    break;
                default:
                    break;
            }
        }

        
    }
}
