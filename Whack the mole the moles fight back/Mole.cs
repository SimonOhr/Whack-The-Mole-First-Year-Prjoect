using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whack_the_mole_the_moles_fight_back
{
    class Mole
    {
        Texture2D tex;
        Texture2D knockedTex;

        public int posX { get; private set; }
        int posY;       
        Vector2 pos;

        int heightInterval = 120;
        int ishitSpeed = 4;

        public Rectangle hitbox;

        public int originalPos { get; private set; }

        public bool isActive;
        public bool isHit;        

        private bool hasReachedPeak;

        public Mole(Texture2D tex, Texture2D knockedTex, int x, int y)
        {
            this.tex = tex;
            this.knockedTex = knockedTex;
            posX = x;
            posY = y;

            pos = new Vector2(posX, posY);

            hitbox = new Rectangle((int)pos.X + 15, (int)pos.Y, tex.Width - 30 , tex.Height - 70);  // offsets due to sprite
            originalPos = (int)pos.Y;            
        }

        public void update(GameTime gt)
        {          
            hitbox.Y = (int)pos.Y + 50; // offset due to sprite

            if (isActive)
            {
                if (pos.Y >= (originalPos - heightInterval) && !hasReachedPeak && !isHit)
                    pos.Y -= 1;
                
                else if (pos.Y <= originalPos && !isHit )
                {
                    hasReachedPeak = true;                    
                    pos.Y += 1;
                }
                if (isHit)
                    pos.Y += ishitSpeed;
                
                if (pos.Y > originalPos && hasReachedPeak || pos.Y > originalPos && isHit)
                {
                    isHit = false;
                    isActive = false; 
                }
            }
            if (!isActive)
            {
                pos.Y = originalPos;
                isHit = false;
                hasReachedPeak = false;
            }            
        }

        public void Draw(SpriteBatch sb)
        {
           // sb.Draw(Game1.debuggRec, hitbox, Color.White);

            if (!isHit)
                sb.Draw(tex, pos, Color.White);
            else
                sb.Draw(knockedTex, pos, Color.White);          
        }
    }
}
