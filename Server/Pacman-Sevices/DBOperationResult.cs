
namespace Pacman_Sevices
{
    /// <summary>Clase que tiene como funcionalidad validar el regreso de las peticiones en la base de datos</summary>
    public class DBOperationResult
    {
        public enum AddResult
        {
            Success,
            NullObject,
            UnknowFail,
            ExistingRecord,
            WrongCredentials,
            ConfirmationIsFalse,
            SQLError
        }
    }
}
