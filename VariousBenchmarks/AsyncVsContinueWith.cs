using BenchmarkDotNet.Attributes;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser]
    public class AsyncVsContinueWith
    {
        const string url = "http://www.matlus.com";
        private HttpClient _httpClient = new HttpClient();

        [GlobalSetup]
        public void Initialize()
        {
            _httpClient = new HttpClient();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }

        [Benchmark]
        public async Task<string> GetHtmlAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync(url);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }


        [Benchmark]
        public Task<string> GetHtmlContinueWith()
        {
            return _httpClient.GetAsync(url).ContinueWith(hrmTask =>
            {
                var httpResponseMessage = hrmTask.Result;
                return httpResponseMessage.Content.ReadAsStringAsync().ContinueWith(strTask =>
                {
                    return strTask.Result;
                });
            }).Unwrap();
        }
    }
}
