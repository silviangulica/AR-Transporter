using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class RouteInfoPanel : MonoBehaviour
{
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

        numeStatie.GetComponent<TextMeshProUGUI>().text = "Route " + currentVehicle.route_id + " - Info";

        if (currentVehicle.bike_accessible == "BIKE_ACCESSIBLE")
        {
            bycicle.SetActive(true);
        }
        if (currentVehicle.wheelchair_accessible == "WHEELCHAIR_ACCESSIBLE")
        {
            disabilities.SetActive(true);
            if (bycicle.activeSelf && disabilities.activeSelf)
            {
                disabilities.transform.position = new Vector3(disabilities.transform.position.x + 150, disabilities.transform.position.y, disabilities.transform.position.z);
            }
        }

        showCrowdedLevel();

    }

    private void showCrowdedLevel()
    {
        DateTime now = DateTime.Now;
        double currentHour = now.Hour + now.Minute / 60.0;
        int crowdedLevel = (int)(4 * Math.Sin((currentHour - 6) * (Math.PI / 12)) + 4) % 4 + 1;

        for (int i = 4; i > crowdedLevel; i--)
        {
            GameObject.Find("om_" + i).SetActive(false);
        }

    }

    void Update()
    {

    }
}