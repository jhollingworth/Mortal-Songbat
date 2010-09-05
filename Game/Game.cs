using System;
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
        public double PlayerHealth { get; set; }
        public double AiHealth { get; set; }

        public string Player { get; set; }
        public string Ai { get; set; }

        public static void Main()
        {
            // Create the static game instance.  
            _myGame = new Game();

            _myGame.Run();
        }

        public SoundGroup Sounds { get; private set; }
        public bool Finished { get; set; }
        protected override void BeginRun()
        {
            base.BeginRun();

            Sounds = SoundManager.Instance.RegisterSoundGroup("Sounds", @"data/sounds/Wave Bank.xwb", @"data/sounds/Sound Bank.xsb");

        //    start by showing the GarageGames splash screen
            var splashScreen = new GuiMainMenu() ;
            GUICanvas.Instance.SetContentControl(splashScreen);

            //var playGUI = new GuiPlay();
            //GUICanvas.Instance.SetContentControl(playGUI);

            //Instance.SceneLoader.Load(@"data\levels\levelData.txscene");
        }
    }
}