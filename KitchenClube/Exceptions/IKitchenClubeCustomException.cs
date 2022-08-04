namespace KitchenClube.Exceptions;

public interface IKitchenClubeCustomException
{
    public string Message { get; }

    public System.Net.HttpStatusCode StatusCode { get; }
}
