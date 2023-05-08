namespace Infrastructure.State
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}