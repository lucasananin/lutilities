using System;
using UnityEngine;

namespace lutilities
{
    public class StringMethods 
    {
        /// <summary>
        /// Checks whether two strings are equal, ignoring case.
        /// </summary>
        public static bool AreStringsEqual(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
