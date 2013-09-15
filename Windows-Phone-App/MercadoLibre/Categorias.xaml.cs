using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MercadoLibre
{
    public partial class Categorias : PhoneApplicationPage
    {

        List<string> ltaCatID = new List<string>();
        List<string> ltaCatNombre = new List<string>();
        string ID = "";

        public Categorias()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string nombre = "";
            if (NavigationContext.QueryString.TryGetValue("categoriaNombre", out nombre))
            {
                PageTitle.Text = nombre;
            }

            if (NavigationContext.QueryString.TryGetValue("categoriaID", out ID))
            {
                CargarCategorias();
            }
            base.OnNavigatedTo(e);
        }

        private void ListaCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaCategorias.SelectedIndex == -1)
                return;
        }

        public void CargarCategorias()
        {
            //this.NavigationService.Navigate(new Uri("/FiltrarBusqueda.xaml?URL=" + "http://prueba.casanovam.com/MercadoSocial/categorias.php?c=" + ID, UriKind.Relative));
            webBrowser1.Navigate(new Uri("http://prueba.casanovam.com/MercadoSocial/categorias.php?c=" + ID, UriKind.RelativeOrAbsolute));
        }

        private void webBrowser1_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string HTML = webBrowser1.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString();
            SetearValores(HTML);
        }

        public void SetearValores(string HTML)
        {
            string Codigo = HTML;
            string CategoriaID = "", CategoriaNOMBRE = "";

            while (Codigo.Contains("@categoria@"))
            {
                CategoriaID = ParsearValor(Codigo, "@id@", "@fid@", 4);
                Codigo = Codigo.Substring(Codigo.IndexOf("@fid@"), Codigo.Length - Codigo.IndexOf("@fid@"));
                ltaCatID.Add(CategoriaID);

                CategoriaNOMBRE = ParsearValor(Codigo, "@nombre@", "@fnombre@", 8);
                Codigo = Codigo.Substring(Codigo.IndexOf("@fnombre@"), Codigo.Length - Codigo.IndexOf("@fnombre@"));
                ltaCatNombre.Add(CategoriaNOMBRE);
            }

            foreach (string str in ltaCatNombre)
            {
                ListaCategorias.Items.Add(str);
            }
        }

        public string ParsearValor(string codigo, string campo, string cierre, int longi1)
        {
            int i = 0, j = 0;
            try
            {

                i = codigo.IndexOf(campo);
                i += longi1;
                codigo = codigo.Substring(i, codigo.Length - i);
                j = codigo.IndexOf(cierre);
                return codigo.Substring(0, j);
            }
            catch
            {
                return "";
            }
        }

        private void Seleccionar_Click(object sender, EventArgs e)
        {
            string URL = "http://adondevoy.zz.mu/mercadolibre/busqueda.php?category=";

            if (ListaCategorias.SelectedItem != null)
            {
                //URL 
                URL += ltaCatID.ElementAt(ListaCategorias.SelectedIndex);
            }


            this.NavigationService.Navigate(new Uri("/Buscar.xaml?URL=" + URL, UriKind.RelativeOrAbsolute));
        }

        private void Ampliar_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (string id in ltaCatID)
            {
                if (i == ListaCategorias.SelectedIndex)
                {
                    NavigationService.Navigate(new Uri("/Categorias.xaml?categoriaID=" + id + "&categoriaNombre=" + ltaCatNombre[i], UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}