using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Guns
{
    [CreateAssetMenu(fileName = "New WeaponsContainer SO", menuName = "Weapons/New WeaponsContainer")]
    public class WeaponsInfoContainer : ScriptableObject
    {

        [SerializeField] 
        private SerializableKeyValuePair<string, WeaponInfo>[] weaponsInfo;

        public IReadOnlyCollection<SerializableKeyValuePair<string, WeaponInfo>> WeaponsInfo => weaponsInfo;
    }



}