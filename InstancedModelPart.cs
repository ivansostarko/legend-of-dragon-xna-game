//-----------------------------------------------------------------------------
// InstancedModelPart.cs
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
class InstancedModelPart
{

    #region Properties

    private int _primitiveCount;
    public int PrimitiveCount
    {
        get { return _primitiveCount; }
    }

    private int _vertexCount;
    public int VertexCount
    {
        get { return _vertexCount; }
    }

    private int _vertexStride;
    public int VertexStride
    {
        get { return _vertexStride; }
    }

    private VertexDeclaration _vertexDeclaration;
    public VertexDeclaration VertexDeclaration
    {
        get { return _vertexDeclaration; }
    }

    private VertexBuffer _vertexBuffer;
    public VertexBuffer VertexBuffer
    {
        get { return _vertexBuffer; }
    }

    private IndexBuffer _indexBuffer;
    public IndexBuffer IndexBuffer
    {
        get { return _indexBuffer; }
    }

    private Game game;
    private VertexElement[] originalVertexDeclaration;

    #endregion

    #region Constructors

    public InstancedModelPart(Game game, ModelMeshPart part, VertexBuffer vertexBuffer, IndexBuffer indexBuffer)
    {
        this.game = game;
        _primitiveCount = part.PrimitiveCount;
        _vertexCount = part.NumVertices;
        _vertexStride = part.VertexStride;
        _vertexDeclaration = part.VertexDeclaration;
        _vertexBuffer = vertexBuffer;
        _indexBuffer = indexBuffer;

        originalVertexDeclaration = part.VertexDeclaration.GetVertexElements();

        InitializeHardwareInstancing();
    }

    #endregion

    #region Private Methods

    private void InitializeHardwareInstancing()
    {
        VertexElement[] extraElements = new VertexElement[4];

        short offset = 0;
        byte usageIndex = 1;
        short stream = 1;

        short sizeOfVector4 = sizeof(float) * 4;

        for (int i = 0; i < extraElements.Length; i++)
        {
            extraElements[i] = new VertexElement(stream, offset,
                                                 VertexElementFormat.Vector4,
                                                 VertexElementMethod.Default,
                                                 VertexElementUsage.TextureCoordinate,
                                                 usageIndex);
            offset += sizeOfVector4;
            usageIndex++;
        }

        ExtendVertexDeclaration(extraElements);
    }

    private void ExtendVertexDeclaration(VertexElement[] extraElements)
    {
        _vertexDeclaration.Dispose();
        int length = originalVertexDeclaration.Length + extraElements.Length;

        VertexElement[] elements = new VertexElement[length];

        originalVertexDeclaration.CopyTo(elements, 0);
        extraElements.CopyTo(elements, originalVertexDeclaration.Length);
        _vertexDeclaration = new VertexDeclaration(game.GraphicsDevice, elements);
    }

    #endregion

}
}
