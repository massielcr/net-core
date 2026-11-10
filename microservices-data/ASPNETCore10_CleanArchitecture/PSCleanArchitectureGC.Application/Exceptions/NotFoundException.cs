using System;
using System.Collections.Generic;
using System.Text;

namespace PSCleanArchitectureGC.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) is not found") { }
    }
}
