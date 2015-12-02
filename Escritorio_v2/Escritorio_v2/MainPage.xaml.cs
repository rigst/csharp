using Escritorio_v2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Escritorio_v2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Gerenciador Gerenciador { get; set; }
        App minhaApp;

        public MainPage()
        {
            this.InitializeComponent();
            textBox.Visibility = Visibility.Collapsed;
            btOk.Visibility = Visibility.Collapsed;
            btCancel.Visibility = Visibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            minhaApp = (App)App.Current;
            Gerenciador = minhaApp.Gerenciador;
        }
        
        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            Condominio cond = ((Condominio)CondominiosListView.SelectedItem);
            if(cond != null)
            {
                string s = cond.Nome;
                this.Frame.Navigate(typeof(BlocoApPage), s);
            }
        }

        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            List<Condominio> lista = minhaApp.Gerenciador.Condominios;
            int count = 0;

            while(existeNum(lista,count))
            {
                count++;
            }

            string nome = "\"Novo Condomínio " + count + "\"";
            Condominio novoCond = new Condominio(nome);
            minhaApp.Gerenciador.Condominios.Add(novoCond);
            App.Update(this);
        }

        private bool existeNum(List<Condominio> lista, int s)
        {
            bool existe = false;
            foreach (var v in lista)
            {
                if (v.Nome.Contains("Novo Condomínio " + s)) existe= true;
            }
            return existe;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            Condominio cond = ((Condominio)CondominiosListView.SelectedItem);
            if (cond != null)
            {
                List<Condominio> lista = minhaApp.Gerenciador.Condominios;
                lista.Remove(cond);
                App.Update(this);
            }
        }

        private void btRename_Click(object sender, RoutedEventArgs e)
        {
            Condominio cond = ((Condominio)CondominiosListView.SelectedItem);
            if(cond != null)
            {
                textBox.Visibility = Visibility.Visible;
                btOk.Visibility = Visibility.Visible;
                btCancel.Visibility = Visibility.Visible;
            }
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            List<Condominio> lista = minhaApp.Gerenciador.Condominios;

            string s = textBox.Text;
            if (s.Length < 1 || s.Equals(" ") || s.Equals("") || existeNaLista(lista, s)) return;

            Condominio cond = ((Condominio)CondominiosListView.SelectedItem);
            foreach(var v in lista)
            {
                if (v.Nome.Equals(cond.Nome))
                {
                    v.Nome = s;
                }
            }
            App.Update(this);
        }        

        private bool existeNaLista(List<Condominio> lista, string s)
        {
            foreach(var v in lista)
            {
                if (v.Nome == s) return true;
            }
            return false;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            textBox.Visibility = Visibility.Collapsed;
            btOk.Visibility = Visibility.Collapsed;
            btCancel.Visibility = Visibility.Collapsed;
            textBox.Text = "";
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
