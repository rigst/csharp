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
        private string controleEdicao = "";

        private static BlocoApPage page;

        public BlocoApPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            page = this;
            this.ViewModel = new BlocoApPageViewModel();
        }
        
        public static BlocoApPage getLastInstance()
        {
            return page;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                    textBox.Visibility = Visibility.Collapsed;
                    btCancel.Visibility = Visibility.Collapsed;
                    btOk.Visibility = Visibility.Collapsed;

                    //ENCONTRA O CONDOMINIO
                    string nome = (string)e.Parameter;
                    App minhaApp = (App)App.Current;
                    Condominio cond = (from c in minhaApp.Gerenciador.Condominios
                                       where c.Nome == nome
                                       select c).FirstOrDefault();
                    this.ViewModel.NomeCondominio = cond.Nome;
                    //ATUALIZA OS BLOCOS
                    UpdateBlocos(cond);
            }

        }
        private void UpdateBlocos(Condominio cond)
        {
            if (cond != null)
            {
                List<Bloco> blocos = cond.Blocos;
                ViewModel.Blocos = blocos;
                //ATUALIZA APARTAMENTOS
                if(blocos.Count != 0)
                {
                    UpdateApartamentos(blocos.First<Bloco>());
                }
            }
        }

        private void UpdateApartamentos(Bloco b)
        {
            List<Apartamento> listAp = b.Apartamentos;
            if(listAp == null || listAp.Count == 0) this.ViewModel.ApAtual = null;
            else
            {
                Apartamento ap = listAp.First<Apartamento>();
                this.ViewModel.ApAtual = ap;
            }
        }
        
        private void BlocosListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            App minhaApp = (App)App.Current;
            Bloco bloco = (Bloco)e.ClickedItem;
            List<Apartamento> listAp = bloco.Apartamentos;
            this.ViewModel.Apartamentos = listAp;

            Apartamento ap = null;
            if (listAp != null && listAp.Count != 0)
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

        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            List<Bloco> lista = ViewModel.Blocos;
            int count = 1;

            while (existeNum(lista, count))
            {
                count++;
            }
            
            Bloco novoBloco = new Bloco(count);
            lista.Add(novoBloco);
            ViewModel.Blocos = lista;
            Update();
        }

        private bool existeNum(List<Bloco> lista, int n)
        {
            bool existe = false;
            foreach (var v in lista)
            {
                if (v.Numero == n) existe = true;
            }
            return existe;
        }

        private void btRename_Click(object sender, RoutedEventArgs e)
        {
            Bloco b = (Bloco)BlocosListView.SelectedItem;
            if (b != null)
            {
                textBox.Visibility = Visibility.Visible;
                btOk.Visibility = Visibility.Visible;
                btCancel.Visibility = Visibility.Visible;
                controleEdicao = "Bloco";
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            textBox.Visibility = Visibility.Collapsed;
            btOk.Visibility = Visibility.Collapsed;
            btCancel.Visibility = Visibility.Collapsed;
            textBox.Text = "";
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            string s = textBox.Text;
            int num = 0;
            if (s.Length < 1 || s.Equals(" ") || s.Equals("")) return;

            try
            {
                num = Int32.Parse(s);
            }
            catch(Exception)
            {
                textBox.Text = "";
                return;
            }
            
            if (controleEdicao.Equals("Bloco"))
            {
                Bloco b = ((Bloco)BlocosListView.SelectedItem);
                List<Bloco> lista = ViewModel.Blocos;
                if (existeNaLista(lista, num))
                {
                    textBox.Text = ""; return;
                }

                foreach (var v in lista)
                {
                    if (v.Numero == b.Numero) v.Numero = num;
                }

                this.Frame.Navigate(typeof(MainPage), ViewModel.Original);
                App.Update(this, ViewModel.Original);
                return;
            }
            else if (controleEdicao.Equals("Apartamento"))
            {
                Apartamento ap = ((Apartamento)ApListView.SelectedItem);
                List<Apartamento> lista = ViewModel.Apartamentos;
                if (existeNaLista(lista, num))
                {
                    textBox.Text = ""; return;
                }

                foreach (var v in lista)
                {
                    if (v.Numero == ap.Numero) v.Numero = num;
                }

                Update();
            }
            else if (controleEdicao.Equals("Box"))
            {
                Apartamento ap = ((Apartamento)ApListView.SelectedItem);
                List<Apartamento> lista = ViewModel.Apartamentos;
                if (ap != null)
                {
                    foreach (var v in lista)
                    {
                        if (ap.Numero == v.Numero)
                        {
                            if (num < 10) v.Box = "0" + num;
                            else v.Box = "" + num;
                        }
                        Update();
                    }
                }
            }

        }

        private bool existeNaLista(List<Bloco> lista, int s)
        {
            foreach (var v in lista)
            {
                if (v.Numero == s) return true;
            }
            return false;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            Bloco b = ((Bloco)BlocosListView.SelectedItem);
            if (b != null)
            {
                List<Bloco> lista = ViewModel.Blocos;
                lista.Remove(b);
                Update();
            }
        }

        private void btNew1_Click(object sender, RoutedEventArgs e)
        {
            List<Apartamento> lista = ViewModel.Apartamentos;
            int count = 100;

            while (existeNaLista(lista, count))
            {
                count++;
            }

            Apartamento novoAp = new Apartamento(count);
            lista.Add(novoAp);
            ViewModel.Apartamentos = lista;
            Update();
        }

        private bool existeNaLista(List<Apartamento> lista, int s)
        {
            foreach (var v in lista)
            {
                if (v.Numero == s) return true;
            }
            return false;
        }

        private void btRename1_Click(object sender, RoutedEventArgs e)
        {
            Apartamento ap = (Apartamento)ApListView.SelectedItem;
            if (ap != null)
            {
                textBox.Visibility = Visibility.Visible;
                btOk.Visibility = Visibility.Visible;
                btCancel.Visibility = Visibility.Visible;
                controleEdicao = "Apartamento";
            }
        }

        private void btDelete1_Click(object sender, RoutedEventArgs e)
        {
            Apartamento ap = ((Apartamento)ApListView.SelectedItem);
            List<Apartamento> lista = ViewModel.Apartamentos;
            if(ap != null)
            {
                lista.Remove(ap);
                Update();
            }
        }


        private void Update()
        {
            this.Frame.Navigate(typeof(MainPage), ViewModel.Original);
            App.Update(this, ViewModel.Original);
            BlocoApPage nova = BlocoApPage.getLastInstance();
            nova.ViewModel = this.ViewModel;
        }

        private void btEditarBox_Click(object sender, RoutedEventArgs e)
        {
            textBox.Visibility = Visibility.Visible;
            btOk.Visibility = Visibility.Visible;
            btCancel.Visibility = Visibility.Visible;
            controleEdicao = "Box";
        }

        private bool existeNum(List<Apartamento> lista, int n)
        {
            bool existe = false;
            foreach (var v in lista)
            {
                if (v.Numero == n) existe = true;
            }
            return existe;
        }
    }
}

     