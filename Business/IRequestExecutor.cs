using System.Collections.Generic;

using JetBrains.Annotations;

namespace BitcoinApi.Business
{
    public interface IRequestExecutor
    {
        [NotNull]
        string Execute([NotNull] string method, [NotNull] Dictionary<string, string> parameters);
    }
}