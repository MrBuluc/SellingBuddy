using System.Net.Http.Json;
using WebApp.Application.Services.DTOs;
using WebApp.Application.Exceptions;

namespace WebApp.Extensions
{
    public static class HttpClientExtension
    {
        public async static Task<MyHttpResponseMessage> PostGetResponseAsync<TResult, TValue>(this HttpClient httpClient, string url, TValue value)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(url, value);
            return new()
            {
                IsSuccessStatusCode = httpResponseMessage.IsSuccessStatusCode,
                Content = httpResponseMessage.IsSuccessStatusCode ? await httpResponseMessage.Content.ReadFromJsonAsync<TResult>() : await httpResponseMessage.Content.ReadFromJsonAsync<ExceptionModel>()
            };
        }

        public async static Task PostAsync<TValue>(this HttpClient httpClient, string url, TValue value)
        {
            await httpClient.PostAsJsonAsync(url, value);
        }

        public async static Task<T?> GetResponseAsync<T>(this HttpClient httpClient, string url) => await httpClient.GetFromJsonAsync<T>(url);
    }
}
