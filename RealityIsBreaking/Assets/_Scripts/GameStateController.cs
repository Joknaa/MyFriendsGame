namespace Reality {
    public class GameStateController {
        public enum GameState {
            Playing,
            Paused,
            CutScene,
            GameOver,
            GameWon
        }

        public static GameStateController Instance => instance ?? (instance = new GameStateController());
        
        private static GameStateController instance;
        private GameState currentGameState;

        public GameState GetState() => currentGameState;
        
        public void SetState_Playing() => currentGameState = GameState.Playing;
        public void SetState_Paused() => currentGameState = GameState.Paused;
        public void SetState_CutScene() => currentGameState = GameState.CutScene;
        public void SetState_GameOver() => currentGameState = GameState.GameOver;
        public void SetState_GameWon() => currentGameState = GameState.GameWon;
        
        
        public void TogglePause() => currentGameState = currentGameState == GameState.Paused ? GameState.Playing : GameState.Paused;
        public bool IsPlaying() => currentGameState == GameState.Playing;
        public bool IsPaused() => currentGameState == GameState.Paused;
        public bool IsCutScene() => currentGameState == GameState.CutScene;
        public bool IsGameOver() => currentGameState == GameState.GameOver;
        public bool IsGameWon() => currentGameState == GameState.GameWon;

    }
}