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
            contentTransform.position = contentTransform.position + new Vector3(-200, 0, 0);
            foreach (var vehicle in _currentStationVehicles)
            {
                var newObject = Instantiate(objectToDuplicate, contentTransform);
                objectToDuplicate = newObject;
            }

            initObjDublicate.SetActive(false);

        }
    }
}