using Escritorio_v2.ViewModel;
using System;
using System.Collections.Generic;
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
    public sealed partial class ProcessoPage : Page
    {
        public BlocoApPageViewModel ViewModel { get; set; }

        private static ProcessoPage page;

        private string controle = "";
        
        public ProcessoPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            page = this;
            this.ViewModel = BlocoApPage.getLastInstance().ViewModel;
            avisoTextBlock.Visibility = Visibility.Collapsed;
            textBlockAux.Visibility = Visibility.Collapsed;
            gridEntrada.Visibility = Visibility.Collapsed;
        }

        public static ProcessoPage getLastInstance()
        {
            return page;
        }

        private void btAceitar_Click(object sender, RoutedEventArgs e)
        {
            Processo p = ViewModel.ProcessoAux;
            p.NumProcesso = ProcessoTextBox.Text;
            p.Reu = ReuTextBox.Text;
            p.Especie = EspecieTextBox.Text;
            try {
                p.ValorAjuizado = Double.Parse(ValorATextBox.Text);
                p.DataAjuizamento = new Data(DataATextBox.Text);
                p.Periodo = new Data(PeriodoTextBox.Text);
                p.UltimoMovimento = new Data(UltMovTextBox.Text);
                avisoTextBlock.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                avisoTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void btAdiciona1_Click(object sender, RoutedEventArgs e)
        {
            //Ajuizado
            textBlockAux.Text = "Digite a data:";
            textBlockAux.Visibility = Visibility.Visible;
            gridEntrada.Visibility = Visibility.Visible;
            controle = "Ajuizado";
        }

        private void Update()
        {
            this.Frame.Navigate(typeof(BlocoApPage), "Processo");
            this.Frame.Navigate(typeof(ProcessoPage), "");
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (controle.Equals("Ajuizado"))
            {
                string s = textBox.Text;
                try
                {
                    Data d = new Data(s);
                    ViewModel.ProcessoAux.addAjuizado(d);
                    textBlockAux.Visibility = Visibility.Collapsed;
                    gridEntrada.Visibility = Visibility.Collapsed;
                    textBox.Text = "";
                    Update();
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controle.Equals("DataCusta"))
            {
                string s = textBox.Text;
                try
                {
                    Data d = new Data(s);
                    ViewModel.CustaAux = new Processo.Custa();
                    ViewModel.CustaAux.Data = d;
                    textBlockAux.Text = "Digite o valor:";
                    controle = "ValorCusta";
                    textBox.Text = "";
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controle.Equals("ValorCusta"))
            {
                string s = textBox.Text;
                try
                {
                    double v = Double.Parse(s);
                    ViewModel.CustaAux.Valor = v;
                    ViewModel.ProcessoAux.Custas.Add(ViewModel.CustaAux);
                    textBlockAux.Visibility = Visibility.Collapsed;
                    gridEntrada.Visibility = Visibility.Collapsed;
                    textBox.Text = "";
                    Update();
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
        }

        private void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                btOk_Click(sender, e);
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            textBlockAux.Text = "";
            textBox.Text = "";
            textBlockAux.Visibility = Visibility.Collapsed;
            gridEntrada.Visibility = Visibility.Collapsed;
        }

        private void btDelete1_Click(object sender, RoutedEventArgs e)
        {
            Data d = (Data)AjuizadosListView.SelectedItem;
            if(d != null)
            {
                ViewModel.ProcessoAux.Ajuizados.Remove(d);
                Update();
            }
        }

        private void btDelete2_Click(object sender, RoutedEventArgs e)
        {
            Processo.Custa c = (Processo.Custa)CustasListView.SelectedItem;
            if (c != null)
            {
                ViewModel.ProcessoAux.Custas.Remove(c);
                Update();
            }
        }

        private void btAdiciona2_Click(object sender, RoutedEventArgs e)
        {
            //Custa
            textBlockAux.Text = "Digite a data:";
            textBlockAux.Visibility = Visibility.Visible;
            gridEntrada.Visibility = Visibility.Visible;
            controle = "DataCusta";
        }
        
    }
}
