using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Route
{
    public int route_id;
    public int agency_id;
    public string route_short_name;
    public string route_long_name;
    public string route_color;
    public RouteType route_type;
    public enum RouteType
    {
        Tram = 0,
        StreetCar = 0,
        Metro = 1,
        Subway = 1,
        Rail = 2,
        Bus = 3,
        Ferry = 4,
        CableTram = 5,
        AerialLift = 6,
        Funicular = 7,
        TrolleyBus = 11,
        Monorail = 12,
    }

    public Route(int id, int agency, string shortName, string longName, string color, RouteType type)
    {
        route_id = id;
        agency_id = agency;
        route_short_name = shortName;
        route_long_name = longName;
        route_color = color;
        route_type = type;
    }

    public override string ToString()
    {
        return $"Route ID: {route_id}, Agency ID: {agency_id}, Short Name: {route_short_name}, Long Name: {route_long_name}, Color: {route_color}, Type: {route_type}";
    }
}