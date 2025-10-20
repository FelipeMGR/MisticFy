using MisticFy.Exceptions.ExceptionsBase;

namespace MisticFy.Exceptions.ExceptionsBase.PlaylistErrors
{
    public class ErrorOnPlaylistSearch(List<string> errorMessages) : MisticFyException
    {
        public List<string> ErrorMessages { get; set; } = errorMessages;
    }
}
