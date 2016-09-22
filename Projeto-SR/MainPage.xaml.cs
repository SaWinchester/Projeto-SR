using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Projeto_SR
{
  
    public sealed partial class MainPage : Page
    {

        private string ipServidor;
        public MainPage()
        {
           this.InitializeComponent();
           //Obtem a referencia da aplicação
           App app = Application.Current as App;
           ListView list = app.listViewMsg;
           ipServidor = app.ipServidor;
            if (list.Items.Count == 0)
                conectaComServidor();
            else
                listViewMsg.ItemsSource = list.ItemsSource;
            listViewMsg.Visibility = Visibility.Visible;

        }

        private bool verificaconexao()
        {
            /*Verfica se o smartphone está conectado a internet caso
                   esteja retorna true, caso não e retornado false*/
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                return true;
            }
            else
               return false;
        }

        private async void conectaComServidor()
        {
            try { 
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(new Uri("http://"+ ipServidor + "/primeiraconexao"));
            string result = await response.Content.ReadAsStringAsync();
            listViewMsg.Items.Add(criaCanvarResposta(result));
            client.Dispose();

            }catch(Exception e)
            {
                listViewMsg.Items.Add(criaCanvarResposta("Ops. Tivemos um problema com o Servidor. Tente Novamente mais tarde...:)"));
            }

        }

        private Canvas criaCanvarResposta(string resposta)
        {
            Canvas canvasMsg = new Canvas();
            TextBlock textBlockMsg = new TextBlock();
            textBlockMsg.Text = resposta;
            textBlockMsg.Width = 220;
            textBlockMsg.TextWrapping = TextWrapping.Wrap;
            textBoxMsg.BorderBrush = new SolidColorBrush(Colors.PowderBlue);
            textBlockMsg.Foreground = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(textBlockMsg, 80);
            Canvas.SetTop(textBlockMsg, 6);

            Image image = new Image();
            image.Width = 68;
            image.Height = 71;
            image.Source = new BitmapImage(new Uri("http://cdn.shopify.com/s/files/1/0185/5092/products/persons-0041.png?v=1369543932"));
            Canvas.SetLeft(image, -2);
            Canvas.SetTop(image, 5);
            
            if ((textBlockMsg.Text.Length + 3) >= image.ActualHeight)
                canvasMsg.Height = Double.Parse("" + (textBlockMsg.Text.Length + 3));
            else
                canvasMsg.Height = image.ActualHeight + 3.0;
            canvasMsg.Width = listViewMsg.ActualWidth - 5;

            canvasMsg.Children.Add(textBlockMsg);
            canvasMsg.Children.Add(image);

            return canvasMsg;
        }

        private Canvas criaCanvarPergunta(String pergunta)
        {
            Canvas canvasMsg = new Canvas();
            TextBlock textBlockMsg = new TextBlock();
            textBlockMsg.Text = pergunta;
            textBlockMsg.Width = 220;
            //textBlockMsg.Height = 69;
            textBlockMsg.TextWrapping = TextWrapping.Wrap;
            textBoxMsg.BorderBrush = new SolidColorBrush(Colors.Black);
            textBlockMsg.Foreground = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(textBlockMsg, 5);
            Canvas.SetTop(textBlockMsg, 6);

            Image image = new Image();
            image.Width = 68;
            image.Height = 71;
            image.Source = new BitmapImage(new Uri("https://s-media-cache-ak0.pinimg.com/originals/2f/ab/1b/2fab1be7f595a58815389c0ff90ca1fb.png"));
            Canvas.SetLeft(image, 222);
            Canvas.SetTop(image, 5);

            if ((textBlockMsg.Text.Length + 3) >= image.ActualHeight)
                canvasMsg.Height = Double.Parse("" + (textBlockMsg.Text.Length + 3));
            else
                canvasMsg.Height = image.ActualHeight + 3.0;
            canvasMsg.Width = listViewMsg.ActualWidth - 5;

            canvasMsg.Children.Add(textBlockMsg);
            canvasMsg.Children.Add(image);

            return canvasMsg;
        }

        private async void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            string pergunta = textBoxMsg.Text;
            textBoxMsg.Text = "";
            listViewMsg.Items.Add(criaCanvarPergunta(pergunta));
            
            listViewMsg.ScrollIntoView(listViewMsg.Items.ToArray()[listViewMsg.Items.Count - 1]);
            string resposta = await enviaMensagemSever(pergunta);
            await Task.Delay(TimeSpan.FromSeconds(1));
            listViewMsg.Items.Add(criaCanvarResposta(resposta));
            listViewMsg.ScrollIntoView(listViewMsg.Items.ToArray()[listViewMsg.Items.Count - 1]);


        }

        private async Task<string> enviaMensagemSever(string pergunta)
        {
            try {
                string resposta = "";

                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(pergunta,System.Text.Encoding.UTF8,"application/json");
                var response = await client.PutAsync("http://" + ipServidor + "/mensagem", content);
                resposta = await response.Content.ReadAsStringAsync();

                return resposta;

            }catch(Exception e)
                {
                   return ("Ops. Tivemos um problema com o Servidor. Tente Novamente mais tarde...:)");
                }
}

        private void salvaListMensagens()
        {
            App app = Application.Current as App;
            app.listViewMsg.ItemsSource = listViewMsg.ItemsSource;
            GC.SuppressFinalize(app);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            salvaListMensagens();
            Frame.Navigate(typeof(Sobre));
        }

        private void textBoxMsg_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxMsg.Background = new SolidColorBrush(Colors.Transparent);
            textBoxMsg.UseSystemFocusVisuals = false;
        }
        
        private void AppBarButton_Click2(object sender, RoutedEventArgs e)
        {
            salvaListMensagens();
            Frame.Navigate(typeof(Configuracao));
        }
    }
}
