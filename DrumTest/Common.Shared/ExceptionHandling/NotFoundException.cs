using System.Net;

namespace Common.Shared.ExceptionHandling
{
    public class NotFoundException<T> : HttpException
    {
        public NotFoundException() : base($"{typeof(T).Name} not found.", HttpStatusCode.NotFound) { }

        public NotFoundException(string id) : base($"{typeof(T).Name} with id {id} not found.", HttpStatusCode.NotFound) { }

        public NotFoundException(int id) : this(id.ToString()) { }

    }
}
