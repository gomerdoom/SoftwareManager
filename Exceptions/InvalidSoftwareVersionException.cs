using System; 

namespace Versions.Exceptions
{
    public class InvalidSoftwareVersionException : Exception
    {
        public InvalidSoftwareVersionException() { }

        public InvalidSoftwareVersionException(string message) 
            : base(message) { }

        public InvalidSoftwareVersionException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}