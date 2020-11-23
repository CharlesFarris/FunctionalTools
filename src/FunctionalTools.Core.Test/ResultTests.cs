using NUnit.Framework;

namespace FunctionalTools.Core.Test
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/>.
    /// </summary>
    internal static class ResultTests
    {
        //--------------------------------------------------
        [Test]
        public static void Failure_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case: default tag
                {
                    var result = Result<object>.Failure();
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.State, Is.EqualTo(ResultState.Failure));
                    Assert.That(result.Tag, Is.EqualTo(string.Empty));
                }

                // use case: custom tag
                {
                    var result = Result<object>.Failure(tag: "tag");
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.State, Is.EqualTo(ResultState.Failure));
                    Assert.That(result.Tag, Is.EqualTo("tag"));
                }
            });
        }

        //--------------------------------------------------
        [Test]
        public static void Success_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case: default tag
                {
                    var result = Result<object>.Success();
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.State, Is.EqualTo(ResultState.Success));
                    Assert.That(result.Tag, Is.EqualTo(string.Empty));
                }

                // use case: custom tag
                {
                    var result = Result<object>.Success(tag: "tag");
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.State, Is.EqualTo(ResultState.Success));
                    Assert.That(result.Tag, Is.EqualTo("tag"));
                }
            });
        }
    }
}