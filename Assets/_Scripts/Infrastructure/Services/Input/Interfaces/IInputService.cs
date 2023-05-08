using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Infrastructure.Services.Input
{

    public interface IInputService : IService
    {
        public Vector2 Axis { get; }
    }
}