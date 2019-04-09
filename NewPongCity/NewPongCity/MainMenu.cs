﻿

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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D logo;
        Texture2D one_player;
        Texture2D two_player;
        Texture2D options;
        Texture2D exit;

        Rectangle topLeftOfSprite;
        Rectangle topLeftOfSprite2;
        Rectangle topLeftOfSprite3;
        Rectangle topLeftOfSprite4;

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
        /// 
        protected override void Update(GameTime gameTime)
        {


            OnClicked();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            Color tintColor = Color.White;
            Rectangle topLeftOfSprite1 = new Rectangle(700, 20, 1000, 700);
            spriteBatch.Draw(logo, topLeftOfSprite1, tintColor);

            topLeftOfSprite = new Rectangle(1000, 700, 450, 100);
            spriteBatch.Draw(one_player, topLeftOfSprite, tintColor);

            topLeftOfSprite2 = new Rectangle(1000, 850, 450, 100);
            spriteBatch.Draw(two_player, topLeftOfSprite2, tintColor);
            topLeftOfSprite3 = new Rectangle(1000, 1000, 450, 100);
            spriteBatch.Draw(options, topLeftOfSprite3, tintColor);
           topLeftOfSprite4 = new Rectangle(1050, 1150, 350, 100);
            spriteBatch.Draw(exit, topLeftOfSprite4, tintColor);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void OnClicked()
        {
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.Tap && topLeftOfSprite.Contains(gesture.Position))
                {
                    Console.WriteLine("ok");

                }
                else if(gesture.GestureType == GestureType.Tap && topLeftOfSprite2.Contains(gesture.Position))
                {

                }
                else if (gesture.GestureType == GestureType.Tap && topLeftOfSprite3.Contains(gesture.Position))
                {

                }
                else if (gesture.GestureType == GestureType.Tap && topLeftOfSprite4.Contains(gesture.Position))
                {
                    Exit();
                }
            }
        }
    }


}
