using System.Net.Http.Json;

namespace WebApiGateway.Infrastructure.Extensions
{
    public static class HttpClientExtension
    {
        public async static Task<TResult?> PostGetResponseAsync<TResult, TValue>(this HttpClient client, string url, TValue value)
        {
            HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync(url, value);

            return httpResponseMessage.IsSuccessStatusCode ? await httpResponseMessage.Content.ReadFromJsonAsync<TResult>() : default;
        }

        public async static Task PostAsync<TValue>(this HttpClient client, string url, TValue value)
        {
            await client.PostAsJsonAsync(url, value);
        }

        public async static Task<T?> GetResponseAsync<T>(this HttpClient client, string url) => await client.GetFromJsonAsync<T>(url);
    }
}
