using NUnit.Framework;

namespace FunctionalTools.Core.Test
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/>.
    /// </summary>
    internal static class ResultTests
    {

        [Test]
        public static void Failure_ValidatesBehavior()
        {
            var result = Result<object>.Failure();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.State, Is.EqualTo(ResultState.Failure));
        }

        [Test]
        public static void Success_ValidatesBehavior()
        {
            var result = Result<object>.Success();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.State, Is.EqualTo(ResultState.Success));
        }

    }

}