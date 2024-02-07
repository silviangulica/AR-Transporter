using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class RouteInfoPanel : MonoBehaviour
{
    public GameObject bussIMG;
    public GameObject tramIMG;
    public GameObject mainPanel;
    private APIManager _apiManager;
    private GameObject numeStatie;

    private Vehicle currentVehicle;

    private GameObject bycicle;
    private GameObject disabilities;
    void Start()
    {
        _apiManager = APIManager.Instance;
        currentVehicle = _apiManager.currentVehicle;
        numeStatie = GameObject.Find("numestatie");

        // Find if there are commodities
        bycicle = GameObject.Find("bicla");
        disabilities = GameObject.Find("dizabilitati");
        bycicle.SetActive(false);
        disabilities.SetActive(false);
        bussIMG.SetActive(false);
        tramIMG.SetActive(false);

        if (currentVehicle.vehicle_type == Vehicle.VehicleType.Bus)
        {
            bussIMG.SetActive(true);
        }
        else if (currentVehicle.vehicle_type == Vehicle.VehicleType.Tram)
        {
            tramIMG.SetActive(true);
        }

        var searchedRoute = _apiManager.lastSortedRoutes.Find(route => route.route_id == currentVehicle.route_id);
        numeStatie.GetComponent<TextMeshProUGUI>().text = "Route " + searchedRoute.route_short_name + " - Info";

        if (currentVehicle.bike_accessible == "BIKE_ACCESSIBLE")
        {
            bycicle.SetActive(true);
        }
        if (currentVehicle.wheelchair_accessible == "WHEELCHAIR_ACCESSIBLE")
        {
            disabilities.SetActive(true);
            if (bycicle.activeSelf && disabilities.activeSelf)
            {
                disabilities.transform.position = new Vector3(disabilities.transform.position.x + 250, disabilities.transform.position.y, disabilities.transform.position.z);
            }
        }

        showCrowdedLevel();

    }

    private void showCrowdedLevel()
    {
        DateTime now = DateTime.Now;
        double currentHour = now.Hour + now.Minute / 60.0;

        // Initialize crowdedLevel to the lowest by default
        int crowdedLevel = 1;


        if ((currentHour >= 11.5 && currentHour < 12.5) ||
            (currentHour >= 13.5 && currentHour < 14.5) ||
            (currentHour >= 16.5 && currentHour < 17.5) ||
            (currentHour >= 19.5 && currentHour < 20.5))
        {
            crowdedLevel = 4; // Peak hours
        }
        else if ((currentHour >= 11 && currentHour < 13) ||
                 (currentHour >= 14 && currentHour < 16) ||
                 (currentHour >= 17 && currentHour < 19) ||
                 (currentHour >= 20 && currentHour < 21))
        {
            crowdedLevel = 3; // High, but not at peak
        }
        else if ((currentHour >= 10 && currentHour < 11) ||
                 (currentHour >= 13 && currentHour < 14) ||
                 (currentHour >= 16 && currentHour < 17) ||
                 (currentHour >= 19 && currentHour < 20) ||
                 (currentHour >= 21 && currentHour < 22))
        {
            crowdedLevel = 2; // Moderate
        }

        for (int i = 1; i <= 4; i++)
        {
            GameObject.Find("om_" + i).SetActive(i <= crowdedLevel);
        }
    }

    void Update()
    {

    }
}