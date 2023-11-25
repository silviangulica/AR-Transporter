using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationService : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isLocationServiceStarted = false;
    public Vector2 GetLocation()
    {

        if (!isLocationServiceStarted || Input.location.status == LocationServiceStatus.Failed)
        {
            return Vector2.zero;
        }

        return new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
    }

    public IEnumerator StartLocationService()
    {
        
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location service is not enabled by user.");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait <= 0)
        {
            Debug.Log("Timed out while starting location service.");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location.");
            yield break;
        }
        isLocationServiceStarted = true;
        Debug.Log("Location service initialized.");


    }
    public static float CalculateHaversineDistance(Vector2 point1, Vector2 point2)
    {
        float R = 6371.0f; // Earth's radius in kilometers
        float lat1 = point1.x * Mathf.Deg2Rad; // Convert degrees to radians
        float lat2 = point2.x * Mathf.Deg2Rad;
        float dLat = (point2.x - point1.x) * Mathf.Deg2Rad;
        float dLon = (point2.y - point1.y) * Mathf.Deg2Rad;

        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                  Mathf.Cos(lat1) * Mathf.Cos(lat2) *
                  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return R * c; // Distance in kilometers
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
