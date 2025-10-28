
namespace Player.States
{
    public class Dead : State
    {
        private PlayerController controller;
        public Dead(PlayerController controller) : base("Dead")
        {
            this.controller = controller;
        }

        public override void Enter()
        {
            base.Enter();
            controller.thisAnimator.SetTrigger("tGameOver");

            GameManager.Instance.gameplayUI.ShowGameOverScreen();
            

            // //make player invulnerable
            // controller.thisLife.isVulnerable = false;

            // //stop gameplay music
            // var gameManager = GameManager.Instance;
            // var gameplayMusic = gameManager.gameplayMusic;
            // var ambienceMusic = gameManager.ambienceMusic;
            // gameplayMusic.Stop();
            // ambienceMusic.Stop();


            //Game over
            // GlobalEvents.Instance.InvokeGamesOver(this, new GameOverArgs());


        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}