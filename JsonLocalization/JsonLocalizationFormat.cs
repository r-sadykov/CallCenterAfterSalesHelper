using System;
using System.Collections.Generic;

namespace JsonLocalization
{
    public class JsonLocalizationFormat : IEquatable<JsonLocalizationFormat>
    {
        public string Key { get; set; }
        public Dictionary<string, string> Value = new Dictionary<string, string>();

        #region Implementation of IEquatable<JsonLocalizationFormat>

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(JsonLocalizationFormat other)
        {
            return Key == other.Key;
        }

        public override int GetHashCode()
        {
            return Key == null ? 0 : Key.GetHashCode();
        }

        #endregion
    }
}
