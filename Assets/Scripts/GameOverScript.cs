using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameOverScript : MonoBehaviour
    {
        public GameObject objectToEnable;
        public float gameOverDuration = 0f;

        private void Start()
        {
            var globalEvents = GlobalEvents.Instance;
            globalEvents.OnGameOver += ((sender, args) => StartCoroutine(StartGameOver()) );
        }

        public IEnumerator StartGameOver()
        {
            // Enable Post Process
            objectToEnable.SetActive(true);
            
            // GameOver duration
            yield return new WaitForSeconds(gameOverDuration);
            
            // Load title scene
            SceneManager.LoadScene("TitleScene");
        }
    }
    
    
}