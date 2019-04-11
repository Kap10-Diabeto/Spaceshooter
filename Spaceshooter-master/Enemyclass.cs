using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Spaceshooter
{
    public class Enemyclass : Basklass
    {
        protected Random ran = new Random();
        protected int hp;

        public Enemyclass(int hp)
        {
            this.hp = hp;
        }

        public override void Update()
        {
            position.Y += speed;
            hitBox.Y = (int)position.Y;
        }

        public void TakeDmg()
        {
            hp--;
            if (hp <= 0)
                isDead = true;
        }
    }

}
