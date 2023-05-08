using Infrastructure.Services;

namespace Infrastructure.Initializers
{
    public abstract class GameEntityInitializer<TEntity>
    {
        protected readonly AllServices allServices;

        public GameEntityInitializer(AllServices allServices)
        {
            this.allServices = allServices;
        }

        public abstract TEntity CreateAndInitialize();

        public abstract void Initialize(TEntity entity);
    }
}