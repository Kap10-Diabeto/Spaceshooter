using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceshooter
{
    class Player : Basklass
    {
        //Bestämmer spelarens koppling till skott. Ritar ut och bestämmer hur snabbt man skjuter och laddar
        Texture2D BulletTexture; 
        float firerate = 0.1f;
        float reloading = 0;
        int HP;

        public int Health //Ger spelaren hp
        {
            get { return HP; }
      
        }

        public Player(Texture2D tex, Texture2D Bt) //Ritar ut spelaren och skotten samt ger spelaren en storlek och bestämmer var den ska "spawna in"
        {
            texture = tex;
            BulletTexture = Bt;
            position = new Vector2(700, 700);
            hitBox = new Rectangle((int)position.X, (int)position.Y, 60, 60);
            HP = 3;

        }

        public override void Update() ////Uppdaterar spelarens alla metoder och funktioner. Dvs det gör så att allt körs som det ska
        {
            KeyboardState state = Keyboard.GetState();

            position = Mouse.GetState().Position.ToVector2();
            if (position.X < 0)
                position.X = 0;
            if (position.X + hitBox.Width > Game1.Viewport.Width)
                position.X = Game1.Viewport.Width - hitBox.Width;
            if (position.Y + hitBox.Height > Game1.Viewport.Height)
                position.Y = Game1.Viewport.Height - hitBox.Height;
            hitBox.Location = position.ToPoint();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && reloading >= firerate)
            {
                Game1.Llist.Add(new Bullet(BulletTexture, new Vector2(position.X + hitBox.Size.X / 2 - 5, position.Y)));
                reloading = 0;
            }
            else
            {
                reloading += (float)Game1.GameTime.ElapsedGameTime.TotalSeconds;
            }
            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;

        }

        public void LooseHP() //Ger spelaren förmågan att förlora HP
        {
            HP--;
        }

    }
}
