using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace NewPongCity
{

    public enum PlayerTypes
    {
        Human, 
        Computer,
        Player2
        //Player2 --Add player2 to enum
    }
    public class Paddle : Sprite
    {
        public PlayerTypes playerType { get; set; }

        private readonly Rectangle screenBounds;

        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds, PlayerTypes playerType) : base(texture, location, screenBounds, gameObjects)
        {
            this.playerType = playerType;
            this.screenBounds = screenBounds;
        }

        public static GameObjects gameObjects { get; }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {

            if (playerType == PlayerTypes.Computer)
            {
                if (gameObjects.Ball.Location.Y + gameObjects.Ball.Height < Location.Y)
                    Velocity = new Vector2(0, -5f);

                if (gameObjects.Ball.Location.Y > Location.Y + Height)
                    Velocity = new Vector2(0, 5f);
            }

            //Move paddle up

            if (playerType == PlayerTypes.Human)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || gameObjects.TouchInput.Up)
                    Velocity = new Vector2(0, -5f);
                if (Keyboard.GetState().IsKeyDown(Keys.Right) || gameObjects.TouchInput.Down)
                    Velocity = new Vector2(0, 5f);
            }

            if (playerType == PlayerTypes.Player2)
            {
                Location = new Vector2(Location.X, gameObjects.Position);

            }
            base.Update(gameTime, gameObjects);
        }
        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, screenBounds.Height - texture.Height);
        }
        public float GetYPosition()
        {
            return Location.Y;
        }

    }

}