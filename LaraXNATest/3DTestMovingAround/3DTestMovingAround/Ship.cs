using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace _3DTestMovingAround
{
    public class Ship
    {
        #region Constants
        const string assetName = "Models\\p1_wedge";
        const int shipSpeed = 1000;

        const int moveUp = -1;
        const int moveDown = 1;

        const int moveLeft = -1;
        const int moveRight = 1;

        const int moveForward = -1;
        const int moveBackwards = 1;
        #endregion

        // Set the 3D model to draw.
        Model ship;

        // The aspect ratio determines how to scale 3D to 2D projection.
        float aspectRatio;

        // Set the position of the model in world space.
        Vector3 modelPosition = Vector3.Zero;

        // Set the position of the camera in world space, for our view matrix.
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);

        // Set the velocity of the model, applied each frame to the model's position.
        Vector3 modelVelocity = Vector3.Zero;

        // Set the model speed.
        Vector3 modelSpeed = Vector3.Zero;

        // Set the model direction.
        Vector3 modelDirection = Vector3.Zero;

        // ################
        Matrix view;
        Matrix proj;
        float avatarYaw;
        Vector3 thirdPersonReference = new Vector3(0.0f, 500.0f, 5000.0f);
        float rotationSpeed = 1f / 60f;
        float forwardSpeed = 50f / 60f;
        static float viewAngle = MathHelper.PiOver4; // Set the field of view of the camera in radians
        static float nearClip = 1.0f;
        static float farClip = 2000.0f;
        int CameraState;
        bool cameraStateKeydown;        

        // ###
        // Camera offsett vector from the avatar. An offset of the form (0, N, -N)
        // will put the camera above and behind the avatar at a 45-degree angle.
        //Vector3 thirdPersonReference = new Vector3(0, 500, -500);

        // ###
        // A rotation Matrix for the direction that the avatar is facing. The rotating
        // Matrix is created by CreateRotationY, using the avatar's current rotation as its parameter.
        //Matrix rotationMatrix = Matrix.CreateRotationY(avatarYaw);

        // ################

        public void LoadContent(ContentManager contentManager, GraphicsDeviceManager graphics)
        {
            ship = contentManager.Load<Model>(assetName);
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // ################
            GetCurrentCamera();
            // ################

            UpdateMovement(currentKeyboardState); // Get some input.
            
            modelPosition += modelDirection * modelSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void UpdateMovement(KeyboardState currentKeyboardState)
        {
            modelSpeed = Vector3.Zero;
            modelDirection = Vector3.Zero;

            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                // Left
                modelSpeed.X = shipSpeed;
                modelDirection.X = moveLeft;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                // Right
                modelSpeed.X = shipSpeed;
                modelDirection.X = moveRight;
            }

            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                // Up
                modelSpeed.Z = shipSpeed;
                modelDirection.Z = moveForward;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                // Down
                modelSpeed.Z = shipSpeed;
                modelDirection.Z = moveBackwards;
            }
        }

        public void Draw()
        {
            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[ship.Bones.Count];
            ship.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple mashes, so loop.
            foreach (ModelMesh mesh in ship.Meshes)
            {
                // This is where the mesh orientation is set,
                // as well as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
                }

                mesh.Draw();
            }
        }

        public void GetCurrentCamera()
        {
        }
    }
}
