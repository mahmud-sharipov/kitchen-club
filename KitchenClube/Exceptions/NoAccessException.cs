namespace KitchenClube.Exceptions;

public class NoAccessException : Exception, IKitchenClubeCustomException
{
    public NoAccessException(string message) : base(message) { }

    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
}
