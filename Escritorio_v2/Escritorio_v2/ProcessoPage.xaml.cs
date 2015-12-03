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
        
        public ProcessoPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            page = this;
            this.ViewModel = BlocoApPage.getLastInstance().ViewModel;
            avisoTextBlock.Visibility = Visibility.Collapsed;
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
    }
}
