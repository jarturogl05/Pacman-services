using DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;



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
            Console.WriteLine("El id: " + user.ID + "\n El usuario: " + user.Name);
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

            if (jugador.Correo == string.Empty || jugador.Nombre == string.Empty || jugador.Password == string.Empty || jugador.Username == string.Empty)
            {
                throw new FormatException("El jugador tiene campos vacios");
            }
            else if (validarCampos.ValidarCorreo(jugador.Correo) == ValidarCampos.ResultadosValidacion.Correoinválido)
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
                PuntuaciónAlta = 0,
                Ranking = new Ranking
                {
                    Posicion = 0
                },
                Usuario = new Usuario{
                        Username = jugador.Username,
                        Password = jugador.Password,
                        Confirmación = "False",
                        Código = int.Parse(jugador.Código)
                }
                },

            };

            foreach (var usuario in usuarios)
            {
                container.JugadorSet.Add(usuario);
            }
            try
            {
                container.SaveChanges();
                result = DBOperationResult.AddResult.Success;
            }
            catch (EntityException)
            {
                result = DBOperationResult.AddResult.SQLError;
            }


            return result;
        }

        public DBOperationResult.AddResult SerachUserInDB(IRegisterService.Jugador jugador)
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
            ModelContainer container = new ModelContainer();
            ICollection<Jugador> Jugadores = new List<Jugador>();
            try
            {
                foreach (var player in container.JugadorSet)
                {
                    if (player.Usuario.Username == jugador.Username || player.Correo == jugador.Correo)
                    {
                        Jugadores.Add(player);
                    }
                }
            }
            catch (EntityException)
            {
                result = DBOperationResult.AddResult.SQLError;
                return result;
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
        private void CheckObjectUser(ILoginService.Usuario usuario)
        {
            if (usuario.Username == string.Empty || usuario.Password == string.Empty)
            {
                throw new FormatException("El jugador tiene campos vacios");
            }
        }

        public string GetEmail(ILoginService.Usuario usuario)
        {
            string email;
            ModelContainer container = new ModelContainer();
            ICollection<Usuario> usuarios = new List<Usuario>();
            try
            {
                foreach (var user in container.UsuarioSet)
                {
                    if (user.Username == usuario.Username)
                    {
                        usuarios.Add(user);
                    }
                }
            }
            catch (EntityException)
            {
                email = null;
                return email;
            }
            email = usuarios.Single().Jugador.Correo;
            return email;
        }

        public DBOperationResult.AddResult ValidateUser(ILoginService.Usuario usuario)
        {
            DBOperationResult.AddResult result;
            try
            {
                CheckObjectUser(usuario);
            }
            catch
            {
                result = DBOperationResult.AddResult.NullObject;
                return result;
            }
            ModelContainer container = new ModelContainer();
            ICollection<Usuario> usuarios = new List<Usuario>();

            try
            {
                foreach (var user in container.UsuarioSet)
                {
                    if (user.Username == usuario.Username && user.Password == usuario.Password)
                    {
                        usuarios.Add(user);
                    }
                }
            }
            catch (EntityException)
            {
                result = DBOperationResult.AddResult.SQLError;
                return result;
            }

            if (usuarios.Any())
            {
                if (usuarios.Single().Confirmación == "False")
                {
                    result = DBOperationResult.AddResult.ConfirmationIsFalse;
                }
                else
                {
                    result = DBOperationResult.AddResult.Success;
                }
            }
            else
            {
                result = DBOperationResult.AddResult.WrongCredentials;
            }
            return result;
        }
    }





    public partial class Services : IConfirmationServices
    {
        public DBOperationResult.AddResult ChangeConfirmationStatus(IConfirmationServices.Jugador jugador)
        {
            DBOperationResult.AddResult result;
            try
            {
                using (var context = new ModelContainer())
                {
                    var jgd = context.JugadorSet
                                    .Where(b => b.Correo == jugador.Correo)
                                    .FirstOrDefault();
                    if (jgd.Usuario.Código == int.Parse(jugador.Código))
                    {
                        jgd.Usuario.Confirmación = "True";
                        context.UsuarioSet.Attach(jgd.Usuario);
                        context.Entry(jgd.Usuario).Property("Confirmación").IsModified = true;
                        context.SaveChanges();
                        result = DBOperationResult.AddResult.Success;
                    }
                    else
                    {
                        result = DBOperationResult.AddResult.WrongCredentials;
                    }
                }
               
            }
            catch (EntityException)
            {
                result = DBOperationResult.AddResult.SQLError;
            }
            return result;
        }

        public DBOperationResult.AddResult GenerateNewCode(IConfirmationServices.Jugador jugador)
        {
            DBOperationResult.AddResult result;
            try
            {
                using (var context = new ModelContainer())
                {
                    var jgd = context.JugadorSet
                                    .Where(b => b.Correo == jugador.Correo)
                                    .FirstOrDefault();
                    jgd.Usuario.Código = int.Parse(jugador.Código);
                    context.UsuarioSet.Attach(jgd.Usuario);
                    context.Entry(jgd.Usuario).Property("Código").IsModified = true;
                    context.SaveChanges();
                    jugador.Código = jgd.Usuario.Código.ToString();
                    SendEmail(jugador);
                }
                result = DBOperationResult.AddResult.Success;
            }
            catch (EntityException)
            {
                result = DBOperationResult.AddResult.SQLError;
            }
            return result;
        }



        public int SendEmail(IConfirmationServices.Jugador jugador)
        {
            int result;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(jugador.Correo);
            msg.From = new MailAddress(ConfigurationManager.AppSettings["pacmanEmail"], "Pacman rey de la colina");
            msg.Subject = "Código de confirmación";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = jugador.Código;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["pacmanEmail"], ConfigurationManager.AppSettings["pacmanEmailPassword"]);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            try
            {
                client.Send(msg);
                result = 1;
            }
            catch (SmtpException)
            {
                result = 0;
            }
            return result;
        }
    }
    public partial class Services : IScoreService
    {
        public int GetScore(IScoreService.User user)
        {
            ModelContainer container = new ModelContainer();
            var jugador = container.JugadorSet.Where(x => x.Usuario.Username == user.Nombre).FirstOrDefault();
            int maxPoint = jugador.PuntuaciónAlta;
            return maxPoint;
        }

        public bool SetScore(IScoreService.User user, double score)
        {
            ModelContainer container = new ModelContainer();
            var jugador = container.JugadorSet.Where(x => x.Usuario.Username == user.Nombre).FirstOrDefault();
            bool result = false;
            if (GetScore(user) < user.Puntuación && jugador != null)
            {
                jugador.PuntuaciónAlta = user.Puntuación;
                using (container)
                {
                    container.Entry(jugador).State = System.Data.Entity.EntityState.Modified;
                    container.SaveChanges();
                }
                result = true;
            }
            return result;
        }
    }
}
