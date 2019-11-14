using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

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
            Console.WriteLine("El id: " + user.ID + "\n El usuario: "+ user.Name);
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
                try
                {
                    item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
    public partial class Services : IRegisterService
    {
        private void CheckObjectJugador(IRegisterService.Jugador jugador)
        {
            ValidarCampos validarCampos = new ValidarCampos();
            if(jugador.Correo == string.Empty || jugador.Nombre == string.Empty || jugador.Password == string.Empty || jugador.Username == string.Empty)
            {
                throw new FormatException("El jugador tiene campos vacios");
            }
            else if(validarCampos.ValidarCorreo(jugador.Correo) == ValidarCampos.ResultadosValidacion.Correoinválido)
            {
                throw new FormatException("El correo no tiene un formato válido" + jugador.Correo);
            }
        }



        public DBOperationResult.AddResult AddUser(IRegisterService.Jugador jugador)
        {
            DBOperationResult.AddResult result;
            try
            {
                CheckObjectJugador(jugador);
            }
            catch (FormatException)
            {
                result = DBOperationResult.AddResult.NullObject;
                return result;
            }

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
                        Password = PassHash(jugador.Password),
                        Confirmación = "False",
                        Código = jugador.Código
                }
                },

            };

            foreach (var usuario in usuarios)
            {
                container.JugadorSet.Add(usuario);
            }
            container.SaveChanges();
            result = DBOperationResult.AddResult.Success;

            return result;
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

        public DBOperationResult.AddResult SerachUserInDB(IRegisterService.Jugador jugador)
        {
            DBOperationResult.AddResult result;

            ModelContainer container = new ModelContainer();
            ICollection<Jugador> Jugadores = new List<Jugador>();
            foreach (var Jugador in container.JugadorSet)
            {
                if (Jugador.Usuario.Username == jugador.Username && Jugador.Correo == jugador.Correo) ;
                {
                    Jugadores.Add(Jugador);
                }
            }

            if (Jugadores.Any())
            {
                result = DBOperationResult.AddResult.ExistingRecord;
            }
            else
            {
                result = DBOperationResult.AddResult.Success;
            }

            return result;
        }
    }
    public partial class Services : ILoginService
    {
        public int ValidateUser(ILoginService.Usuario usuario)
        {
            ModelContainer container = new ModelContainer();
            foreach (var Usuario in container.UsuarioSet)
            {
                if (Usuario.Username == usuario.Username && Usuario.Password == PassHash(usuario.Password))
                {
                    return 1;
                }
            }
            return 0;
        }
    }

    public partial class Services : IConfirmationServices
    {
        public int ChangeConfirmationStatus(IConfirmationServices.Jugador jugador)
        {

            int resultado  = 0;
            using (var context = new ModelContainer())
            {

                var jgd = context.JugadorSet
                                .Where(b => b.Correo == jugador.Correo)
                                .FirstOrDefault();

                if (jgd.Usuario.Código == jugador.Código)
                {
                    jgd.Usuario.Confirmación = "True";
                    context.UsuarioSet.Attach(jgd.Usuario);
                    context.Entry(jgd.Usuario).Property("Confirmación").IsModified = true;
                    context.SaveChanges();
                    resultado = 1;
                }
               
            }

            return resultado;
        }

        public int GenerateNewCode(IConfirmationServices.Jugador jugador)
        {
            using (var context = new ModelContainer())
            {
                var jgd = context.JugadorSet
                                .Where(b => b.Correo == jugador.Correo)
                                .FirstOrDefault();


                Console.WriteLine(jgd.Usuario.Código);

                Random random = new Random();
                jgd.Usuario.Código = random.Next(0, 999999).ToString();

                context.UsuarioSet.Attach(jgd.Usuario);
                context.Entry(jgd.Usuario).Property("Código").IsModified = true;
                context.SaveChanges();
                jugador.Código = jgd.Usuario.Código;
                SendEmail(jugador);
            }

            return 1;
        }



        public int SendEmail(IConfirmationServices.Jugador jugador)
        {
            
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(jugador.Correo);
            msg.From = new MailAddress("pacmanlisuv@gmail.com", "Pacman rey de la colina");
            msg.Subject = "Código de confirmación";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = jugador.Código;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("pacmanlisuv@gmail.com", "pacmanpacman05");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;            
            client.Send(msg);
            return 1;
        }
    }
}
