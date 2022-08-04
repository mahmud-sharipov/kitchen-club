namespace KitchenClube.Exceptions
{
    public class BadRequestException : Exception, IKitchenClubeCustomException
    {
        public BadRequestException(string message) : base(message) { }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}
