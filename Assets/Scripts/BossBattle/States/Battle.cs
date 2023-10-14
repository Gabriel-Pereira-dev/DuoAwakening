using StateMachineNamespace;

namespace BossBattle.States
{
    public class Battle : State
    {
        public Battle() : base("Battle")
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            // Set max health and show boss bar
            var gameManager = GameManager.Instance;
            var gameplayUI = gameManager.gameplayUI;
            var boss = gameManager.boss;
            var bossLife = boss.GetComponent<LifeScript>();
            gameplayUI.ToggleBossBar(true);
            gameplayUI.bossHealthBar.SetMaxHealth(bossLife.maxHealth);
        }

        public override void Exit()
        {
            base.Exit();
            
            // Hide boss bar
            var gameManager = GameManager.Instance;
            var gameplayUI = gameManager.gameplayUI;
            gameplayUI.ToggleBossBar(false);
        }

        public override void Update()
        {
            base.Update();
            
            // Update health
            var gameManager = GameManager.Instance;
            var gameplayUI = gameManager.gameplayUI;
            var boss = gameManager.boss;
            var bossLife = boss.GetComponent<LifeScript>();
            gameplayUI.bossHealthBar.SetHealth(bossLife.health);
        }
    }
}