using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }

        private Email() { }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email is required");

            Value = value.ToLowerInvariant();
        }

        public override string ToString() => Value;
    }
}
