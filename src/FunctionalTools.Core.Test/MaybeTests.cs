using System;
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
        [Test]
        public static void From_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case: from null value
                {
                    var maybe = Maybe<object>.From(default(object));
                    Assert.That(maybe.HasNoValue, Is.True);
                    Assert.That(
                        () => maybe.Value,
                        Throws.TypeOf<InvalidOperationException>().With.Property("Message")
                            .EqualTo(Constants.ErrorMessages.MaybeHasNoValue));
                    Assert.That(maybe.HasValue, Is.False);
                }

                // use case: from not-null value
                {
                    var value = new object();
                    var maybe = Maybe<object>.From(value);
                    Assert.That(maybe.HasNoValue, Is.False);
                    Assert.That(maybe.HasValue, Is.True);
                    Assert.That(maybe.Value, Is.EqualTo(value));
                }
            });
        }

        //--------------------------------------------------
        [Test]
        public static void EqualityOperators_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                var hasNoValue = Maybe<object>.From(null);
                Assert.That(hasNoValue.HasNoValue, Is.True);

                var value = new object();
                var hasValue = Maybe<object>.From(value);
                Assert.That(hasValue.HasValue, Is.True);

                // use case: validate IEquatable.Equals
                Assert.That(hasNoValue.Equals(hasNoValue), Is.True);
                Assert.That(hasNoValue.Equals(hasValue), Is.False);
                Assert.That(hasValue.Equals(hasNoValue), Is.False);
                Assert.That(hasValue.Equals(hasValue), Is.True);
                Assert.That(hasValue.Equals(Maybe<object>.From(new object())), Is.False);

                // use case: validate operator== (Maybe<T> vs T)
                Assert.That(hasNoValue == default(object), Is.False);
                Assert.That(hasNoValue == value, Is.False);
                Assert.That(hasValue == default(object), Is.False);
                Assert.That(hasValue == value, Is.True);
                Assert.That(hasValue == new object(), Is.False);

                // use case: validate operator!= (Maybe<T> vs T)
                Assert.That(hasNoValue != default(object), Is.True);
                Assert.That(hasNoValue != value, Is.True);
                Assert.That(hasValue != default(object), Is.True);
                Assert.That(hasValue != value, Is.False);
                Assert.That(hasValue != new object(), Is.True);

                // use case: validate operator== (Maybe<T> vs Maybe<T>)
                Assert.That(hasNoValue == Maybe<object>.From(default), Is.True);
                Assert.That(hasNoValue == hasValue, Is.False);
                Assert.That(hasValue == hasNoValue, Is.False);
                Assert.That(hasValue == Maybe<object>.From(value), Is.True);
                Assert.That(hasValue == Maybe<object>.From(new object()), Is.False);

                // use case: validate operator== (Maybe<T> vs Maybe<T>)
                Assert.That(hasNoValue != Maybe<object>.From(default), Is.False);
                Assert.That(hasNoValue != hasValue, Is.True);
                Assert.That(hasValue != hasNoValue, Is.True);
                Assert.That(hasValue != Maybe<object>.From(value), Is.False);
                Assert.That(hasValue != Maybe<object>.From(new object()), Is.True);
            });
        }

        //--------------------------------------------------
        [Test]
        public static void GetHasCode_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case: has no value
                {
                    var maybe = Maybe<object>.From(default);
                    Assert.That(maybe.GetHashCode(), Is.EqualTo(0));
                }

                // use case: has value
                {
                    var value = new object();
                    var maybe = Maybe<object>.From(value);
                    Assert.That(maybe.GetHashCode(), Is.EqualTo(value.GetHashCode()));
                }
            });
        }

        //--------------------------------------------------
        [Test]
        public static void ToString_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case: has no value
                {
                    var maybe = Maybe<object>.From(default);
                    Assert.That(maybe.ToString(), Is.EqualTo("No Value"));
                }

                // use case: has value
                {
                    var value = new object();
                    var maybe = Maybe<object>.From(value);
                    Assert.That(maybe.ToString(), Is.EqualTo(value.ToString()));
                }
            });
        }

        //--------------------------------------------------
        [Test]
        public static void Unwrap_ValidatesBehavior()
        {
            Assert.Multiple(() =>
            {
                // use case:  has no value and no default value
                {
                    var maybe = Maybe<object>.From(default);
                    Assert.That(maybe.Unwrap(), Is.EqualTo(default));
                }

                // use case:  has no value and default value specified
                {
                    var maybe = Maybe<object>.From(default);
                    var defaultValue = new object();
                    Assert.That(maybe.Unwrap(defaultValue), Is.EqualTo(defaultValue));
                }

                // use case:  has value
                {
                    var value = new object();
                    var maybe = Maybe<object>.From(value);
                    Assert.That(maybe.Unwrap(), Is.EqualTo(value));
                }
            });
        }
    }
}