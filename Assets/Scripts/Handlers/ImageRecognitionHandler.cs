using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vuforia;

public class ImageRecognitionHandler : MonoBehaviour
{
    public APIManager apiManager;
    public LocationService locationService;
    public GameObject arPanelTarget;
    public TextMeshPro arPanelText;

    public int callCount = 0;


    private Vector2 previousLocation;

    void Start()
    {
        StartCoroutine(locationService.StartLocationService());
        previousLocation = Vector2.zero;

    }

    public void OnImageRecognized()
    {

        Vector2 currentLocation = locationService.GetLocation();
        /*Vector2 currentLocation = new Vector2(47.180870f, 27.572713f);*/
        /* Vector2 currentLocation  = new Vector2(47.19052f,
             27.55848f);
         // e pt testing doar:
         if (callCount % 2 == 0)
         {
             currentLocation = new Vector2(47.19052f,
                 27.55848f);
         }
         else
         {
             currentLocation = new Vector2(47.166740f, 27.570924f);
         }*/

        callCount++;
        float distance = LocationService.CalculateHaversineDistance(previousLocation, currentLocation);
        Debug.Log("Distance: " + distance);
        if (distance > 0.05) // 5m
        {
            // Update the previousLocation with the current location
            previousLocation = currentLocation;

            Stop nearestStop = apiManager.FindNearestStop(currentLocation);

            if (nearestStop != null)
            {
                // get buses for the nearest stop + update the arPanel
                Debug.Log(nearestStop);
                arPanelText.text = nearestStop.stop_name;
                arPanelTarget.SetActive(true); // Show the AR panel
            }
            else
            {
                Debug.LogError("No nearest stop found");
                arPanelTarget.SetActive(false);
            }
        }
    }


}


