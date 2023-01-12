#region Using

using System;
using BERlogic.CallCenter.Configuration;

// ReSharper disable MemberCanBePrivate.Global

#endregion

namespace BERlogic.CallCenter.Extensions
{
    /// <summary>
    /// Contains extension methods that help with the flow of the application.
    /// </summary>
    public static class SmartExtensions
    {
        /// <summary>Specified the default comparison mode when comparing string values.</summary>
        public static readonly StringComparison DefaultComparison = StringComparison.OrdinalIgnoreCase;

        public static bool IsEnvironment(this string hostingEnvironment, string environmentName) => string.Equals(environmentName, hostingEnvironment, DefaultComparison);

        public static bool IsDemo(this string hostingEnvironment) => hostingEnvironment.IsEnvironment(EnvironmentName.Demo);
    }
}
