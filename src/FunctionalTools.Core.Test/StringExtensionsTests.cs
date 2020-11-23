using JetBrains.Annotations;
using NUnit.Framework;

namespace FunctionalTools.Core.Test
{
    /// <summary>
    /// Tests for <see cref="StringExtensions"/>.
    /// </summary>
    internal static class StringExtensionsTests
    {
        //--------------------------------------------------
        [TestCase(null, ExpectedResult = "")]
        [TestCase("abc", ExpectedResult = "abc")]
        [NotNull]
        public static string ToSafeString_ValidatesBehavior([CanBeNull] string value)
        {
            return value.ToSafeString();
        }

        //--------------------------------------------------
        [Test]
        public static void ToSafeString_ValidateNullDefaultValueException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.That(
                () => default(string).ToSafeString(defaultValue: null),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("defaultValue"));
        }

        //--------------------------------------------------
        [TestCase(null, "defaultValue", ExpectedResult = "defaultValue")]
        [TestCase("abc", "defaultValue", ExpectedResult = "abc")]
        [NotNull]
        public static string ToSafeString_ValidatesDefaultValueBehavior(
            [CanBeNull] string value,
            [NotNull] string defaultValue)
        {
            return value.ToSafeString(defaultValue);
        }
    }
}