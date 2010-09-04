using GarageGames.Torque.GameUtil;
using GarageGames.Torque.GUI;

namespace StarterGame2D
{
    /// <summary>
    /// This is the main game class for your game
    /// </summary>
    public class Game : TorqueGame
    {
        private static Game _myGame;

        /// <summary>
        /// A static property that lets you easily get the Game instance from any Game class.
        /// </summary>
        public static Game Instance
        {
            get { return _myGame; }
        }

        public int Player1ControllerIndex { get; set; }
        public int PlayerScore { get; set; }
        public int HealthLevel { get; set; }

        public static void Main()
        {
            // Create the static game instance.  
            _myGame = new Game();

            // begin the game.  Further setup is done in BeginRun()
            _myGame.Run();
        }


        /// <summary>
        /// Called after the graphics device is created and before the game is about to start running.
        /// </summary>
        protected override void BeginRun()
        {
            base.BeginRun();

            //start by showing the GarageGames splash screen
            var splashScreen = new GuiMainMenu() ;
            GUICanvas.Instance.SetContentControl(splashScreen);
        }
    }
}