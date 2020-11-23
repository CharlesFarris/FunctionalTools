using NUnit.Framework;

namespace FunctionalTools.Core.Test
{
    /// <summary>
    /// Tests for <see cref="ResultExtensions"/>.
    /// </summary>
    internal static class ResultExtensionsTests
    {

        [Test]
        public static void OnFailure_ValidatesBehavior()
        {
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
        }

    }
    
}