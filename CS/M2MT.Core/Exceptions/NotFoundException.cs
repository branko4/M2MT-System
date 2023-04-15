using System;

namespace M2MT.Shared.Exceptions
{
    public class NotFoundException<SearchedObject> : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
    }
}
