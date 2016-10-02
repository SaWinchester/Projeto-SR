using Projeto_SR.DataBase;
using Projeto_SR.functions;
using Projeto_SR.Functions;
using Projeto_SR.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private MessengerRepository mr;
        private int seq;
        public MainPage()
        {
            this.InitializeComponent();
            RegisterService.registerService();
            verifica_dados();
        }

        private async void verifica_dados()
        {

            mr = await DbFunctions.obtem_conexao_bd();
            List<Messenger> messegers = await DbFunctions.obtem_messeger(mr);

            if (messegers != null && messegers.Count > 0) carrega_mensagens(messegers);
            else { seq = 0; conectaComServidor(); }
        }

        private void carrega_mensagens(List<Messenger> messegers)
        {
            int indice = 0;
            seq = messegers[messegers.Count - 1].seq;
            foreach (var item in messegers)
            {
                Debug.Write(item.seq + "  " + item.type + "\n");
                define_mensagem(item, indice);
                indice++;
            }
        }

        private void define_mensagem(Messenger item, int indice)
        {
            if (item.type.Equals("R") && item.seq == indice)
                listViewMsg.Items.Add(criaCanvarResposta(item.mensagem));
            else 
                if (item.type.Equals("P") && item.seq == indice) listViewMsg.Items.Add(criaCanvarPergunta(item.mensagem));
            else 
                if (item.type.Equals("R")) listViewMsg.Items.Add(criaCanvarResposta(item.mensagem));
            else 
                if(item.type.Equals("P")) listViewMsg.Items.Add(criaCanvarPergunta(item.mensagem));
        }

        private async void conectaComServidor()
        {
            try
            {
                string resposta = await ProcessaAPI.first_connect();
                Messenger me = new Messenger(resposta, "R",seq++);
                DbFunctions.salva_messenger(me, mr);
                listViewMsg.Items.Add(criaCanvarResposta(resposta));
            }
            catch (Exception)
            {
                listViewMsg.Items.Add(criaCanvarResposta("Ops. Tivemos um problema com o Servidor. Tente Novamente mais tarde...:)"));
            }

        }

        private Canvas criaCanvarResposta(string resposta)
        {
            Canvas canvasMsg = new Canvas();

            ImageBrush ib = new ImageBrush();
            

            TextBlock textBlockMsg = new TextBlock();
            textBlockMsg.Text = resposta;
            textBlockMsg.Width = 220;
            textBlockMsg.TextWrapping = TextWrapping.WrapWholeWords;
            textBlockMsg.Foreground = new SolidColorBrush(Colors.White);
            Canvas.SetLeft(textBlockMsg, 160);
            Canvas.SetTop(textBlockMsg, 6);

            if ((textBlockMsg.Text.Length + 3) >= 118)
            {
                ib.ImageSource = new BitmapImage(new Uri("ms-appx://Projeto-SR/Assets/balao_respota.png"));
                canvasMsg.Height = Double.Parse("" + (textBlockMsg.Text.Length + 3));
            }
            else
            {
                ib.ImageSource = new BitmapImage(new Uri("ms-appx://Projeto-SR/Assets/balao_respota_two.png"));
                canvasMsg.Height = 118;
            }
            canvasMsg.Width = listViewMsg.ActualWidth - 5.0;
            canvasMsg.Background = ib;

            canvasMsg.Children.Add(textBlockMsg);
            return canvasMsg;
        }

        private Canvas criaCanvarPergunta(String pergunta)
        {
            Canvas canvasMsg = new Canvas();

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("ms-appx://Projeto-SR/Assets/balao_pergunta.png"));
            //canvasMsg.Background = ib;

            TextBlock textBlockMsg = new TextBlock();
            textBlockMsg.Text = pergunta;
            textBlockMsg.Width = 250;
            textBlockMsg.TextWrapping = TextWrapping.Wrap;
            textBlockMsg.Foreground = new SolidColorBrush(Colors.White);
            Canvas.SetLeft(textBlockMsg, 15);
            Canvas.SetTop(textBlockMsg, 12);

            if ((textBlockMsg.Text.Length + 3) >= 118)
            {
                //ib.ImageSource = new BitmapImage(new Uri("ms-appx://Projeto-SR/Assets/balao_respota.png"));
                canvasMsg.Height = Double.Parse("" + (textBlockMsg.Text.Length + 3));
            }
            else
            {
                //ib.ImageSource = new BitmapImage(new Uri("ms-appx://Projeto-SR/Assets/balao_respota_two.png"));
                canvasMsg.Height = 118;
            }
            canvasMsg.Width = listViewMsg.ActualWidth - 15.0;
            canvasMsg.Background = ib;


            canvasMsg.Children.Add(textBlockMsg);

            return canvasMsg;
        }

        private async void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            string pergunta = textBoxMsg.Text;
            textBoxMsg.Text = "";
            listViewMsg.Items.Add(criaCanvarPergunta(pergunta));

            Messenger me = new Messenger(pergunta, "P",seq++);
            DbFunctions.salva_messenger(me, mr);

            listViewMsg.ScrollIntoView(listViewMsg.Items.ToArray()[listViewMsg.Items.Count - 1]);
            string resposta = await ProcessaAPI.get_serve_response(pergunta);
            Messenger meResp = new Messenger(resposta, "R", seq++);
            DbFunctions.salva_messenger(meResp, mr);
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

        private void altera_visual(object sender, RoutedEventArgs e)
        {
            //#FF52B4AB
            //textBoxMsg.Back
            textBoxMsg.Foreground = new SolidColorBrush(Color.FromArgb(100, 82, 180, 171));
            textBoxMsg.Background = new SolidColorBrush(Color.FromArgb(100, 7, 32, 56));
            textBoxMsg.SelectionHighlightColor = new SolidColorBrush(Color.FromArgb(100, 233, 123, 34));
        }
    }
}
