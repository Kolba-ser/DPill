using UnityEngine;

namespace Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }
    }
}