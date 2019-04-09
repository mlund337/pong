using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Collections.Concurrent;
using NewPongCity.Shared;

namespace NewPongCity
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        string[] ballColors = new string[] { "ball", "ballWhite", "ballGreen", "ballSmile" };
        int ballSelector;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Vector2 fontLocation;
        Texture2D background;
        Rectangle gameBoundaries;
        private Paddle playerPaddle;
        private Paddle computerPaddle;
        private Ball ball;
        private GameObjects gameObjects;

        //Network players
        ConcurrentQueue<string> _messageQueue;
        string _lastMessage;
        PlayerData[] _otherPlayers;
        PlayerData _self;

        LiteNetNetworkClient netClient;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            //Multiplayer
            _messageQueue = new ConcurrentQueue<string>();
            _otherPlayers = new PlayerData[2];

            for (int i = 0; i < _otherPlayers.Length; i++)
            {
                //init self
                _otherPlayers[i] = new PlayerData();
                _otherPlayers[i].Location = new Point(0, 0);
                _otherPlayers[i].IsPresent = false;
                _otherPlayers[i].PlayerId = i + 1;
            }
        }

        public PlayerData GetPlayerData() => _self;
        public PlayerData[] GetPlayers() => _otherPlayers;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.VerticalDrag | GestureType.Flick | GestureType.Tap;

            //Multiplayer
            netClient = new LiteNetNetworkClient();
            netClient.AddLoggingFunc(AddToMessageQueue);
            netClient.RegisterPlayerHandler(GetPlayerData);
            netClient.RegisterAllPlayersHandler(GetPlayers);
            netClient.Start();

            //init self
            _self = new PlayerData();
            _self.Location = new Point(20, 20);

            base.Initialize();

        }


        public void AddToMessageQueue(string message)
        {
            _messageQueue.Enqueue(message);
        }
        public string GetLastMessage()
        {
            if (_messageQueue.IsEmpty)
            {
                return _lastMessage;
            }

            string temp;
            bool success = _messageQueue.TryDequeue(out temp);
            if (success)
            {
                _lastMessage = temp;
            }

            return _lastMessage;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Random rnd = new Random();
            ballSelector = rnd.Next(0, 4);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            var paddleTexture = Content.Load<Texture2D>("Bat");

            background = Content.Load<Texture2D>("Background");
            playerPaddle = new Paddle(paddleTexture, Vector2.Zero, gameBoundaries, PlayerTypes.Human);

            var computerPaddleLocation = new Vector2(gameBoundaries.Width - paddleTexture.Width, 0);

            computerPaddle = new Paddle(paddleTexture, computerPaddleLocation, gameBoundaries, PlayerTypes.Player2);

            ball = new Ball(Content.Load<Texture2D>(ballColors[ballSelector]), Vector2.Zero, gameBoundaries);
            ball.AttachTo(playerPaddle);

            gameObjects = new GameObjects { PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle, Ball = ball };
           

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
            gameObjects.Position = _otherPlayers[0].Location.Y;
            Console.WriteLine("Holaa " + playerPaddle.GetYPosition());
            SendClientUpdate((int)playerPaddle.GetYPosition());
            // TODO: Add your update logic here
            playerPaddle.Update(gameTime, gameObjects);
            computerPaddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);
            netClient.Update(gameTime, (int)playerPaddle.GetYPosition());
            base.Update(gameTime);
        }

        //private void SendClientUpdate(PlayerActions playerAction)
        private void SendClientUpdate(int playerAction)
        {
            //we send actions rather than direct keys because the player could have remapped the keys to whatever they find useful
            // server only cares about what action the client took.
            netClient.SendClientActions(playerAction);
        }
        private void GetTouchInput()
        {
            while(TouchPanel.IsGestureAvailable)
            {

                
                var gesture = TouchPanel.ReadGesture();
                if (gesture.Delta.Y > 0) { 
                    gameObjects.TouchInput.Down = true;
                }

                if (gesture.Delta.Y < 0) { 
                    gameObjects.TouchInput.Up = true;
                }
                if (gesture.GestureType == GestureType.Tap)
                    gameObjects.TouchInput.Tapped = true;

                
            }
            
        }
        protected Texture2D GetTexture(string name)
        {
            return Content.Load<Texture2D>(name);
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
            spriteBatch.Draw(background, gameBoundaries, Color.White);
            playerPaddle.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
