using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{

    public APIConfig apiConfig;
    private Stop[] stops;

    private Vehicle[] vehicles;
    private Route[] routes;

    private Trip[] trips;
    private StopTime[] stopTimes;


    void Start()
    {
        StartCoroutine(GetStops());
        StartCoroutine(GetVehicles());
        StartCoroutine(GetRoutes());
        StartCoroutine(GetTrips());
        StartCoroutine(GetStopTimes());
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
            // foreach (Stop stop in stops)
            // {
            //     Debug.Log(stop.ToString());
            // }
        }
    }

    IEnumerator GetVehicles()
    {
        string url = "https://api.tranzy.dev/v1/opendata/vehicles";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("X-API-KEY", apiConfig.apiKey);
        request.SetRequestHeader("X-Agency-Id", "1");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching vehicles: " + request.error);
        }
        else
        {
            string jsonString = "{\"vehicles\":" + request.downloadHandler.text + "}";
            vehicles = JsonUtility.FromJson<VehicleArray>(jsonString).vehicles;
            // foreach (Vehicle vehicle in vehicles)
            // {
            //     Debug.Log(vehicle.ToString());
            // }
        }
    }

    IEnumerator GetRoutes()
    {
        string url = "https://api.tranzy.dev/v1/opendata/routes";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("X-API-KEY", apiConfig.apiKey);
        request.SetRequestHeader("X-Agency-Id", "1");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching routes: " + request.error);
        }
        else
        {
            string jsonString = "{\"routes\":" + request.downloadHandler.text + "}";
            routes = JsonUtility.FromJson<RouteArray>(jsonString).routes;
            // foreach (Route route in routes)
            // {
            //     Debug.Log(route.ToString());
            // }
        }
    }

    IEnumerator GetTrips()
    {
        string url = "https://api.tranzy.dev/v1/opendata/trips";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("X-API-KEY", apiConfig.apiKey);
        request.SetRequestHeader("X-Agency-Id", "1");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching trips: " + request.error);
        }
        else
        {
            string jsonString = "{\"trips\":" + request.downloadHandler.text + "}";
            trips = JsonUtility.FromJson<TripArray>(jsonString).trips;
            // foreach (Trip trip in trips)
            // {
            //     Debug.Log(trip.ToString());
            // }
        }
    }

    IEnumerator GetStopTimes()
    {
        string url = "https://api.tranzy.dev/v1/opendata/stop_times";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("X-API-KEY", apiConfig.apiKey);
        request.SetRequestHeader("X-Agency-Id", "1");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching stop times: " + request.error);
        }
        else
        {
            string jsonString = "{\"stop_times\":" + request.downloadHandler.text + "}";
            stopTimes = JsonUtility.FromJson<StopTimeArray>(jsonString).stop_times;
            // foreach (StopTime stopTime in stopTimes)
            // {
            //     Debug.Log(stopTime.ToString());
            // }
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

    public StopTime[] GetStopTimesForStop(Stop stop)
    {
        List<StopTime> stopTimesLocal = new List<StopTime>();
        foreach (StopTime stopTime in stopTimes)
        {
            if (stopTime.stop_id == stop.stop_id)
            {
                stopTimesLocal.Add(stopTime);
            }
        }
        return stopTimesLocal.ToArray();
    }

    public Vehicle GetVehicleByTripID(string tripID)
    {
        foreach (Vehicle vehicle in vehicles)
        {
            if (vehicle.trip_id == tripID)
            {
                return vehicle;
            }
        }
        return null;
    }

    public Route GetRouteByID(int routeID)
    {
        foreach (Route route in routes)
        {
            if (route.route_id == routeID)
            {
                return route;
            }
        }
        return null;
    }
}

