using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.Extensions {
    public static class StringExtensions {
        public static bool StringEquals(this string source, string other) {
            return string.Equals(source, other, StringComparison.OrdinalIgnoreCase);
        }
        public static bool Equals(string source, string other) {
            return string.Equals(source, other, StringComparison.OrdinalIgnoreCase);
        }
    }
}
