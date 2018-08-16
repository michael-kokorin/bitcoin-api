using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using BitcoinApi.Business.RequestMethodFormatter;
using BitcoinApi.Shared;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BitcoinApi.Business
{
    internal sealed class RequestExecutor: IRequestExecutor
    {
        private readonly IConfigurationProvider _configurationProvider;

        private readonly IEnumerable<IRequestMethodFormatter> _formatters;

        public RequestExecutor(IConfigurationProvider configurationProvider, IEnumerable<IRequestMethodFormatter> formatters)
        {
            _configurationProvider = configurationProvider;
            _formatters = formatters;
        }

        public string Execute(string method, Dictionary<string, string> parameters)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(_configurationProvider.GetValue("bitcoin:rpcurl"));
            var userName = _configurationProvider.GetValue("bitcoin:rpcusername");
            var password = _configurationProvider.GetValue("bitcoin:rpcpassword");
            webRequest.Credentials = new NetworkCredential(userName, password);
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            var entity = new JObject {new JProperty("jsonrpc", "1.0"), new JProperty("id", "1"), new JProperty("method", method)};

            var formatter = _formatters.FirstOrDefault(_ => _.Method == method);
            if(formatter == null)
            {
                throw new InvalidOperationException("Method is unknown");
            }

            var parametersArray = formatter.Validate(parameters);
            entity.Add("params", parametersArray);

            var content = JsonConvert.SerializeObject(entity);
            var contentData = Encoding.UTF8.GetBytes(content);
            webRequest.ContentLength = contentData.Length;
            try
            {
                using(var dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(contentData, 0, contentData.Length);
                    dataStream.Dispose();
                }
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException(
                    $"There was a problem sending the request to the wallet. {exception.Message} {exception.GetType().FullName}",
                    exception);
            }

            try
            {
                string json;

                using(var webResponse = webRequest.GetResponse())
                {
                    using(var stream = webResponse.GetResponseStream())
                    {
                        using(var reader = new StreamReader(stream))
                        {
                            var result = reader.ReadToEnd();
                            reader.Dispose();
                            json = result;
                        }
                    }
                }

                return json;
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException(
                    $"There was a problem sending the request to the wallet. {exception.Message} {exception.GetType().FullName}",
                    exception);
            }
        }
    }
}