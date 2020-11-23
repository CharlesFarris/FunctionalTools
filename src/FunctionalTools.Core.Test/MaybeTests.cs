using NUnit.Framework;

namespace FunctionalTools.Core.Test
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/>.
    /// </summary>
    internal static class MaybeTests
    {
        //--------------------------------------------------
        [Test]
        public static void Empty_ValidatesBehavior()
        {
            Assert.That(Maybe<object>.Empty.HasNoValue, Is.True);
        }
        
        //--------------------------------------------------
    
    }
}