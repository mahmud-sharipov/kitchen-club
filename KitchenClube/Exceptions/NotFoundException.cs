using System.Net;

namespace KitchenClube.Exceptions
{
    public class NotFoundException : Exception, IKitchenClubeCustomException
    {
        //TODO remove this constructor
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string entityName, Guid id) : this(entityName, id.ToString())
        {
        }

        public NotFoundException(string entityName, string id) : base($"{entityName} with '{id}' does not exists!")
        {
        }

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
