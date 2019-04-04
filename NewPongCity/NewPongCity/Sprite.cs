using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace NewPongCity
{
    public abstract class Sprite
    {
        protected readonly Texture2D texture;
        public Vector2 Location;
        protected readonly Rectangle gameBoundaries;

        public int Width
        {
            get { return texture.Width; }
        }
        public int Height
        {
            get { return texture.Height; }
        }
        public Vector2 Velocity { get; protected set; }

        public Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries, GameObjects gameObjects)
        {
            this.texture = texture;
            this.Location = location;
            this.gameBoundaries = gameBoundaries;
            Velocity = Vector2.Zero;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += Velocity;

            CheckBounds();
        }
        protected abstract void CheckBounds();
    }

}