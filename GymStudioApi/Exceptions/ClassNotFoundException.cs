using System;

namespace GymStudioApi.Exceptions
{
    public class ClassNotFoundException : Exception
    {
        public ClassNotFoundException(String message) : base (message)
        {
        }
    }
}