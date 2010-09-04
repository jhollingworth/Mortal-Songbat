using GarageGames.Torque.GameUtil;
using GarageGames.Torque.GUI;
using MortalSongbat.GUI;

namespace MortalSongbat
{
    public class Game : TorqueGame
    {
        private static Game _myGame;

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

            _myGame.Run();
        }

        protected override void BeginRun()
        {
            base.BeginRun();

        //    start by showing the GarageGames splash screen
            var splashScreen = new GuiMainMenu() ;
            GUICanvas.Instance.SetContentControl(splashScreen);

            //var playGUI = new GuiPlay();
            //GUICanvas.Instance.SetContentControl(playGUI);

            //Instance.SceneLoader.Load(@"data\levels\levelData.txscene");
        }
    }
}