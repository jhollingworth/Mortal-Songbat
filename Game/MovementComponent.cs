#region

using System;
using System.Threading;
using GarageGames.Torque.Core;
using GarageGames.Torque.Platform;
using GarageGames.Torque.Sim;
using GarageGames.Torque.T2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MortalSongbat.GUI;
using Action = MortalSongbat.GUI.Action;

#endregion

namespace MortalSongbat
{
    [TorqueXmlSchemaType]
    [TorqueXmlSchemaDependency(Type = typeof (T2DPhysicsComponent))]
    public class MovementComponent : TorqueComponent, ITickObject
    {
        public Orientation Orientation { get; set; }
        public Action Action { get; set; }

        public int PlayerNumber
        {
            get { return _playerNumber; }
            set { _playerNumber = value; }
        }

        public void InterpolateTick(float k)
        {
        }

        public void ProcessTick(Move move, float elapsed)
        {
            if (move != null)
            {
                // set our test object's Velocity based on stick/keyboard input
                _sceneObject.Physics.VelocityX = move.Sticks[0].X*20.0f;

                if(_sceneObject.Physics.VelocityX < 0)
                {
                    Orientation = Orientation.Left;

                    Action = Action.Walking;
                    _sceneObject.FlipX = false;
                }
                else if (_sceneObject.Physics.VelocityX > 0)
                {
                    Orientation = Orientation.Right;
                    Action = Action.Walking;
                    _sceneObject.FlipX = true;

                }
                else
                {
                    Action = Action.Standing;   
                }
            }
        }

        protected override bool _OnRegister(TorqueObject owner)
        {
            if (!base._OnRegister(owner) || !(Owner is T2DSceneObject))
                return false;

            // retain a reference to this component's owner object
            _sceneObject = owner as T2DSceneObject;
            _SetupInputMap(_sceneObject, _playerNumber, "gamepad" + _playerNumber, "keyboard");

            // tell the process list to notifiy us with ProcessTick and InterpolateTick events
            ProcessList.Instance.AddTickCallback(Owner, this);

            return true;
        }

        private void _OnBackButton(float val)
        {
            if (val > 0.0f)
                Game.Instance.Exit();
        }


        private void _SetupInputMap(TorqueObject player, int playerIndex, String gamePad, String keyboard)
        {
            // Set player as the controllable object
            PlayerManager.Instance.GetPlayer(playerIndex).ControlObject = player;

            // Get input map for this player and configure it
            var inputMap = PlayerManager.Instance.GetPlayer(playerIndex).InputMap;

            var gamepadId = InputManager.Instance.FindDevice(gamePad);
            if (gamepadId >= 0)
            {
                inputMap.BindMove(gamepadId, (int) XGamePadDevice.GamePadObjects.LeftThumbX,
                                  MoveMapTypes.StickAnalogHorizontal, 0);

                inputMap.BindCommand(gamepadId, (int)XGamePadDevice.GamePadObjects.LeftThumbY, Jump, null);

                inputMap.BindAction(gamepadId, (int) XGamePadDevice.GamePadObjects.Back, _OnBackButton);
            }

            // keyboard controls
            var keyboardId = InputManager.Instance.FindDevice(keyboard);
            if (keyboardId >= 0)
            {
                inputMap.BindMove(keyboardId, (int) Keys.Right, MoveMapTypes.StickDigitalRight, 0);
                inputMap.BindMove(keyboardId, (int) Keys.Left, MoveMapTypes.StickDigitalLeft, 0);
                inputMap.BindCommand(keyboardId, (int)Keys.Up, Jump, null);
                inputMap.BindMove(keyboardId, (int) Keys.Down, MoveMapTypes.StickDigitalDown, 0);
                // WASD
                inputMap.BindMove(keyboardId, (int) Keys.D, MoveMapTypes.StickDigitalRight, 0);
                inputMap.BindMove(keyboardId, (int) Keys.A, MoveMapTypes.StickDigitalLeft, 0);
                inputMap.BindCommand(keyboardId, (int) Keys.W, Jump, null);
                inputMap.BindMove(keyboardId, (int) Keys.S, MoveMapTypes.StickDigitalDown, 0);
                inputMap.BindCommand(keyboardId, (int) Keys.Space, Jump, null);
                inputMap.BindCommand(keyboardId, (int) Keys.F, Flower, null);
            }
        }

        private void Jump()
        {
            if(_sceneObject.Position.Y > 26)
            {
                _sceneObject.Physics.ApplyImpulse(new Vector2(0, -3));
            }
        }

        private void Flower()
        {
            ((T2DAnimatedSprite) _sceneObject).PlayAnimation(
                TorqueObjectDatabase.Instance.FindObject<T2DAnimationData>("FlowerAnimation"), true
            );
        }

        //======================================================

        private T2DSceneObject _sceneObject;
        private int _playerNumber;
    }
}