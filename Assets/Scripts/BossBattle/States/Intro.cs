using StateMachineNamespace;
using UnityEngine;

namespace BossBattle.States
{
    public class Intro : State
    {
        public readonly float duration = 3f;
        public float timeElapsed = 0f;
        
        public Intro() : base("Intro")
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            // Reset stuff
            timeElapsed = 0f;
            
            //Get gameManager
            var gameManager = GameManager.Instance;
            
            // Enable boss battle parts
            gameManager.bossBattleParts.SetActive(true);
            
            // Stop gameplay music
            var gameplayMusic = gameManager.gameplayMusic;
            gameManager.StartCoroutine(FadeAudioSource.StartFade(gameplayMusic, 0f, 2f));
            
            // Play boss music
            var bossMusic = gameManager.bossMusic;
            var bossTargetVolume = bossMusic.volume;
            bossMusic.volume = 0;
            gameManager.StartCoroutine(FadeAudioSource.StartFade(bossMusic, bossTargetVolume,0.5f));
            bossMusic.Play();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= duration)
            {
                var bossBattleHandler = GameManager.Instance.bossBattleHandler;
                bossBattleHandler.stateMachine.ChangeState(bossBattleHandler.stateBattle);
            }
        }
    }
}