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
        int points = 0;
        int highscore;
        Texture2D startBG;
        Texture2D buster;
        Texture2D EA;
        Texture2D HPHeart;
        SpriteFont pointsfont;
        List<Explosion> explosions = new List<Explosion>();
        Texture2D explosion;
        List<Buster> busters = new List<Buster>();
        Random ran = new Random();
        bool mainMenu = true;
        const string FILEPATH = @"C:\Users\Admin\Desktop\Spaceshooter-master\Spaceshooter-master\score.txt";
        KeyboardState oldState;

        public static Viewport Viewport
        {
            get;
            private set;
        }
        public static GameTime GameTime
        {
            get;
            private set;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (File.Exists("score.txt"))
            {
                StreamReader sr = new StreamReader("score.txt");
                string s = sr.ReadLine();
                int.TryParse(s, out highscore);
                sr.Close();
            }

            else
            {
                File.Create("score.txt");
                StreamWriter sw = new StreamWriter("score.txt");
                highscore = 0;
                sw.WriteLine("0");
                sw.Close();
            }
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
            HPHeart = Content.Load<Texture2D>("HPH");
            pointsfont = Content.Load<SpriteFont>("Pointsfont");
            Player1 = new Player(x, b);
            buster = Content.Load<Texture2D>("Buster");
            EA = Content.Load<Texture2D>("EnemyA");
            Llist.Add(new Enemy_A(EA));
            explosion = Content.Load<Texture2D>("BOOM");

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
            GameTime = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                busters.Add(new Buster(buster, 6, 1, Player1.Position + new Vector2(12, 0)));

            if (!mainMenu)
            {

                Player1.Update();

                base.Update(gameTime);
                foreach (var item in Llist)
                {
                    item.Update();
                }
                int slump = ran.Next(0,230);
                if (slump < 10)
                {
                    Llist.Add(new Enemy_A(EA));
                }


                foreach (Buster item in busters)
                {
                    item.Update();
                }
                foreach (Basklass item in Llist)
                {
                    item.Update();
                }

                Bullethit();
                RemoveEnemy();

                foreach (Explosion item in explosions)
                {
                    item.Update();
                }
                Death();
            }

            else
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
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

                foreach (var item in busters)
                {
                    item.Draw(spriteBatch);
                }

                foreach (var item in Llist)
                {
                    item.Draw(spriteBatch);
                }

                for (int i = 1; i <= Player1.Health; i++)
                {
                    spriteBatch.Draw(HPHeart, new Rectangle(30 * i, 30, 30, 30), Color.White);
                }

                spriteBatch.DrawString(pointsfont, points.ToString(), new Vector2(Window.ClientBounds.Width - 50, 30), Color.White);

                spriteBatch.End();

            }
            else
            {
                spriteBatch.Draw(startBG, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.Draw(ST, new Rectangle(425, 400, 572, 63), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);

            foreach (var item in explosions)
            {
                item.Draw(spriteBatch);
            }

        }

        void Bullethit()
        {
            List<Basklass> BULLET = Llist.Where(x => x is Bullet).ToList();
            for (int i = 0; i < BULLET.Count; i++)
            {
                for (int j = 0; j < Llist.Count; j++)
                {
                    if (BULLET[i].HitBox.Intersects(Llist[j].HitBox) && !(Llist[j] is Bullet))
                    {
                        BULLET[i].IsDead = true;
                        (Llist[j] as Enemyclass).TakeDmg();
                        points = points + 1;

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
                else if (Llist[j] is Enemyclass)

                explosions.Add(new Explosion(explosion, 9, 9, new Vector2(Llist[j].HitBox.X - 20, Llist[j].HitBox.Y - 10)));
            }
            Llist = temp;
        }

        void Death()
        {
            for (int i = 0; i < Llist.Count; i++)
            {

                if (Llist[i].HitBox.Y > Window.ClientBounds.Height)
                {
                    Llist[i].IsDead = true;
                    Player1.LooseHP();
                    if (Player1.Health == 0)
                    {
                        if (points > highscore)
                        {
                            StreamWriter sw = new StreamWriter("score.txt");
                            sw.Write(points);
                            highscore = points;
                            sw.Close();
                        }


                        Exit();
                    }
                }

            }

        }
    }
}
