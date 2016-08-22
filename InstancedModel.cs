//-----------------------------------------------------------------------------
// InstancedModel.cs
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
class InstancedModel
{
    #region Properties

    public List<InstancedModelPart> ModelParts = new List<InstancedModelPart>();

    private List<Matrix> instanceMatrices;

    private Model _baseModel;
    public Model BaseModel
    {
        get { return _baseModel; }
    }

    private int _numInstances;
    public int NumInstances
    {
        set { _numInstances = value; }
        get { return _numInstances; }
    }

    private Game game;
    private Effect instancedEffect;
    private FreeCamera camera;
    private float magnitude;

    #endregion

    #region Constructors

    public InstancedModel(Game game, string model, FreeCamera camera, float mag)
    {
        this.game = game;
        this.camera = camera;
        this.magnitude = mag;
        instancedEffect = game.Content.Load<Effect>("Effects/Tree");
        instanceMatrices = new List<Matrix>();


        _numInstances = 1;
        LoadModel(model);
    }

    public InstancedModel(Game game, Model model, FreeCamera camera, float mag)
    {
        this.game = game;
        this.camera = camera;
        this.magnitude = mag;
        instancedEffect = game.Content.Load<Effect>("Effects/Tree");
        this._baseModel = model;
        instanceMatrices = new List<Matrix>();

        _numInstances = 1;
        LoadModel();
    }

    #endregion

    #region LoadModel

    public void LoadModel(string _fileName)
    {
        if (!String.IsNullOrEmpty(_fileName))
        {
            _baseModel = game.Content.Load<Model>(_fileName);
            foreach (ModelMesh mesh in _baseModel.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    ModelParts.Add(new InstancedModelPart(game, part, mesh.VertexBuffer, mesh.IndexBuffer));
                }
            }
        }
    }

    public void LoadModel()
    {
        foreach (ModelMesh mesh in _baseModel.Meshes)
        {
            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                ModelParts.Add(new InstancedModelPart(game, part, mesh.VertexBuffer, mesh.IndexBuffer));
            }
        }
    }

    #endregion

    #region Public Methods

    public void AddTransform(Matrix transform)
    {
        this.instanceMatrices.Add(transform);
    }

    #endregion

    #region Draw

    public void DrawInstances(GameTime gameTime)
    {
        instancedEffect.CurrentTechnique = instancedEffect.Techniques["Instanced"];
        instancedEffect.Parameters["ViewProjection"].SetValue(camera.View *
                camera.Projection);
        instancedEffect.Parameters["Time"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);
        instancedEffect.Parameters["magnitude"].SetValue(magnitude);


        foreach (InstancedModelPart part in this.ModelParts)
        {

            game.GraphicsDevice.VertexDeclaration = part.VertexDeclaration;
            game.GraphicsDevice.Vertices[0].SetSource(part.VertexBuffer, 0, part.VertexStride);
            game.GraphicsDevice.Indices = part.IndexBuffer;
            instancedEffect.Begin();
            foreach (EffectPass pass in instancedEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                Matrix[] subList = new Matrix[_numInstances];
                instanceMatrices.CopyTo(0, subList, 0, _numInstances);
                DrawHardwareInstancing(subList, part.VertexCount, part.PrimitiveCount);
                pass.End();
            }
            instancedEffect.End();
        }
    }

    private void DrawHardwareInstancing(Matrix[] matrix, int vertexCount, int primitiveCount)
    {
        const int sizeofMatrix = sizeof(float) * 16;
        int instanceDataSize = sizeofMatrix * matrix.Length;

        DynamicVertexBuffer instanceDataStream = new DynamicVertexBuffer(game.GraphicsDevice,
                instanceDataSize,
                BufferUsage.WriteOnly);

        instanceDataStream.SetData(matrix, 0, matrix.Length, SetDataOptions.Discard);

        VertexStreamCollection vertices = game.GraphicsDevice.Vertices;

        vertices[0].SetFrequencyOfIndexData(matrix.Length);

        vertices[1].SetSource(instanceDataStream, 0, sizeofMatrix);
        vertices[1].SetFrequencyOfInstanceData(1);

        game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 0, vertexCount, 0, primitiveCount);

        vertices[0].SetSource(null, 0, 0);
        vertices[1].SetSource(null, 0, 0);
    }

    #endregion
}
}
