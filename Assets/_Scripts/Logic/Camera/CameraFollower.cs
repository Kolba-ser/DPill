using UnityEngine;

namespace Logic.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float followSpeed;
        [SerializeField] private float rotationAngleX;
        [SerializeField] private int distance;
        [SerializeField] private float offsetY;

        [SerializeField]
        private Transform following;

        private void LateUpdate()
        {
            if (!following)
                return;
            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();
            transform.rotation = rotation;
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * followSpeed);
        }

        public void Follow(GameObject following)
        {
            this.following = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }

    }
}