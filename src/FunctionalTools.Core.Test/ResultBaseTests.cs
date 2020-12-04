using System;
using NUnit.Framework;

namespace FunctionalTools.Core.Test
{
    /// <summary>
    /// Tests for <see cref="ResultBase"/>.
    /// </summary>
    internal static class ResultBaseTests
    {
        //--------------------------------------------------
        [Test]
        public static void Constructor_ValidatesBehavior()
        {
            Assert.That(() => new TestResult(ResultState.Unknown),
                Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("state"));
        }


        //--------------------------------------------------
        private sealed class TestResult : ResultBase
        {
            public TestResult(ResultState state)
                : base(state, tag: "", Error.None)
            {
            }
        }
    }
}