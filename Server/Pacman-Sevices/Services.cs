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
    /// <summary>Clase parcial que contiene los métodos para realizar un registro</summary>
    /// <seealso cref="Pacman_Sevices-services.IRegisterService" />
    public partial class Services : IRegisterService
    {

        /// <summary>Revisa el objeto jugador en busca de inconsistencias.</summary>
        /// <param name="jugador">Un jugador.</param>
        /// <returns>El resultado de la validacion</returns>
        /// <exception cref="FormatException">
        /// El objeto contiene campos vacios
        /// El correo no cumple con los criterios: " + jugador.Correo
        /// </exception>
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

        /// <summary>Añade un usuario a la base de datos.</summary>
        /// <param name="jugador">El jugador.</param>
        /// <returns>El resultado de la operacion</returns>
        /// <exception cref="EntityException">En caso de que ocurra un error en la base de datos</exception>

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

        /// <summary>Busca un jugador en la base de datos.</summary>
        /// <param name="jugador">El jugador.</param>
        /// <returns>El resultado de la operacion</returns>
        /// <exception cref="EntityException">En caso de que ocurra un error en la base de datos</exception>
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



    /// <summary>Clase parcial que contiene los métodos para realizar el inicio se sesión</summary>
    /// <seealso cref="Pacman_Sevices-services.ILoginService" />
    public partial class Services : ILoginService
    {
        /// <summary>Revisa el objeto usuario en busca de inconsistencias.</summary>
        /// <param name="usuario">Un usuario.</param>
        /// <returns>El resultado de la validacion</returns>
        /// <exception cref="FormatException">
        /// El objeto contiene campos vacios
        /// </exception>
        private void CheckObjectUser(ILoginService.Usuario usuario)
        {
            if (usuario.Username == string.Empty || usuario.Password == string.Empty)
            {
                throw new FormatException("El jugador tiene campos vacios");
            }
        }

        /// <summary>Busca el correo del usuario en la base de datos.</summary>
        /// <param name="usuario">El usuario.</param>
        /// <returns>El correo del usuario/returns>
        /// <exception cref="EntityException">En caso de que ocurra un error en la base de datos</exception>
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

        /// <summary>Busca si el usuario está en la base de datos.</summary>
        /// <param name="usuario">El usuario.</param>
        /// <returns>El resultado de la operacion</returns>
        /// <exception cref="EntityException">En caso de que ocurra un error en la base de datos</exception>
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



    /// <summary>Clase parcial que contiene los métodos para realizar la confirmación del jugador</summary>
    /// <seealso cref="Pacman_Sevices-services.IConfirmationServices" />

    public partial class Services : IConfirmationServices
    {

        /// <summary>Cambia el estado de confirmación de un jugador a Verdadero.</summary>
        /// <param name="jugador">El jugador.</param>
        /// <returns>El resultado de la operacion</returns>
        /// <exception cref="EntityException">En caso de que ocurra un error en la base de datos</exception>
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

        /// <summary>Genera un nuevo código para el jugador</summary>
        /// <param name="jugador">El jugador.</param>
        /// <returns>El resultado de la operacion</returns>
        /// <exception cref="EntityException">En caso de que ocurra un error en la base de datos</exception>
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


        /// <summary>Envía un correo via SMTP</summary>
        /// <param name="jugador">El jugador.</param>
        /// <returns>El resultado de la operacion</returns>
        /// <exception cref="SmtpException">En caso de que no se pueda enviar el correo/exception>
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

    /// <summary>Clase parcial que contiene los métodos para realizar la confirmación del jugador</summary>
    /// <seealso cref="Pacman_Sevices-services.IScoreService" />

    public partial class Services : IScoreService
    {
        /// <summary>obtiene el score de un usuario</summary>
        /// <param name="user">El jugador.</param>
        /// <returns>La puntuación</returns>
        /// <exception cref="EntityException">En caso de que ocurra un error en la base de datos</exception>
        public int GetScore(IScoreService.User user)
        {
            int maxPoint = 0;
            try
            {
                ModelContainer container = new ModelContainer();
                var jugador = container.JugadorSet.Where(x => x.Usuario.Username == user.Nombre).FirstOrDefault();
                maxPoint = jugador.PuntuaciónAlta;
            }
            catch (EntityException)
            {
                maxPoint = 0;
            }

            return maxPoint;
        }

        /// <summary>Registra una nueva puntuación para el usuario</summary>
        /// <param name="user">El jugador.</param>
        /// <returns>El resultado de la operación</returns>
        /// <exception cref="EntityException">En caso de que ocurra un error en la base de datos</exception>
        public bool SetScore(IScoreService.User user, double score)
        {
            bool result = false;
            try
            {
                ModelContainer container = new ModelContainer();
                var jugador = container.JugadorSet.Where(x => x.Usuario.Username == user.Nombre).FirstOrDefault();
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
            }
            catch (EntityException)
            {
                result = false;
            }
 
            return result;
        }
    }



    /// <summary>Clase parcial que contiene los métodos para el chat</summary>
    /// <seealso cref="Pacman_Sevices-services.IChatServices" />
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class Services : IChatService
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;

        /// <summary>Agrega un nuevo jugador a la sala de chat</summary>
        /// <param name="name">El nombre de usuario del jugador.</param>
        /// <returns>El ID del jugador</returns>
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
        /// <summary>Quita al usuario de lista de usuarios</summary>
        /// <param name="id"">El ID del usuario.</param>
        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(": " + user.Name + " disconnected from chat!", 0);
            }
        }
        /// <summary>Registra una nueva puntuación para el usuario</summary>
        /// <param name="message">El mensaje del jugador en el chat.</param>
        /// <param name="id">El id del jugador en el chat.</param>
        /// <returns>El resultado de la operación</returns>
        /// <exception cref="NullReferenceException">En caso de que un jugador tenga un valor nulo en la lista</exception>
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
                catch (NullReferenceException)
                {
                    throw new NullReferenceException("No hay jugador con ID");
                }
            }
        }
    }
}
