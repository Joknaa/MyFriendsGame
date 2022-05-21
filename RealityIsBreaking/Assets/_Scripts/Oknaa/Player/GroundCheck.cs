using UnityEngine;

namespace Old {
    public class GroundCheck : MonoBehaviour {
        private GameObject Player;

        private void Start() {
            Player = gameObject.transform.parent.gameObject;
        }
    }
}