using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class TitleScreenUI : MonoBehaviour
    {
        public Animator thisAnimator;
        public float fadeOutDuration = 1.5f;

        public bool isFadingOut;

        public void FadeOut()
        {
            //Debounce
            if (isFadingOut) return;
            isFadingOut = true;
            
            // Begin animator
            thisAnimator.enabled = true;
            
            // Schedule scene
            StartCoroutine(TransitionToNextScene());
        }

        private IEnumerator TransitionToNextScene()
        {
            yield return new WaitForSeconds(fadeOutDuration);

            SceneManager.LoadScene("Scenes/MainScene");
        }
    }
}
