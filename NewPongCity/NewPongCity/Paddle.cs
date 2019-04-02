using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace NewPongCity
{
    public class Paddle : Sprite
    {
        private readonly Rectangle screenBounds;
        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds) : base(texture, location, screenBounds, gameObjects)
        {
            this.screenBounds = screenBounds;
        }

        public static GameObjects gameObjects { get; }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            //Move paddle up
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || gameObjects.TouchInput.Up )
                Velocity = new Vector2(0, -5f);
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || gameObjects.TouchInput.Down)
                Velocity = new Vector2(0, 5f);
            base.Update(gameTime, gameObjects);
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