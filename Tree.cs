//-----------------------------------------------------------------------------
// Tree.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@gmail.com)
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
    class Tree
    {

        #region Properties

        protected Model model;

        private Vector3 position;
        private float scale;

        private Game game;

        private float magnitude;

        public float Magnitude
        {
            get { return magnitude; }
            set { magnitude = value; }
        }

        #endregion

        #region Constructors

        public Tree(Game game, Vector3 position, float scale, Model model)
        {
            this.position = position;
            this.scale = scale;
            this.model = model;
            this.Magnitude = 0.004f;
            this.game = game;
        }

        public Tree(Game game, Vector3 position, float scale, Model model, float magnitude)
        {
            this.position = position;
            this.scale = scale;
            this.model = model;
            this.Magnitude = magnitude;
            this.game = game;
        }

        #endregion

        #region LoadModel

        public virtual void LoadModel(string path, Effect effect)
        {
            this.model = game.Content.Load<Model>(path);
        }

        #endregion

        #region Draw

        public void Draw(FreeCamera camera, GameTime gameTime)
        {
            Matrix[] boneTransforms = new Matrix[this.model.Bones.Count];
            this.model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            foreach (ModelMesh mesh in this.model.Meshes)
            {
                Matrix World = boneTransforms[mesh.ParentBone.Index] *
                    Matrix.CreateScale(this.scale) *
                    Matrix.CreateTranslation(position);

                foreach (Effect effect in mesh.Effects)
                {
                    effect.CurrentTechnique = effect.Techniques["Textured"];
                    effect.Parameters["World"].SetValue(World);
                    effect.Parameters["ViewProjection"].SetValue(camera.View *
                        camera.Projection);
                    effect.Parameters["Time"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);
                    effect.Parameters["magnitude"].SetValue(this.magnitude);
                    effect.Parameters["baseHeight"].SetValue(this.position.Y);
                }
                mesh.Draw();
            }
        }

        #endregion

    }
}
