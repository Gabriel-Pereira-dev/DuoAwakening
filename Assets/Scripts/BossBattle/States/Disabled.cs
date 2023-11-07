using StateMachineNamespace;
using UnityEngine;

namespace BossBattle.States
{
    public class Disabled : State
    {
        public Disabled() : base("Disabled")
        {
        }

        public override void Enter()
        {
            base.Enter();
            if(GameManager.Instance.boss != null) GameManager.Instance.boss.SetActive(false);
        }

        public override void Exit()
        {
            base.Exit();
            if(GameManager.Instance.boss != null) GameManager.Instance.boss.SetActive(true);
        }
    }
}