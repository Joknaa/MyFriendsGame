namespace Reality {
    public class GameStateController {
        public enum GameState {
            Desktop,
            EmailReading,
            Playing_FirstHalf,
            Playing_SecondHalf,
            Paused,
            PhoneCall,
            CutScene,
            GameOver,
            GameWon,
        }

        public static GameStateController Instance => instance ?? (instance = new GameStateController());
        
        private static GameStateController instance;
        private GameState currentGameState;

        public GameState GetState() => currentGameState;
        
        public void SetState_Playing_FirstHalf() => currentGameState = GameState.Playing_FirstHalf;
        public void SetState_Playing_SecondHalf() => currentGameState = GameState.Playing_SecondHalf;
        public void SetState_Paused() => currentGameState = GameState.Paused;
        public void SetState_CutScene() => currentGameState = GameState.CutScene;
        public void SetState_GameOver() => currentGameState = GameState.GameOver;
        public void SetState_GameWon() => currentGameState = GameState.GameWon;
        public void SetState_PhoneCall() => currentGameState = GameState.PhoneCall;
        public void SetState_EmailReading() => currentGameState = GameState.EmailReading;
        
        public void SetState_Desktop() => currentGameState = GameState.Desktop;
        
        
        public void TogglePause() => currentGameState = currentGameState == GameState.Paused ? GameState.Playing_FirstHalf : GameState.Paused;
        public bool IsPlaying() => currentGameState == GameState.Playing_FirstHalf || currentGameState == GameState.Playing_SecondHalf;
        public bool IsPaused() => currentGameState == GameState.Paused;
        public bool IsCutScene() => currentGameState == GameState.CutScene;
        public bool IsGameOver() => currentGameState == GameState.GameOver;
        public bool IsGameWon() => currentGameState == GameState.GameWon;
        public bool IsPhoneCall() => currentGameState == GameState.PhoneCall;
        public bool IsEmailReading() => currentGameState == GameState.EmailReading;
        public bool IsDesktop() => currentGameState == GameState.Desktop;
        
        
        public bool IsPlayable() => currentGameState == GameState.Playing_FirstHalf || currentGameState == GameState.PhoneCall;

    }
}