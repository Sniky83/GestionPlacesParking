namespace GestionPlacesParking.Core.Application.Exceptions
{
    internal class MultipleReservationException : Exception
    {
        public MultipleReservationException()
        {
        }

        public MultipleReservationException(string message)
            : base(message)
        {
        }

        public MultipleReservationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
