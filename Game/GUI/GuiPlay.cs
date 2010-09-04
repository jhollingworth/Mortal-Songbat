﻿using GarageGames.Torque.Core;
using GarageGames.Torque.GameUtil;
using GarageGames.Torque.GUI;
using GarageGames.Torque.MathUtil;
using GarageGames.Torque.T2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MortalSongbat.GUI
{
    public class GuiPlay : GUISceneview, IGUIScreen
    {
        private readonly GUITextStyle styleMain = new GUITextStyle();
        private readonly GUIBitmapStyle bitmapStyle = new GUIBitmapStyle();
        private readonly GUIText _player1Health;
        private readonly GUIText _player2Health;
        private readonly GUIBitmap _target;
        private AiPlayer _aiPlayer;

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

            _player1Health = new GUIText();
            _player1Health.Style = styleMain;
            _player1Health.Position = new Vector2(25, 25);
            _player1Health.Size = new Vector2(400, 120);
            _player1Health.Visible = true;
            _player1Health.Folder = this;
            _player1Health.Text = "Player Score:";

            _player2Health = new GUIText();
            _player2Health.Style = styleMain;
            _player2Health.Position = new Vector2(780, 25);
            _player2Health.Size = new Vector2(400, 120);
            _player2Health.Visible = true;
            _player2Health.Folder = this;
            _player2Health.Text = "Health Level";

            bitmapStyle = new GUIBitmapStyle();
            bitmapStyle.SizeToBitmap = false;

            Game.Instance.AiHealth = Game.Instance.PlayerHealth = 100;

        }

        private static void PlayerCollision(T2DSceneObject ourObject, T2DSceneObject theirObject, T2DCollisionInfo info, ref T2DResolveCollisionDelegate resolve, ref T2DCollisionMaterial physicsMaterial)
        {
            if (theirObject is T2DAnimatedSprite)
            {
                var player = (T2DAnimatedSprite) TorqueObjectDatabase.Instance.FindObject<T2DSceneObject>("Player1");
                var aiPlayer = (T2DAnimatedSprite) TorqueObjectDatabase.Instance.FindObject<T2DSceneObject>("Player2");

                if (player.AnimationData.Name.Contains("SpecialMove"))
                {
                    Game.Instance.AiHealth -= 2.5;
                }
                else if (aiPlayer.AnimationData.Name.Contains("SpecialMove"))
                {
                    Game.Instance.PlayerHealth -= 2.5;
                }
            }
        }

        public override void OnRender(Vector2 offset, RectangleF updateRect)
        {
            var player = (T2DAnimatedSprite)TorqueObjectDatabase.Instance.FindObject<T2DSceneObject>("Player1");
            var ai = (T2DAnimatedSprite)TorqueObjectDatabase.Instance.FindObject<T2DSceneObject>("Player2");

            if(_aiPlayer == null)
            {
                _aiPlayer = new AiPlayer(
                    ai,
                    player
                );
                player.Collision.OnCollision = PlayerCollision;
                ai.Collision.OnCollision = PlayerCollision;

                player.PlayAnimation(
                    TorqueObjectDatabase.Instance.FindObject<T2DAnimationData>(Game.Instance.Player + "Standing")
                );

                ai.PlayAnimation(
                   TorqueObjectDatabase.Instance.FindObject<T2DAnimationData>(Game.Instance.Ai + "Walking")
                );
            }

            _aiPlayer.Render();

            var sceneObject = player;

            if(sceneObject != null)
            {
                var movement = sceneObject.Components.FindComponent<MovementComponent>();

                if(movement != null)
                {
                    _player1Health.Text = Game.Instance.Player + ": " + Game.Instance.PlayerHealth + "%";
                    _player2Health.Text = Game.Instance.Ai + ": " + Game.Instance.AiHealth + "%";
                }
            }

            base.OnRender(offset, updateRect);
        }
    }
}