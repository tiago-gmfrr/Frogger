using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using froggerMonogameChristianRusso;

namespace froggerMonogameChristianRusso
{
    class spriteGeneric
    {
        public Texture2D textureSprite;
        public Vector2 positionSprite;
        public Rectangle rectangleSprite;
        protected Game _game;
        public bool visible = true;


        public spriteGeneric(Game game)
        {
            _game = game;
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            textureSprite = texture;
            positionSprite = position;


        }
        public void LoadContent()
        {

        }
        public void Update()
        {
            rectangleSprite = new Rectangle((int)positionSprite.X, (int)positionSprite.Y, textureSprite.Width, textureSprite.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(textureSprite, positionSprite, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}

