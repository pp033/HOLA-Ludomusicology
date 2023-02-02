using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonDeserializer
{    
    public List<List<int>> Deserialize(string jsonfile)
    {
       TextAsset json = Resources.Load<TextAsset>(jsonfile);
       return JsonConvert.DeserializeObject<List<List<int>>>(json.text);
    }
}
