//-----------------------------------------------------------------------------
// Water.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@gmail.com)
// Dizajn Ruben: Petran (rubenpetran@gmail.com)
// Copyright (C)219 GAME STUDIO. All rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
//using System.Device;

namespace Legenda_o_Zmaju
{
    public class Water
    {
        private RenderTarget2D refractionRT;
        private Texture2D refractionMap;
        private RenderTarget2D reflectionRT;
        private Texture2D reflectionMap;
        private Matrix reflectionViewMatrix;
        public GraphicsDevice device;
        public float waterHeight;
        private int WIDTH;
        private int HEIGHT;
        public Vector3 position;
        private Effect effect;
        private Texture2D waterBumpMap;
        private VertexPositionTexture[] waterVertices;
        public delegate void RenderRefractionHandler();
        public delegate void RenderReflectionHandler(Matrix view, Vector3 reflectionCamPosition);

        public Water(int width, int height, Vector3 position, float waterHeight)
        {
            this.WIDTH = width;
            this.HEIGHT = height;
            this.position = position;
            this.waterHeight = waterHeight;
        }

        public void Initialize(GraphicsDevice device, ContentManager content)
        {
            this.device = device;
            refractionRT = new RenderTarget2D(device, 512, 512, 1, SurfaceFormat.Color);
            reflectionRT = new RenderTarget2D(device, 512, 512, 1, SurfaceFormat.Color);
            effect = content.Load<Effect>("Effects/Water");
            waterBumpMap = content.Load<Texture2D>("Textures/waterbump");
            SetUpWaterVertices();
        }

        private void SetUpWaterVertices()
        {
            waterVertices = new VertexPositionTexture[6];

            waterVertices[0] = new VertexPositionTexture(new Vector3(0, waterHeight, 0), new Vector2(0, 1));
            waterVertices[1] = new VertexPositionTexture(new Vector3(WIDTH, waterHeight, HEIGHT), new Vector2(1, 0));
            waterVertices[2] = new VertexPositionTexture(new Vector3(0, waterHeight, HEIGHT), new Vector2(0, 0));

            waterVertices[3] = new VertexPositionTexture(new Vector3(0, waterHeight, 0), new Vector2(0, 1));
            waterVertices[4] = new VertexPositionTexture(new Vector3(WIDTH, waterHeight, 0), new Vector2(1, 1));
            waterVertices[5] = new VertexPositionTexture(new Vector3(WIDTH, waterHeight, HEIGHT), new Vector2(1, 0));
        }

        public void DrawRefractionMap(Camera camera, RenderRefractionHandler render)
        {
            Vector3 planeNormalDirection = new Vector3(0, -1, 0);
            planeNormalDirection.Normalize();
            Vector4 planeCoefficients = new Vector4(planeNormalDirection, waterHeight);

            Matrix camMatrix = camera.View * camera.Projection;
            Matrix invCamMatrix = Matrix.Invert(camMatrix);
            invCamMatrix = Matrix.Transpose(invCamMatrix);

            planeCoefficients = Vector4.Transform(planeCoefficients, invCamMatrix);
            Plane refractionClipPlane = new Plane(planeCoefficients);

  //          device.ClipPlanes[0].Plane = refractionClipPlane;
//            device.ClipPlanes[0].IsEnabled = true;

            //device.SetRenderTarget(0, refractionRT);
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            
            render.Invoke();

    //        device.SetRenderTarget(0, null);
            refractionMap = refractionRT.GetTexture();
      //      device.ClipPlanes[0].IsEnabled = false;
        }

        public void DrawReflectionMap(Camera camera, RenderReflectionHandler render)
        {
            reflectionViewMatrix = ((FreeCamera)camera).UpdateReflectiveMatrices();
            float reflectionCamYCoord = -camera.Position.Y + (waterHeight * 2);
            Vector3 reflectionCamPosition = new Vector3(camera.Position.X, reflectionCamYCoord, camera.Position.Z);

            Vector3 planeNormalDirection = new Vector3(0, 1, 0);
            planeNormalDirection.Normalize();
            Vector4 planeCoefficients = new Vector4(planeNormalDirection, -3f);

            Matrix camMatrix = reflectionViewMatrix * camera.Projection;
            Matrix invCamMatrix = Matrix.Invert(camMatrix);
            invCamMatrix = Matrix.Transpose(invCamMatrix);

            planeCoefficients = Vector4.Transform(planeCoefficients, invCamMatrix);
            Plane refractionClipPlane = new Plane(planeCoefficients);

            //device.ClipPlanes[0].Plane = refractionClipPlane;
            //device.ClipPlanes[0].IsEnabled = true;
            //device.SetRenderTarget(0, reflectionRT);
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            
            render.Invoke(reflectionViewMatrix, reflectionCamPosition);

            device.SetRenderTarget(0, null);
            reflectionMap = reflectionRT.GetTexture();

            device.ClipPlanes[0].IsEnabled = false;
        }

        public void DrawWater(Camera camera, GameTime gameTime)
        {
            effect.CurrentTechnique = effect.Techniques["Water"];
            Matrix worldMatrix = Matrix.CreateTranslation(position - new Vector3(WIDTH/2,0,HEIGHT/2));
            effect.Parameters["World"].SetValue(worldMatrix);
            effect.Parameters["View"].SetValue(camera.View);
            effect.Parameters["Projection"].SetValue(camera.Projection);
            effect.Parameters["ReflectionView"].SetValue(reflectionViewMatrix);
            effect.Parameters["WaterBumpMap"].SetValue(waterBumpMap);
            effect.Parameters["ReflectionMap"].SetValue(reflectionMap);
            effect.Parameters["RefractionMap"].SetValue(refractionMap);
            effect.Parameters["waveLength"].SetValue(0.1f);
            effect.Parameters["waveHeight"].SetValue(0.3f);
            effect.Parameters["CamPos"].SetValue(camera.Position);
            effect.Parameters["dullColor"].SetValue(Color.LightCyan.ToVector4());
            effect.Parameters["dullBlendFactor"].SetValue(0.2f);
            float elapsedTime = (float)gameTime.TotalGameTime.TotalMilliseconds / 100000.0f;
            effect.Parameters["Time"].SetValue(elapsedTime);
            effect.Parameters["windDirection"].SetValue(Matrix.CreateRotationZ(MathHelper.PiOver2));

            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                device.VertexDeclaration = new VertexDeclaration(device, VertexPositionTexture.VertexElements);
                device.DrawUserPrimitives(PrimitiveType.TriangleList, waterVertices, 0, 2);
                pass.End();
            }
            effect.End();

        }

    }
}
