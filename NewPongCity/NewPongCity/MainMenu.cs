

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace NewPongCity
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainMenu: Game
    {
        enum GameState
        {
            menu,
            one_player,
            two_player,
            exit,
        }

        GameState _state;


        string[] ballColors = new string[] { "ball", "ballWhite", "ballGreen", "ballSmile" };
        int ballSelector;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D background;
        Rectangle gameBoundaries;
        private Paddle playerPaddle;
        private Paddle computerPaddle;
        private Ball ball;
        private GameObjects gameObjects;
        
        Texture2D logo;
        Texture2D one_player;
        Texture2D two_player;
        Texture2D options;
        Texture2D exit;

        Rectangle _oneplayer;
        Rectangle _two_player;
        Rectangle _options;
        Rectangle _exit;
       // private bool pushedStartGameButton = true;

        public MainMenu()
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
            // TODO: Add your initialization logic here
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
            logo = Content.Load<Texture2D>("pong_city");
            one_player = Content.Load<Texture2D>("button");
            two_player = Content.Load<Texture2D>("button2");
            options = Content.Load<Texture2D>("button3");
            exit = Content.Load<Texture2D>("button4");

            /////////////////////////////////////////////////////////////////////////
            Random rnd = new Random();
            ballSelector = rnd.Next(0, 4);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            var paddleTexture = Content.Load<Texture2D>("Bat");

            background = Content.Load<Texture2D>("Background");
            playerPaddle = new Paddle(paddleTexture, Vector2.Zero, gameBoundaries, PlayerTypes.Human);

            var computerPaddleLocation = new Vector2(gameBoundaries.Width - paddleTexture.Width, 0);

            computerPaddle = new Paddle(paddleTexture, computerPaddleLocation, gameBoundaries, PlayerTypes.Computer);

            ball = new Ball(Content.Load<Texture2D>(ballColors[ballSelector]), Vector2.Zero, gameBoundaries);
            ball.AttachTo(playerPaddle);

            gameObjects = new GameObjects { PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle, Ball = ball };

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
        /// 
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (_state)
            {
                case GameState.one_player:
                    UpdateOnePlayer(gameTime);
                    break;
                case GameState.two_player:
                    UpdateTwoPlayer(gameTime);
                    break;
                default:
                    UpdateMainMenu(gameTime);
                    break;
            }
            
           
            
        }
        void UpdateMainMenu(GameTime gameTime)
        {
                OnClicked();
            
        }

        void UpdateOnePlayer(GameTime gameTime)
        {
            Console.WriteLine("oneplayer");
            gameObjects.TouchInput = new TouchInput();
            GetTouchInput();
            playerPaddle.Update(gameTime, gameObjects);
            computerPaddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);
        }

        void UpdateTwoPlayer(GameTime gameTime)
        {

            Console.WriteLine("twoplayer");
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
            switch (_state)
            {
                
                case GameState.one_player:
                    DrawOnePlayer(gameTime);
                    break;
                case GameState.two_player:
                    DrawTwoPlayer(gameTime);
                    break;
                default:
                    DrawMainMenu(gameTime);
                    break;
            }

        }

     

       void DrawTwoPlayer(GameTime gameTime)
        {
            Console.WriteLine("twoplayer");
          
        }

        void DrawOnePlayer(GameTime gameTime)
        {
            Console.WriteLine("oneplayer");
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, gameBoundaries, Color.White);
            playerPaddle.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();
           
        }

        void DrawMainMenu(GameTime gameTime)
        {
            _state = GameState.menu;
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            Color tintColor = Color.White;
            Rectangle _logo = new Rectangle(700, 20, 1000, 700);
            spriteBatch.Draw(logo, _logo, tintColor);
            _oneplayer = new Rectangle(1000, 700, 450, 100);
            spriteBatch.Draw(one_player, _oneplayer, tintColor);
            _two_player = new Rectangle(1000, 850, 450, 100);
            spriteBatch.Draw(two_player, _two_player, tintColor);
            _options = new Rectangle(1000, 1000, 450, 100);
            spriteBatch.Draw(options, _options, tintColor);
            _exit = new Rectangle(1050, 1150, 350, 100);
            spriteBatch.Draw(exit, _exit, tintColor);
            spriteBatch.End();

        }

        


        public void OnClicked()
        {
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.Tap && _oneplayer.Contains(gesture.Position))
                {
                    _state = GameState.one_player;

                }
                else if(gesture.GestureType == GestureType.Tap && _two_player.Contains(gesture.Position))
                {
                    _state = GameState.two_player;
                }
                else if (gesture.GestureType == GestureType.Tap && _options.Contains(gesture.Position))
                {

                }
                else if (gesture.GestureType == GestureType.Tap && _exit.Contains(gesture.Position))
                {
                    Exit();
                }
            }
        }

        private void GetTouchInput()
        {
            while (TouchPanel.IsGestureAvailable)
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
    }


}
