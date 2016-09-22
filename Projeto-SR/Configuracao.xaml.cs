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
            App app = Application.Current as App;
            textBox.Text = app.ipServidor;
            GC.SuppressFinalize(app);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            App app = Application.Current as App;
            app.ipServidor = textBox.Text;
            GC.SuppressFinalize(app);
        }

        private void limparIp(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void limparConversas(object sender, RoutedEventArgs e)
        {
            App app = Application.Current as App;
            app.listViewMsg.Items.Clear();
            GC.SuppressFinalize(app);
        }
    }
}
