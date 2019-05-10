using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Spaceshooter //Basklassen för fienderna 
{

    enum EnemySpeed //Skapar en enumfunktion med en varibel för långsam hastighet, och en med snabb 
    {
        Slow = 1 ,
        Fast = 2
    }

    public class Enemyclass : Basklass //Ärver av basklass
    {
        protected Random ran = new Random(); //Ger alla fiender som ärver av klassen random
        protected int hp; //Ger alla fiender som ärver av klassen hp

        public Enemyclass(int hp)
        {
            this.hp = hp;
        }

        public override void Update() //Uppdaterar alla fiender som ärver av klassens speed och position
        {
            position.Y += speed;
            hitBox.Y = (int)position.Y;
        }

        public void TakeDmg() // //Ger alla fiender som ärver av klassen förmågan att ta skada och "dö"
        {
            hp--;
            if (hp <= 0)
                isDead = true;
        }
    }

}
