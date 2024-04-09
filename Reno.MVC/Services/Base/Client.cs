using System.Net.Http;

namespace Reno.MVC.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }

    public partial class Client : IClient
    {
        public HttpClient HttpClient { get => _httpClient; }
    }
}
