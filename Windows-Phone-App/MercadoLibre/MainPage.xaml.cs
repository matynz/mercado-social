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
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;

namespace MercadoLibre
{
    public partial class MainPage : PhoneApplicationPage
    {
        bool showError;
        // Constructor
        WebClient downloader = new WebClient();
        WebClient downloaderCategorias = new WebClient();
        WebClient downloaderDestacados = new WebClient();
        WebClient downloaderMasVendidos = new WebClient();
            
        List<string> ltaCatID = new List<string>();
        List<string> ltaDesID = new List<string>();
        List<string> ltaCatNombre = new List<string>();
        List<string> ltaDesNombre = new List<string>();
        List<string> ltaDesImagen = new List<string>();

        List<string> ltaVenID = new List<string>();
        List<string> ltaVenNombre = new List<string>();
        List<string> ltaVenImagen = new List<string>();
        string token = "";
        public MainPage()
        {
            InitializeComponent();
            showError = false;
            if (!showError)
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    ShowErrorConnection();
                }
                else
                {
                    CargarSitio();
                    CargarCategorias();
                    CargarDestacados();
                    CargarMasVendidos();
                }
                showError = true;
            }

            if (!IsolatedStorageSettings.ApplicationSettings.Contains("crearTiles"))
            {
                createTile();
                IsolatedStorageSettings.ApplicationSettings["crearTiles"] = true;
            }            
        }

        private void createTile()
        {
            //LIVE TILES
            ShellTile TileToFind = ShellTile.ActiveTiles.First();

            // Application should always be found
            if (TileToFind != null)
            {
                StandardTileData NewTileData = new StandardTileData
                {
                    Title = "Mercado Libre",
                    BackgroundImage = new Uri("Background.png", UriKind.Relative),                    
                    BackTitle = "",
                    BackBackgroundImage = new Uri("BackBackground.png", UriKind.Relative),                    
                };

                // Update the Application Tile
                TileToFind.Update(NewTileData);
            }
        }

        private void ShowErrorConnection()
        {

            //Luego le aviso al usuario que no se pudo cargar nueva información.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("Ha habido un error intentando descargar los datos.\nPor favor asegúrese de contar con acceso de red y vuelva a intentarlo.");
            });

        }

        private void Cuenta_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Buscar.xaml", UriKind.Relative));
        }

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    ApplicationBarIconButton Cuenta = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
                    ApplicationBarIconButton Buscar = (ApplicationBarIconButton)ApplicationBar.Buttons[1];

                    Cuenta.IsEnabled = false;
                    Buscar.IsEnabled = false;
                    ShowErrorConnection();                    
                }
                else
                {
                    CargarSitio();
                    CargarCategorias();
                    CargarDestacados();
                    CargarMasVendidos();
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
        }

        public void CargarCategorias()
        {
            webBrowser1.Navigate(new Uri("http://prueba.casanovam.com/MercadoSocial/categorias.php", UriKind.RelativeOrAbsolute));
        }

        public void CargarSitio()
        {
            try
            {
                Uri rssUri = new Uri("http://www.mercadolibre.com.ar/jm/ml.web.pulse.PulsePageController?as_categ_id=&as_XML", UriKind.Absolute);
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Parsear);
                downloader.DownloadStringAsync(rssUri);

                ApplicationBarIconButton Cuenta = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
                ApplicationBarIconButton Buscar = (ApplicationBarIconButton)ApplicationBar.Buttons[1];

                Cuenta.IsEnabled = true;
                Buscar.IsEnabled = true;
            }
            catch
            {
                ApplicationBarIconButton Cuenta = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
                ApplicationBarIconButton Buscar = (ApplicationBarIconButton)ApplicationBar.Buttons[1];

                Cuenta.IsEnabled = false;
                Buscar.IsEnabled = false;
            }
        }

        public void CargarDestacados()
        {
            webBrowser2.Navigate(new Uri("http://prueba.casanovam.com/MercadoSocial/featured.php", UriKind.RelativeOrAbsolute));
        }

        public void CargarMasVendidos()
        {
            webBrowser3.Navigate(new Uri("http://prueba.casanovam.com/MercadoSocial/hot.php", UriKind.RelativeOrAbsolute));
        }


        void Parsear(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null || e.Error != null)
                {
                    MessageBox.Show("Ocurrió un error al intentar descargar la información de internet. Intentelo de nuevo más tarde.");
                }
                else
                {
                    ObtenerInformacionFinal(e.Result);
                }
            }
            catch (Exception)
            { }
        }

        public class EstructuraListaBotones
        {
            public string Keyword1
            {
                get;
                set;
            }

            public string Keyword2
            {
                get;
                set;
            }

        }

        public void ObtenerInformacionFinal(string xmlUrl)
        {
            List<EstructuraListaBotones> dataSource = new List<EstructuraListaBotones>() { };
            XDocument loadedData = XDocument.Parse(xmlUrl);
            bool primero = true, segundo = false;
            EstructuraListaBotones est = new EstructuraListaBotones();

            foreach (var c in loadedData.Descendants("word"))
            {
                string KEYWORD = "";
                KEYWORD = c.Element("keyword").Value;
                if (primero == true)
                {
                    est.Keyword1 = KEYWORD;
                    primero = false;
                    segundo = true;
                }
                else
                {
                    est.Keyword2 = KEYWORD;
                    dataSource.Add(new EstructuraListaBotones { Keyword1 = est.Keyword1, Keyword2 = est.Keyword2 });
                    primero = true;
                    segundo = false;
                }
            }

            this.ListaMasBuscados.ItemsSource = dataSource;

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

        private void ListaCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaCategorias.SelectedIndex == -1)
                return;

            int i = 0;
            foreach (string id in ltaCatID)
            {
                if (i == ListaCategorias.SelectedIndex)
                {
                    NavigationService.Navigate(new Uri("/Categorias.xaml?categoriaID=" + id + "&categoriaNombre=" + ltaCatNombre[i], UriKind.RelativeOrAbsolute));
                }
                i++;
            }
        }     

        private void button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Button b = sender as Button;

                this.NavigationService.Navigate(new Uri("/Buscar.xaml?Datos=" + b.Content.ToString(), UriKind.Relative));
            }
            catch(Exception)
            {}
        }

        private void webBrowser2_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string HTML = webBrowser2.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString();
            SetearValoresDestacados(HTML);
        }

        private void ListaDestacados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedIndex = ListaDestacados.SelectedIndex;

            if (SelectedIndex == -1)
            {
                return;
            }

            NavigationService.Navigate(new Uri("/Producto.xaml?itemID=" + ltaDesID[ListaDestacados.SelectedIndex], UriKind.RelativeOrAbsolute));         
        }

        public void SetearValoresDestacados(string HTML)
        {
            string Codigo = HTML;
            string ItemID = "", ItemName = "", ItemImage = "";

            List<ImageData> dataSource = new List<ImageData>() { };

            while (Codigo.Contains("@item@"))
            {
                ItemID = ParsearValor(Codigo, "@id@", "@fid@", 4);
                Codigo = Codigo.Substring(Codigo.IndexOf("@fid@"), Codigo.Length - Codigo.IndexOf("@fid@"));
                ltaDesID.Add(ItemID);

                ItemName = ParsearValor(Codigo, "@nombre@", "@fnombre@", 8);
                Codigo = Codigo.Substring(Codigo.IndexOf("@fnombre@"), Codigo.Length - Codigo.IndexOf("@fnombre@"));
                ltaDesNombre.Add(ItemName);

                ItemImage = ParsearValor(Codigo, "@imagen@", "@fimagen@", 8);
                Codigo = Codigo.Substring(Codigo.IndexOf("@fimagen@"), Codigo.Length - Codigo.IndexOf("@fimagen@"));
                ltaDesImagen.Add(ItemImage);
                dataSource.Add(new ImageData() { TituloArticulo = ItemName, ImagenArticulo = new Uri(ItemImage, UriKind.RelativeOrAbsolute) });
            }

            this.ListaDestacados.ItemsSource = dataSource;

        }

        public void SetearValoresMasVendidos(string HTML)
        {
            string Codigo = HTML;
            string ItemID = "", ItemName = "", ItemImage = "";

            List<ImageData> dataSource = new List<ImageData>() { };

            while (Codigo.Contains("@item@"))
            {
                ItemID = ParsearValor(Codigo, "@id@", "@fid@", 4);
                Codigo = Codigo.Substring(Codigo.IndexOf("@fid@"), Codigo.Length - Codigo.IndexOf("@fid@"));
                ltaVenID.Add(ItemID);

                ItemName = ParsearValor(Codigo, "@nombre@", "@fnombre@", 8);
                Codigo = Codigo.Substring(Codigo.IndexOf("@fnombre@"), Codigo.Length - Codigo.IndexOf("@fnombre@"));
                ltaVenNombre.Add(ItemName);

                ItemImage = ParsearValor(Codigo, "@imagen@", "@fimagen@", 8);
                Codigo = Codigo.Substring(Codigo.IndexOf("@fimagen@"), Codigo.Length - Codigo.IndexOf("@fimagen@"));
                ltaVenImagen.Add(ItemImage);
                dataSource.Add(new ImageData() { TituloArticulo = ItemName, ImagenArticulo = new Uri(ItemImage, UriKind.RelativeOrAbsolute) });
            }

            this.ListaMasVendidos.ItemsSource = dataSource;

        }

        private void webBrowser3_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string HTML = webBrowser3.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString();
            SetearValoresMasVendidos(HTML);
        }

        private void ListaMasVendidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedIndex = ListaMasVendidos.SelectedIndex;

            if (SelectedIndex == -1)
            {
                return;
            }

            NavigationService.Navigate(new Uri("/Producto.xaml?itemID=" + ltaVenID[ListaMasVendidos.SelectedIndex], UriKind.RelativeOrAbsolute));         
        }
    }
}