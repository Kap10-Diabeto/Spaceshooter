using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Spaceshooter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Space;
        Player Player1;
        Texture2D ST;
        Texture2D startBG;
        bool mainMenu = true;
        public static Viewport Viewport
        {
            get;
            private set;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1440;
            graphics.PreferredBackBufferHeight = 810;
            graphics.ApplyChanges();
            Viewport = new Viewport(0, 0, 1440, 810);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Space = Content.Load<Texture2D>("SpaceB");
            startBG = Content.Load<Texture2D>("SpaceB");
            Texture2D x = Content.Load<Texture2D>("Player1");
            ST = Content.Load<Texture2D>("starttext");
            Player1 = new Player(x);

            Color[] color = new Color[startBG.Width * startBG.Height];
            startBG.GetData(color);
            for (int i = 0; i < color.Length; i++)
            {
                color[i] *= .5f;
                color[i].A = 255;
            }
            startBG.SetData(color);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!mainMenu)
            {

                Player1.Update();

                base.Update(gameTime);

            }

            else
            {
                if (Keyboard.GetState().GetPressedKeys().Length>0 )
                {
                    mainMenu = false;
                    Color[] color = new Color[startBG.Width * startBG.Height];
                    startBG.GetData(color);
                    for (int i = 0; i < color.Length; i++)
                    {
                        color[i] *= 2f;
                        color[i].A = 255;
                    }
                    startBG.SetData(color);
                }
            }

        }
     

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (!mainMenu)
            {
                spriteBatch.Draw(Space, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                Player1.Draw(spriteBatch);
                spriteBatch.End();

            }
            else
            {
                spriteBatch.Draw(startBG, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.Draw(ST, new Rectangle(425, 400, 572, 63), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);

        }
    }
}
