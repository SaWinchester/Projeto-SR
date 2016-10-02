using Projeto_SR.DataBase;
using Projeto_SR.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Projeto_SR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Configuracao : Page
    {
        public Configuracao()
        {
            this.InitializeComponent();
            
        }

        private void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void limparConversas(object sender, RoutedEventArgs e)
        {
            MessengerRepository mr = await DbFunctions.obtem_conexao_bd();

            

            List<Messenger> messengers = await mr.SelectAllMessengerAsync();

            await Task.Delay(TimeSpan.FromSeconds(1));

            foreach (var item in messengers)
            {
                await mr.DeleteMessengerAsync(item);
            }
            await Task.Delay(TimeSpan.FromSeconds(1));

            await new MessageDialog("Excluindo Conversa!").ShowAsync();


            GC.SuppressFinalize(mr);
        }
    }
}
