//-----------------------------------------------------------------------------
// FreeCamera.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@gmail.com)
// Dizajn Ruben: Petran (rubenpetran@gmail.com)
// Copyright (C)219 GAME STUDIO. All rights reserved.
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Legenda_o_Zmaju
{
    public class FreeCamera : Camera
    {
        private Vector3 angle;
        private float speed;
        private float turnSpeed;
        public Vector3 forward;
        public Vector3 left;

        #region Properties
        public float TurnSpeed
        {
            get { return turnSpeed; }
            set { turnSpeed = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Vector3 Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        #endregion

        public FreeCamera(Vector3 position)
            : base(position)
        {
            speed = 250f;
            turnSpeed = 90f;
        }

        public FreeCamera(Vector3 position, float speed, float turnSpeed)
            : base(position)
        {
            this.speed = speed;
            this.turnSpeed = turnSpeed;
        }

        
        public void Update(int Width, float TotalSeconds)
        {
            int center = Width / 2;

            float delta = TotalSeconds;
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);


            if (gamePad.IsConnected)
            {
                angle.X -= gamePad.ThumbSticks.Right.Y * turnSpeed * 0.001f;
                angle.Y += gamePad.ThumbSticks.Right.X * turnSpeed * 0.001f;

                forward = Vector3.Normalize(new Vector3((float)Math.Sin(-angle.Y), (float)Math.Sin(angle.X), (float)Math.Cos(-angle.Y)));
                left = Vector3.Normalize(new Vector3((float)Math.Cos(angle.Y), 0f, (float)Math.Sin(angle.Y)));

                position -= forward * speed * gamePad.ThumbSticks.Left.Y * delta;
                position += left * speed * gamePad.ThumbSticks.Left.X * delta;

                View = Matrix.Identity;
                View *= Matrix.CreateTranslation(-position);
                View *= Matrix.CreateRotationZ(angle.Z);
                View *= Matrix.CreateRotationY(angle.Y);
                View *= Matrix.CreateRotationX(angle.X);
            }
            else
            {
                KeyboardState keyboard = Keyboard.GetState();
                MouseState mouse = Mouse.GetState();

                int centerX = center;
                int centerY = center;

                Mouse.SetPosition(centerX, centerY);

                angle.X += MathHelper.ToRadians((mouse.Y - centerY) * turnSpeed * 0.01f); // pitch
                angle.Y += MathHelper.ToRadians((mouse.X - centerX) * turnSpeed * 0.01f); // yaw

                forward = Vector3.Normalize(new Vector3((float)Math.Sin(-angle.Y), (float)Math.Sin(angle.X), (float)Math.Cos(-angle.Y)));
                left = Vector3.Normalize(new Vector3((float)Math.Cos(angle.Y), 0f, (float)Math.Sin(angle.Y)));

                if (keyboard.IsKeyDown(Keys.W))
                    position -= forward * speed * delta;

                if (keyboard.IsKeyDown(Keys.S))
                    position += forward * speed * delta;

                if (keyboard.IsKeyDown(Keys.A))
                    position -= left * speed * delta;

                if (keyboard.IsKeyDown(Keys.D))
                    position += left * speed * delta;

                if (keyboard.IsKeyDown(Keys.Z))
                    position += Vector3.Down * speed * delta;

                if (keyboard.IsKeyDown(Keys.X))
                    position += Vector3.Up * speed * delta;

                View = Matrix.Identity;
                View *= Matrix.CreateTranslation(-position);
                View *= Matrix.CreateRotationZ(angle.Z);
                View *= Matrix.CreateRotationY(angle.Y);
                View *= Matrix.CreateRotationX(angle.X);
            }
        }

        public Matrix UpdateReflectiveMatrices()
        {
            Matrix reflectiveViewMatrix;
            reflectiveViewMatrix = Matrix.Identity;
            reflectiveViewMatrix *= Matrix.CreateTranslation(new Vector3(-position.X, position.Y - 10f, -position.Z));
            reflectiveViewMatrix *= Matrix.CreateRotationZ(angle.Z);
            reflectiveViewMatrix *= Matrix.CreateRotationY(angle.Y);
            reflectiveViewMatrix *= Matrix.CreateRotationX(-angle.X);
            return reflectiveViewMatrix;
        }
        
    }
}
