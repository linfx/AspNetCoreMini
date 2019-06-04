using System;
using System.Collections.Specialized;
using System.IO;

namespace AspNetCoreMini.Http.Features
{
    public interface IHttpRequestFeature
    {
        Uri Url { get; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
    }
}
