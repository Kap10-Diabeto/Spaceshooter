using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceshooter
{
    class Enemy_B:Enemyclass
    {
        public Enemy_B(Texture2D tex) : base(1)
        {
            speed = (int)EnemySpeed.Slow;
            texture = tex;
            hp = 5;
            position = new Vector2(ran.Next(0, 1440), -100);
            hitBox = new Rectangle((int)position.X, (int)position.Y, 70, 70);
        }
    }

}

