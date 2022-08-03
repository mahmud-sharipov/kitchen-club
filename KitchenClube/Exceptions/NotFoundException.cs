namespace KitchenClube.Exceptions
{
    public class NotFoundException : Exception, IKitchenClubeCustomException
    {
        public NotFoundException(string entityName, Guid id) : this(entityName, id.ToString())
        {
        }

        public NotFoundException(string entityName, string id) : base($"{entityName} with '{id}' does not exists!")
        {
        }

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
