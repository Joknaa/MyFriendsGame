namespace _Scripts {
    public class GameStateController {
        public enum GameState {
            Playing,
            Paused,
            CutScene,
        }

        public static GameStateController Instance => instance ?? (instance = new GameStateController());
        
        private static GameStateController instance;
        private GameState currentGameState;

        public GameState GetState() => currentGameState;
        
        public void SetState_Playing() => currentGameState = GameState.Playing;
        public void SetState_Paused() => currentGameState = GameState.Paused;
        public void SetState_CutScene() => currentGameState = GameState.CutScene;
        
        
        public void TogglePause() => currentGameState = currentGameState == GameState.Paused ? GameState.Playing : GameState.Paused;
        public bool IsPlaying() => currentGameState == GameState.Playing;
        public bool IsPaused() => currentGameState == GameState.Paused;
        public bool IsCutScene() => currentGameState == GameState.CutScene;
        
        
        
        public bool IsPlayableState() => currentGameState == GameState.Playing || currentGameState == GameState.CutScene;

    }
}