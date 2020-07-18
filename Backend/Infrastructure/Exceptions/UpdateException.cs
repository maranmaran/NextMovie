using System;

namespace Infrastructure.Exceptions
{
    public class UpdateException : Exception
    {
        public UpdateException(Guid id, string message, Exception ex = null)
            : base($"Could not update entity.\nEntity id: {id}\nMessage: {message}", ex)
        {
        }

        public UpdateException(Guid id, Exception ex = null)
            : base($"Could not update entity.\nEntity id: {id}", ex)
        {
        }
    }
}