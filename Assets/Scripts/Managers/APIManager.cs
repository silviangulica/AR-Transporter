using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{

    public APIConfig apiConfig;
    private Stop[] stops;

    /*private Vehicle[] vehicles;
    private Route[] routes;*/


    void Start()
    {
        StartCoroutine(GetStops());
    }

    IEnumerator GetStops()
    {
        string url = "https://api.tranzy.dev/v1/opendata/stops";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("X-API-KEY", apiConfig.apiKey);
        request.SetRequestHeader("X-Agency-Id", "1");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching stops: " + request.error);
        }
        else
        {
            string jsonString = "{\"stops\":" + request.downloadHandler.text + "}";
            stops = JsonUtility.FromJson<StopArray>(jsonString).stops;
            foreach (Stop stop in stops)
            {
                Debug.Log(stop.ToString());
            }
        }
    }

    public Stop FindNearestStop(Vector2 location)
    {
        Stop nearestStop = null;
        float nearestDistance = float.MaxValue;
        foreach (Stop stop in stops)
        {
            float distance = Vector2.Distance(location, new Vector2((float)stop.stop_lat, (float)stop.stop_lon));
            if (distance < nearestDistance)
            {
                nearestStop = stop;
                nearestDistance = distance;
            }
        }
        return nearestStop;
    }
}
    
