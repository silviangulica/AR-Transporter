using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Services
{
    public class PanelService : MonoBehaviour
    {
        private APIManager _apiManager;
        public GameObject objectToDuplicate;
        
        private List<Vehicle> _currentStationVehicles;
        private Stop _currentStationStop;
        private List<Route> _currentStationRoutes;

        void Start()
        {
            _apiManager = APIManager.Instance;
            _currentStationStop = _apiManager.lastStop;
            _currentStationVehicles = _apiManager.lastSortedVehicles;
            _currentStationRoutes = _apiManager.lastSortedRoutes;

            foreach (var vehicle in _currentStationVehicles)
            {
                var newObject = Instantiate(objectToDuplicate, transform);
                var text = newObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
                
                text.text = $"{_currentStationRoutes.Find(x => x.route_id == vehicle.route_id).route_short_name} - {vehicle.label}";
            }
        }
    }
}