using System;
using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    /// <summary>
    /// Extension methods for manipulating strings.
    /// </summary>
    public static class StringExtensions
    {
        //--------------------------------------------------
        [NotNull]
        public static string ToSafeString([CanBeNull] this string value, [NotNull] string defaultValue = "")
        {
            if (defaultValue is null)
            {
                throw new ArgumentNullException(nameof(defaultValue));
            }

            return value ?? defaultValue;
        }
    }
}