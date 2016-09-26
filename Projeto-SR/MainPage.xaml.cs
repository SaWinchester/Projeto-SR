using Projeto_SR.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Projeto_SR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void conectaComServidor()
        {
            try
            {
                string resposta = await ProcessaAPI.first_connect();
                listViewMsg.Items.Add(criaCanvarResposta(resposta));

            }
            catch (Exception e)
            {
                listViewMsg.Items.Add(criaCanvarResposta("Ops. Tivemos um problema com o Servidor. Tente Novamente mais tarde...:)"));
            }

        }

        private Canvas criaCanvarResposta(string resposta)
        {
            Canvas canvasMsg = new Canvas();
            TextBlock textBlockMsg = new TextBlock();
            textBlockMsg.Text = resposta;
            textBlockMsg.Width = listViewMsg.ActualWidth - 85;
            textBlockMsg.TextWrapping = TextWrapping.WrapWholeWords;
            textBlockMsg.Foreground = new SolidColorBrush(Colors.White);
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
            textBlockMsg.Width = listViewMsg.ActualWidth - 85;
            textBlockMsg.TextWrapping = TextWrapping.Wrap;
            textBlockMsg.Foreground = new SolidColorBrush(Colors.White);
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
            string resposta = await ProcessaAPI.get_serve_response(pergunta);
            await Task.Delay(TimeSpan.FromSeconds(1));
            listViewMsg.Items.Add(criaCanvarResposta(resposta));
            listViewMsg.ScrollIntoView(listViewMsg.Items.ToArray()[listViewMsg.Items.Count - 1]);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Sobre));
        }

        private void AppBarButton_Click2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Configuracao));
        }

    }
}
