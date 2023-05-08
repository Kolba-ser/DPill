using System;
using UnityEngine;

namespace Logic.Gameplay.Player
{
    public class PlayerZoneTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask dangerLayer;
        [SerializeField] private LayerMask safeLayer;

        public bool IsUnderDanger { get; private set; }

        private Action onDanger;
        private Action onSafe;

        public event Action OnDanger
        {
            add
            {
                if(IsUnderDanger)
                    value();
                onDanger += value;
            }

            remove => onDanger -= value;
        }

        public event Action OnSafe
        {
            add
            {
                if (!IsUnderDanger)
                    value();
                onSafe += value;
            }

            remove => onSafe -= value;
        }

        private void OnTriggerEnter(Collider other)
        {
            int otherLayer = other.gameObject.layer;

            CheckLayer(otherLayer);
        }

        private void CheckLayer(int otherLayer)
        {
            var layerMask = LayerMask.GetMask(LayerMask.LayerToName(otherLayer));
            if (layerMask == dangerLayer)
            {
                IsUnderDanger = true;
                onDanger?.Invoke();
            }
            if (layerMask == safeLayer)
            {
                IsUnderDanger = false;
                onSafe?.Invoke();
            }
        }
    }
}