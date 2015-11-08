using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Escritorio_v1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Gerenciador gerente = new Gerenciador();
        private FlipView listaCondominios;
        private ListView listaBlocos;

        public MainPage()
        {
            this.InitializeComponent();

            Condominio c1 = new Condominio("COND 1");
            Condominio c2 = new Condominio("COND 2");
            Condominio c3 = new Condominio("COND 3");
            Condominio c4 = new Condominio("COND 4");
            gerente.addCondominio(c1);
            gerente.addCondominio(c2);
            gerente.addCondominio(c3);
            gerente.addCondominio(c4);
            Bloco b1 = new Bloco(1);
            Bloco b2 = new Bloco(2);
            Bloco b3 = new Bloco(3);
            c1.addBloco(b1);
            c1.addBloco(b2);
            c2.addBloco(b3);

            //CONDOMINIOS
            listaCondominios = flipCond;
            listaCondominios.ItemsSource = gerente;

            //BLOCOS
            listaBlocos = listBlocos;
            listaBlocos.ItemsSource = listaCondominios.SelectedItem;
            
        }
        

        private void flipCond_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {/*
            listaBlocos.Items.Clear();
            listaBlocos.ItemsSource = listaCondominios.SelectedItem;*/
        }
        
    }
}
