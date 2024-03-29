using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNATest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Sprite mSprite;
        Sprite mSpriteTwo;
        Sprite mSpriteThree;

        //Sprite mBackgroundOne;
        //Sprite mBackgroundTwo;
        //Sprite mBackgroundThree;
        //Sprite mBackgroundFour;
        //Sprite mBackgroundFive;

        Wizard mWizardSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            mSprite = new Sprite();
            mSpriteTwo = new Sprite();
            mSpriteThree = new Sprite();

            //mBackgroundOne = new Sprite();
            //mBackgroundOne.Scale = 2.0f;

            //mBackgroundTwo = new Sprite();
            //mBackgroundTwo.Scale = 2.0f;

            //mBackgroundThree = new Sprite();
            //mBackgroundThree.Scale = 2.0f;

            //mBackgroundFour = new Sprite();
            //mBackgroundFour.Scale = 2.0f;

            //mBackgroundFive = new Sprite();
            //mBackgroundFive.Scale = 2.0f;

            mWizardSprite = new Wizard();

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

            // TODO: use this.Content to load your game content here
            mSprite.LoadContent(this.Content, "SquareGuy");
            mSprite.Position = new Vector2(125, 245);

            mSpriteTwo.LoadContent(this.Content, "SquareGuy");
            mSpriteTwo.Position = new Vector2(300, 100);

            mSpriteThree.LoadContent(this.Content, "SquareGuy");
            mSpriteThree.Position = new Vector2(500, 100);

            //mBackgroundOne.LoadContent(this.Content, "Background01");
            //mBackgroundOne.Position = new Vector2(0,0);

            //mBackgroundTwo.LoadContent(this.Content, "Background02");
            //mBackgroundTwo.Position = new Vector2(mBackgroundOne.Position.X + mBackgroundOne.Size.Width, 0);

            //mBackgroundThree.LoadContent(this.Content, "Background03");
            //mBackgroundThree.Position = new Vector2(mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width, 0);

            //mBackgroundFour.LoadContent(this.Content, "Background04");
            //mBackgroundFour.Position = new Vector2(mBackgroundThree.Position.X + mBackgroundThree.Size.Width, 0);

            //mBackgroundFive.LoadContent(this.Content, "Background05");
            //mBackgroundFive.Position = new Vector2(mBackgroundFour.Position.X + mBackgroundFour.Size.Width, 0);

            mWizardSprite.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //if (mBackgroundOne.Position.X < -mBackgroundOne.Size.Width)
            //{
            //    mBackgroundOne.Position.X = mBackgroundFive.Position.X + mBackgroundFive.Size.Width;
            //}

            //if (mBackgroundTwo.Position.X < -mBackgroundTwo.Size.Width)
            //{
            //    mBackgroundTwo.Position.X = mBackgroundOne.Position.X + mBackgroundOne.Size.Width;
            //}

            //if (mBackgroundThree.Position.X < -mBackgroundThree.Size.Width)
            //{
            //    mBackgroundThree.Position.X = mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width;
            //}

            //if (mBackgroundFour.Position.X < -mBackgroundFour.Size.Width)
            //{
            //    mBackgroundFour.Position.X = mBackgroundThree.Position.X + mBackgroundThree.Size.Width;
            //}

            //if (mBackgroundFive.Position.X < -mBackgroundFive.Size.Width)
            //{
            //    mBackgroundFive.Position.X = mBackgroundFour.Position.X + mBackgroundFour.Size.Width;
            //}

            //Vector2 aDirection = new Vector2(-1, 0);
            //Vector2 aSpeed = new Vector2(160, 0);

            //mBackgroundOne.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //mBackgroundTwo.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //mBackgroundThree.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //mBackgroundFour.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //mBackgroundFive.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            mWizardSprite.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Begin();

            //mBackgroundOne.Draw(this.spriteBatch);
            //mBackgroundTwo.Draw(this.spriteBatch);
            //mBackgroundThree.Draw(this.spriteBatch);
            //mBackgroundFour.Draw(this.spriteBatch);
            //mBackgroundFive.Draw(this.spriteBatch);

            mSprite.Draw(this.spriteBatch);
            mSpriteTwo.Draw(this.spriteBatch);
            mSpriteThree.Draw(this.spriteBatch);

            mWizardSprite.Draw(this.spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
