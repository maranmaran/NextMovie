using System;

namespace Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Guid id, string message, Exception ex = null)
            : base($"Entity not found.\nEntity id: {id}\nMessage: {message}", ex)
        {
        }

        public NotFoundException(Guid id, Exception ex = null)
            : base($"Entity not found.\nEntity id: {id}", ex)
        {
        }
    }
}