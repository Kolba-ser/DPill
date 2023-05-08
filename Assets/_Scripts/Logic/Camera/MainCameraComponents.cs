using UnityEngine;

namespace Logic.Camera
{
    public class MainCameraComponents : MonoBehaviour
    {
        [field: SerializeField] 
        public CameraFollower Follower { get; private set; }
    }
}