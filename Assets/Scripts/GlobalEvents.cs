
    using System;
    using EventArgs;
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
        
        public void InvokeOnBossDoorOpen(object sender, BossDoorOpenArgs args){ OnBossDoorOpen?.Invoke(sender,args);}
        public void InvokeOnBossRoomEnter(object sender, BossRoomEnterArgs args){ OnBossRoomEnter?.Invoke(sender,args);}
        public void InvokeOnGameOver(object sender, GameOverArgs args){ OnGameOver?.Invoke(sender,args);}
        public void InvokeOnGameWon(object sender, GameWonArgs args){ OnGameWon?.Invoke(sender,args);}
    }
