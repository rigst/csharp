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
using Windows.Storage;
using Windows.Storage.Pickers;
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
                    textBlockAux.Text = "";
                    textBlockAux.Visibility = Visibility.Collapsed;
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
                textBlockAux.Text = "Digite o número do bloco:";
                textBlockAux.Visibility = Visibility.Visible;
                textBox.Visibility = Visibility.Visible;
                btOk.Visibility = Visibility.Visible;
                btCancel.Visibility = Visibility.Visible;
                controleEdicao = "Bloco";
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            textBlockAux.Text = "";
            textBlockAux.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Collapsed;
            btOk.Visibility = Visibility.Collapsed;
            btCancel.Visibility = Visibility.Collapsed;
            textBox.Text = "";
        }
      

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (btOk.Visibility == Visibility.Collapsed) return;
            else if (controleEdicao.Equals("Salvo"))
            {
                textBox.Visibility = Visibility.Collapsed;
                btOk.Visibility = Visibility.Collapsed;
                return;
            }
            string s = textBox.Text;
            int num = 0;
            if (controleEdicao.Equals("Observação"))
            {
                ViewModel.CotaAux.Observacao = s;
                ViewModel.ApAtual.addCota(ViewModel.CotaAux);
                Update();
            }
            else if (controleEdicao.Equals("NomeAp"))
            {
                ViewModel.ApAtual.Nome = s;
                Update();
            }
            else if (controleEdicao.Equals("Espécie"))
            {
                ViewModel.ProcessoAux.Especie = s;
                textBlockAux.Text = "Digite o valor ajuizado:";
                controleEdicao = "ValorAjuizado";
                textBox.Text = "";
            }

            if (s.Length < 1 || s.Equals(" ") || s.Equals("")) return;

            if (controleEdicao.Equals("DataVencimento"))
            {
                try
                {
                    ViewModel.CotaAux = new Cota();
                    Data dataV = new Data(s);
                    ViewModel.CotaAux.DataVencimento = dataV;
                    textBlockAux.Text = "Digite o valor pago:";
                    controleEdicao = "ValorPago";
                    textBox.Text = "";
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("Réu"))
            {
                ViewModel.ProcessoAux.Reu = s;
                textBlockAux.Text = "Digite a data de ajuizamento:";
                controleEdicao = "ProcessoDataAjuizamento";
                textBox.Text = "";
            }
            else if (controleEdicao.Equals("ValorPago"))
            {
                try
                {
                    double valor = Double.Parse(s);
                    ViewModel.CotaAux.ValorPago = valor;
                    textBlockAux.Text = "Digite a data de pagamento:";
                    controleEdicao = "DataPagamento";
                    textBox.Text = "";

                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("DataPagamento"))
            {
                try
                {
                    Data dataP = new Data(s);
                    ViewModel.CotaAux.DataPagamento = dataP;
                    textBlockAux.Text = "Digite uma observação:";
                    controleEdicao = "Observação";
                    textBox.Text = "";
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("Devedor"))
            {
                ViewModel.AcordoAux = new Acordo();
                ViewModel.AcordoAux.Devedor = s;
                textBlockAux.Text = "Digite o número do processo:";
                controleEdicao = "NumeroProcesso";
                textBox.Text = "";
            }
            else if (controleEdicao.Equals("NumeroProcesso"))
            {
                ViewModel.AcordoAux.Processo = s;
                textBlockAux.Text = "Digite a data de assinatura:";
                controleEdicao = "DataAssinatura";
                textBox.Text = "";
            }
            else if (controleEdicao.Equals("DataAssinatura"))
            {
                try
                {
                    Data dataAssinatura = new Data(s);
                    ViewModel.AcordoAux.DataAssinatura = dataAssinatura;
                    textBlockAux.Text = "Digite a data de início:";
                    controleEdicao = "DataInicio";
                    textBox.Text = "";

                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("DataInicio"))
            {
                try
                {
                    Data dataInicio = new Data(s);
                    ViewModel.AcordoAux.Inicio = dataInicio;
                    textBlockAux.Text = "Digite a data de fim:";
                    controleEdicao = "DataFim";
                    textBox.Text = "";

                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("DataFim"))
            {
                try
                {
                    Data dataFim = new Data(s);
                    ViewModel.AcordoAux.Fim = dataFim;
                    textBlockAux.Text = "Digite o valor total:";
                    controleEdicao = "ValorTotal";
                    textBox.Text = "";

                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("ValorTotal"))
            {
                try
                {
                    double valorTotal = Double.Parse(s);
                    ViewModel.AcordoAux.ValorTotal = valorTotal;
                    textBlockAux.Text = "Digite o valor da parcela:";
                    controleEdicao = "ValorParcela";
                    textBox.Text = "";

                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("ValorParcela"))
            {
                try
                {
                    double parcela = Double.Parse(s);
                    ViewModel.AcordoAux.ValorParcela = parcela;
                    textBlockAux.Text = "Digite a forma de atualização:";
                    controleEdicao = "FormaAtualização";
                    textBox.Text = "";
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("FormaAtualização"))
            {
                ViewModel.AcordoAux.FormaAtualizacao = s;
                textBlockAux.Text = "Digite o número de parcelas:";
                controleEdicao = "NumeroParcelas";
                textBox.Text = "";
            }
            else if (controleEdicao.Equals("NumeroParcelas"))
            {
                try
                {
                    int numParcelas = Int32.Parse(s);
                    ViewModel.AcordoAux.NumParcelas = numParcelas;
                    ViewModel.ApAtual.addAcordo(ViewModel.AcordoAux);
                    Update();
                }
                catch(Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("ProcessoNumero"))
            {
                try
                {
                    ViewModel.ProcessoAux = new Processo();
                    ViewModel.ProcessoAux.NumProcesso = s;
                    textBlockAux.Text = "Digite o nome do réu:";
                    controleEdicao = "Réu";
                    textBox.Text = "";

                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("ProcessoDataAjuizamento"))
            {
                try
                {
                    Data novaData = new Data(s);
                    ViewModel.ProcessoAux.DataAjuizamento = novaData;
                    textBlockAux.Text = "Digite a espécie do processo:";
                    controleEdicao = "Espécie";
                    textBox.Text = "";
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("ValorAjuizado"))
            {
                try
                {
                    double valorA = Double.Parse(s);
                    ViewModel.ProcessoAux.ValorAjuizado = valorA;
                    textBlockAux.Text = "Digite o período";
                    controleEdicao = "Período";
                    textBox.Text = "";
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("Período"))
            {
                try
                {
                    Data periodo = new Data(s);
                    ViewModel.ProcessoAux.Periodo = periodo;
                    textBlockAux.Text = "Digite a data do último movimento:";
                    controleEdicao = "UltimoMov";
                    textBox.Text = "";
                }
                catch (Exception) { textBox.Text = ""; return; }
            }
            else if (controleEdicao.Equals("UltimoMov"))
            {
                try
                {
                    Data ult = new Data(s);
                    ViewModel.ProcessoAux.UltimoMovimento = ult;
                    ViewModel.ApAtual.Processos.Add(ViewModel.ProcessoAux);
                    Update();
                }
                catch (Exception) { textBox.Text = ""; return; }
            }


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
            if (lista == null) return;
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
                textBlockAux.Text = "Digite o número do apartamento:";
                textBlockAux.Visibility = Visibility.Visible;
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
            if (ApListView.SelectedItem == null || ViewModel.ApAtual == null) return;
            textBlockAux.Text = "Digite o número do box:";
            textBlockAux.Visibility = Visibility.Visible;
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

        private void btAdicionar1_Click(object sender, RoutedEventArgs e)
        {
            //ADICIONA COTA
            textBlockAux.Text = "Digite a data de vencimento:";
            textBlockAux.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;
            btOk.Visibility = Visibility.Visible;
            btCancel.Visibility = Visibility.Visible;
            controleEdicao = "DataVencimento";
        }
        
        private void btDeletar1_Click(object sender, RoutedEventArgs e)
        {
            Cota cota = (Cota)CotasListView.SelectedItem;
            if(cota != null)
            {
                ViewModel.ApAtual.Cotas.Remove(cota);
                Update();
            }
        }

        private void btAdicionar2_Click(object sender, RoutedEventArgs e)
        {
            //ADICIONA ACORDO
            textBlockAux.Text = "Digite o nome do devedor:";
            textBlockAux.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;
            btOk.Visibility = Visibility.Visible;
            btCancel.Visibility = Visibility.Visible;
            controleEdicao = "Devedor";
        }

        private void btDeletar2_Click(object sender, RoutedEventArgs e)
        {
            Acordo acordo = (Acordo)AcordosListView.SelectedItem;
            if(acordo != null)
            {
                ViewModel.ApAtual.Acordos.Remove(acordo);
                Update();
            }
        }

        private void btEditarNome_Click(object sender, RoutedEventArgs e)
        {
            if (ApListView.SelectedItem == null || ViewModel.ApAtual == null) return;
            textBlockAux.Text = "Digite o nome:";
            textBlockAux.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;
            btOk.Visibility = Visibility.Visible;
            btCancel.Visibility = Visibility.Visible;
            controleEdicao = "NomeAp";
        }

        private void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                btOk_Click(sender, e);
            }
        }

        private void btAdicionar3_Click(object sender, RoutedEventArgs e)
        {
            //ADICIONA PROCESSO
            textBlockAux.Text = "Digite o número do processo:";
            textBlockAux.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;
            btOk.Visibility = Visibility.Visible;
            btCancel.Visibility = Visibility.Visible;
            controleEdicao = "ProcessoNumero";
        }

        private void btDeletar3_Click(object sender, RoutedEventArgs e)
        {
            Processo p = (Processo)ProcessosListView.SelectedItem;
            if(p != null)
            {
                ViewModel.ApAtual.Processos.Remove(p);
                Update();
            }
        }

        private async void btSave_Click(object sender, RoutedEventArgs e)
        {
            App minhaApp = (App)App.Current;
            StorageFolder fold = Windows.Storage.ApplicationData.Current.LocalFolder;
            //C:\Users\Rodrigo\AppData\Local\Packages\ab4e3a68-b729-4a53-b8d8-05b98dc64adf_m5kereyqkfrtt\LocalState
            StorageFile file = await fold.CreateFileAsync("armazenado.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, minhaApp.Gerenciador.getRelatorio());
            textBox.Text = "Arquivo Salvo com Sucesso!";
            textBox.Visibility = Visibility.Visible;
        }

        private async void btSaveAs_Click(object sender, RoutedEventArgs e)
        {
            App minhaApp = (App)App.Current;
            FolderPicker openPicker = new FolderPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await openPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                StorageFile file = await folder.CreateFileAsync("salvo.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, minhaApp.Gerenciador.getRelatorio());
                textBox.Text = "Arquivo Salvo com Sucesso!";
                textBox.Visibility = Visibility.Visible;
                btOk.Visibility = Visibility.Visible;
                controleEdicao = "Salvo";
            }
        }
    }
}

     