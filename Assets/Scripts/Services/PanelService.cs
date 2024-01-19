using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Services
{
    public class PanelService : MonoBehaviour
    {
        public GameObject objectToDuplicate;
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
            var initObjDublicate = objectToDuplicate;
            foreach (var vehicle in _currentStationVehicles)
            {
                var newObject = Instantiate(objectToDuplicate, contentTransform);
                objectToDuplicate = newObject;
                var text = newObject.GetComponentInChildren<TextMeshProUGUI>();
                text.text = vehicle.route_id + " ";
                if (vehicle.vehicle_type == Vehicle.VehicleType.Tram)
                {
                    text.text += "Tram";
                }
                else
                {
                    text.text += "Bus";
                }
            }

            initObjDublicate.SetActive(false);

        }
    }
}