using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace froggerMonogameChristianRusso
{
    class Player : spriteGeneric
    {
        private int vie = 5;
        private bool estMort = false;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        public Player(Game1 game) : base(game)
        {

        }

        public int Vie { get => vie; set => vie = value; }
        public bool EstMort { get => estMort; set => estMort = value; }

        public void Respawn()
        {
            
            positionSprite.X = _game.GraphicsDevice.Viewport.Width / 2 - 64;
            positionSprite.Y = 990;
            estMort = true;
        }
        public void Update(GameTime gameTime)
        {
            
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            if (visible == true)
            {
                if (currentKeyboardState.IsKeyDown(Keys.W))
                {

                    positionSprite.Y -= 10;
                }
                if (currentKeyboardState.IsKeyDown(Keys.S))
                {
                    positionSprite.Y += 10;
                }
                if (currentKeyboardState.IsKeyDown(Keys.A))
                {
                    positionSprite.X -= 10;
                }
                if (currentKeyboardState.IsKeyDown(Keys.D))
                {
                    positionSprite.X += 10;
                }
                positionSprite.X = MathHelper.Clamp(positionSprite.X, 0, _game.GraphicsDevice.Viewport.Width - textureSprite.Width);
                positionSprite.Y = MathHelper.Clamp(positionSprite.Y, 0, _game.GraphicsDevice.Viewport.Height - textureSprite.Height);
            }

            

            base.Update();
        }
    }
}
