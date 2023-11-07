using UnityEngine;
using UnityEngine.SceneManagement;

namespace BossBattle
{
    public class BossDeathSequence : MonoBehaviour
    {
        public FadeMusic fadeMusic;

        public void FadeOutMusic()
        {
            fadeMusic.FadeOut();
        }

        public void SwitchToTitleScreen()
        {
            FadeOutMusic();
            SceneManager.LoadScene("CreditsScene");
        }
    }
}