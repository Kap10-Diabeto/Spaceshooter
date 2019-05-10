using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Spaceshooter
{
    public class Game1 : Game  //Här deklareras alla klassvariabler i koden
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
        Texture2D EB;
        Texture2D HPHeart;
        SpriteFont pointsfont;
        List<Explosion> explosions = new List<Explosion>();
        Texture2D explosion;
        List<Buster> busters = new List<Buster>();
        Random ran = new Random();
        bool mainMenu = true;
        //const string FILEPATH = @"C:\Users\Admin\Desktop\Spaceshooter-master\Spaceshooter-master\score.txt";
        KeyboardState oldState;

        public static Viewport Viewport //Gör så att man kommer åt storleken på skrämen
        {
            get;
            private set;
        }

        public static GameTime GameTime // Hur länge applikationen är igång och hur långt det är mellan varje frame
        {
            get;
            private set;
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (File.Exists("score.txt")) //Om filen score finns så sparas poängen ifall det är mer än det värdet som redan finns i filen
            {
                StreamReader sr = new StreamReader("score.txt");
                string s = sr.ReadLine();
                int.TryParse(s, out highscore);
                sr.Close();
            }

            else //Om filen score inte finns skapas den och får värdet 0
            {
                File.Create("score.txt");
                StreamWriter sw = new StreamWriter("score.txt");
                highscore = 0;
                sw.WriteLine("0");
                sw.Close();
            }
        }

        protected override void Initialize() //Bestämmer viewportens värden
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1440;
            graphics.PreferredBackBufferHeight = 810;
            graphics.ApplyChanges();
            Viewport = new Viewport(0, 0, 1440, 810);

            base.Initialize();
        }

        protected override void LoadContent() //Laddar in alla klassvariabler och ger dem en sprite/en bild/ett utseende
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
            EB = Content.Load<Texture2D>("EnemyB");
            Llist.Add(new Enemy_A(EA));
            explosion = Content.Load<Texture2D>("BOOM");

            Color[] color = new Color[startBG.Width * startBG.Height]; //Skapar en tonad startskärm
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

        protected override void Update(GameTime gameTime) //Uppdaterar alla metoder och funktioner. Dvs det gör så att allt körs som det ska
        {
            GameTime = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                busters.Add(new Buster(buster, 6, 1, Player1.Position + new Vector2(12, 0)));

            if (!mainMenu) //Om man inte är i main menu, körs allt nedan, alltså spelet
            {

                Player1.Update();

                base.Update(gameTime);
                foreach (var item in Llist)
                {
                    item.Update();
                }
                int slump = ran.Next(0,230);
                if (slump < 6)
                {
                    Llist.Add(new Enemy_A(EA));
                }

                if (slump < 1)
                {
                    Llist.Add(new Enemy_B(EB));
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
                Busterhit();
                RemoveEnemy();

                foreach (Explosion item in explosions)
                {
                    item.Update();
                }
                Death();
            }

            else // Om spelet är i mainmenu, är allt pausat med en tonad bakgrund
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


        protected override void Draw(GameTime gameTime) //Ritar ut det som behöver ritas ut
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (!mainMenu) //Ritar ut allt nedan ifall du inte är i mainmenu
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
            else //Ritar ut den tonade startbakgrunden och texten "Press any button to begin!"
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

        void Bullethit() //Berättar vad som ska hända när ett skott träffar. Fienden tar skada och spelaren får poäng
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

        void Busterhit() //Berättar vad som ska hända när en buster träffar. Fienden tar skada och spelaren får poäng
        {
            List<Basklass> Buster = Llist.Where(x => x is Buster).ToList();
            for (int i = 0; i < Buster.Count; i++)
            {
                for (int j = 0; j < Llist.Count; j++)
                {
                    if (Buster[i].HitBox.Intersects(Llist[j].HitBox) && !(Llist[j] is Buster))
                    {
                        Buster[i].IsDead = true;
                        (Llist[j] as Enemyclass).TakeDmg();
                        points = points + 1;

                    }
                }

            }

        }

        void RemoveEnemy() //Tar bort fienden och ritar ut en explosion när fienden blir "dödad"
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

        void Death() //Berättar vad som ska hända när spelaren förlorat alla sina hjärtan. Poängen jämförs och sparas om det är en ny highscore och stängs sedan av
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
