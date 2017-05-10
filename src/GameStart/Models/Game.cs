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
        public int ApiId { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserName { get; set; }
        public bool Tradeable { get; set; }

        public static JArray GetGames(string requestString)
        {
            var client = new RestClient("https://igdbcom-internet-game-database-v1.p.mashape.com/games/");
            var request = new RestRequest(requestString, Method.GET);
            request.AddHeader("X-Mashape-Key", EnvironmentVariables.ApiKey);
            request.AddHeader("Accept", "application/json");

            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            //Since this api returns an array, just JArray instead.
            JArray result = JsonConvert.DeserializeObject<JArray>(response.Content);


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
