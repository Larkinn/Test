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

namespace Test
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
    
        Matrix view;
        Matrix proj;

        Model box;
        Texture2D boxTexture;
        Texture2D avatarTexture;

        Vector3 avatarPosition = new Vector3(0, 0, -20);
        Vector3 avatarHeadOffset = new Vector3(0, 10, 0);

        float avatarYaw;

        Vector3 cameraReference = new Vector3(0, 0, 10);
        Vector3 thirdPersonReference = new Vector3(0, 500, -500);

        float rotationSpeed = 1f / 60f;
        float forwardSpeed = 50f / 60f;

        static float viewAngle = MathHelper.PiOver4;

        static float nearClip = 1.0f;
        static float farClip = 2000.0f;

        int cameraState;

        

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
            this.IsMouseVisible = true;

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
            box = Content.Load<Model>("box");
            boxTexture = Content.Load<Texture2D>("boxtexture");
            avatarTexture = Content.Load<Texture2D>("boxtexture");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
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
            GetCurrentCamera();
            UpdateAvatarPosition();

            base.Update(gameTime);
        }

        public void GetCurrentCamera()
        {
            cameraState = 2;
        }

        public void UpdateAvatarPosition()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            //if(keyboardState.IsKeyDown(Keys.Left))
            if(mouseState.RightButton == ButtonState.Pressed)
            {
                avatarYaw += rotationSpeed;
            }

            //if(keyboardState.IsKeyDown(Keys.Right))
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                avatarYaw -= rotationSpeed;
            }

            // Forward
            if(keyboardState.IsKeyDown(Keys.W))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(avatarYaw);
                Vector3 v = new Vector3(0, 0, forwardSpeed);
                v = Vector3.Transform(v, forwardMovement);
                avatarPosition.Z += v.Z;
                avatarPosition.X += v.X;
            }

            // Backwards
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(avatarYaw);
                Vector3 v = new Vector3(0, 0, -forwardSpeed);
                v = Vector3.Transform(v, forwardMovement);
                avatarPosition.Z += v.Z;
                avatarPosition.X += v.X;
            }

            // Left
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Matrix movement = Matrix.CreateRotationX(avatarYaw);
                Vector3 v = new Vector3(forwardSpeed, 0, 0);
                v = Vector3.Transform(v, movement);
                avatarPosition.X += v.X;
            }

            // Right
            if (keyboardState.IsKeyDown(Keys.D))
            {
                Matrix movement = Matrix.CreateRotationX(avatarYaw);
                Vector3 v = new Vector3(-forwardSpeed, 0, 0);
                v = Vector3.Transform(v, movement);
                avatarPosition.X += v.X;
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
            UpdateCameraThirdPerson();

            DrawBoxes();

            Matrix world = Matrix.CreateRotationY(avatarYaw) * Matrix.CreateTranslation(avatarPosition);
            DrawModel(box, world, avatarTexture);

            base.Draw(gameTime);
        }

        public void UpdateCameraThirdPerson()
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(avatarYaw);
            Vector3 transformedReference = Vector3.Transform(thirdPersonReference, rotationMatrix);
            Vector3 cameraPosition = transformedReference + avatarPosition;
            view = Matrix.CreateLookAt(cameraPosition, avatarPosition, new Vector3(0.0f, 1.0f, 0.0f));
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;
            proj = Matrix.CreatePerspectiveFieldOfView(viewAngle, aspectRatio, nearClip, farClip);
        }

        public void DrawBoxes()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    DrawModel(box, Matrix.CreateTranslation(j * 60, 0, i * 60), boxTexture);
                }
            }
        }

        public void DrawModel(Model model, Matrix world, Texture2D texture)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect b in mesh.Effects)
                {
                    b.Projection = proj;
                    b.View = view;
                    b.World = world;
                    b.Texture = texture;
                    b.TextureEnabled = true;
                }
                mesh.Draw();
            }
        }




    }
}
