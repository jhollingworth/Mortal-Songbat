using System.Collections;
using GarageGames.Torque.GUI;
using GarageGames.Torque.Platform;
using GarageGames.Torque.Sim;
using GarageGames.Torque.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MortalSongbat.GUI
{
    internal class GuiMainMenu : GUIBitmap, IGUIScreen
    {
        public GuiMainMenu()
        {
            Name = "GuiMainMenu";

            //create the style for the main menu background
            var bitmapStyle = new GUIBitmapStyle();
            Style = bitmapStyle;
            Bitmap = @"data\images\LandingPage";

            //create the style for the menu buttons
            var buttonStyle = new GUIButtonStyle();
            buttonStyle.FontType = "Arial22"; //@"data\images\MyFont";
            buttonStyle.TextColor[CustomColor.ColorBase] = new Color(100, 100, 100, 255); //normal menu text color
            buttonStyle.TextColor[CustomColor.ColorHL] = Color.Red; //highlighter color
            buttonStyle.TextColor[CustomColor.ColorNA] = Color.Silver; //disabled color
            buttonStyle.TextColor[CustomColor.ColorSEL] = Color.DarkRed; //select color
            buttonStyle.Alignment = TextAlignment.JustifyCenter;
            buttonStyle.Focusable = true;

            float positionX = 640;
            float positionY = 200;

            var option1 = new GUIButton();
            option1.Style = buttonStyle;
            option1.Size = new Vector2(520, 100);
            option1.Position = new Vector2(positionX - (option1.Size.X/2), positionY);
            option1.Visible = true;
            option1.Folder = this;
            option1.ButtonText = "Start New Game";
            option1.OnSelectedDelegate = OnStartGame;
            option1.OnGainFocus(null);
            _buttons.Add(option1);

            var option4 = new GUIButton();
            option4.Style = buttonStyle;
            option4.Size = new Vector2(500, 100);
            option4.Position = new Vector2(positionX - (option1.Size.X/2), positionY + 50);
            option4.Visible = true;
            option4.Folder = this;
            option4.ButtonText = "Quit";
            option4.OnSelectedDelegate = OnQuit;
            _buttons.Add(option4);

            //setup the input map
            SetupInputMap();
        }

        private void SetupInputMap()
        {
            InputMap.BindCommand(Game.Instance.Player1ControllerIndex, (int) XGamePadDevice.GamePadObjects.A, null,
                                 OnStartGame);
            InputMap.BindCommand(Game.Instance.Player1ControllerIndex, (int) XGamePadDevice.GamePadObjects.Y, null,
                                 OnQuit);
            InputMap.BindCommand(Game.Instance.Player1ControllerIndex, (int) XGamePadDevice.GamePadObjects.Back, null,
                                 Game.Instance.Exit);

            var keyboardId = InputManager.Instance.FindDevice("keyboard");
            InputMap.BindCommand(keyboardId, (int) Keys.Down, null, MoveDown);
            InputMap.BindCommand(keyboardId, (int) Keys.Up, null, MoveUp);
            InputMap.BindCommand(keyboardId, (int) Keys.Enter, null, Select);
        }

        private void MoveDown()
        {
            if (_currentSelection + 1 < _buttons.Count)
            {
                //clear all other options
                for (var index = 0; index < _buttons.Count; index++)
                    ((GUIButton) _buttons[index]).OnLoseFocus(null);

                _currentSelection++;
                ((GUIButton) _buttons[_currentSelection]).OnGainFocus(null);
            }
        }

        private void MoveUp()
        {
            if (_currentSelection - 1 >= 0)
            {
                //clear all other options
                for (var index = 0; index < _buttons.Count; index++)
                    ((GUIButton) _buttons[index]).OnLoseFocus(null);

                _currentSelection--;
                ((GUIButton) _buttons[_currentSelection]).OnGainFocus(null);
            }
        }

        private void Select()
        {
            ((GUIButton) _buttons[_currentSelection]).OnSelectedDelegate();
        }

        private static void OnStartGame()
        {
            var playGUI = new GuiPlay();
            GUICanvas.Instance.SetContentControl(playGUI);

            Game.Instance.SceneLoader.Load(@"data\levels\levelData.txscene");
        }

        private static void OnQuit()
        {
            //shutdown the game
            TorqueEngineComponent.Instance.Exit();
        }

        private GUIButton option3 = new GUIButton();

        private bool _showBuy;
        private int _currentSelection;
        private readonly ArrayList _buttons = new ArrayList();
    }
}