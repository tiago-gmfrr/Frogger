using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace froggerMonogameChristianRusso
{
    class PickUp : spriteGeneric
    {
        private float respawnTime = 3;
        private int vitesse = 4;
        public PickUp(Game1 game) : base(game)
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
