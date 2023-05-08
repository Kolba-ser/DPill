using Infrastructure.Services.Interfaces;
using System;

namespace Infrastructure.Services
{
    public class AllServices : IDisposable
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public TService RegisterSingle<TService>(TService implemantation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implemantation;

        public TService GetSingle<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }

        public void Dispose()
        {
            _instance = null;
        }
    }
}
