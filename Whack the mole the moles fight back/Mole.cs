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

        public Rectangle hitbox;

        public int originalValue { get; private set; }

        public bool isActive;
        public bool isHit;

        private bool goingUp;
        private bool goingDown;
        private bool hasReachedPeak;

        public Mole(Texture2D tex, Texture2D knockedTex, int x, int y)
        {
            this.tex = tex;
            this.knockedTex = knockedTex;
            posX = x;
            posY = y;

            pos = new Vector2(posX, posY);

            hitbox = new Rectangle(x, y, 106, 80);
            originalValue = (int)pos.Y;
        }

        public void update(GameTime gt)
        {
           // hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;

            if (isActive)
            {
                if (pos.Y >= (originalValue - 120) && !hasReachedPeak && !isHit)
                    pos.Y -= 1;

                else if (pos.Y <= originalValue && !isHit)
                {
                    hasReachedPeak = true;

                    pos.Y += 1;
                }

                else if (isHit)
                    pos.Y += 4;

                if (pos.Y >= originalValue && hasReachedPeak)
                {
                    isActive = false;
                    hasReachedPeak = false;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (!isHit)
                sb.Draw(tex, pos, Color.White);
            else
                sb.Draw(knockedTex, pos, Color.White);

        }
    }
}
