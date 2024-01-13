using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vuforia;
using System;

public class ImageRecognitionHandler : MonoBehaviour
{
    public APIManager apiManager;
    public LocationService locationService;
    public GameObject arPanelTarget;
    public TextMeshPro arPanelText;

    public List<GameObject> routesObjects;

    public int callCount = 0;


    private List<Vehicle> vehicles;
    private List<Route> routes;
    private StopTime[] stopTimes;


    private Vector2 previousLocation;

    void Start()
    {
        StartCoroutine(locationService.StartLocationService());
        previousLocation = Vector2.zero;

    }

    void Update()
    {

    }

    public void OnImageRecognized()
    {

        Vector2 currentLocation = locationService.GetLocation();
        //Vector2 currentLocation = new Vector2(47.180870f, 27.572713f);
        //Vector2 currentLocation = new Vector2(47.19052f, 27.55848f);
        // e pt testing doar:
        // if (callCount % 2 == 0)
        // {
        //     currentLocation = new Vector2(47.19052f,
        //         27.55848f);
        // }
        // else
        // {
        //     currentLocation = new Vector2(47.166740f, 27.570924f);
        // }

        callCount++;
        float distance = LocationService.CalculateHaversineDistance(previousLocation, currentLocation);
        Debug.Log("Distance: " + distance);
        if (distance > 0.05) // 5m
        {
            // Update the previousLocation with the current location
            previousLocation = currentLocation;

            Stop nearestStop = apiManager.FindNearestStop(currentLocation);
            stopTimes = apiManager.GetStopTimesForStop(nearestStop);

            vehicles = GetVehicles(stopTimes);
            //Debug.Log("Vehicles: " + vehicles.Count);


            routes = GetRoutes(vehicles);
            //Debug.Log("Routes: " + routes.Count);

            SortVehicles(nearestStop);

            if (nearestStop != null)
            {
                Debug.Log(nearestStop);
                arPanelText.text = nearestStop.stop_name;
                arPanelTarget.SetActive(true); // Show the AR panel
                var numberOfVehicles = vehicles.Count;

                foreach (var obj in routesObjects)
                {
                    obj.SetActive(false);
                }

                for (var i = 0; i < Math.Min(vehicles.Count, 4); i++)
                {
                    var textMeshPro = routesObjects[i].transform.GetChild(1).GetComponent<TextMeshPro>();
                    textMeshPro.text = $"{routes.Find(e => e.route_id == vehicles[i].route_id).route_short_name}";
                    routesObjects[i].SetActive(true);
                }

            }
            else
            {
                Debug.LogError("No nearest stop found");
                arPanelTarget.SetActive(false);
            }
        }
    }

    private List<Vehicle> GetVehicles(StopTime[] stopTimes)
    {
        List<Vehicle> getVehicles = new List<Vehicle>();
        foreach (StopTime stopTime in stopTimes)
        {
            Vehicle vehicle = apiManager.GetVehicleByTripID($"{stopTime.trip_id}");
            if (vehicle != null)
            {
                getVehicles.Add(vehicle);
            }
        }
        return getVehicles;
    }

    private List<Route> GetRoutes(List<Vehicle> vehicles)
    {
        List<Route> routes = new List<Route>();
        foreach (Vehicle vehicle in vehicles)
        {
            Route route = apiManager.GetRouteByID(vehicle.route_id);
            if (route != null)
            {
                routes.Add(route);
            }
        }
        return routes;
    }

    private void SortVehicles(Stop nearestStop)
    {
        vehicles.Sort((x, y) =>
            {
                var temp_dist = Vector2.Distance(new Vector2((float)x.latitude, (float)x.longitude), new Vector2((float)nearestStop.stop_lat, (float)nearestStop.stop_lon));
                var dist = temp_dist.CompareTo(Vector2.Distance(new Vector2((float)y.latitude, (float)y.longitude), new Vector2((float)nearestStop.stop_lat, (float)nearestStop.stop_lon)));
                return dist;
            }
        );
    }

}


