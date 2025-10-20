namespace MisticFy.Exceptions.ExceptionsBase.ArtistErrors
{
    public class ErrorOnArtistSearch(List<string> errorMessages) : MisticFyException
    {
        public List<string> ErrorMessages { get; set; } = errorMessages;
    }
}
