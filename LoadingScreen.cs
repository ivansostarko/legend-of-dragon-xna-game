//-----------------------------------------------------------------------------
// LoadingScreen.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@hotmail.com)
// Dizajn Ruben: Petran (rubenpetran@gmail.com)
// Copyright (C)219 GAME STUDIO. All rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Legenda_o_Zmaju
{
class LoadingScreen
{
    Loader loader;
    SpriteBatch sb;
    IEnumerator<float> enumerator;
    Texture2D barTex;
    Texture2D backroundTex;

    public LoadingScreen(ContentManager content, GraphicsDevice gd)
    {
        loader = new Loader(content);
        sb = new SpriteBatch(gd);
        enumerator = loader.GetEnumerator();


        backroundTex = content.Load<Texture2D>("Grafika/loading");

        // 1-pixel white texture for solid bars
        barTex = new Texture2D(gd, 1, 1);
        uint[] texData = { 0xffffffff };
        barTex.SetData<uint>(texData);
    }

    public Loader Update()
    {

        bool incomplete = enumerator.MoveNext();
        return incomplete ? null : loader;
    }

    public void Draw()
    {
        Rectangle screenRect = new Rectangle(0, 0, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height);
        Vector2 backroundTexPos = new Vector2(screenRect.Center.X - backroundTex.Width / 2, screenRect.Center.Y - backroundTex.Height / 2);


        Rectangle loadingBarPos = new Rectangle();
        loadingBarPos.Width = 400; // pixels
        loadingBarPos.Height = 30;
        loadingBarPos.X = 250;
        loadingBarPos.Y = 250;
        loadingBarPos.Offset((int)backroundTexPos.X, (int)backroundTexPos.Y);

        Color screenBackgroundColor = topLeftPixelColor(backroundTex);
        Color barColor = Color.Orange;
        Color barBackgroundColor = Color.White;
        int barBackgroundExpand = 2;

        sb.GraphicsDevice.Clear(screenBackgroundColor);
        sb.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);
        sb.Draw(backroundTex, backroundTexPos, Color.White);

        Rectangle barBackground = loadingBarPos;
        barBackground.Inflate(barBackgroundExpand, barBackgroundExpand);
        sb.Draw(barTex, barBackground, barBackgroundColor);

        Rectangle bar = loadingBarPos;
        float completeness = enumerator.Current;
        bar.Width = (int)(loadingBarPos.Width * completeness);

        sb.Draw(barTex, bar, barColor);
        sb.End();
    }

    Color topLeftPixelColor(Texture2D tex)
    {
        Color[] colors = { Color.Magenta };
        tex.GetData<Color>(0, new Rectangle(0, 0, 1, 1), colors, 0, 1);
        return colors[0];
    }
}
}
