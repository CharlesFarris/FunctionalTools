using NUnit.Framework;

namespace FunctionalTools.Core.Test
{
    /// <summary>
    /// Tests for <see cref="ResultExtensions"/>.
    /// </summary>
    internal static class ResultExtensionsTests
    {
        //--------------------------------------------------
        [Test]
        public static void OnFailure_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case: null result parameter
                {
                    Assert.That(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        () => default(Result<object>).OnFailure(result => { }),
                        Throws.ArgumentNullException.With.Property("ParamName").EqualTo("result"));
                }

                // use case: null action parameter
                {
                    Assert.That(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        () => Result<object>.Failure().OnFailure(action: null),
                        Throws.ArgumentNullException.With.Property("ParamName").EqualTo("action"));
                }

                // use case:  failure action called if failure
                {
                    var failureActionCalled = false;
                    var _ = Result<object>.Failure()
                        .OnFailure(result => { failureActionCalled = true; });
                    Assert.That(failureActionCalled, Is.True);
                }

                // use case:  failure action not called if success
                {
                    var failureActionCalled = false;
                    var _ = Result<object>.Success(new object())
                        .OnFailure(result => { failureActionCalled = true; });
                    Assert.That(failureActionCalled, Is.False);
                }
            });
        }

        //--------------------------------------------------
        [Test]
        public static void ToResult_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case: default tag value
                {
                    var value = new object();
                    var result = value.ToResult();
                    Assert.That(result.State, Is.EqualTo(ResultState.Success));
                    Assert.That(result.Unwrap(), Is.EqualTo(value));
                    Assert.That(result.Tag, Is.EqualTo(string.Empty));
                }

                // use case: custom tag value
                {
                    var value = new object();
                    var result = value.ToResult(tag: "tag");
                    Assert.That(result.State, Is.EqualTo(ResultState.Success));
                    Assert.That(result.Unwrap(), Is.EqualTo(value));
                    Assert.That(result.Tag, Is.EqualTo("tag"));
                }
            });
        }

        //--------------------------------------------------
        [Test]
        public static void ToResultNotNull_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case: fail on null value default tag
                {
                    var result = default(object).ToResultNotNull();
                    Assert.That(result.State, Is.EqualTo(ResultState.Failure));
                    Assert.That(result.Tag, Is.EqualTo(string.Empty));
                }

                // use case: fail on null value custom tag
                {
                    var result = default(object).ToResultNotNull(tag: "tag");
                    Assert.That(result.State, Is.EqualTo(ResultState.Failure));
                    Assert.That(result.Tag, Is.EqualTo("tag"));
                }

                // use case: success default tag
                {
                    var value = new object();
                    var result = value.ToResultNotNull();
                    Assert.That(result.State, Is.EqualTo(ResultState.Success));
                    Assert.That(result.Unwrap(), Is.EqualTo(value));
                    Assert.That(result.Tag, Is.EqualTo(string.Empty));
                }

                // use case: success custom tag
                {
                    var value = new object();
                    var result = value.ToResultNotNull(tag: "tag");
                    Assert.That(result.State, Is.EqualTo(ResultState.Success));
                    Assert.That(result.Unwrap(), Is.EqualTo(value));
                    Assert.That(result.Tag, Is.EqualTo("tag"));
                }
            });
        }
    }
}