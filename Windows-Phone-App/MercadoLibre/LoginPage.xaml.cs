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
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Ingresar_Click(object sender, EventArgs e)
        {
            if (Pivots.SelectedIndex == 0)
            {
                MessageBox.Show("Loguearse");
            }
            else
            {
                MessageBox.Show("Registrarse"); 
            }
        }
    }
}