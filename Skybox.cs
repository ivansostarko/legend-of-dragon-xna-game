//-----------------------------------------------------------------------------
// Skybox.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@hotmail.com)
// Dizajn Ruben: Petran (rubenpetran@gmail.com)
// Copyright (C)219 GAME STUDIO. All rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Legenda_o_Zmaju
{
public class Skybox
{
    private VertexPositionNormalTexture[] cubeVertices;
    private VertexBuffer vertexBuffer;
    private TextureCube texture;
    private Effect effect;

    #region Initialize
    public Skybox(ContentManager content)
    {
        texture = content.Load<TextureCube>("Textures/Sunny");
        effect = content.Load<Effect>("Effects/skybox");
        //Initialize();
    }

    public void Initialize(GraphicsDevice device)
    {
        cubeVertices = new VertexPositionNormalTexture[36];

        Vector3 topLeftFront = new Vector3(-1.0f, 1.0f, 1.0f);
        Vector3 bottomLeftFront = new Vector3(-1.0f, -1.0f, 1.0f);
        Vector3 topRightFront = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 bottomRightFront = new Vector3(1.0f, -1.0f, 1.0f);
        Vector3 topLeftBack = new Vector3(-1.0f, 1.0f, -1.0f);
        Vector3 topRightBack = new Vector3(1.0f, 1.0f, -1.0f);
        Vector3 bottomLeftBack = new Vector3(-1.0f, -1.0f, -1.0f);
        Vector3 bottomRightBack = new Vector3(1.0f, -1.0f, -1.0f);

        Vector2 textureTopLeft = new Vector2(0.0f, 0.0f);
        Vector2 textureTopRight = new Vector2(1.0f, 0.0f);
        Vector2 textureBottomLeft = new Vector2(0.0f, 1.0f);
        Vector2 textureBottomRight = new Vector2(1.0f, 1.0f);

        Vector3 frontNormal = new Vector3(0.0f, 0.0f, 1.0f);
        Vector3 backNormal = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 topNormal = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 bottomNormal = new Vector3(0.0f, -1.0f, 0.0f);
        Vector3 leftNormal = new Vector3(-1.0f, 0.0f, 0.0f);
        Vector3 rightNormal = new Vector3(1.0f, 0.0f, 0.0f);


        cubeVertices[0] =
            new VertexPositionNormalTexture(
            topLeftFront, frontNormal, textureTopLeft);
        cubeVertices[1] =
            new VertexPositionNormalTexture(
            bottomLeftFront, frontNormal, textureBottomLeft);
        cubeVertices[2] =
            new VertexPositionNormalTexture(
            topRightFront, frontNormal, textureTopRight);
        cubeVertices[3] =
            new VertexPositionNormalTexture(
            bottomLeftFront, frontNormal, textureBottomLeft);
        cubeVertices[4] =
            new VertexPositionNormalTexture(
            bottomRightFront, frontNormal, textureBottomRight);
        cubeVertices[5] =
            new VertexPositionNormalTexture(
            topRightFront, frontNormal, textureTopRight);


        cubeVertices[6] =
            new VertexPositionNormalTexture(
            topLeftBack, backNormal, textureTopRight);
        cubeVertices[7] =
            new VertexPositionNormalTexture(
            topRightBack, backNormal, textureTopLeft);
        cubeVertices[8] =
            new VertexPositionNormalTexture(
            bottomLeftBack, backNormal, textureBottomRight);
        cubeVertices[9] =
            new VertexPositionNormalTexture(
            bottomLeftBack, backNormal, textureBottomRight);
        cubeVertices[10] =
            new VertexPositionNormalTexture(
            topRightBack, backNormal, textureTopLeft);
        cubeVertices[11] =
            new VertexPositionNormalTexture(
            bottomRightBack, backNormal, textureBottomLeft);


        cubeVertices[12] =
            new VertexPositionNormalTexture(
            topLeftFront, topNormal, textureBottomLeft);
        cubeVertices[13] =
            new VertexPositionNormalTexture(
            topRightBack, topNormal, textureTopRight);
        cubeVertices[14] =
            new VertexPositionNormalTexture(
            topLeftBack, topNormal, textureTopLeft);
        cubeVertices[15] =
            new VertexPositionNormalTexture(
            topLeftFront, topNormal, textureBottomLeft);
        cubeVertices[16] =
            new VertexPositionNormalTexture(
            topRightFront, topNormal, textureBottomRight);
        cubeVertices[17] =
            new VertexPositionNormalTexture(
            topRightBack, topNormal, textureTopRight);


        cubeVertices[18] =
            new VertexPositionNormalTexture(
            bottomLeftFront, bottomNormal, textureTopLeft);
        cubeVertices[19] =
            new VertexPositionNormalTexture(
            bottomLeftBack, bottomNormal, textureBottomLeft);
        cubeVertices[20] =
            new VertexPositionNormalTexture(
            bottomRightBack, bottomNormal, textureBottomRight);
        cubeVertices[21] =
            new VertexPositionNormalTexture(
            bottomLeftFront, bottomNormal, textureTopLeft);
        cubeVertices[22] =
            new VertexPositionNormalTexture(
            bottomRightBack, bottomNormal, textureBottomRight);
        cubeVertices[23] =
            new VertexPositionNormalTexture(
            bottomRightFront, bottomNormal, textureTopRight);


        cubeVertices[24] =
            new VertexPositionNormalTexture(
            topLeftFront, leftNormal, textureTopRight);
        cubeVertices[25] =
            new VertexPositionNormalTexture(
            bottomLeftBack, leftNormal, textureBottomLeft);
        cubeVertices[26] =
            new VertexPositionNormalTexture(
            bottomLeftFront, leftNormal, textureBottomRight);
        cubeVertices[27] =
            new VertexPositionNormalTexture(
            topLeftBack, leftNormal, textureTopLeft);
        cubeVertices[28] =
            new VertexPositionNormalTexture(
            bottomLeftBack, leftNormal, textureBottomLeft);
        cubeVertices[29] =
            new VertexPositionNormalTexture(
            topLeftFront, leftNormal, textureTopRight);


        cubeVertices[30] =
            new VertexPositionNormalTexture(
            topRightFront, rightNormal, textureTopLeft);
        cubeVertices[31] =
            new VertexPositionNormalTexture(
            bottomRightFront, rightNormal, textureBottomLeft);
        cubeVertices[32] =
            new VertexPositionNormalTexture(
            bottomRightBack, rightNormal, textureBottomRight);
        cubeVertices[33] =
            new VertexPositionNormalTexture(
            topRightBack, rightNormal, textureTopRight);
        cubeVertices[34] =
            new VertexPositionNormalTexture(
            topRightFront, rightNormal, textureTopLeft);
        cubeVertices[35] =
            new VertexPositionNormalTexture(
            bottomRightBack, rightNormal, textureBottomRight);

        vertexBuffer = new VertexBuffer(
            device,
            VertexPositionNormalTexture.SizeInBytes * cubeVertices.Length, BufferUsage.None);

        vertexBuffer.SetData<VertexPositionNormalTexture>(cubeVertices);

    }

    #endregion

    #region Draw
    public void Draw(GraphicsDevice device, Vector3 pos, Matrix viewProj) {
        device.VertexDeclaration = new VertexDeclaration(
            device,
            VertexPositionNormalTexture.VertexElements);

        effect.CurrentTechnique = effect.Techniques["RenderScene"];
        effect.Parameters["WorldViewProjection"].SetValue(Matrix.CreateTranslation(pos) * viewProj);
        effect.Parameters["textureDiffuse"].SetValue(texture);

        effect.Begin();


        foreach (EffectPass pass in effect.CurrentTechnique.Passes)
        {
            pass.Begin();

            device.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionNormalTexture.SizeInBytes);

            device.DrawPrimitives(
                PrimitiveType.TriangleList,
                0,
                12
            );

            pass.End();
        }
        effect.End();
    }

    #endregion
}
}
