using Assets._Scripts.Logic.Level.Data;
using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Locations
{
    [CreateAssetMenu(fileName = "New LocationMetaData SO", menuName = "Locations/New LocationsContainer")]
    public class LocationsInfoContainer : ScriptableObject
    {
        [SerializeField] private LocationMetaData[] locations;

        public IReadOnlyCollection<LocationMetaData> Locations => locations;

        public LocationMetaData GetByIndex(int index)
        {
            if (index.InBounds(locations))
                return locations[index];

            return null;
        }
    }
}