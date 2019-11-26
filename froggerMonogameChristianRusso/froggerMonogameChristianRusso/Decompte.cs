using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace froggerMonogameChristianRusso
{
    class Decompte
    {
        protected Vector2 positionTexte;
        protected SpriteFont font;
        private float timer = 3f;
        private bool finish = false;
        protected Game game;
        public bool visible = true;

        public float Timer { get => timer; set => timer = value; }
        public bool Finish { get => finish; set => finish = value; }

        public Decompte(Game1 _game)
        {
            game = _game;
        }
        public void LoadContent(string fontname, Vector2 position)
        {
            font = game.Content.Load<SpriteFont>(fontname);
            positionTexte = position;
        }
        public void Update(GameTime gameTime)
        {
            
            if (Finish == false)
            {
                Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                visible = false;
            }
            
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible == true)
            {
                spriteBatch.DrawString(font, Math.Round(Timer).ToString(), positionTexte, Color.White);
            }
            
        }
        
    }
}
