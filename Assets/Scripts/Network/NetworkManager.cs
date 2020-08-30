using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using System.Collections;

namespace UnityPUBGAPI.Network
{
    #region DeserializeClasses
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attributes
    {
        [JsonProperty("createdAt")]
        public DateTime CreatedAt;
    }

    public class Datum
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("attributes")]
        public Attributes Attributes;
    }

    public class Links
    {
        [JsonProperty("self")]
        public string Self;
    }

    public class Meta
    {
    }

    public class PUBGResponse
    {
        [JsonProperty("data")]
        public List<Datum> Data;

        [JsonProperty("links")]
        public Links Links;

        [JsonProperty("meta")]
        public Meta Meta;
    }



    #endregion
    public class NetworkManager
    {
        public PUBGResponse TournamentsList { get; private set; }
        private static NetworkManager _instance;
        public static NetworkManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NetworkManager();
                }
                return _instance;
            }
        }
        private float progress;

        #region APIKey
        private readonly string APIKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI3MDQ0NjlhMC1jYTRhLTAxMzgtM2JjOC0yOTY2M2Y3ZDk5YjIiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNTk4NTAxOTc1LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6ImFudG9uaW9hcmN1cmk3In0.W7oU2Xm9yNMX_Aso4YGFOhW3YZGgUleP94SK4silOfQ";
        private readonly string URL = "https://api.pubg.com/tournaments";
        #endregion
        public IEnumerator GetTournamentsInfo()
        {
            TournamentsList = new PUBGResponse();
            /*Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", APIKey);
            headers.Add("accept", "application/vnd.api+json");
            tournamentStream = ObservableWWW.Get(URL, headers);
            tournamentStream.Subscribe(p =>
            {
                TournamentsList = JsonConvert.DeserializeObject<Tournaments>(p);
                foreach (var item in TournamentsList.data)
                {
                    Debug.Log(item.Id);
                }
            });*/

            UnityWebRequest webRequest = new UnityWebRequest(URL);
            webRequest.SetRequestHeader("Authorization", APIKey);
            webRequest.SetRequestHeader("accept", "application/vnd.api+json");
            var asyncOperation =  webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                progress = asyncOperation.progress;
                yield return null;
            }

            if (!string.IsNullOrEmpty(webRequest.error))
            {
                Debug.Log(progress);
                Debug.LogError(webRequest.error);
            }


            TournamentsList = JsonConvert.DeserializeObject<PUBGResponse>(webRequest.downloadHandler.text);
            foreach (var item in TournamentsList.Data)
            {
                Debug.Log(item.Id);
            }

        }
    }
}
