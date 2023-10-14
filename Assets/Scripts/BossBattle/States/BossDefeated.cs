using StateMachineNamespace;
using UnityEngine;

namespace BossBattle.States
{
    public class BossDefeated : State
    {
        public BossDefeated() : base("BossDefeated")
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            var gameManager = GameManager.Instance;

            // Create death sequence
            var boss = gameManager.boss;
            var sequencePrefab = gameManager.bossDeathSequence;
            var a = Object.Instantiate(sequencePrefab, boss.transform.position, sequencePrefab.transform.rotation);
            
            // Stop boss music
            var bossMusic = gameManager.bossMusic;
            var bossTargetVolume = 0f;
            gameManager.StartCoroutine(FadeAudioSource.StartFade(bossMusic, bossTargetVolume,0.5f));
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            
        }
    }
}