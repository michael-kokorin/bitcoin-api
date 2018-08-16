using System.Collections.Generic;

using JetBrains.Annotations;

using Newtonsoft.Json.Linq;

namespace BitcoinApi.Business.RequestMethodFormatter
{
    public interface IRequestMethodFormatter
    {
        [NotNull]
        string Method { get; }

        [NotNull]
        JArray Validate([NotNull] Dictionary<string, string> parameters);
    }
}