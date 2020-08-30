using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
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
}
