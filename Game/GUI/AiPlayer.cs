using System;
using GarageGames.Torque.Core;
using GarageGames.Torque.T2D;
using Microsoft.Xna.Framework;

namespace MortalSongbat.GUI
{
    public class AiPlayer
    {
        private readonly T2DAnimatedSprite _player;
        private readonly T2DAnimatedSprite _actualPlayer;

        private int _count;
        private Random _random;

        public AiPlayer(T2DAnimatedSprite player, T2DAnimatedSprite actualPlayer)
        {
            _player = player;
            _actualPlayer = actualPlayer;

            _count = 0;
            _random = new Random(0);
        }

        public void Render()
        {
            if (_random.Next(100) % 2 == 0)
            {
                if (_player.Position.X < _actualPlayer.Position.X)
                {
                    _player.Physics.ApplyImpulse(new Vector2(1, 0));
                    _player.FlipX = true;
                }
                else if (_player.Position.X > _actualPlayer.Position.X)
                {
                    _player.Physics.ApplyImpulse(new Vector2(-1, 0));
                    _player.FlipX = false;
                }
            }

            if (Math.Abs(_player.Position.X - _actualPlayer.Position.X) < 5)
            {
                if(_random.Next(100) % 2 == 0 && _count % 50 == 0)
                {
                        _player.PlayAnimation(
                        TorqueObjectDatabase.Instance.FindObject<T2DAnimationData>(Game.Instance.Ai + "SpecialMove"),
                        true
                    );
                }
                else
                {
                    if (Math.Abs(_player.Position.X - _actualPlayer.Position.X) < 1)
                    {
                        if (_player.Position.X > _actualPlayer.Position.X)
                        {
                            _player.Physics.ApplyImpulse(new Vector2(3, 0));
                            _player.FlipX = true;
                        }
                        else if (_player.Position.X < _actualPlayer.Position.X)
                        {
                            _player.Physics.ApplyImpulse(new Vector2(-3, 0));
                            _player.FlipX = false;
                        }
                    }
                }

            }


            if (_player.Physics.VelocityY != 0)
            {
                _player.Physics.ApplyImpulse(new Vector2(0, 1));
            }

            _count++;
        }
    }
}
