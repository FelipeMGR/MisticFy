namespace MisticFy.Exceptions.ExceptionsBase.AlbumErrors
{
    public class ErrorOnAlbumSearch(List<string> errorMessages) : MisticFyException
    {
        public List<string> ErrorMessages { get; set; } = errorMessages;
    }
}
