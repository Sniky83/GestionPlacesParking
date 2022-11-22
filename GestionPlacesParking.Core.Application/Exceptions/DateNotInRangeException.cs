namespace GestionPlacesParking.Core.Application.Exceptions
{
    internal class DateNotInRangeException : Exception
    {
        public DateNotInRangeException()
        {
        }

        public DateNotInRangeException(string message)
            : base(message)
        {
        }

        public DateNotInRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
