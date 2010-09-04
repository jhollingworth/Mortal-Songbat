using System;
using GarageGames.Torque.T2D;
using Microsoft.Xna.Framework;

namespace MortalSongbat.GUI
{
    public class AiPlayer
    {
        private readonly T2DSceneObject _player;

        private int _count;

        public AiPlayer(T2DSceneObject player)
        {
            _player = player;

            _count = 0;
        }

        public void Render()
        {
            if (_count%60 == 0)
            {
                _player.Physics.ApplyImpulse(new Vector2(-3, 0));
            }

            if (_player.Physics.VelocityX > 0)
            {
                _player.Physics.ApplyImpulse(new Vector2(-5, 0));
            }

            if (_player.Physics.VelocityY > 0)
            {
                _player.Physics.ApplyImpulse(new Vector2(0, -1));
            }

        _count++;
        }
    }
}
