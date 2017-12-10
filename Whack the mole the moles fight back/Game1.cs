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

        Mole[,] moles;

        double timer, reset;
        float interval = 1;

        Random rnd = new Random();

        MouseState mouseState, oldMouseState;
      

        Color backgroundColor;

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

            moles = new Mole[7, 10];

            for (int i = 0; i < moles.GetLength(0); i++)
            {
                for (int j = 0; j < moles.GetLength(1); j++)
                {
                    int x = j * (moleTex.Width + 50);
                    int y = 200 + (i * (moleTex.Height + 50));
                    moles[i, j] = new Mole(moleTex, moleKOTex, x, y);
                }                
            }
            backgroundColor = new Color(111, 209, 72);           
        }

       
        protected override void UnloadContent()
        {
           
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            Activate(gameTime);
            CheckButtonState();
            foreach (Mole m in moles)
            {
                m.update(gameTime);
            }
            base.Update(gameTime);
        }

        private void Activate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            
            if (timer > interval)
            {
                int waveSize = rnd.Next(0, 5);
                do
                {
                    int molePosY = rnd.Next(0, moles.GetLength(0));
                    int molePosX = rnd.Next(0, moles.GetLength(1));
                    moles[molePosY, molePosX].isActive = true;
                    waveSize--;
                } while (waveSize > 0);                                                         
                timer = reset;
            }
        }
        private void CheckButtonState()
        {
           
            mouseState = Mouse.GetState();
            for (int i = 0; i < moles.GetLength(0); i++)
            {
                for (int j = 0; j < moles.GetLength(1); j++)
                {
                    if(moles[i,j].hitbox.Contains(mouseState.Position)
                        && mouseState.LeftButton == ButtonState.Pressed
                        && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        moles[i, j].isHit = true;
                    }
                    
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTex, new Vector2(0, 0), Color.White);
            for (int i = 0; i < moles.GetLength(0); i++)
            {
                for (int j = 0; j < moles.GetLength(1); j++)
                {
                    moles[i, j].Draw(spriteBatch);
                    spriteBatch.Draw(holeTex, new Vector2(moles[i, j].posX, moles[i, j].originalValue), Color.White);
                    spriteBatch.Draw(holeForegroundTex, new Vector2(moles[i, j].posX, moles[i, j].originalValue), Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
