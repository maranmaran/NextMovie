using System;

namespace Infrastructure.Exceptions
{
    public class NotFoundException<T> : Exception where T : struct
    {
        public NotFoundException(T id, string message, Exception ex = null)
            : base($"Entity not found.\nEntity id: {id}\nMessage: {message}", ex)
        {
        }

        public NotFoundException(T id, Exception ex = null)
            : base($"Entity not found.\nEntity id: {id}", ex)
        {
        }
    }
}