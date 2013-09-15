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
    public partial class Buscar : PhoneApplicationPage
    {
        string Codigo, cantidad1, cantidad2, cantidadArticulos, cantidadPaginas, PaginaAnterior = "", PaginaSiguiente = "", ArticuloID, ArticuloTitulo, ArticuloImagen, ArticuloUrl, ArticuloPrecio;
        List<string> listaID = new List<string>();
        bool forwardEffect = false;
        bool backEffect = false;
        bool filtroActivado = false;

        public Buscar()
        {
            
            InitializeComponent();
            //this.Loaded += Buscar_Loaded;
            
        }

        void Buscar_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string URL = this.NavigationContext.QueryString["URL"];
                if (!string.IsNullOrEmpty(URL))
                {
                    filtroActivado = true;
                    CargarSitio(URL);
                }
            }
            catch (Exception)
            { }
        }


        public class ImageData
        {
            public Uri ImagenArticulo
            {
                get;
                set;
            }

            public string TituloArticulo
            {
                get;
                set;
            }

            public string Precio
            {
                get;
                set;
            }
        }



        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                string URL = this.NavigationContext.QueryString["URL"];
                if (!string.IsNullOrEmpty(URL))
                {
                    while(URL.Contains("$"))
                    {
                        URL = URL.Replace("$", "&");
                    }
                    filtroActivado = true;
                    CargarSitio(URL);
                }
            }
            catch (Exception)
            { 
            }
            try
            {
                string URL = this.NavigationContext.QueryString["Datos"];
                if (!string.IsNullOrEmpty(URL))
                {
                    CargarSitio(URL);
                }
            }
            catch (Exception)
            { }
        }

        public void CargarSitio(string articulo)
        {
            if (articulo.IndexOf(" ") != -1)
            {
                while (articulo.Contains(" "))
                {
                    articulo = articulo.Replace(" ", "+");
                }
            }

            if (!filtroActivado)
            {
                string finish = "http://adondevoy.zz.mu/mercadolibre/busqueda.php?q=";
                finish = finish + articulo;

                finish = finish + "&search=search&offset=0";
                webBrowser1.Navigate(new Uri(finish, UriKind.Absolute));
            }
            else
            {
                Microsoft.Phone.Shell.ApplicationBarIconButton btn = ApplicationBar.Buttons[0] as Microsoft.Phone.Shell.ApplicationBarIconButton; btn.IsEnabled = true;
                webBrowser1.Navigate(new Uri(articulo+"&search=search&offset=0", UriKind.Absolute));
                filtroActivado = false;
            }

            
        }

       

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            try
            {
                CargarSitio(buscarArticulo.Text);
                Microsoft.Phone.Shell.ApplicationBarIconButton btn = ApplicationBar.Buttons[0] as Microsoft.Phone.Shell.ApplicationBarIconButton; btn.IsEnabled = true;
            }
            catch (Exception)
            { }
        }

        private void webBrowser1_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string HTML = webBrowser1.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString();
            SetearValores(HTML);
        }

        public void SetearValores(string HTML)
        {
            //try
            //{
            listaID.Clear();
            PaginaSiguiente = PaginaAnterior = "";
            Codigo = HTML;
            Codigo = Codigo.Substring(Codigo.IndexOf("<body>"), Codigo.Length - Codigo.IndexOf("<body>"));
            int i = 0, j = 0;
            //MessageBox.Show(Codigo);

            cantidad1 = ParsearValor(Codigo, "@cantidad1@", "@cierre1@", 11);
            //MessageBox.Show(Codigo);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierre1@"), Codigo.Length - Codigo.IndexOf("@cierre1@"));
            //MessageBox.Show(cantidad1);

            cantidad2 = ParsearValor(Codigo, "@cantidad2@", "@cierre2@", 11);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierre2@"), Codigo.Length - Codigo.IndexOf("@cierre2@"));
            //MessageBox.Show(cantidad2);

            cantidadArticulos = ParsearValor(Codigo, "@cantidada@", "@cierrea@", 11);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierrea@"), Codigo.Length - Codigo.IndexOf("@cierrea@"));
            //MessageBox.Show(cantidadArticulos);

            cantidadPaginas = ParsearValor(Codigo, "@cantpag@", "@cierre3@", 9);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierre3@"), Codigo.Length - Codigo.IndexOf("@cierre3@"));
            //MessageBox.Show(cantidadPaginas);

            if (Codigo.IndexOf("@cierre4@") != -1)
            {
                PaginaAnterior = ParsearValor(Codigo, "@paganterior@", "@cierre4@", 13);
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierre4@"), Codigo.Length - Codigo.IndexOf("@cierre4@"));
                //MessageBox.Show(PaginaAnterior);
            }

            if (Codigo.IndexOf("@cierre5@") != -1)
            {
                PaginaSiguiente = ParsearValor(Codigo, "@pagsiguiente@", "@cierre5@", 14);
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierre5@"), Codigo.Length - Codigo.IndexOf("@cierre5@"));
                //MessageBox.Show(PaginaSiguiente);
            }

            List<ImageData> dataSource = new List<ImageData>()
            {
                //new ImageData(){NombreVideo = "Las cosas obvias de la vida", ImagenVideo=new Uri("/Imagenes/German1.jpg",UriKind.RelativeOrAbsolute)},
            };

            while (Codigo.Contains("@id@"))
            {
                ArticuloID = ParsearValor(Codigo, "@id@", "@cierree@", 4);
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierree@"), Codigo.Length - Codigo.IndexOf("@cierree@"));
                //MessageBox.Show(ArticuloID);
                listaID.Add(ArticuloID);

                ArticuloTitulo = ParsearValor(Codigo, "@titulo@", "@cierret@", 8);
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierret@"), Codigo.Length - Codigo.IndexOf("@cierret@"));
                //MessageBox.Show(ArticuloTitulo);

                ArticuloImagen = ParsearValor(Codigo, "@img@", "@cierrei@", 5);
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierrei@"), Codigo.Length - Codigo.IndexOf("@cierrei@"));
                //MessageBox.Show(ArticuloImagen);

                ArticuloUrl = ParsearValor(Codigo, "@url@", "@cierreu@", 5);
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierreu@"), Codigo.Length - Codigo.IndexOf("@cierreu@"));
                //MessageBox.Show(ArticuloUrl);

                ArticuloPrecio = ParsearValor(Codigo, "@precio@", "@cierrep@", 8);
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierrep@"), Codigo.Length - Codigo.IndexOf("@cierrep@"));
                //MessageBox.Show(ArticuloPrecio);

                dataSource.Add(new ImageData() { TituloArticulo = ArticuloTitulo, ImagenArticulo = new Uri(ArticuloImagen, UriKind.RelativeOrAbsolute), Precio = ArticuloPrecio });
            }
            pagina1.Text = cantidad1 + " al " + cantidad2;
            //pagina2.Text = cantidad2;
            cantArticulos.Text = "Resultados: ";
            cantArticulos.Text += cantidadArticulos;
            try
            {
                if (int.Parse(cantidadArticulos) <= 50)
                {
                    pagina1.Text = "1 al " + cantidadArticulos;
                }
            }
            catch(Exception)
            {
            }

            if (PaginaSiguiente != "")
            {
                if (PaginaSiguiente.IndexOf("offset") != -1)
                {
                    forwardEffect = true;                    
                    //MessageBox.Show(PaginaSiguiente);
                    Microsoft.Phone.Shell.ApplicationBarIconButton btn = ApplicationBar.Buttons[3] as Microsoft.Phone.Shell.ApplicationBarIconButton; btn.IsEnabled = true;
                    //appBarSiguiente.IsEnabled = true;
                }
            }
            else
            {
                forwardEffect = false;
                Microsoft.Phone.Shell.ApplicationBarIconButton btn = ApplicationBar.Buttons[3] as Microsoft.Phone.Shell.ApplicationBarIconButton; btn.IsEnabled = false;
                //appBarSiguiente.IsEnabled = false;
            }

            if (PaginaAnterior != "")
            {
                if (PaginaAnterior.IndexOf("offset") != -1)
                {
                    //MessageBox.Show(PaginaAnterior);
                    backEffect = true;
                    Microsoft.Phone.Shell.ApplicationBarIconButton btn = ApplicationBar.Buttons[2] as Microsoft.Phone.Shell.ApplicationBarIconButton; btn.IsEnabled = true;
                    //appBarAnterior.IsEnabled = true;
                }
            }
            else
            {
                backEffect = false;
                Microsoft.Phone.Shell.ApplicationBarIconButton btn = ApplicationBar.Buttons[2] as Microsoft.Phone.Shell.ApplicationBarIconButton; btn.IsEnabled = false;
                //appBarAnterior.IsEnabled = false;
            }

            pagina1.Visibility = Visibility.Visible;
            //pagina2.Visibility = Visibility.Visible;
            cantArticulos.Visibility = Visibility.Visible;
            textBlock1.Visibility = Visibility.Visible;
            textBlock2.Visibility = Visibility.Visible;
            this.ListaArticulos.ItemsSource = dataSource;
            //}
            //catch
            //{
            //}
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

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            //webBrowser1.InvokeScript("eval", "window.location.reload(true);");
        }

        private void appBarSiguiente_Click(object sender, EventArgs e)
        {
            while (PaginaSiguiente.Contains("amp;"))
            {
                PaginaSiguiente = PaginaSiguiente.Replace("amp;", "");
            }
            webBrowser1.Navigate(new Uri(PaginaSiguiente, UriKind.RelativeOrAbsolute));
        }

        private void appBarAnterior_Click(object sender, EventArgs e)
        {
            while (PaginaAnterior.Contains("amp;"))
            {
                PaginaAnterior = PaginaAnterior.Replace("amp;", "");
            }
            webBrowser1.Navigate(new Uri(PaginaAnterior, UriKind.RelativeOrAbsolute));
        }

        private void nameVID_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void ListaArticulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedIndex = ListaArticulos.SelectedIndex;

            if (SelectedIndex == -1)
            {
                return;
            }

            NavigationService.Navigate(new Uri("/Producto.xaml?itemID=" + listaID[ListaArticulos.SelectedIndex], UriKind.RelativeOrAbsolute));
        }

        private void GestureListener_Flick(object sender, FlickGestureEventArgs e)
        {            
            //Si es de izquierda a Derecha, borro los datos ingresados
            if (e.Angle >= 0 && e.Angle <= 35 || e.Angle >= 330 && e.Angle <= 360)
            {
                if (backEffect == true)
                {
                    while (PaginaAnterior.Contains("amp;"))
                    {
                        PaginaAnterior = PaginaAnterior.Replace("amp;", "");
                    }
                    webBrowser1.Navigate(new Uri(PaginaAnterior, UriKind.RelativeOrAbsolute));
                }
            }

            //Si es de derecha a izquierda, calculo los resultados
            else if (e.Angle >= 150 && e.Angle <= 200)
            {
                if (forwardEffect == true)
                {
                    while (PaginaSiguiente.Contains("amp;"))
                    {
                        PaginaSiguiente = PaginaSiguiente.Replace("amp;", "");
                    }
                    webBrowser1.Navigate(new Uri(PaginaSiguiente, UriKind.RelativeOrAbsolute));
                }
            }
        }
        private void Filtrar_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/FiltrarBusqueda.xaml?Dato="+buscarArticulo.Text, UriKind.Relative));
        }
    }
}