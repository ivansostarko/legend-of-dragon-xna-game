//-----------------------------------------------------------------------------
// Loader.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@gmail.com)
// Dizajn Ruben: Petran (rubenpetran@gmail.com)
// Copyright (C)219 GAME STUDIO. All rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading; 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Legenda_o_Zmaju
{

   
    class Loader : IEnumerable<float>
    {


        public Model playerModel;
        
        public List<Model> levelModels = new List<Model>();
        public SpriteFont font;
    


        ContentManager content;
        int loadedItems = 0;
        int totalItems;

        public Loader(ContentManager content)
        {
            this.content = content;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<float> GetEnumerator()
        {
            
            string[] levelList = pretend_content_Load_strings("levelList");

            
            totalItems = 3 + levelList.Length;

            yield return progress(); 

            Debug.WriteLine("Loading levels");
            foreach (string levelName in levelList)
            {
                levelModels.Add(pretend_content_Load_Model(levelName));
                yield return progress();
            }

            Debug.WriteLine("Loading player model");
            playerModel = pretend_content_Load_Model("player model");
            yield return progress();

            Debug.WriteLine("Loading font");
            font = content.Load<SpriteFont>("Fonts/kootenay");
            yield return progress();

            string loadedCheckMessage = String.Format("Loaded {0} items. Expected {1} items.", loadedItems, totalItems);
            Debug.WriteLine(loadedCheckMessage);
            Debug.Assert(loadedItems == totalItems, "Loader.totalItems needs adjusting.", loadedCheckMessage);
            yield return 1;
        }

        float progress()
        {
            ++loadedItems;
            return (float)loadedItems / totalItems;
        }

        string[] pretend_content_Load_strings(string name)
        {
            actBusy(10);
            return new string[] { "level one", "level two", "level three", "etc", "let's add some longer names for", "some variety in", "load time" };
        }

        Model pretend_content_Load_Model(string name)
        {
            actBusy(name.Length); 
            return null;
        }

        void actBusy(int howBusy)
        {
            //Dužina uèitavanja
            Thread.Sleep(100 * howBusy);
        }

    }
}
