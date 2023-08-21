using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MockWebFramework.Http
{
    internal class HttpQuery
    {
        private static Regex _queryRegex = new Regex("([-._~\\w\\d]+)=([-._~\\w\\d]+)", RegexOptions.Compiled);



        public Dictionary<string, string> Parameters { get; } = new();

        public HttpQuery(string query)
        {
            var matches = _queryRegex.Matches(query);

            foreach (Match match in matches)
            {
                Parameters.TryAdd(match.Groups[1].Value, match.Groups[2].Value);
            }
        }
    }
}
