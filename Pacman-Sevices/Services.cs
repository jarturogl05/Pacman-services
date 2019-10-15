using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Sevices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class Services : IChatService
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        public int Connect(String name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;
            users.Add(user);
            return user.ID;
        }
        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(": " + user.Name + " disconnected from chat!", 0);
            }
        }
        public void SendMsg(String message, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + " ";
                }
                answer += message;
                item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }
        }
    }
    public partial class Services : IRegisterService
    {
        public int AddUser(IRegisterService.Jugador jugador)
        {
            DataAccess.ModelContainer container = new ModelContainer();
            ICollection<Jugador> usuarios = new List<Jugador>
        {
            new Jugador{
                Nombre = jugador.Nombre,
                Correo = jugador.Correo,
                Elo = "",
                PuntuaciónAlta = "",
                Puntuación = "",
                PantallasGanadas = "",

                Ranking = new Ranking
                {
                    Posicion = "0"
                },
                Usuario = new Usuario{
                        Username = jugador.Username,
                        Password = PassHash(jugador.Password)
                }
            },

        };

            foreach (var usuario in usuarios)
            {
                container.JugadorSet.Add(usuario);
            }
            container.SaveChanges();

            return 1;
        }
        /// <summary>Hashea un parametro ingresado.</summary>
        /// <param name="data">El parametro.</param>
        /// <returns>El parametro en SHA1</returns>
        private String PassHash(String data)
        {
            SHA1 sha = SHA1.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder stringBuilderValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                stringBuilderValue.Append(hashData[i].ToString());
            }
            return stringBuilderValue.ToString();
        }
    }
    public partial class Services : ILoginService
    {
        public int ValidateUser(ILoginService.Usuario usuario)
        {
            ModelContainer container = new ModelContainer();
            foreach (var Usuario in container.UsuarioSet)
            {
                if (Usuario.Username == usuario.Username && Usuario.Password == usuario.Password)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
