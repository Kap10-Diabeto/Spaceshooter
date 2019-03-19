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


        public Player(Texture2D tex)
        {
            texture = tex;
            position = new Vector2(400, 300);
            hitBox = new Rectangle((int)position.X, (int)position.Y, 60, 60);


        }

        public override void Update()
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
        }

    }
}
