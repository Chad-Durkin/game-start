using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NuGet.Protocol.Core.v3;

namespace GameStart.Models
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [JsonProperty("id")]
        public int ApiId { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public static JArray GetGames()
        {
            var client = new RestClient("https://igdbcom-internet-game-database-v1.p.mashape.com/games/");
            var request = new RestRequest("?fields=name&limit=10&offset=0&order=release_dates.date%3Adesc&search=zelda", Method.GET);
            request.AddHeader("X-Mashape-Key", EnvironmentVariables.ApiKey);
            request.AddHeader("Accept", "application/json");

            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JArray result = JsonConvert.DeserializeObject<JArray>(response.Content);
            //var result = JsonConvert.DeserializeObject<List<Game>>(jsonResponse["id"].ToString());

            return result;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
