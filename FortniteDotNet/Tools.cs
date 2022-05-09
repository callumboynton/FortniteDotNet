using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FortniteDotNet.Models.Other;
using FortniteDotNet.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FortniteDotNet
{
    public static class Tools
    {
        public static IServiceCollection AddFortniteDotNet(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IEventsService, EventsService>();
            services.AddSingleton<IFortniteService, FortniteService>();
            services.AddSingleton<IFriendsService, FriendsService>();
            services.AddSingleton<IPartyService, PartyService>();

            return services;
        }
        
        internal static async Task<T> Handle<T>(this HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));
            
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(responseString);

            try
            {
                var exception = JsonConvert.DeserializeObject<EpicException>(responseString);
                exception.RawException = responseString;
                throw exception;
            }
            catch (JsonException jsonEx)
            {
                throw jsonEx;
            }
        }

        internal static async Task Handle(this HttpResponseMessage response)
            => await response.Handle<object>();

        internal static string ToBase64(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return Convert.ToBase64String(Encoding.Default.GetBytes(value));
        }

        internal static StringContent ToJson(this object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        }
        
        internal static async Task<Cosmetic> GetCosmeticByName(string name, string type)
        {
            
            
            var query = HttpUtility.ParseQueryString(string.Empty);
            query.Add("lang", "en");
            query.Add("searchLang", "en");
            query.Add("matchMethod", "starts");
            query.Add("name", name);
            query.Add("backendType", type);

            using var client = new HttpClient();
            var response = await client.GetAsync($"https://fortnite-api.com/v2/cosmetics/br/search?{query}").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<FortniteAPIResponse<Cosmetic>>(responseString).Data;
            
            try
            {
                var error = JsonConvert.DeserializeObject<FortniteAPIResponse<Cosmetic>>(responseString).Error;
                throw new Exception(error);
            }
            catch (JsonException jsonEx)
            {
                throw jsonEx;
            }
        }
    }
}