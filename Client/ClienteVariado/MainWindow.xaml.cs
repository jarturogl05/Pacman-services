using ClienteVariado.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteVariado
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (isConnected)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }
        }

    }
    public partial class MainWindow: Window
    {
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginServiceClient client = new LoginServiceClient("NetTcpBinding_ILoginService");
            ILoginServiceUsuario usuario = new ILoginServiceUsuario();
            usuario.Username = tbUser.Text;
            usuario.Password = tbPassword.Password;
            if (client.ValidateUser(usuario) == 1)
            {
                lbLogin.Content = "Valid user";
            }
            else
            {
                lbLogin.Content = "Invalid user";
            }
        }
    }
    public partial class MainWindow : Window
    {
        private void btRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterServiceClient register = new RegisterServiceClient("NetTcpBinding_IRegisterService");
            IRegisterServiceJugador jugador = new IRegisterServiceJugador();
            jugador.Correo = "mailbox";
            jugador.Nombre = "namebox";
            jugador.Username = "reyOtaco";
            jugador.Password = "radiohead05";
            register.AddUser(jugador);
        }
    }
    public partial class MainWindow : Window, IChatServiceCallback
    {
        bool isConnected = false;
        ChatServiceClient client;
        int ID;

        void Connect()
        {
            if (!isConnected)
            {
                client = new ChatServiceClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUsername.Text);
                tbUsername.IsEnabled = false;
                buttonConnect.Content = "Disconnect";
                isConnected = true;
                client.SendMsg("connected to chat!!", ID);
            }
        }
        void Disconnect()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUsername.IsEnabled = true;
                buttonConnect.Content = "Connect";
                isConnected = false;
            }
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }
        }
        public void MsgCallback(String message)
        {
            lbMsj.Items.Add(message);
            lbMsj.ScrollIntoView(lbMsj.Items[lbMsj.Items.Count - 1]);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Disconnect();
        }

        private void tbIDK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(tbIDK.Text, ID);
                    tbIDK.Text = string.Empty;
                }
            }
        }
    }
}
