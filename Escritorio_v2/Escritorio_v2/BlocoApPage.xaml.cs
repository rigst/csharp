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
using Escritorio_v2.ViewModel;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Escritorio_v2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlocoApPage : Page
    {
        public BlocoApPageViewModel ViewModel { get; set; }
        public BlocoApPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            this.ViewModel = new BlocoApPageViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                string nome = (string)e.Parameter;
                App minhaApp = (App)App.Current;
                Condominio cond = (from c in minhaApp.Gerenciador.Condominios
                                    where c.Nome == nome
                                    select c).FirstOrDefault();
                List<Bloco> blocos = cond.Blocos;
                this.ViewModel.Blocos = blocos;
                if(blocos.Count != 0)
                {
                    Bloco b = blocos.First<Bloco>();
                    List<Apartamento> listAp = b.Apartamentos;
                    this.ViewModel.Apartamentos = listAp;

                    if(listAp.Count != 0)
                    {
                        Apartamento ap = listAp.First<Apartamento>();
                        this.ViewModel.ApAtual = ap;
                    }
                }
                
            }
        }

        private void BlocosListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            App minhaApp = (App)App.Current;
            Bloco bloco = (Bloco)e.ClickedItem;
            List<Apartamento> listAp = bloco.Apartamentos;
            this.ViewModel.Apartamentos = listAp;

            Apartamento ap = null;
            if (listAp.Count != 0)
            {
                ap = listAp.First<Apartamento>();
            }
            this.ViewModel.ApAtual = ap;
        }

        private void ApListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Apartamento ap = (Apartamento)e.ClickedItem;
            this.ViewModel.ApAtual = ap;
        }
    }
}
