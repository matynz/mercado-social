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
    public partial class Registrate : PhoneApplicationPage
    {
        bool checkeado = true;

        public Registrate()
        {
            InitializeComponent();
            webBrowser1.Navigate(new Uri("https://registration.mercadolibre.com.ar/registration/", UriKind.RelativeOrAbsolute));
        }

        private void webBrowser1_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string HTML = webBrowser1.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString();
            MessageBox.Show(HTML);
            int indexOfMailExistente = HTML.IndexOf("Este e-mail ya existe");
            if (indexOfMailExistente != -1)
            {
                MessageBox.Show("El mail ingresado ya pertenece a una cuenta en servicio. Si olvidaste tu contraseña, vé a 'Entrar' y luego selecciona la opcion 'Olvide mi contraseña'");
            }
            else
            {
                int indexOfTeRegistraste = HTML.IndexOf("Te registraste");
                if (indexOfTeRegistraste != -1 && webBrowser1.Source.ToString() == "https://registration.mercadolibre.com.ar/registration/welcome-mobile")
                {
                    MessageBox.Show("¡Enhorabuena te has registrado satisfactoriamente!");
                    NavigationService.Navigate(new Uri("", UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void textBlock7_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Se olvidó de completar el 'Nombre'");
                return;
            }
            if (textBox1.Text.Length <= 1)
            {
                MessageBox.Show("Ese nombre no existe. Ingresa tu verdadero nombre");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Se olvidó de completar el 'Apellido'");
                return;
            }
            if (textBox2.Text.Length <= 1)
            {
                MessageBox.Show("Ese apellido no existe. Ingresa tu verdadero apellido");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Se olvidó de completar el 'E-mail'");
                return;
            }
            if (textBox3.Text.Length < 8)
            {
                MessageBox.Show("La dirección de correo electronico proporcionada no es valida.");
                return;
            }
            if (!textBox3.Text.Contains("@"))
            {
                MessageBox.Show("La dirección de correo electronico proporcionada no es valida.");
                return;
            }
            if (!textBox3.Text.Contains("."))
            {
                MessageBox.Show("La dirección de correo electronico proporcionada no es valida.");
                return;
            }
            if (!(textBox3.Text.All(c => Char.IsLetterOrDigit(c) || c == '_' || c == '.' || c == '-' || c == '@')))
            {
                MessageBox.Show("La dirección de correo electronico proporcionada no es valida.");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Se olvidó de completar la confirmación de el 'E-mail'");
                return;
            }
            if (textBox4.Text.Length < 8)
            {
                MessageBox.Show("La dirección de correo electronico proporcionada no es valida.");
                return;
            }
            if (!textBox4.Text.Contains("@"))
            {
                MessageBox.Show("La dirección de correo electronico proporcionada no es valida.");
                return;
            }
            if (!textBox4.Text.Contains("."))
            {
                MessageBox.Show("La dirección de correo electronico proporcionada no es valida.");
                return;
            }
            if (!(textBox4.Text.All(c => Char.IsLetterOrDigit(c) || c == '_' || c == '.' || c == '-' || c == '@')))
            {
                MessageBox.Show("La dirección de correo electronico proporcionada no es valida.");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox5.Password))
            {
                MessageBox.Show("Se olvidó de completar la 'Contraseña'");
                return;
            }
            if (textBox5.Password.Length < 6)
            {
                MessageBox.Show("La longitud de la contraseña es demasiado corta. La misma debe tener entre 6 y 20 caracteres");
                return;
            }
            if (textBox5.Password.Length > 20)
            {
                MessageBox.Show("La longitud de la contraseña es demasiado larga. La misma debe tener entre 6 y 20 caracteres");
                return;
            }
            //if (string.IsNullOrWhiteSpace(textBox6.Text))
            //{
            //    MessageBox.Show("Se olvidó de ingresar su 'Telefono'");
            //    return;
            //}
            //if (!(textBox6.Text.All(c => Char.IsDigit(c))))
            //{
            //    MessageBox.Show("El número de telefono ingresado no es valido. Por favor verifique que sea correcto e intentelo nuevamente");
            //    return;
            //}
            //if (textBox6.Text.Length > 60)
            //{
            //    MessageBox.Show("El longitud máxima para el Telefono es de 60 caracteres");
            //    return;
            //}
            //if (listBox1.Items.Count == 0)
            //{
            //    MessageBox.Show("Se olvidó de seleccionar su 'Provincia'");
            //    return;
            //}
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Las direcciones de E-mail indicadas no coinciden");
                return;
            }

            //MessageBox.Show(webBrowser1.InvokeScript("eval", new string[] { "document.documentElement.innerHTML;" }).ToString());
            webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupFirstName').value = '" + textBox1.Text + "';" });
            webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupLastName').value = '" + textBox2.Text + "';" });
            webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupEmail').value = '" + textBox3.Text + "';" });
            webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupRepEmail').value = '" + textBox4.Text + "';" });
            webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupPassword').value = '" + textBox5.Password + "';" });
            //webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupPhoneNumber').value = '" + textBox6.Text + "';" });

            if (checkBox1.IsChecked == true)
            {
                webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupNewsletter').click();" });
                webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupNewsletter').click();" });
            }
            else
            {
                webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupNewsletter').click();" });
            }

            webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('register').click();" });


        }

        private void listBox1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupState').value" });
                string ciudadd = webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupState').value" }).ToString();
                MessageBox.Show(ciudadd);
                //webBrowser1.InvokeScript("eval", new string[] { "document.getElementsByTagName('signUp.state').click();" });
            }
            catch
            {
                MessageBox.Show("No se pudo obtener el ID de la provincia");
            }
                string ciudad = webBrowser1.InvokeScript("eval", new string[] { "document.getElementById('signupState').value" }).ToString();
                string ciudadNombre = " ";
                if (ciudad == "AR-B")
                {
                    ciudadNombre += "Buenos Aires";
                }
                else if (ciudad == "AR-C")
                {
                    ciudadNombre += "Capital Federal";
                }
                else if (ciudad == "AR-K")
                {
                    ciudadNombre += "Catamarca";
                }
                else if (ciudad == "AR-H")
                {
                    ciudadNombre += "Chaco";
                }
                else if (ciudad == "AR-U")
                {
                    ciudadNombre += "Chubut";
                }
                else if (ciudad == "AR-W")
                {
                    ciudadNombre += "Corrientes";
                }
                else if (ciudad == "AR-X")
                {
                    ciudadNombre += "Córdoba";
                }
                else if (ciudad == "AR-E")
                {
                    ciudadNombre += "Entre Ríos";
                }
                else if (ciudad == "AR-P")
                {
                    ciudadNombre += "Formosa";
                }
                else if (ciudad == "AR-Y")
                {
                    ciudadNombre += "Jujuy";
                }
                else if (ciudad == "AR-L")
                {
                    ciudadNombre += "La Pampa";
                }
                else if (ciudad == "AR-F")
                {
                    ciudadNombre += "La Rioja";
                }
                else if (ciudad == "AR-M")
                {
                    ciudadNombre += "Mendoza";
                }
                else if (ciudad == "AR-N")
                {
                    ciudadNombre += "Misiones";
                }
                else if (ciudad == "AR-Q")
                {
                    ciudadNombre += "Neuquén";
                }
                else if (ciudad == "AR-R")
                {
                    ciudadNombre += "´Río Negro";
                }
                else if (ciudad == "AR-A")
                {
                    ciudadNombre += "Salta";
                }
                else if (ciudad == "AR-J")
                {
                    ciudadNombre += "San Juan";
                }
                else if (ciudad == "AR-D")
                {
                    ciudadNombre += "San Luis";
                }
                else if (ciudad == "AR-Z")
                {
                    ciudadNombre += "Santa Cruz";
                }
                else if (ciudad == "AR-S")
                {
                    ciudadNombre += "Santa Fe";
                }
                else if (ciudad == "AR-G")
                {
                    ciudadNombre += "Santiago del Estero";
                }
                else if (ciudad == "AR-V")
                {
                    ciudadNombre += "Tierra del Fuego";
                }
                else if (ciudad == "AR-T")
                {
                    ciudadNombre += "Tucumán";
                }
                else
                {
                    ciudadNombre = "";
                }

                listBox1.Items.Clear();
                listBox1.Items.Add(ciudadNombre);
            
        }

        private void textBlock7_MouseEnter(object sender, MouseEventArgs e)
        {
            image2.Source = new BitmapImage(new Uri(@"Imagenes/btn_ejemplopresed.png", UriKind.Relative));
        }

        private void textBlock7_MouseLeave(object sender, MouseEventArgs e)
        {
            image2.Source = new BitmapImage(new Uri(@"Imagenes/btn_ejemplo.png", UriKind.Relative));
        }
    }
}