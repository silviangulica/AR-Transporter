using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class PanelService : MonoBehaviour
    {
        public GameObject objectBusToDuplicate;
        public GameObject objectTramToDuplicate;
        public Transform contentTransform;

        private APIManager _apiManager;

        private List<Vehicle> _currentStationVehicles;
        private List<Route> _lastSortedRoutes;
        private List<Route> _currentStationRoutes;

        private void Start()
        {
            TextMeshProUGUI text;
            _apiManager = APIManager.Instance;
            _currentStationVehicles = _apiManager.lastSortedVehicles;
            _lastSortedRoutes = _apiManager.lastSortedRoutes;

            for (int i = 0; i < _currentStationVehicles.Count; i++)
            {
                Debug.Log(_currentStationVehicles[i]);
            }

            foreach (var vehicle in _currentStationVehicles)
            {
                Debug.Log(vehicle);
                if (vehicle.vehicle_type == Vehicle.VehicleType.Tram)
                {
                    var newObject = Instantiate(objectTramToDuplicate, contentTransform);
                    text = newObject.GetComponentInChildren<TextMeshProUGUI>();
                    newObject.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(vehicle));
                    text.text = "Tram ";

                }
                else
                {
                    var newObject = Instantiate(objectBusToDuplicate, contentTransform);
                    text = newObject.GetComponentInChildren<TextMeshProUGUI>();
                    newObject.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(vehicle));
                    text.text = "Bus ";
                }


                var searchedRoute = _lastSortedRoutes.Find(route => route.route_id == vehicle.route_id);
                text.text += searchedRoute.route_short_name;
            }

            objectBusToDuplicate.SetActive(false);
            objectTramToDuplicate.SetActive(false);

        }

        private void ButtonClicked(Vehicle vehicle)
        {
            _apiManager.currentVehicle = vehicle;
        }
    }
}