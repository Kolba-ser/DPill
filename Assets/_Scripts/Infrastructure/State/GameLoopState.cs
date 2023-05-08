namespace Infrastructure.State
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}