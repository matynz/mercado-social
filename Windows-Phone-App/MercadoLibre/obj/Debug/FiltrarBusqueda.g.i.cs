﻿#pragma checksum "C:\Users\Sebastian\Desktop\MercadoLibreFinal\MercadoLibre\MercadoLibre\FiltrarBusqueda.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EF142817AD979742D6D030F3A59AB6EE"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18051
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MercadoLibre {
    
    
    public partial class FiltrarBusqueda : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.ListPicker Categoria;
        
        internal System.Windows.Controls.TextBox Minimo;
        
        internal System.Windows.Controls.TextBox Maximo;
        
        internal Microsoft.Phone.Controls.WebBrowser webBrowser1;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MercadoLibre;component/FiltrarBusqueda.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.Categoria = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("Categoria")));
            this.Minimo = ((System.Windows.Controls.TextBox)(this.FindName("Minimo")));
            this.Maximo = ((System.Windows.Controls.TextBox)(this.FindName("Maximo")));
            this.webBrowser1 = ((Microsoft.Phone.Controls.WebBrowser)(this.FindName("webBrowser1")));
        }
    }
}

