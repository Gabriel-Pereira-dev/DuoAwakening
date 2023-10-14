using BossBattle.States;
using StateMachineNamespace;

namespace BossBattle
{
    public class BossBattleHandler
    {
        public StateMachine stateMachine;
        public Disabled stateDisabled;
        public Waiting stateWaiting;
        public Intro stateIntro;
        public Battle stateBattle;
        public BossVictorious stateVictorious;
        public BossDefeated stateDefeated;
        
        public BossBattleHandler()
        {
            // Create stateMachine
            stateMachine = new StateMachine();
            stateDisabled = new Disabled();
            stateWaiting = new Waiting();
            stateIntro = new Intro();
            stateBattle = new Battle();
            stateVictorious = new BossVictorious();
            stateDefeated = new BossDefeated();
            
            stateMachine.ChangeState(stateDisabled);
            GameManager.Instance.bossBattleParts.SetActive(false);
            
            // Register Listeners
            var globalEvents = GlobalEvents.Instance;
            globalEvents.OnBossDoorOpen += ((sender, args) => stateMachine.ChangeState(stateWaiting));
            globalEvents.OnBossRoomEnter += ((sender, args) => stateMachine.ChangeState(stateIntro));
            globalEvents.OnGameOver += ((sender, args) => stateMachine.ChangeState(stateVictorious));
            globalEvents.OnGameWon += ((sender, args) => stateMachine.ChangeState(stateDefeated));
        }

        public void Update()
        {
            stateMachine.Update();
        }

        public bool IsActive()
        {
            return stateMachine.currentStateName == stateBattle.name;
        }
        
        public bool IsInCutscene()
        {
            return stateMachine.currentStateName == stateIntro.name;
        }
    }
}