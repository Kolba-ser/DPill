using Infrastructure.State;

namespace Infrastructure.Services.Restart
{
    public class GameRestartService : IGameRestartService
    {
        private readonly GameStateMachine gameStateMachine;

        public GameRestartService(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Restart() => 
            gameStateMachine.Enter<RestartState>();
    }
}