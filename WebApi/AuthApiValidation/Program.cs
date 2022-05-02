using System;
using AuthApiValidation.Models;
using FluentValidation.Results;

namespace AuthApiValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            Foo foo = new Foo();
            foo.Validate();
        }
    }
}
