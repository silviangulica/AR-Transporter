using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Mapbox : MonoBehaviour
{
    private APIManager _apiManager;
    public string accessToken;
    public float centerLatitude = -34.615662f;
    public float centerLongitude = -58.503338f;
    public float zoom = 12.0f;
    public int bearing = 0;
    public int pitch = 0;

    public enum style
    {
        Light,
        Dark,
        Streets,
        Outdoors,
        Satellite,
        SatelliteStreets
    };

    public style mapStyle = style.Streets;

    public enum resolution
    {
        low = 1,
        high = 2
    };

    public resolution mapResolution = resolution.low;

    private int mapWidth = 800;
    private int mapHeight = 600;

    private string[] styleStr = new string[]
        { "light-v10", "dark-v11", "streets-v11", "outdoors-v11", "satellite-v9", "satellite-streets-v11" };

    private string url = "";
    private bool mapIsLoading = false;
    private Rect rect;
    private bool updateMap = false;

    private string accessTokenLast;
    private float centerLatitudeLast = -34.615662f;
    private float centerLongitudeLast = -58.503338f;
    private float zoomLast = 12.0f;
    private int bearingLast = 0;
    private int pitchLast = 0;
    private style mapStyleLast = style.Streets;
    private resolution mapResolutionLast = resolution.low;
    private RawImage _rawImage;



    void Start()
    {
        _apiManager = APIManager.Instance;
        centerLatitude = (float)_apiManager.currentVehicle.latitude;
        centerLongitude = (float)_apiManager.currentVehicle.longitude;
        _rawImage = gameObject.GetComponent<RawImage>();
        StartCoroutine(GetMapbox());
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        mapWidth = (int)Math.Round(rect.width);
        mapHeight = (int)Math.Round(rect.height);
    }

    // Update is called once per frame
    // 10 sec
    private float _counter = 1;
    void Update()
    {
        if (_counter > 0)
        {
            _counter -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(_apiManager.GetVehicles());
            _apiManager.currentVehicle = _apiManager.GetVehicleByID(_apiManager.currentVehicle.id);
            _counter = 5;
            if (_apiManager.currentVehicle != null)
            {
                centerLatitude = (float)_apiManager.currentVehicle.latitude;
                centerLongitude = (float)_apiManager.currentVehicle.longitude;
                updateMap = true;
            }
            if (!updateMap || (accessTokenLast == accessToken && Mathf.Approximately(centerLatitudeLast, centerLatitude)
                                                              && Mathf.Approximately(centerLongitudeLast,
                                                                  centerLongitude) && Mathf.Approximately(zoomLast, zoom)
                                                              && bearingLast == bearing && pitchLast == pitch &&
                                                              mapStyleLast == mapStyle &&
                                                              mapResolutionLast == mapResolution)) return;
            rect = _rawImage.rectTransform.rect;
            mapWidth = (int)Math.Round(rect.width);
            mapHeight = (int)Math.Round(rect.height);
            StartCoroutine(GetMapbox());
            updateMap = false;

        }
    }

    IEnumerator GetMapbox()
    {
        url = "https://api.mapbox.com/styles/v1/mapbox/" + styleStr[(int)mapStyle] + "/static/" + centerLongitude +
              "," + centerLatitude + "," + zoom + "," + bearing + "," + pitch + "/" + mapWidth + "x" + mapHeight +
              "?access_token=" + accessToken;
        mapIsLoading = true;
        var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
        else
        {
            mapIsLoading = false;
            gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            accessTokenLast = accessToken;
            centerLatitudeLast = centerLatitude;
            centerLongitudeLast = centerLongitude;
            zoomLast = zoom;
            bearingLast = bearing;
            pitchLast = pitch;
            mapStyleLast = mapStyle;
            mapResolutionLast = mapResolution;
            updateMap = true;
        }
    }

}
