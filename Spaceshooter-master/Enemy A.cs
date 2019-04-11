using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceshooter
{
    class Enemy_A: Enemyclass
    {
        public Enemy_A(Texture2D tex) : base(1)
        {
            speed = 1;
            texture = tex;
            position = new Vector2(ran.Next(0, 1440),-50);
            hitBox = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }
    }
}
