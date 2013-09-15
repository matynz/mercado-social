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
    public partial class Preguntas : PhoneApplicationPage
    {

        string CodigoPreguntas = "";
        List<ImageData> dataSource = new List<ImageData>()
        {
            //new lista de preguntas
        };

        public Preguntas()
        {
            InitializeComponent();
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

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string itmID = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("itemID", out itmID))
            {
                webBrowser2.Navigate(new Uri("http://adondevoy.zz.mu/mercadolibre/example_question.php?item_id=" + itmID, UriKind.RelativeOrAbsolute));
            }
            base.OnNavigatedTo(e);
        }


        private void webBrowser2_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string HTML = webBrowser2.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString();
            SetearPreguntas(HTML);
            MessageBox.Show("Listo");
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
                preguntA = preguntA.Substring(index,preguntA.Length-index);
                index = respuestA.IndexOf("@");
                index++;
                respuestA = respuestA.Substring(index,respuestA.Length-index);

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
    }
}