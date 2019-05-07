using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceshooter
{
    class Bullet:Basklass
    {
        public Bullet(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            position = pos;
            hitBox = new Rectangle((int)position.X, (int)position.Y, 20, 20);
        }
        public override void Update()
        {
            position.Y -= speed;
            hitBox.Y = (int)position.Y;
            if(position.Y < -100)
            {
                isDead = true;
            }
        }
    }
}
