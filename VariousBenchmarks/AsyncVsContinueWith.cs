using BenchmarkDotNet.Attributes;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArrayVsDictionaryBenchmark
{
    [MemoryDiagnoser]
    public class AsyncVsContinueWith : IDisposable
    {
        private bool _disposed;
        private readonly Uri _url = new Uri("http://www.matlus.com");
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
            var httpResponseMessage = await _httpClient.GetAsync(_url).ConfigureAwait(false);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        }


        [Benchmark]
        public Task<string> GetHtmlContinueWith()
        {
            return _httpClient.GetAsync(_url).ContinueWith(hrmTask =>
            {
                var httpResponseMessage = hrmTask.Result;
                return httpResponseMessage.Content.ReadAsStringAsync().ContinueWith(strTask =>
                {
                    return strTask.Result;
                });
            }).Unwrap();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _httpClient.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
