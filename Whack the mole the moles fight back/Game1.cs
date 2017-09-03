using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Whack_the_mole_the_moles_fight_back
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D moleTex;
        Texture2D moleKOTex;   
        Texture2D holeTex;
        Texture2D holeForegroundTex;
        Texture2D backgroundTex;
        Texture2D malletTex;
        Texture2D lightningTex;

        Vector2 moleArrayPos;

        Mole mole;

        Mole[,] moleArray;

        double timer, reset;
        float interval = 1;

        Random rnd = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            this.Window.Position = new Point(0, 0);
            graphics.PreferredBackBufferHeight = 1600;
            graphics.PreferredBackBufferWidth = 1250;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            moleTex = Content.Load<Texture2D>(@"MOLE_mole");
            moleKOTex = Content.Load<Texture2D>(@"MOLE_mole_KO");
            holeTex = Content.Load<Texture2D>(@"MOLE_hole");
            holeForegroundTex = Content.Load<Texture2D>(@"MOLE_hole_foreground");
            backgroundTex = Content.Load<Texture2D>(@"MOLE_background");
            malletTex = Content.Load<Texture2D>(@"MOLE_mallet");
            lightningTex = Content.Load<Texture2D>(@"MOLE_lightning");

            IsMouseVisible = true;

            moleArray = new Mole[7, 10];

            for (int i = 0; i < moleArray.GetLength(0); i++)
            {
                for (int j = 0; j < moleArray.GetLength(1); j++)
                {
                    int x = j * (moleTex.Width + 50);
                    int y = 200 + (i * (moleTex.Height + 50));
                    moleArray[i, j] = new Mole(moleTex, x, y);
                }
                
            }

        }

       
        protected override void UnloadContent()
        {
           
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > interval)
            {
                int molePosY = rnd.Next(0, moleArray.GetLength(0));
                int molePosX = rnd.Next(0, moleArray.GetLength(1));
                moleArray[molePosY, molePosX].isActive = true;

                timer = reset;
            }

            foreach (Mole m in moleArray)
            {
                m.update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTex, new Vector2(0, 0), Color.White);
            for (int i = 0; i < moleArray.GetLength(0); i++)
            {
                for (int j = 0; j < moleArray.GetLength(1); j++)
                {
                    moleArray[i, j].Draw(spriteBatch);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
