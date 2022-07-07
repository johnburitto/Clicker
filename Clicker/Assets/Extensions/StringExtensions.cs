using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Extensions
{
    public static class StringExtensions
    {
        public static string Repeat(this string str, int numberOfRepeat)
        {
            return string.Concat(Enumerable.Repeat(str, numberOfRepeat));
        }
    }
}
