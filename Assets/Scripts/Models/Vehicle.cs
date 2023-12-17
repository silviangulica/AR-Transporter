using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vehicle
{
    public int id;
    public string label;
    public double latitude;
    public double longitude;
    public string timestamp;

    public VehicleType vehicle_type;
    public string bike_accessible;

    public string wheelchair_accessible;

    public string x_provider;

    public string x_rand;

    public int speed;

    public int route_id;

    public string trip_id;

    public enum VehicleType
    {
        Tram = 0,
        Subway = 1,
        Rail = 2,
        Bus = 3,
        Ferry = 4,
        Cable_tram = 5,
        Aerial_lift = 6,
        Funicular = 7,
        TrolleyBus = 11,
        Monorail = 12,
    }

    public Vehicle(int id, string label, double latitude, double longitude, string timestamp, VehicleType vehicle_type, string bike_accessible, string wheelchair_accessible, string x_provider, string x_rand, int speed, int route_id, string trip_id)
    {
        this.id = id;
        this.label = label;
        this.latitude = latitude;
        this.longitude = longitude;
        this.timestamp = timestamp;
        this.vehicle_type = vehicle_type;
        this.bike_accessible = bike_accessible;
        this.wheelchair_accessible = wheelchair_accessible;
        this.x_provider = x_provider;
        this.x_rand = x_rand;
        this.speed = speed;
        this.route_id = route_id;
        this.trip_id = trip_id;
    }

    public override string ToString()
    {
        return "Vehicle{" +
                "id=" + id +
                ", label='" + label + '\'' +
                ", latitude=" + latitude +
                ", longitude=" + longitude +
                ", timestamp='" + timestamp + '\'' +
                ", vehicle_type=" + vehicle_type +
                ", bike_accessible='" + bike_accessible + '\'' +
                ", wheelchair_accessible='" + wheelchair_accessible + '\'' +
                ", x_provider='" + x_provider + '\'' +
                ", x_rand='" + x_rand + '\'' +
                ", speed=" + speed +
                ", route_id=" + route_id +
                ", trip_id='" + trip_id + '\'' +
                '}';
    }
}