
    using System;
    using UnityEngine;

    public class GlobalEvents : MonoBehaviour
    {
        // Singleton
        public static GlobalEvents Instance { get; private set; }
        
        // Event Handlers
        public event EventHandler<BossDoorOpenArgs> OnBossDoorOpen;
        public event EventHandler<BossRoomEnterArgs> OnBossRoomEnter;
        public event EventHandler<GameOverArgs> OnGameOver;
        public event EventHandler<GameWonArgs> OnGameWon;
        
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            
        }

        void Update()
        {

        }
    }
