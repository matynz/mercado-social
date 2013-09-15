using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace MercadoLibre
{
    public partial class Producto : PhoneApplicationPage
    {
        string marcaAgua = "Escriba su pregunta";
        string itemID = "";
        string Codigo = "", CodigoPreguntas="";

        string tituloArticulo = "", urlArticulo = "", precioArticulo = "", vendidosArticulos = "", condicionArticulo = "", imgArticulo = "",
       picture0Articulo = "", picture1Articulo = "", picture2Articulo = "", picture3Articulo = "", picture4Articulo = "", picture5Articulo = "",
       mpagoArticulo = "", ciudadArticulo = "", estadoArticulo = "", paisArticulo = "", garantiaArticulo = "";

        List<ImageData> dataSource = new List<ImageData>()
        {
            //new lista de preguntas
        };
        
        public Producto()
        {
            InitializeComponent();
            Preguntas.Text = marcaAgua;            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string itmID = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("itemID", out itmID))
            {
                itemID = itmID;
                webBrowser1.Navigate(new Uri("http://adondevoy.zz.mu/mercadolibre/example_item.php?item_id=" + itemID, UriKind.RelativeOrAbsolute));
                webBrowser2.Navigate(new Uri("http://adondevoy.zz.mu/mercadolibre/example_question.php?item_id=" + itmID, UriKind.RelativeOrAbsolute));
            }

            ShellTile tiletopin = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/Producto.xaml?itemID=" + itemID));
            ApplicationBarIconButton Pinear = (ApplicationBarIconButton)ApplicationBar.Buttons[2];
            if (tiletopin != null)
            {
                Pinear.IsEnabled = false;
            }
            else
                Pinear.IsEnabled = true;

            base.OnNavigatedTo(e);
        }

        private void webBrowser1_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                string HTML = webBrowser1.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString();
                SetearValores(HTML);
            }
            catch (Exception)
            { }
        }

        public void SetearValores(string HTML)
        {
            Codigo = HTML;

            tituloArticulo = ParsearValor(Codigo, "@titulo@", "@cierret@", 8);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierret@"), Codigo.Length - Codigo.IndexOf("@cierret@"));

            urlArticulo = ParsearValor(Codigo, "@url@", "@cierreu@", 5);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierreu@"), Codigo.Length - Codigo.IndexOf("@cierreu@"));

            if (urlArticulo != "")
            {
                webBrowser3.Navigate(new Uri("http://adondevoy.zz.mu/mercadolibre/descripcionArticulo.php?url=" + urlArticulo, UriKind.RelativeOrAbsolute));
            }

            precioArticulo = ParsearValor(Codigo, "@precio@", "@cierrep@", 8);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierrep@"), Codigo.Length - Codigo.IndexOf("@cierrep@"));

            vendidosArticulos = ParsearValor(Codigo, "@vendidos@", "@cierrev@", 10);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierrev@"), Codigo.Length - Codigo.IndexOf("@cierrev@"));

            condicionArticulo = ParsearValor(Codigo, "@condicion@", "@cierrec@", 11);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierrec@"), Codigo.Length - Codigo.IndexOf("@cierrec@"));

            imgArticulo = ParsearValor(Codigo, "@img@", "@cierrei@", 5);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierrei@"), Codigo.Length - Codigo.IndexOf("@cierrei@"));

            picture0Articulo = ParsearValor(Codigo, "@picture0@", "@cierre0@", 10);
            if (Codigo.IndexOf("@cierre0@") != -1)
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierre0@"), Codigo.Length - Codigo.IndexOf("@cierre0@"));

            picture1Articulo = ParsearValor(Codigo, "@picture1@", "@cierre1@", 10);
            if (Codigo.IndexOf("@cierre1@") != -1)
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierre1@"), Codigo.Length - Codigo.IndexOf("@cierre1@"));

            picture2Articulo = ParsearValor(Codigo, "@picture2@", "@cierre2@", 10);
            if (Codigo.IndexOf("@cierre2@") != -1)
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierre2@"), Codigo.Length - Codigo.IndexOf("@cierre2@"));

            picture3Articulo = ParsearValor(Codigo, "@picture3@", "@cierre3@", 10);
            if (Codigo.IndexOf("@cierre3@") != -1)
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierre3@"), Codigo.Length - Codigo.IndexOf("@cierre3@"));

            picture4Articulo = ParsearValor(Codigo, "@picture4@", "@cierre4@", 10);
            if (Codigo.IndexOf("@cierre4@") != -1)
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierre4@"), Codigo.Length - Codigo.IndexOf("@cierre4@"));

            picture5Articulo = ParsearValor(Codigo, "@picture5@", "@cierre5@", 10);
            if (Codigo.IndexOf("@cierre5@") != -1)
                Codigo = Codigo.Substring(Codigo.IndexOf("@cierre5@"), Codigo.Length - Codigo.IndexOf("@cierre5@"));

            mpagoArticulo = ParsearValor(Codigo, "@mpago@", "@cierrem@", 7);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierrem@"), Codigo.Length - Codigo.IndexOf("@cierrem@"));

            ciudadArticulo = ParsearValor(Codigo, "@ciudad@", "@cierreciu@", 8);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierreciu@"), Codigo.Length - Codigo.IndexOf("@cierreciu@"));

            estadoArticulo = ParsearValor(Codigo, "@estado@", "@cierreest@", 8);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierreest@"), Codigo.Length - Codigo.IndexOf("@cierreest@"));

            paisArticulo = ParsearValor(Codigo, "@pais@", "@cierrepais@", 6);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierrepais@"), Codigo.Length - Codigo.IndexOf("@cierrepais@"));

            garantiaArticulo = ParsearValor(Codigo, "@garantia@", "@cierreg@", 10);
            Codigo = Codigo.Substring(Codigo.IndexOf("@cierreg@"), Codigo.Length - Codigo.IndexOf("@cierreg@"));
            
            titulotxt.Text = tituloArticulo;
            preciotxt.Text = precioArticulo;

            imagetxt.Source = new BitmapImage(new Uri(imgArticulo,UriKind.RelativeOrAbsolute));
            if (condicionArticulo == "new")
            {
                condiciontxt.Text = "ARTÍCULO NUEVO";
            }
            else if (condicionArticulo == "used")
            {
                condiciontxt.Text = "ARTÍCULO USADO";
            }

            vendidostxt.Text = "Vendidos: " + vendidosArticulos;
            ciudadtxt.Text = "Localización: " + ciudadArticulo + "(" + estadoArticulo + ")" + "-" + paisArticulo;
            if (picture0Articulo != "")
            {
                picture0.Source = new BitmapImage(new Uri(picture0Articulo, UriKind.RelativeOrAbsolute));
            }
            if (picture1Articulo != "")
            {
                picture1.Source = new BitmapImage(new Uri(picture1Articulo, UriKind.RelativeOrAbsolute));
            }
            if (picture2Articulo != "")
            {
                picture2.Source = new BitmapImage(new Uri(picture2Articulo, UriKind.RelativeOrAbsolute));
            }
            if (picture3Articulo != "")
            {
                picture3.Source = new BitmapImage(new Uri(picture3Articulo, UriKind.RelativeOrAbsolute));
            }
            if (picture4Articulo != "")
            {
                picture4.Source = new BitmapImage(new Uri(picture4Articulo, UriKind.RelativeOrAbsolute));
            }
            if (picture5Articulo != "")
            {
                picture5.Source = new BitmapImage(new Uri(picture5Articulo, UriKind.RelativeOrAbsolute));
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

        private void picture0_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/VerImagenes.xaml?imagen=" + picture0Articulo, UriKind.RelativeOrAbsolute));
        }

        private void picture1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/VerImagenes.xaml?imagen=" + picture1Articulo, UriKind.RelativeOrAbsolute));
        }

        private void picture2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/VerImagenes.xaml?imagen=" + picture2Articulo, UriKind.RelativeOrAbsolute));
        }

        private void picture3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/VerImagenes.xaml?imagen=" + picture3Articulo, UriKind.RelativeOrAbsolute));
        }

        private void picture4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/VerImagenes.xaml?imagen=" + picture4Articulo, UriKind.RelativeOrAbsolute));
        }

        private void picture5_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/VerImagenes.xaml?imagen=" + picture5Articulo, UriKind.RelativeOrAbsolute));
        }


        private void webBrowser2_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string HTML = webBrowser2.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString();
            SetearPreguntas(HTML);
        }

        public void SetearPreguntas(string HTML)
        {
            CodigoPreguntas = HTML;
            string preguntA = "", respuestA = "";
            while (CodigoPreguntas.Contains("@cierrep"))
            {
                preguntA = ParsearValor(CodigoPreguntas, "@pregunta", "@cierrep", 9);
                CodigoPreguntas = CodigoPreguntas.Substring(CodigoPreguntas.IndexOf("@cierrep"), CodigoPreguntas.Length - CodigoPreguntas.IndexOf("@cierrep"));
                respuestA = ParsearValor(CodigoPreguntas, "@respuesta", "@cierrer", 10);

                int index = 0;
                index = preguntA.IndexOf("@");
                index++;
                preguntA = preguntA.Substring(index, preguntA.Length - index);
                index = respuestA.IndexOf("@");
                index++;
                respuestA = respuestA.Substring(index, respuestA.Length - index);

                preguntA = Environment.NewLine + preguntA + Environment.NewLine;
                respuestA = Environment.NewLine + respuestA + Environment.NewLine;

                if (preguntA.Contains("background:"))
                {
                    preguntA = "Tuvimos que borrar esta pregunta porque no cumple con nuestras las Politicas de Publicación de MercadoLibre";
                    respuestA = "";
                }

                dataSource.Add(new ImageData() { Pregunta = preguntA, Respuesta = respuestA });
                CodigoPreguntas = CodigoPreguntas.Substring(CodigoPreguntas.IndexOf("@cierrer"), CodigoPreguntas.Length - CodigoPreguntas.IndexOf("@cierrer"));
            }

            listBox1.ItemsSource = dataSource;
        }

        public class ImageData
        {
            public string Pregunta
            {
                get;
                set;
            }

            public string Respuesta
            {
                get;
                set;
            }
        }

        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Preguntas.xaml?itemID="+itemID,UriKind.RelativeOrAbsolute));
        }

        private void Favoritos_Click(object sender, EventArgs e)
        {
        }

        private void Share_Click(object sender, EventArgs e)
        {
            //ShareStatusTask shareTask = new ShareStatusTask();
            ShareLinkTask shareLinkTask = new ShareLinkTask();
            shareLinkTask.Title = tituloArticulo;
            shareLinkTask.LinkUri = new Uri(urlArticulo, UriKind.Absolute);//new Uri("http://adondevoy.zz.mu/mercadolibre/example_item.php?item_id=" + itemID, UriKind.RelativeOrAbsolute);
            shareLinkTask.Message = precioArticulo;
            shareLinkTask.Show();            
           
            //shareTask.Show();*/
        }

        private void Panorama_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (MiPanorama.SelectedIndex == 3)
            {
                ApplicationBar.Buttons.Clear();
                ApplicationBarIconButton MiBoton = new ApplicationBarIconButton();
                MiBoton.IconUri = new Uri("/Icons/appbar.new.rest.png", UriKind.Relative);
                MiBoton.Text = "Preguntar";
                MiBoton.IsEnabled = false;
                MiBoton.Click += NuevaPregunta_Click;
                ApplicationBar.Buttons.Add(MiBoton);
            }
            else if(ApplicationBar.Buttons.Count == 1)
            {
                if (Preguntas.Text == marcaAgua || Preguntas.Text == "")
                {
                    Preguntas.Text = marcaAgua;
                    Preguntas.Visibility = Visibility.Collapsed;
                }

                ApplicationBar.Buttons.Clear();

                ApplicationBarIconButton Favoritos = new ApplicationBarIconButton();
                Favoritos.IconUri = new Uri("Icons/StarButton.png", UriKind.Relative);
                Favoritos.Text = "Agregar";
                Favoritos.IsEnabled = false;
                Favoritos.Click += Favoritos_Click;
                ApplicationBar.Buttons.Add(Favoritos);

                ApplicationBarIconButton Share = new ApplicationBarIconButton();
                Share.IconUri = new Uri("Icons/appbar.feature.share.rest.png", UriKind.Relative);
                Share.Text = "Compartir";
                Share.Click += Share_Click;
                ApplicationBar.Buttons.Add(Share);

                try
                {
                    ShellTile tiletopin = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/Producto.xaml?itemID=" + itemID));
                    ApplicationBarIconButton Pinear = new ApplicationBarIconButton();
                    Pinear.IconUri = new Uri("Icons/Pin.png", UriKind.Relative);
                    Pinear.Text = "Anclar";

                    if (tiletopin != null)
                    {
                        Pinear.IsEnabled = false;
                    }
                    else
                        Pinear.IsEnabled = true;

                    Pinear.Click += Pin_Click;
                    ApplicationBar.Buttons.Add(Pinear);
                }
                catch (Exception)
                { }
            }

        }

        void NuevaPregunta_Click(object sender, EventArgs e)
        {
            Preguntas.Visibility = Visibility.Visible;
            ApplicationBar.Buttons.Clear();
                        
            ApplicationBarIconButton Preguntar = new ApplicationBarIconButton();
            Preguntar.IconUri = new Uri("Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative);
            Preguntar.Text = "Confirmar";
            Preguntar.IsEnabled = false;
            Preguntar.Click += Preguntar_Click ;
            ApplicationBar.Buttons.Add(Preguntar);
        }

        void Preguntar_Click(object sender, EventArgs e)
        {
            try
            {
                           
            }
            catch (Exception)
            { }
            MessageBox.Show("Su pregunta se ha publicado con exito");
            Preguntas.Text = marcaAgua;

            ApplicationBar.Buttons.Clear();
            ApplicationBarIconButton MiBoton = new ApplicationBarIconButton();
            MiBoton.IconUri = new Uri("Icons/appbar.new.rest.png", UriKind.Relative);
            MiBoton.Text = "Preguntar";
            MiBoton.IsEnabled = false;
            MiBoton.Click += NuevaPregunta_Click;
            ApplicationBar.Buttons.Add(MiBoton);

            Preguntas.Visibility = Visibility.Collapsed;
        }

        private void Preguntas_GotFocus_1(object sender, RoutedEventArgs e)
        {
            if (Preguntas.Text == marcaAgua)
            {
                Preguntas.Text = "";
            }
        }

        private void Preguntas_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (Preguntas.Text == "")
            {
                Preguntas.Text = marcaAgua;
            }
        }

        void Pin_Click(object sender, EventArgs e)
        {
            Uri imgAnclar = new Uri("Background.png", UriKind.RelativeOrAbsolute);
            ShellTile tiletopin = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/Producto.xaml?itemID=" + itemID));

            if (tiletopin == null)
            {
                try
                {
                    StandardTileData standardTileData = new StandardTileData();
                    standardTileData.BackgroundImage = imgAnclar;
                    standardTileData.Title = tituloArticulo;
                    standardTileData.BackTitle = "Mercado Libre";
                    standardTileData.BackContent = tituloArticulo;
                    standardTileData.BackBackgroundImage = new Uri("BackBackgroundVacio.png", UriKind.Relative);
                    ShellTile.Create(new Uri("/Producto.xaml?itemID=" + itemID, UriKind.Relative), standardTileData);
                    ApplicationBarIconButton Pinear = (ApplicationBarIconButton)ApplicationBar.Buttons[2];
                    Pinear.IsEnabled = false;
                }
                catch
                {
                    MessageBox.Show("No se ha podido anclar al inicio");
                }
            }
            else
            {
                MessageBox.Show("Este producto ya está anclado");
            }        
        }

        private void NavToMain_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}