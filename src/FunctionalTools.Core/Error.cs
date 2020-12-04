using System;
using JetBrains.Annotations;

namespace FunctionalTools.Core
{
    /// <summary>
    /// Struct containing error information.
    /// </summary>
    public readonly struct Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Error([CanBeNull] string message, [NotNull] Exception exception)
        {
            this._message = message.ToSafeString().Trim();
            this.Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Error([CanBeNull] Exception exception)
        {
            this._message = string.Empty;
            this.Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public Error([CanBeNull] string message)
        {
            this._message = message.ToSafeString().Trim();
            this.Exception = null;
        }

        [NotNull]
        public string Message =>
            string.IsNullOrEmpty(this._message)
                ? this.Exception is null
                    ? string.Empty
                    : this.Exception.Message
                : this._message;

        [CanBeNull] public Exception Exception { get; }

        public static readonly Error None = new Error(message: null);

        [CanBeNull] private readonly string _message;
    }
}