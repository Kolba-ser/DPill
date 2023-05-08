using UnityEngine;
using UnityEngine.XR;

namespace Infrastructure.Services.Input.Mobile
{
    public class MobileInputService : InputService
    {
        private readonly Joystick joystick;
        private readonly Camera camera;

        public override Vector2 Axis => WorldAxis();

        public MobileInputService(Joystick joystick, Camera camera)
        {
            this.joystick = joystick;
            this.camera = camera;
        }

        private Vector2 WorldAxis()
        {
            return camera.transform.TransformDirection(joystick.Direction);
        }
    }
}