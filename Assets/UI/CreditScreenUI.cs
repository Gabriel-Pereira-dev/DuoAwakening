using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CreditScreenUI : MonoBehaviour
    {
        public FadeMusic fadeMusic;

        public void FadeOutMusic()
        {
            fadeMusic.FadeOut();
        }

        public void SwitchToTitleScreen()
        {
            FadeOutMusic();
            SceneManager.LoadScene("TitleScene");
        }
    }
}