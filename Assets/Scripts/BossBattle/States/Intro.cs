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
            
            // Enable boss battle parts
            GameManager.Instance.bossBattleParts.SetActive(true);
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