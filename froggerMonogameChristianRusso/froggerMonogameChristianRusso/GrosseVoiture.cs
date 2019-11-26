using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace froggerMonogameChristianRusso
{
    class GrosseVoiture : spriteGeneric
    {
        private float respawnTime = 2;
        private int vitesse = 5;
        public GrosseVoiture(Game1 game) : base(game)
        {

        }

        public float RespawnTime { get => respawnTime; set => respawnTime = value; }
        public int Vitesse { get => vitesse; set => vitesse = value; }

        public void Update(GameTime gameTime)
        {
            positionSprite.X -= Vitesse;
            if (positionSprite.X < -300)
            {
                visible = false;
            }

            base.Update();
        }
    }
}
