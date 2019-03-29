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
        public static List<Basklass> Llist = new List<Basklass>();
        Player Player1;
        Texture2D ST;
        Texture2D startBG;
        Texture2D buster;
        List<Buster> busters = new List<Buster>();
        bool mainMenu = true;
        KeyboardState oldState;

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
            Texture2D b = Content.Load<Texture2D>("Bullet");
            ST = Content.Load<Texture2D>("starttext");
            Player1 = new Player(x);
            buster = Content.Load<Texture2D>("Buster");

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
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                busters.Add(new Buster(buster, 6, 1, Player1.Position + new Vector2(12,0)));

                if (!mainMenu)
            {

                Player1.Update();

                base.Update(gameTime);

                foreach (Buster item in busters)
                {
                    item.Update();
                }

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
            oldState = Keyboard.GetState();

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

            foreach (var item in busters)
            {
                item.Draw(spriteBatch);
            }

            Bullet b;
            foreach (var item in Llist){
                if (item is Bullet)
                {
                    b = item as Bullet;
                }
            }

        }

        void Bullethit()
        {
            List<Basklass> BULLET = Llist.Where(x => x is Bullet).ToList();
            for (int i = 0; i < BULLET.Count; i++)
            {
                for (int j = 0; j < Llist.Count; j++)
                {
                    if (BULLET[i].HitBox.Intersects(Llist[j].HitBox)  && !(Llist[j] is Bullet))
                    {
                        BULLET[i].IsDead = true;
                        (Llist[j] as Enemyclass).TaSkada();
                       
                    }
                }
               
            }
        }

        void RemoveEnemy()
        {
            List<Basklass> temp = new List<Basklass>();
            for (int j = 0; j < Llist.Count; j++)
            {
                if (!Llist[j].IsDead)
                    temp.Add(Llist[j]);
               // else if (Llist[j] is Enemyclass)

                   // explosion.Add(new Explosion(explosion, 9, 9, new Vector2(Llist[j].HitBox.X - 20, Llist[j].HitBox.Y - 10)));
            }
        }
    }
}
