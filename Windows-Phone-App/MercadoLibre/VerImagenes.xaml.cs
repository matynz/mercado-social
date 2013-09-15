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

namespace MercadoLibre
{
    public partial class VerImagenes : PhoneApplicationPage
    {
        public VerImagenes()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string imageURL = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("imagen", out imageURL))
            {
                imagen.Source = new BitmapImage(new Uri(imageURL, UriKind.RelativeOrAbsolute));
            }
            base.OnNavigatedTo(e);
        }
    }
}