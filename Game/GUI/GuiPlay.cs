using GarageGames.Torque.GUI;
using GarageGames.Torque.MathUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MortalSongbat.GUI
{
    public class GuiPlay : GUISceneview, IGUIScreen
    {
        private readonly GUITextStyle styleMain = new GUITextStyle();
        private readonly GUIBitmapStyle bitmapStyle = new GUIBitmapStyle();
        private readonly GUIText _playerScore1;
        private readonly GUIText _healthLevel;
        private readonly GUIBitmap _target;

        public GuiPlay()
        {
            var playStyle = new GUIStyle();
            Name = "GuiPlay";
            Style = playStyle;
            Size = new Vector2(1280, 720);
            Folder = GUICanvas.Instance;


            styleMain = new GUITextStyle();
            styleMain.FontType = "Arial22"; // @"data\images\MyFont";
            styleMain.TextColor[0] = Color.Red;
            styleMain.SizeToText = false;
            styleMain.Alignment = TextAlignment.JustifyCenter;
            styleMain.PreserveAspectRatio = true;

            _playerScore1 = new GUIText();
            _playerScore1.Style = styleMain;
            _playerScore1.Position = new Vector2(25, 25);
            _playerScore1.Size = new Vector2(400, 120);
            _playerScore1.Visible = true;
            _playerScore1.Folder = this;
            _playerScore1.Text = "Player Score:";

            _healthLevel = new GUIText();
            _healthLevel.Style = styleMain;
            _healthLevel.Position = new Vector2(780, 25);
            _healthLevel.Size = new Vector2(400, 120);
            _healthLevel.Visible = true;
            _healthLevel.Folder = this;
            _healthLevel.Text = "Health Level";

            bitmapStyle = new GUIBitmapStyle();
            bitmapStyle.SizeToBitmap = false;

            _target = new GUIBitmap();
            _target.Style = bitmapStyle;
            _target.Size = new Vector2(64);
            _target.Bitmap = @"data\images\Target";
            _target.Folder = this;
            _target.Visible = true;
            _target.Position = Vector2.Divide(Size, 2) - Vector2.Divide(_target.Size, 2);
        }


        public override void OnRender(Vector2 offset, RectangleF updateRect)
        {
            _playerScore1.Text = "Player Score: " + Game.Instance.PlayerScore;
            _healthLevel.Text = "Health Level: " + Game.Instance.HealthLevel;

            base.OnRender(offset, updateRect);
        }
    }
}