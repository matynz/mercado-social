using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MercadoLibre
{
    public partial class FiltrarBusqueda : PhoneApplicationPage
    {
        string DATO_A_BUSCAR = "";
        string marcaAguaCat = "Seleccione una Categoria";
        string marcaAguaTipo = "Seleccione un Tipo";
        string marcaAguaEst = "Seleccione un Estado";
        string marcaAguaMin = "Ingresar Valor";

        string[] Estados = { "Nuevo", "Usado" };
        string[] Tipos = { "Celular", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet", "Tablet" };
        string[] Categorias = { "Todas", "Autos","Otras" };
        bool elegiCat = false;
        string uri;

        WebClient downloader = new WebClient();
        WebClient downloaderCategorias = new WebClient();
        List<string> ltaCatID = new List<string>();
        List<string> ltaCatNombre = new List<string>();

        public FiltrarBusqueda()
        {
            InitializeComponent();
            CargarCategorias();
            this.Loaded += FiltrarBusqueda_Loaded;
        }

        void FiltrarBusqueda_Loaded(object sender, RoutedEventArgs e)
        {
            Minimo.Text = marcaAguaMin;
            Maximo.Text = marcaAguaMin;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri == new Uri("/Categorias.xaml", UriKind.Relative))
            {
                uri = this.NavigationContext.QueryString["URL"];
                //Categoria.Text = uri;
                elegiCat = true;
            }
            else
            {
                DATO_A_BUSCAR = this.NavigationContext.QueryString["Dato"];
            }
        }

        public void CargarCategorias()
        {
            webBrowser1.Navigate(new Uri("http://prueba.casanovam.com/MercadoSocial/categorias.php", UriKind.RelativeOrAbsolute));
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
                Categoria.Items.Add(str);
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

        private void Categoria_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           /* if (Categoria.SelectedIndex == -1)
                return;

            int i = 0;
            foreach (string id in ltaCatID)
            {
                if (i == Categoria.SelectedIndex)
                {
                    NavigationService.Navigate(new Uri("/Categorias.xaml?categoriaID=" + id + "&categoriaNombre=" + ltaCatNombre[i], UriKind.RelativeOrAbsolute));
                }
            }*/
        }


        private void Confirmar_Click(object sender, EventArgs e)
        {

            string URL = "http://adondevoy.zz.mu/mercadolibre/busqueda.php?q=" + DATO_A_BUSCAR;
           
           
            if (Categoria.SelectedItem != null)
            {
                URL += "$category=" + Categoria.SelectedItem;
            }            
           
            if (!string.IsNullOrWhiteSpace(Minimo.Text) && !string.IsNullOrWhiteSpace(Maximo.Text) && Minimo.Text != marcaAguaMin && Maximo.Text != marcaAguaMin)
            {
                URL += "$price=" + Minimo.Text + "-" + Maximo.Text;
            }
            this.NavigationService.Navigate(new Uri("/Buscar.xaml?URL=" + URL, UriKind.RelativeOrAbsolute));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
           /* if(tb == Categoria)
            {
                if (Categoria.Text == marcaAguaCat)
                {
                    Categoria.Text = "";
                }
            }

            else if(tb == Tipo)
            {
                if (Tipo.Text == marcaAguaTipo)
                {
                    Tipo.Text = "";
                }
            }

            else if(sender == Estado)
            {
                if(Estado.Text == marcaAguaEst)
                {
                   Estado.Text = "";
                }
            }*/

            if(tb == Minimo)
            {
                if(Minimo.Text == marcaAguaMin)
                {
                   Minimo.Text = "";
                }
            }

            else if(tb == Maximo)
            {
                if(Maximo.Text == marcaAguaMin)
                {
                   Maximo.Text = "";
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
           /* if(tb == Categoria)
            {
                if (string.IsNullOrWhiteSpace(Categoria.Text))
                {
                    Categoria.Text = marcaAguaCat;
                }
            }

            else if(tb == Tipo)
            {
                if (string.IsNullOrWhiteSpace(Tipo.Text))
                {
                    Tipo.Text = marcaAguaTipo;
                }
            }

            else if(sender == Estado)
            {
                if (string.IsNullOrWhiteSpace(Estado.Text))
                {
                   Estado.Text = marcaAguaEst;
                }
            }
            */
            if(tb == Minimo)
            {
                if (string.IsNullOrWhiteSpace(Minimo.Text))
                {
                   Minimo.Text = marcaAguaMin;
                }
            }

            else if(tb == Maximo)
            {
                if (string.IsNullOrWhiteSpace(Maximo.Text))
                {
                   Maximo.Text = marcaAguaMin;
                }
            }
        }

        private void Categoria_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Categorias.xaml", UriKind.Relative));
        }

        private void NavToMain_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
              
    }
}