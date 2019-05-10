using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceshooter
{
    class Enemy_A: Enemyclass //Bestämmer atributen för Enemy A
    {
        public Enemy_A(Texture2D tex) : base(1)
        {
            speed = (int)EnemySpeed.Fast; //Snabb fart
            texture = tex;
            position = new Vector2(ran.Next(0, 1440),-100); //Bestämmer var fienden ska spawna in
            hitBox = new Rectangle((int)position.X, (int)position.Y, 50, 50);  //Bestämmer storleken på fienden och dess hitbox
        }
    }
}
