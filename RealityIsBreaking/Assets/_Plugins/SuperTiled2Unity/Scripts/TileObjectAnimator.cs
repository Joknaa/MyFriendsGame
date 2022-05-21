using UnityEngine;

namespace SuperTiled2Unity {
    public class TileObjectAnimator : MonoBehaviour {
        public float m_AnimationFramerate;
        public Sprite[] m_AnimationSprites;
        private int m_AnimationIndex;

        private float m_Timer;

        private void Update() {
            m_Timer += Time.deltaTime;
            var frameTime = 1.0f / m_AnimationFramerate;

            if (m_Timer > frameTime) {
                m_AnimationIndex++;
                if (m_AnimationIndex >= m_AnimationSprites.Length) m_AnimationIndex = 0;

                var renderer = GetComponent<SpriteRenderer>();
                renderer.sprite = m_AnimationSprites[m_AnimationIndex];

                m_Timer = frameTime - m_Timer;
            }
        }
    }
}