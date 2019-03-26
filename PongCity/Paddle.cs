using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace PongCity
{
    public class Paddle : Sprite
    {
        private readonly Rectangle screenBounds;
        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds) : base(texture, location)
        {
            this.screenBounds = screenBounds;
        }

        public override void Update(GameTime gameTime)
        {
            //Move paddle up
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                Velocity = new Vector2(0, -5f);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Velocity = new Vector2(0, 5f);
            base.Update(gameTime);
        }
        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, screenBounds.Height - texture.Height);
        }

    }

    public abstract class Sprite
    {
        protected readonly Texture2D texture;
        public Vector2 Location;
        public int Width
        {
            get { return texture.Width; }
        }
        public int Height
        {
            get { return texture.Height; }
        }
        public Vector2 Velocity { get; protected set; }

        public Sprite(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            this.Location = location;
            Velocity = Vector2.Zero;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            Location += Velocity;

            CheckBounds();
        }
        protected abstract void CheckBounds();
    }
}