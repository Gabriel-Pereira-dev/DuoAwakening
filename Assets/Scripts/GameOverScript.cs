using System;
using System.Collections;
using EventArgs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameOverScript : MonoBehaviour
    {
        public GameObject objectToEnable;
        public float gameOverDuration = 6f;
        public bool isTriggered;
        private void Start()
        {
            var globalEvents = GlobalEvents.Instance;
            globalEvents.OnGameOver += OnGameOver;
        }

        public void OnGameOver(object sender, GameOverArgs args)
        {
            // Debounce
            if (isTriggered) return;
            isTriggered = true;
            
            // Activate object
            objectToEnable.SetActive(true);
            
            // Reload Scene
            StartCoroutine(ReloadScene());
        }

        public IEnumerator ReloadScene()
        {   
            // GameOver duration
            yield return new WaitForSeconds(gameOverDuration);
            
            // Load title scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    
}