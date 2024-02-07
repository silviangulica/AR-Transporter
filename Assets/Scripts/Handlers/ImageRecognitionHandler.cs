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

    private const float AverageSpeedKmH = 30.0f;

    void Start()
    {
        StartCoroutine(locationService.StartLocationService());
        previousLocation = Vector2.zero;
        apiManager = APIManager.Instance;
    }

    void Update()
    {

    }

    public void OnImageRecognized()
    {

        // Statia de la Universitate
        Vector2 currentLocation = new Vector2(47.19052f, 27.55848f);

        //Vector2 currentLocation = locationService.GetLocation();
        // Vector2 currentLocation = new Vector2(47.180870f, 27.572713f);
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

            vehicles = apiManager.GetVehicles(stopTimes);
            //Debug.Log("Vehicles: " + vehicles.Count);

            routes = apiManager.GetRoutes(vehicles);
            //Debug.Log("Routes: " + routes.Count);

            apiManager.SortVehicles(vehicles, nearestStop);
            // Debug.Log("STAAAAART");
            // foreach (var vehicle in apiManager.lastSortedVehicles)
            // {
            //     Debug.Log(vehicle);
            // }
            // Debug.Log("END");
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
                    float distanceToStop = LocationService.CalculateHaversineDistance(new Vector2((float)vehicles[i].latitude, (float)vehicles[i].longitude), new Vector2((float)nearestStop.stop_lat, (float)nearestStop.stop_lon));
                    var textMeshPro = routesObjects[i].transform.GetChild(1).GetComponent<TextMeshPro>();
                    var minutesUntilArrival = routesObjects[i].transform.GetChild(2).GetComponent<TextMeshPro>();
                    var searchedRoute = routes.Find(e => e.route_id == vehicles[i].route_id);

                    float timeToArrival = distanceToStop / AverageSpeedKmH * 60;

                    if ((int)searchedRoute.route_type == (int)Vehicle.VehicleType.Tram)
                    {
                        textMeshPro.text = "Tram ";
                    }
                    else
                    {
                        textMeshPro.text = "Bus ";
                    }
                    textMeshPro.text += $"{searchedRoute.route_short_name}";

                    minutesUntilArrival.text = $"{(int)timeToArrival} min";



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
}


