using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Enums.Common;
using FortniteDotNet.Models.Common;

namespace FortniteDotNet.Util
{
    internal static class RequestHelper
    {
        internal static async Task<T> GetDataAsync<T>(this WebClient client, string address)
        {
            try
            {
                var response = await client.DownloadStringTaskAsync(address).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (WebException ex)
            {
                using var reader = new StreamReader(ex.Response.GetResponseStream());
                var body = await reader.ReadToEndAsync().ConfigureAwait(false);
                var exception = JsonConvert.DeserializeObject<EpicException>(body);
                throw exception;
            }
        }
        
        internal static async Task<T> UploadDataAsync<T>(this WebClient client, string address, string data, Method method)
        {
            try
            {
                var response = await client.UploadStringTaskAsync(address, method.ToString(), data).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (WebException ex)
            {
                using var reader = new StreamReader(ex.Response.GetResponseStream());
                var body = await reader.ReadToEndAsync().ConfigureAwait(false);
                var exception = JsonConvert.DeserializeObject<EpicException>(body);
                throw exception;
            }
        }

        internal static async Task<T> PostDataAsync<T>(this WebClient client, string address, string data)
            => await UploadDataAsync<T>(client, address, data, Method.POST);

        internal static async Task<T> PutDataAsync<T>(this WebClient client, string address, string data)
            => await UploadDataAsync<T>(client, address, data, Method.PUT);

        internal static async Task DeleteDataAsync(this WebClient client, string address)
            => await UploadDataAsync<object>(client, address, string.Empty, Method.DELETE);
    }
}