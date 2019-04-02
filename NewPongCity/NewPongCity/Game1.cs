using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace NewPongCity
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Paddle paddle;
        private Ball ball;
        private GameObjects gameObjects;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.VerticalDrag | GestureType.Flick | GestureType.Tap;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            paddle = new Paddle(Content.Load<Texture2D>("Bat"), Vector2.Zero, new Rectangle(0,0, Window.ClientBounds.Width, Window.ClientBounds.Height));
            ball = new Ball(Content.Load<Texture2D>("ball"), Vector2.Zero);
            ball.AttachTo(paddle);

            gameObjects = new GameObjects { Paddle = paddle, Ball = ball };

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            gameObjects.TouchInput = new TouchInput();
            GetTouchInput();

            // TODO: Add your update logic here
            paddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);

            base.Update(gameTime);
        }

        private void GetTouchInput()
        {
            while(TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                if (gesture.Delta.Y > 0)
                    gameObjects.TouchInput.Down = true;

                if (gesture.Delta.Y < 0)
                    gameObjects.TouchInput.Up = true;

                if (gesture.GestureType == GestureType.Tap)
                    gameObjects.TouchInput.Tapped = true;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            paddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
