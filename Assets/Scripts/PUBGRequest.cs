using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityPUBGAPI.Network;

public class PUBGRequest : MonoBehaviour
{
    [SerializeField]
    private GameObject dataTextPrefab;
    [SerializeField]
    private GameObject dataContainer;
    [SerializeField]
    private GameObject dataViewPort;
    private readonly string APIKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI3MDQ0NjlhMC1jYTRhLTAxMzgtM2JjOC0yOTY2M2Y3ZDk5YjIiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNTk4NTAxOTc1LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6ImFudG9uaW9hcmN1cmk3In0.W7oU2Xm9yNMX_Aso4YGFOhW3YZGgUleP94SK4silOfQ";
    private readonly string URL = "https://api.pubg.com/tournaments";

    private void CleanTournamentList()
    {
        foreach (Transform item in dataContainer.transform)
        {
            Destroy(item.gameObject);
        }
    }
    public void Refresh()
    {
        CleanTournamentList();
        StartCoroutine(GetInfoCorroutine());
    }
    private IEnumerator GetInfoCorroutine()
    {
        UnityWebRequest webRequest =  UnityWebRequest.Get(URL);
        webRequest.SetRequestHeader("Authorization", APIKey);
        webRequest.SetRequestHeader("accept", "application/vnd.api+json");
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.LogError(webRequest.error);
        }
        else
        {
            PUBGResponse response = JsonConvert.DeserializeObject<PUBGResponse>(webRequest.downloadHandler.text);
            foreach (var data in response.Data)
            {
                {
                    var element = Instantiate(dataTextPrefab, dataContainer.transform);
                    element.transform.localScale = Vector3.one;
                    element.GetComponent<TextMeshProUGUI>().text = data.Id;
                }
                {
                    var element = Instantiate(dataTextPrefab, dataContainer.transform);
                    element.transform.localScale = Vector3.one;
                    element.GetComponent<TextMeshProUGUI>().text = data.Attributes.CreatedAt.ToShortDateString();
                }
            }
        }
    }
}
