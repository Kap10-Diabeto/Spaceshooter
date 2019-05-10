using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceshooter
{
    public class Basklass //Här samlas attribut som är gemensamt för alla andra klasser
    {
        protected Texture2D texture; //Skapar ett texturattribut
        protected Vector2 position; //Ger en position
        protected Rectangle hitBox; //Ger en hitbox
        protected float speed = 20; //Ger en hastighet
        protected bool isDead = false; //Ger möjligheten att tas bort

        public Vector2 Position
        {
            get { return position; }
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public Rectangle HitBox
        {
            get { return hitBox; }
        }


        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, Color.White);
        }

    }
}
