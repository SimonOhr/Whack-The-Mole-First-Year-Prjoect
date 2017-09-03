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

        int posX;
        int posY;
        Vector2 pos;

        Rectangle hitbox;

        int originalValue;

        public bool isActive;

        private bool goingUp;
        private bool goingDown;
        private bool hasReachedPeak;

        public Mole(Texture2D tex, int x, int y)
        {
            this.tex = tex;
            posX = x;
            posY = y;

            pos = new Vector2(posX, posY);

            hitbox = new Rectangle(posX, posY, 106, 80);
            originalValue = (int)pos.Y;
        }

        public void update(GameTime gt)
        {
            hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;

            if (isActive)
            {                             
                if (pos.Y >= (originalValue - 50) && !hasReachedPeak)
                {                    
                    pos.Y -= 5;                    
                }                

                if (pos.Y <= (originalValue - 50))
                {S
                    hasReachedPeak = true;

                    pos.Y += 5;
                }

                if (pos.Y >= originalValue && hasReachedPeak)
                {
                    isActive = false;
                    hasReachedPeak = false;
                }
            }
        }
    



    public void Draw(SpriteBatch sb)
    {
        sb.Draw(tex, pos, Color.White);
    }
}
}
