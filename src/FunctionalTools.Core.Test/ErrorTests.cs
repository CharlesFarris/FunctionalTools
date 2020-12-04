using System;
using NUnit.Framework;

namespace FunctionalTools.Core.Test
{
    /// <summary>
    /// Tests for <see cref="Error"/>.
    /// </summary>
    internal static class ErrorTests
    {
        [Test]
        public static void Constructor_ValidatesBehavior()
        {
            // use case: default constructor
            {
                var error = new Error();
                Assert.That(error.Message, Is.Empty);
                Assert.That(error.Exception, Is.Null);
            }
            
            // use case: full constructor
            {
                var exception = new Exception();
                var error = new Error("message", exception);
                Assert.That(error.Message, Is.EqualTo("message"));
                Assert.That(error.Exception, Is.EqualTo(exception));
            }
            
            // use case: message only constructor
            {
                var error = new Error(message:"message");
                Assert.That(error.Message, Is.EqualTo("message"));
                Assert.That(error.Exception, Is.Null);
            }
            
            // use case: exception only constructor
            {
                var exception = new Exception("exception message");
                var error = new Error(exception);
                Assert.That(error.Message, Is.EqualTo("exception message"));
                Assert.That(error.Exception, Is.EqualTo(exception));
            }
        }
        
    }
}