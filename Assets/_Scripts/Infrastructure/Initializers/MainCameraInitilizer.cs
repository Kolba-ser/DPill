using Infrastructure.Services;
using Infrastructure.Services.Player;
using JetBrains.Annotations;
using Logic.Camera;
using UnityEngine;

namespace Infrastructure.Initializers
{
    public class MainCameraInitilizer : GameEntityInitializer<MainCameraComponents>
    {

        public MainCameraInitilizer(AllServices allServices) : base(allServices)
        {
        }

        public override MainCameraComponents CreateAndInitialize()
        {
            var cameraComponents = Camera.main.GetComponent<MainCameraComponents>();
            Initialize(cameraComponents);
            return cameraComponents;
        }

        public override void Initialize(MainCameraComponents components)
        {
            GameObject player = allServices.GetSingle<IPersistentPlayerService>().Player.gameObject;
            components.Follower.Follow(player);
        }
    }
}