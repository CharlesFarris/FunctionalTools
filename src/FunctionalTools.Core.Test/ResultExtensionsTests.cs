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
                    var _ = Result<object>.Success()
                        .OnFailure(result => { failureActionCalled = true; });
                    Assert.That(failureActionCalled, Is.False);
                }
            });
        }
    }
}