namespace Infrastructure.State
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}