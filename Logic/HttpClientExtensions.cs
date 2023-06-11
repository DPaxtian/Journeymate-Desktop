using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public static class HttpClientExtensions
{
    public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, HttpContent content, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
        {
            Content = content
        };

        return httpClient.SendAsync(request, cancellationToken);
    }
}
