//-----------------------------------------------------------------------------
// Entity.cs
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
public class Entity
{
    private Vector3 position;
    public Vector3 rotation;
    private Model model;
    private float scale;
    private Matrix localWorld;
    public Vector3 modelVelocity;
    public Dictionary<ModelMeshPart, Texture2D> effectMapping;
    public delegate void RenderHandler();

    #region Properties
    public Matrix LocalWorld
    {
        get { return localWorld; }
        set { localWorld = value; }
    }

    public float Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public Model Model
    {
        get { return model; }
        set { model = value; }
    }

    public Vector3 Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public void RotationX(float rotX) { rotation.X = rotX; }
    public void RotationY(float rotY) { rotation.Y = rotY; }
    public void RotationZ(float rotZ) { rotation.Z = rotZ; }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    #endregion

    public Entity()
    {
        position = Vector3.Zero;
        RotationX(0);
        RotationY(0);
        RotationZ(0);
        scale = 1.0f;
        localWorld = Matrix.Identity;
        effectMapping = new Dictionary<ModelMeshPart, Texture2D>();
    }

    #region LoadModels
    public void LoadModel(ContentManager content, string path)
    {
        model = content.Load<Model>(path);
        GetTextures(model);
    }

    #endregion

    #region Draw Methods
    public void Draw(bool enableTexture, Effect effect, RenderHandler setParameter)
    {
        if (setParameter != null)
            setParameter.Invoke();

        localWorld = Matrix.CreateRotationY(rotation.Y)
                     * Matrix.CreateRotationX(rotation.X)
                     * Matrix.CreateRotationZ(rotation.Z)
                     * Matrix.CreateTranslation(position)
                     * Matrix.CreateScale(scale);

        Matrix[] transforms = new Matrix[model.Bones.Count];
        model.CopyAbsoluteBoneTransformsTo(transforms);

        foreach (ModelMesh meshes in model.Meshes)
        {
            foreach (ModelMeshPart parts in meshes.MeshParts)
            {
                effect.Parameters["World"].SetValue(transforms[meshes.ParentBone.Index] * localWorld);
                if (enableTexture && effectMapping[parts] != null)
                    effect.Parameters["Texture"].SetValue(effectMapping[parts]);
                parts.Effect = effect;
            }
            meshes.Draw();
        }
    }

    public void Draw(Effect effect, bool textured)
    {
        localWorld = Matrix.CreateRotationY(rotation.Y)
                     * Matrix.CreateRotationX(rotation.X)
                     * Matrix.CreateRotationZ(rotation.Z)
                     * Matrix.CreateTranslation(position)
                     * Matrix.CreateScale(scale);

        Matrix[] transforms = new Matrix[model.Bones.Count];
        model.CopyAbsoluteBoneTransformsTo(transforms);

        foreach (ModelMesh meshes in model.Meshes)
        {
            foreach (ModelMeshPart parts in meshes.MeshParts)
            {
                effect.Parameters["World"].SetValue(transforms[meshes.ParentBone.Index] * localWorld);
                if (textured)
                    effect.Parameters["Texture"].SetValue(effectMapping[parts]);
                parts.Effect = effect;
            }
            meshes.Draw();
        }
    }

    public void DrawBasic(Matrix View, Matrix Projection)
    {
        localWorld = Matrix.CreateRotationY(rotation.Y)
                     * Matrix.CreateRotationX(rotation.X)
                     * Matrix.CreateRotationZ(rotation.Z)
                     * Matrix.CreateTranslation(position)
                     * Matrix.CreateScale(scale);

        Matrix[] transforms = new Matrix[model.Bones.Count];
        model.CopyAbsoluteBoneTransformsTo(transforms);

        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.EnableDefaultLighting();
                effect.World = transforms[mesh.ParentBone.Index] * localWorld;

                effect.View = View;
                effect.Projection = Projection;
            }
            mesh.Draw();
        }

    }

    #endregion

    public void GetTextures(Microsoft.Xna.Framework.Graphics.Model model)
    {
        Dictionary<Effect, Texture2D> effectDictionary = new Dictionary<Effect, Texture2D>();
        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (BasicEffect oldEffect in mesh.Effects)
            {
                if (!effectDictionary.ContainsKey(oldEffect))
                    effectDictionary.Add(oldEffect, oldEffect.Texture);
            }

            foreach (ModelMeshPart meshPart in mesh.MeshParts)
            {
                effectMapping.Add(meshPart, effectDictionary[meshPart.Effect]);
            }
        }
    }

}
}
