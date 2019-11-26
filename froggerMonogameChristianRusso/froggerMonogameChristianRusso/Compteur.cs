using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace froggerMonogameChristianRusso
{
    class Compteur
    {
        protected Vector2 positionTexte;
        protected SpriteFont font;
        private float timer = 0;       
        protected Game game;
        public bool actif = true;
        private int minute = 0;
        string afficheTemps = "";
        string score = "";

        public float Timer { get => timer; set => timer = value; }
        public int Minute { get => minute; set => minute = value; }
        public string AfficheTemps { get => afficheTemps; set => afficheTemps = value; }
        public string Score { get => score; set => score = value; }

        public Compteur(Game1 _game)
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
            AfficheTemps = "Votre temps " + Minute + ":";
            score = Minute.ToString() + ".";
            if (actif == true)
            {
                Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
                       
            if (Timer < 60)
            {
                if (Timer < 9)
                {
                    score += "0";
                    AfficheTemps += "0";
                }
               score += Math.Round(Timer).ToString();
                AfficheTemps += Math.Round(Timer).ToString();
            }
            else
            {
                Minute++;
                Timer = 0;
            }
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
               
                spriteBatch.DrawString(font, AfficheTemps, positionTexte, Color.Black);
            

        }

    }
}
