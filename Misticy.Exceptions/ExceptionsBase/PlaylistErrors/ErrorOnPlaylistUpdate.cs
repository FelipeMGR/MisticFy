using MisticFy.Exceptions.ExceptionsBase;

namespace MisticFy.Exceptions.ExceptionsBase.PlaylistErrors
{
    public class ErrorOnPlaylistUpdate(List<string> errorMessages) : MisticFyException
    {
        public List<string> ErrorMessages { get; } = errorMessages;
    }
}
