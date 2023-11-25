using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Stop
{
     public int stop_id;
     public string stop_name;
     public string stop_desc;
     public double stop_lat;
     public double stop_lon;
     public LocationType location_type;
     public enum LocationType
    {
        StopOrPlatform = 0, 
        Station = 1,        
        EntranceExit = 2,   
        GenericNode = 3,   
        BoardingArea = 4   
    }

    public Stop(int id, string name, string desc, double lat, double lon, LocationType type)
    {
        stop_id = id;
        stop_name = name;
        stop_desc = desc;
        stop_lat = lat;
        stop_lon = lon;
        location_type = type;
    }

    public override string ToString()
    {
        return $"Stop ID: {stop_id}, Name: {stop_name}, Desc: {stop_desc}, Latitude: {stop_lat}, Longitude: {stop_lon}, Type: {location_type}";
    }
}
