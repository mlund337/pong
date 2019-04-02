﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NewPongCity
{
    public class Ball : Sprite
    {
        private Paddle attachedToPaddle;
        private static GameObjects gameObjects;

        public Ball(Texture2D texture, Vector2 location) : base(texture, location, gameObjects)
        {

        }
        protected override void CheckBounds()
        {
            
        }
        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if((Keyboard.GetState().IsKeyDown(Keys.Space) || gameObjects.TouchInput.Tapped) && attachedToPaddle != null)
            {
                var newVelocity = new Vector2(5f, attachedToPaddle.Velocity.Y);
                Velocity = newVelocity;
                attachedToPaddle = null;
            }
            if(attachedToPaddle != null)
            {
                Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width;
                Location.Y = attachedToPaddle.Location.Y;
            }
            
            base.Update(gameTime, gameObjects);
        }

        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
        }
    }
}