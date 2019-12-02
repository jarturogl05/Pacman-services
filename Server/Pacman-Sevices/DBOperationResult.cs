
namespace Pacman_Sevices
{
    public class DBOperationResult
    {
        public enum AddResult
        {
            Success,
            NullObject,
            UnknowFail,
            ExistingRecord,
            WrongCredentials,
            ConfirmationIsFalse
        }
    }
}
