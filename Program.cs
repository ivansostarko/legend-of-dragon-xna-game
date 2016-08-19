//-----------------------------------------------------------------------------
// Program.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@gmail.com)
// Dizajn Ruben: Petran (rubenpetran@gmail.com)
// Copyright (C)219 GAME STUDIO. All rights reserved.
//-----------------------------------------------------------------------------
using System;

namespace Legenda_o_Zmaju
{
    static class Program
    {
        /// <summary>
        /// Glavi dio igre
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}

