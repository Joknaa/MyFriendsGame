using UnityEngine.SceneManagement;

namespace Reality {
    public class ScenesController {
        
        public static ScenesController Instance => instance ?? (instance = new ScenesController());
        private static ScenesController instance;


        public void LoadMainMenu() {
            GameStateController.Instance.SetState_Playing_FirstHalf();
            SceneManager.LoadScene(1);
        }

        public void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadActualGame() {
            SceneManager.LoadScene(2);
        }
    }
}