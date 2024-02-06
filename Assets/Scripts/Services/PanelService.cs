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
        private Stop _currentStationStop;
        private List<Route> _currentStationRoutes;

        private void Start()
        {
            _apiManager = APIManager.Instance;
            _currentStationStop = _apiManager.lastStop;
            _currentStationVehicles = _apiManager.lastSortedVehicles;
            _currentStationRoutes = _apiManager.lastSortedRoutes;
            TextMeshProUGUI text;
            foreach (var vehicle in _currentStationVehicles)
            {
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
                text.text += vehicle.route_id;
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