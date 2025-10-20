using MisticFy.Exceptions.ExceptionsBase;

namespace MisticFy.Exceptions.ExceptionsBase.PlaylistErrors
{
    public class ErrorOnPlaylistCreation(List<string> errorMessages) : MisticFyException
    {
        public List<string> ErrorMessages { get; } = errorMessages;

    }
}
