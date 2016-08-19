//-----------------------------------------------------------------------------
// Game1.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@gmail.com)
// Dizajn Ruben: Petran (rubenpetran@gmail.com)
// Infokup 2010.
// Powered by Game Engine "Zrinski"
// Copyright (C)219 GAME STUDIO. All rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Legenda_o_Zmaju
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        //Menu objekti
        private enum Screen
        {
            //Dodaci 
            DodatakOne,
            DodatakTwo,
            DodatakThree,
            DodatakFour,
            DodatakFive,
            DodatakSix,
            DodatakSeven,
            DodatakEight,

            //Knjige edukacija
            KnjigaOne,
            KnjigaTwo,
            KnjigaThree,
            KnjigaFour,
            KnjigaFive,
            KnjigaSix,
            KnjigaSeven,
            KnjigaEight,
            KnjigaNine,

            //Menu 
            Title,
            Intro,
            Main,
            Inventory,
            Menu
        }
        Screen mCurrentScreen = Screen.Title;
        //Screen mCurrentScreen = Screen.Intro;

        //Pauza menu
        private enum MenuOptions
        {
            Resume,
            Inventory,
            ExitGame
        }
       

        //Teksture za menu 
        Texture2D mTitleScreen;
        Texture2D mIntroScreen;
        Texture2D mMainScreen;
        Texture2D mInventoryScreen;
        Texture2D mMenu;
        Texture2D mMenuOptions;

        //Teksture za dodatne sadržaje
        Texture2D mDodatakOne;
        Texture2D mDodatakTwo;
        Texture2D mDodatakThree;
        Texture2D mDodatakFour;
        Texture2D mDodatakFive;
        Texture2D mDodatakSix;
        Texture2D mDodatakSeven;
        Texture2D mDodatakEight;

        //Teksture za edukaciju
        Texture2D mKnjigaOne;
        Texture2D mKnjigaTwo;
        Texture2D mKnjigaThree;
        Texture2D mKnjigaFour;
        Texture2D mKnjigaFive;
        Texture2D mKnjigaSix;
        Texture2D mKnjigaSeven;
        Texture2D mKnjigaEight;
        Texture2D mKnjigaNine;


        KeyboardState mPreviousKeyboardState;

       
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Uèitavanje kamere
        private FreeCamera camera;

        //Uèitavanje glavnih modela
        private Skybox skybox;
        private Entity terrain;
        private Entity ograda;
        private Entity arsenal;
        private Entity trznica;
        private Entity staza;
        private Entity dvoraczidovi;
        private Entity selo;
        private Entity tabla;
        private Entity most;
        private Entity bunari;
        private Entity strazarnica;
        private Entity ljetnikovac;
        private Entity crkva;
        private Entity konji;
        private Entity kokosi;
        private Entity items;
        private Entity stado;
        private Entity selodrugo;
        private Entity krave;
        private Entity knjige;
        private Entity crtezi;
        private Entity trava;
        private Entity meta;
        private Entity dvorackroviste;
        private Entity dvoraccesta;
        private Entity dvoracpalaca;
        private Entity dvoracostatak;
        private Entity PlatnoOne;
        private Entity PlatnoTwo;
        private Entity PlatnoThree;
        private Entity PlatnoFour;
        private Entity PlatnoFive;
        private Entity PlatnoSix;
        private Entity PlatnoSeven;
        private Entity PlatnoEight;
        private Entity camac;
        private Entity KnjigaOne;
        private Entity KnjigaTwo;
        private Entity KnjigaThree;
        private Entity KnjigaFour;
        private Entity KnjigaFive;
        private Entity KnjigaSix;
        private Entity KnjigaSeven;
        private Entity KnjigaEight;
        private Entity ArsenalOne;
        private Entity ArsenalTwo;
        private Entity groblje;
        private Entity seloOne;
        private Entity seloTwo;
        private Entity selojedan;
        private Entity selodva;
        private Entity papir;
        private Entity dvoracrest;
        SpriteFont _spr_font;

        //FPS
        int _total_frames = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;


        

      

        float maxEmitterDistance = 150.0f;
        float maxVelocity = 30.0f;

        private Texture2D interfaceuser;
        private Water water;
        private Effect simple;

        //Drvo Trava
        List<Tree> trees;
        Model treeA;
        Random randomGenerator;


        Effect treeEffect;
        public const int MAX_INSTANCES = 20000;

        //Loading 
        Loader loader;
        LoadingScreen loadingScreen;

        SpriteFont font;

       

       
     

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Optimizirano za rezoluciju FULL HD 1920x1080
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;
            this.graphics.IsFullScreen = false;
             

          
            
        }

        
        protected override void Initialize()
        {
            Mouse.SetPosition(graphics.GraphicsDevice.Viewport.Width / 2,
                                graphics.GraphicsDevice.Viewport.Height / 2);



            //KAMERA DEFAULT POZICIJA -4520,40,-500
            camera = new FreeCamera(new Vector3(-4520, 40, -500));
            camera.NearPlane = 1.0f;
            camera.FarPlane = 10000f;
            camera.Speed = 110f;
            camera.TurnSpeed = 25;

            skybox = new Skybox(Content);
            terrain = new Entity();
            ograda = new Entity();
            arsenal = new Entity();
            trznica = new Entity();
            staza = new Entity();
            dvoraczidovi = new Entity();
            selo = new Entity();
            tabla = new Entity();
            most = new Entity();
            bunari = new Entity();
            strazarnica = new Entity();
            ljetnikovac = new Entity();
            crkva = new Entity();
            konji = new Entity();
            camac = new Entity();
            kokosi = new Entity();
            items = new Entity();
            stado = new Entity();
            selodrugo = new Entity();
            krave = new Entity();
            knjige = new Entity();
            crtezi = new Entity();
            trava = new Entity();
            meta = new Entity();
            dvorackroviste = new Entity();
            dvoraccesta = new Entity();
            dvoracpalaca = new Entity();
            dvoracostatak = new Entity();
            PlatnoOne = new Entity();
            PlatnoTwo = new Entity();
            PlatnoThree = new Entity();
            PlatnoFour = new Entity();
            PlatnoFive = new Entity();
            PlatnoSix = new Entity();
            PlatnoSeven = new Entity();
            PlatnoEight = new Entity();
            KnjigaOne = new Entity();
            KnjigaTwo = new Entity();
            KnjigaThree = new Entity();
            KnjigaFour = new Entity();
            KnjigaFive = new Entity();
            KnjigaSix = new Entity();
            KnjigaSeven = new Entity();
            KnjigaEight = new Entity();
            ArsenalOne = new Entity();
            ArsenalTwo = new Entity();
            groblje = new Entity();
            seloOne = new Entity();
            seloTwo = new Entity();
            selojedan = new Entity();
            selodva = new Entity();
            papir = new Entity();
            dvoracrest = new Entity();






            //Definiranje vode
            water = new Water(13600, 13000, Vector3.Zero, 5.0f);


            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Menu
            mTitleScreen = Content.Load<Texture2D>("MENU/slika1");
            mIntroScreen = Content.Load<Texture2D>("Dodaci/dodatak");
            mMainScreen = Content.Load<Texture2D>("MENU/slika4");
            mInventoryScreen = Content.Load<Texture2D>("MENU/slika2");
            mMenu = Content.Load<Texture2D>("MENU/Menu");
            mMenuOptions = Content.Load<Texture2D>("MENU/MenuOptions");

            //Edukativne knjige
            mKnjigaOne = Content.Load<Texture2D>("Edukacija/Knjiga1");
            mKnjigaTwo = Content.Load<Texture2D>("Edukacija/Knjiga2");
            mKnjigaThree = Content.Load<Texture2D>("Edukacija/Knjiga3");
            mKnjigaFour = Content.Load<Texture2D>("Edukacija/Knjiga4");
            mKnjigaFive = Content.Load<Texture2D>("Edukacija/Knjiga5");
            mKnjigaSix = Content.Load<Texture2D>("Edukacija/Knjiga6");
            mKnjigaSeven = Content.Load<Texture2D>("Edukacija/Knjiga7");
            mKnjigaEight = Content.Load<Texture2D>("Edukacija/Knjiga8");
            mKnjigaNine = Content.Load<Texture2D>("Edukacija/Knjiga9");

            //Dodaci
            mDodatakOne = Content.Load<Texture2D>("Dodaci/dodatak1");
            mDodatakTwo = Content.Load<Texture2D>("Dodaci/dodatak2");
            mDodatakThree = Content.Load<Texture2D>("Dodaci/dodatak3");
            mDodatakFour = Content.Load<Texture2D>("Dodaci/dodatak4");
            mDodatakFive = Content.Load<Texture2D>("Dodaci/dodatak5");
            mDodatakSix = Content.Load<Texture2D>("Dodaci/dodatak6");
            mDodatakSeven = Content.Load<Texture2D>("Dodaci/dodatak7");
            mDodatakEight = Content.Load<Texture2D>("Dodaci/dodatak8");

            water.Initialize(GraphicsDevice, Content);
            terrain.LoadModel(Content, "Models/teren/teren");
            ograda.LoadModel(Content, "Models/ograda/ograda");
            arsenal.LoadModel(Content, "Models/arsenal/arsenal");
            trznica.LoadModel(Content, "Models/trznica/trznica");
            staza.LoadModel(Content, "Models/staza/staza");
            dvoraczidovi.LoadModel(Content, "Models/dvorac/dvorac-zidovi");
            selo.LoadModel(Content, "Models/selo/selo");
            tabla.LoadModel(Content, "Models/tabla/tabla");
            most.LoadModel(Content, "Models/most/most");
            bunari.LoadModel(Content, "Models/bunari/bunari");
            strazarnica.LoadModel(Content, "Models/strazarnica/strazarnica");
            ljetnikovac.LoadModel(Content, "Models/ljetnikovac/ljetnikovac");
            crkva.LoadModel(Content, "Models/crkva/crkva");
            konji.LoadModel(Content, "Models/konji/konji");
            kokosi.LoadModel(Content, "Models/kokosi/kokosi");
            items.LoadModel(Content, "Models/items/items");
            stado.LoadModel(Content, "Models/stado/stado");
            selodrugo.LoadModel(Content, "Models/selodrugo/selodrugo");
            krave.LoadModel(Content, "Models/krave/krave");
            knjige.LoadModel(Content, "Models/knjige/knjige");
            crtezi.LoadModel(Content, "Models/crtezi/crtezi");
            trava.LoadModel(Content, "Models/trava/trava");
            meta.LoadModel(Content, "Models/meta/meta");
            dvorackroviste.LoadModel(Content, "Models/dvorac/dvorac-kroviste");
            dvoracrest.LoadModel(Content, "Models/dvorac/dvorac-rest");
            dvoraccesta.LoadModel(Content, "Models/dvorac/dvorac-cesta");

            dvoracpalaca.LoadModel(Content, "Models/dvorac/dvorac-palaca");
            dvoracostatak.LoadModel(Content, "Models/dvorac/dvorac-ostatak");
            PlatnoOne.LoadModel(Content, "Models/crtezi/PlatnoOne");
            PlatnoTwo.LoadModel(Content, "Models/crtezi/PlatnoTwo");
            PlatnoThree.LoadModel(Content, "Models/crtezi/PlatnoThree");
            PlatnoFour.LoadModel(Content, "Models/crtezi/PlatnoFour");
            PlatnoFive.LoadModel(Content, "Models/crtezi/PlatnoFive");
            PlatnoSix.LoadModel(Content, "Models/crtezi/PlatnoSix");
            PlatnoSeven.LoadModel(Content, "Models/crtezi/PlatnoSeven");
            PlatnoEight.LoadModel(Content, "Models/crtezi/PlatnoEight");

            KnjigaOne.LoadModel(Content, "Models/knjige/KnjigaOne");
            KnjigaTwo.LoadModel(Content, "Models/knjige/KnjigaTwo");
            KnjigaThree.LoadModel(Content, "Models/knjige/KnjigaThree");
            KnjigaFour.LoadModel(Content, "Models/knjige/KnjigaFour");
            KnjigaFive.LoadModel(Content, "Models/knjige/KnjigaFive");
            KnjigaSix.LoadModel(Content, "Models/knjige/KnjigaSix");
            KnjigaSeven.LoadModel(Content, "Models/knjige/KnjigaSeven");
            KnjigaEight.LoadModel(Content, "Models/knjige/KnjigaEight");
            ArsenalOne.LoadModel(Content, "Models/arsenal/ArsenalOne");
            ArsenalTwo.LoadModel(Content, "Models/arsenal/ArsenalTwo");

            groblje.LoadModel(Content, "Models/selo/groblje");
            seloOne.LoadModel(Content, "Models/selo/seloOne");
            seloTwo.LoadModel(Content, "Models/selo/seloTwo");
            selodva.LoadModel(Content, "Models/selodrugo/selodva");
            selojedan.LoadModel(Content, "Models/selodrugo/selojedan");

            loadingScreen = new LoadingScreen(Content, graphics.GraphicsDevice);

            camac.LoadModel(Content, "Animations/camac/camac");
            papir.LoadModel(Content, "Models/papir/papir");


            _spr_font = Content.Load<SpriteFont>("Fonts/kootenay");

            LoadTrees();

            interfaceuser = Content.Load<Texture2D>("Grafika/interface");
            simple = Content.Load<Effect>("Effects/Simple");
            skybox.Initialize(graphics.GraphicsDevice);
            font = Content.Load<SpriteFont>("Fonts/Arial");






            

        }
        void videoPlayer_OnVideoComplete(object sender, EventArgs e)
        {
            Exit();

        }

        void intro_OnVideoComplete(object sender, EventArgs e)
        {
            

        }
    
        private void LoadTrees()
        {
            trees = new List<Tree>();
            

            treeEffect = Content.Load<Effect>("Effects/Tree");

            treeA = Content.Load<Model>("Models/treea");
            

            RemapModel(this, treeA, treeEffect);

            ///////////////POZDCIJE SVIH STABALA U IGRI/////////////////
           
            Tree tree = new Tree(this, Vector3.Zero, 0.1f, treeA, 0.008f);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(10.0f, 15.0f, 25.0f),
                6.07f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4700.0f, 0.0f, -350.0f),
                 5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4911.0f, 0.0f, -200.0f),
                 6.03f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4144.0f, 0.0f, -200.0f),
                 5.03f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3767.0f, 0.0f, -200.0f),
                 6.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3933.0f, 0.0f, -622.0f),
                 5.05f, treeA);
            trees.Add(tree);
           
            tree = new Tree(this, new Vector3(-3932.0f, 0.0f, -1051.0f),
                 6.77f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3976.0f, 0.0f, -1477.0f),
                 5.01f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3926.0f, 0.0f, -1864.0f),
                 6.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3916.0f, 0.0f, -2288.0f),
                 5.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3967.0f, 0.0f, -2688.0f),
                 6.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3946.0f, 0.0f, -3046.0f),
                5.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-37053.0f, 0.0f, -434.0f),
                 6.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3751.0f, 0.0f, -815.0f),
                 5.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3725.0f, 0.0f, -1260.0f),
                 6.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3751.0f, 0.0f, -1634.0f),
                 5.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3751.0f, 0.0f, -2061.0f),
                 6.09f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3761.0f, 0.0f, -2488.0f),
                 5.09f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3757.0f, 0.0f, -2862.0f),
                6.07f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3785.0f, 0.0f, -3277.0f),
                5.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4525.0f, 0.0f, -1155.0f),
                 6.23f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4571.0f, 0.0f, -1551.0f),
                 5.014f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4566.0f, 0.0f, -1915.0f),
                 6.46f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4591.0f, 0.0f, -2304.0f),
                 5.14f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4588.0f, 0.0f, -2789.0f),
                 6.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4555.0f, 0.0f, -3131.0f),
                 5.32f, treeA);
            trees.Add(tree);
   
            tree = new Tree(this, new Vector3(-4560.0f, 0.0f, -3563.0f),
                6.75f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4545.0f, 0.0f, -3921.0f),
               5.46f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4597.0f, 0.0f, -4204.0f),
               6.46f, treeA);
            trees.Add(tree); 

            tree = new Tree(this, new Vector3(-4573.0f, 0.0f, -4621.0f),
               5.02f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4861.0f, 0.0f, -1277.0f),
                 6.05f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4816.0f, 0.0f, -2062.0f),
                 5.72f, treeA);
            trees.Add(tree);
         
            tree = new Tree(this, new Vector3(-4541.0f, 0.0f, -2773.0f),
                 6.13f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4561.0f, 0.0f, -3560.0f),
                5.24f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4564.0f, 0.0f, -4346.0f),
               6.15f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4300.0f, 0.0f, -4700.0f),
                 5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4342.0f, 0.0f, 351.0f),
                 6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4321.0f, 0.0f, 761.0f),
                 5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4360.0f, 0.0f, 1140.0f),
                 6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4311.0f, 0.0f, 1543.0f),
                 5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4390.0f, 0.0f, 1914.0f),
                 6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4332.0f, 0.0f, 2313.0f),
                5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4311.0f, 0.0f, 2763.0f),
                6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4477.0f, 0.0f, 3132.0f),
                5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4487.0f, 0.0f, 3551.0f),
                6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4390.0f, 0.0f, 3951.0f),
                5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4463.0f, 0.0f, 4364.0f),
                6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4440.0f, 0.0f, 4712.0f),
                5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4732.0f, 0.0f, 164.0f),
                6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4755.0f, 0.0f, 552.0f),
                 5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4755.0f, 0.0f, 925.0f),
                 6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4731.0f, 0.0f, 1364.0f),
                 5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4754.0f, 0.0f, 1750.0f),
                 6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4700.0f, 0.0f, 2163.0f),
                5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4704.0f, 0.0f, 2551.0f),
                6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4764.0f, 0.0f, 2966.0f),
                5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4788.0f, 0.0f, 3361.0f),
                6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4866.0f, 0.0f, 3762.0f),
                5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4866.0f, 0.0f, 4151.0f),
                6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4764.0f, 0.0f, 4563.0f),
                5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4864.0f, 0.0f, 4963.0f),
                6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 1900.0f),
               5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 2300.0f),
               6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 2700.0f),
               5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 3100.0f),
              6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 3500.0f),
             5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 3900.0f),
             6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 3300.0f),
            5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 3900.0f),
            6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 4300.0f),
           5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 4700.0f),
           6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -4500.0f),
          5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -4100.0f),
         6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -3700.0f),
         5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -3300.0f),
         6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -2900.0f),
         5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -2500.0f),
         6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -2100.0f),
         5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -1700.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -1300.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -900.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -500.0f),
       5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, -100.0f),
       6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, 300.0f),
       5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, 700.0f),
       6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4850.0f, 0.0f, 1100.0f),
       5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -4600.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -4200.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -3800.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -3400.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -3000.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -2600.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -2200.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -1800.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -1400.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -1000.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -600.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, -200.0f),
       5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 400.0f),
      6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 800.0f),
      5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4650.0f, 0.0f, 1200.0f),
       6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(4000.0f, 0.0f, 4600.0f),
      5.08f, treeA);
            trees.Add(tree);
            
            tree = new Tree(this, new Vector3(3400.0f, 0.0f, 4600.0f),
      6.08f, treeA);
            trees.Add(tree);
            
            tree = new Tree(this, new Vector3(2900.0f, 0.0f, 4600.0f),
      5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(2300.0f, 0.0f, 4600.0f),
     6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(1700.0f, 0.0f, 4600.0f),
     5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(2100.0f, 0.0f, 4600.0f),
     6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(1500.0f, 0.0f, 4600.0f),
     5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(900.0f, 0.0f, 4600.0f),
     6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(300.0f, 0.0f, 4600.0f),
     5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-300.0f, 0.0f, 4600.0f),
     16.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-900.0f, 0.0f, 4600.0f),
     5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-1400.0f, 0.0f, 4600.0f),
     6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-2000.0f, 0.0f, 4600.0f),
     5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-2000.0f, 0.0f, 4600.0f),
     6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-2600.0f, 0.0f, 4600.0f),
     5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3100.0f, 0.0f, 4800.0f),
   6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3700.0f, 0.0f, 4800.0f),
   5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(600.0f, 0.0f, 900.0f),
            6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(200.0f, 0.0f, 1900.0f),
           5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(1450.0f, 0.0f, 430.0f),
           6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(2650.0f, 0.0f, 130.0f),
           8.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(3450.0f, 0.0f, 530.0f),
       8.08f, treeA);
            trees.Add(tree);


            tree = new Tree(this, new Vector3(900.0f, 0.0f, 2000.0f),
          5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(3900.0f, 0.0f, -300.0f),
           6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(2000.0f, 0.0f, 100.0f),
           5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(24500.0f, 0.0f, 2500.0f),
           6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(3100.0f, 0.0f, 700.0f),
          5.08f, treeA);
            trees.Add(tree);
            
            tree = new Tree(this, new Vector3(4100.0f, 0.0f, -4600.0f),
         6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(3500.0f, 0.0f, -4600.0f),
         5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(2900.0f, 0.0f, -4600.0f),
         6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(2300.0f, 0.0f, -4600.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(1700.0f, 0.0f, -4600.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(1100.0f, 0.0f, -4600.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(600.0f, 0.0f, -4600.0f),
        5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(0.0f, 0.0f, -4600.0f),
        6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-600.0f, 0.0f, -4600.0f),
       5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-1100.0f, 0.0f, -4600.0f),
       6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-1700.0f, 0.0f, -4600.0f),
       5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-2300.0f, 0.0f, -4600.0f),
       6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-2900.0f, 0.0f, -4600.0f),
       5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3400.0f, 0.0f, -4600.0f),
      6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4000.0f, 0.0f, -4600.0f),
      5.08f, treeA);
            trees.Add(tree);
           
            tree = new Tree(this, new Vector3(-1600.0f, 0.0f, -1400.0f),
    6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-2600.0f, 0.0f, -2200.0f),
    5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-1000.0f, 0.0f, -1000.0f),
6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-2000.0f, 0.0f, -0.0f),
    5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-3200.0f, 0.0f, 900.0f),
    6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(-4000.0f, 0.0f, 1200.0f),
  5.08f, treeA);
            trees.Add(tree);
            tree = new Tree(this, new Vector3(-2700.0f, 0.0f, 500.0f),
6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(600.0f, 0.0f, -1140.0f),
  6.08f, treeA);
            trees.Add(tree);
            
            tree = new Tree(this, new Vector3(500.0f, 0.0f, -1500.0f),
   5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(1300.0f, 0.0f, -2040.0f),
 6.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(2000.0f, 0.0f, -2233.0f),
 5.08f, treeA);
            trees.Add(tree);

            tree = new Tree(this, new Vector3(211.0f, 0.0f, -1433.0f),
 6.08f, treeA);
            trees.Add(tree);
/////////////////////////////////////////////////////////////////////////////////


            randomGenerator = new Random();

          

            for (int i = 0; i < MAX_INSTANCES; i++)
            {
                float posX = 31.0f * (float)randomGenerator.NextDouble() - 13.0f;
                float posZ = 31.0f * (float)randomGenerator.NextDouble() - 13.0f;

                    Matrix.CreateTranslation(new Vector3(posX, 0.0f, posZ));

               
            }
        }

        
        protected override void UnloadContent()
        {
            
        }


        protected override void Update(GameTime gameTime)
        {

          

           
            

            if (loadingScreen != null)
            {

                IsFixedTimeStep = false; 
                loader = loadingScreen.Update();
                if (loader != null)
                {
                    
                    loadingScreen = null;
                    IsFixedTimeStep = true; 
                }
            }
            else
            {
                
            }
            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsed_time >= 1000.0f)
            {
                _fps = _total_frames;
                _total_frames = 0;
                _elapsed_time = 0;
            }

           

            camera.Update(graphics.GraphicsDevice.Viewport.Width, (float)gameTime.ElapsedGameTime.TotalSeconds);



            


        }
        
        protected override void Draw(GameTime gameTime)
        {
            if (loadingScreen != null)
            {
                loadingScreen.Draw();
            }
            else
            {
          
                _total_frames++;

                water.DrawRefractionMap(camera, new Water.RenderRefractionHandler(RenderRefraction));
                water.DrawReflectionMap(camera, new Water.RenderReflectionHandler(RenderReflection));

                GraphicsDevice.Clear(Color.Black);
                skybox.Draw(graphics.GraphicsDevice, camera.Position, camera.View * camera.Projection);
                terrain.Draw(true, simple, delegate { setParameters(); });
                ograda.Draw(true, simple, delegate { setParameters(); });
                arsenal.Draw(true, simple, delegate { setParameters(); });
                trznica.Draw(true, simple, delegate { setParameters(); });
                staza.Draw(true, simple, delegate { setParameters(); });
                dvoraczidovi.Draw(true, simple, delegate { setParameters(); });
                selo.Draw(true, simple, delegate { setParameters(); });
                tabla.Draw(true, simple, delegate { setParameters(); });
                most.Draw(true, simple, delegate { setParameters(); });
                camac.Draw(true, simple, delegate { setParameters(); });
                bunari.Draw(true, simple, delegate { setParameters(); });
                strazarnica.Draw(true, simple, delegate { setParameters(); });
                ljetnikovac.Draw(true, simple, delegate { setParameters(); });
                crkva.Draw(true, simple, delegate { setParameters(); });
                konji.Draw(true, simple, delegate { setParameters(); });
                kokosi.Draw(true, simple, delegate { setParameters(); });
                items.Draw(true, simple, delegate { setParameters(); });
                stado.Draw(true, simple, delegate { setParameters(); });
                selodrugo.Draw(true, simple, delegate { setParameters(); });
                krave.Draw(true, simple, delegate { setParameters(); });
                knjige.Draw(true, simple, delegate { setParameters(); });
                crtezi.Draw(true, simple, delegate { setParameters(); });
                trava.Draw(true, simple, delegate { setParameters(); });
                meta.Draw(true, simple, delegate { setParameters(); });
                dvorackroviste.Draw(true, simple, delegate { setParameters(); });
                dvoraccesta.Draw(true, simple, delegate { setParameters(); });
                dvoracpalaca.Draw(true, simple, delegate { setParameters(); });
                dvoracostatak.Draw(true, simple, delegate { setParameters(); });
                PlatnoOne.Draw(true, simple, delegate { setParameters(); });
                PlatnoTwo.Draw(true, simple, delegate { setParameters(); });
                PlatnoThree.Draw(true, simple, delegate { setParameters(); });
                PlatnoFour.Draw(true, simple, delegate { setParameters(); });
                PlatnoFive.Draw(true, simple, delegate { setParameters(); });
                PlatnoSix.Draw(true, simple, delegate { setParameters(); });
                PlatnoSeven.Draw(true, simple, delegate { setParameters(); });
                PlatnoEight.Draw(true, simple, delegate { setParameters(); });

                KnjigaOne.Draw(true, simple, delegate { setParameters(); });
                KnjigaTwo.Draw(true, simple, delegate { setParameters(); });
                KnjigaThree.Draw(true, simple, delegate { setParameters(); });
                KnjigaFour.Draw(true, simple, delegate { setParameters(); });
                KnjigaFive.Draw(true, simple, delegate { setParameters(); });
                KnjigaSix.Draw(true, simple, delegate { setParameters(); });
                KnjigaSeven.Draw(true, simple, delegate { setParameters(); });
                KnjigaEight.Draw(true, simple, delegate { setParameters(); });

                ArsenalOne.Draw(true, simple, delegate { setParameters(); });
                dvoracrest.Draw(true, simple, delegate { setParameters(); });
                ArsenalTwo.Draw(true, simple, delegate { setParameters(); });
                groblje.Draw(true, simple, delegate { setParameters(); });
                seloOne.Draw(true, simple, delegate { setParameters(); });
                seloTwo.Draw(true, simple, delegate { setParameters(); });

                selojedan.Draw(true, simple, delegate { setParameters(); });
                selodva.Draw(true, simple, delegate { setParameters(); });

                papir.Draw(true, simple, delegate { setParameters(); });
 
                water.DrawWater(camera, gameTime);


                foreach (Tree tree in trees)
                    tree.Draw(camera, gameTime);


                

                
                //Poèetak SpriteBatch
                
               
                spriteBatch.Begin();
               
                
                
                spriteBatch.Draw(interfaceuser, new Vector2(0, 0), Color.White);

                //spriteBatch.DrawString(_spr_font, string.Format("FPS={0}", _fps),
                  //  new Vector2(220.0f, 150.0f), Color.White);

                
                    


                
              
                
                
                spriteBatch.End();
              
                base.Draw(gameTime);
            }
        }

        private void RenderRefraction()
        {
            terrain.Draw(true, simple, delegate { setParameters(); });
            
        }

        private void RenderReflection(Matrix view, Vector3 reflectionCamPosition)
        {
            skybox.Draw(graphics.GraphicsDevice, reflectionCamPosition, view * camera.Projection);
            terrain.Draw(true, simple, delegate { setParametersReflection(view); });
            ograda.Draw(true, simple, delegate { setParametersReflection(view); });
            arsenal.Draw(true, simple, delegate { setParametersReflection(view); });
            trznica.Draw(true, simple, delegate { setParametersReflection(view); });
            staza.Draw(true, simple, delegate { setParametersReflection(view); });
            dvoraczidovi.Draw(true, simple, delegate { setParametersReflection(view); });
            selo.Draw(true, simple, delegate { setParametersReflection(view); });
            tabla.Draw(true, simple, delegate { setParametersReflection(view); });
            most.Draw(true, simple, delegate { setParametersReflection(view); });
            camac.Draw(true, simple, delegate { setParametersReflection(view); });
            bunari.Draw(true, simple, delegate { setParametersReflection(view); });
            strazarnica.Draw(true, simple, delegate { setParametersReflection(view); });
            ljetnikovac.Draw(true, simple, delegate { setParametersReflection(view); });
            crkva.Draw(true, simple, delegate { setParametersReflection(view); });
            konji.Draw(true, simple, delegate { setParametersReflection(view); });
            kokosi.Draw(true, simple, delegate { setParametersReflection(view); });
            items.Draw(true, simple, delegate { setParametersReflection(view); });
            stado.Draw(true, simple, delegate { setParametersReflection(view); });
            selodrugo.Draw(true, simple, delegate { setParametersReflection(view); });
            krave.Draw(true, simple, delegate { setParametersReflection(view); });
            knjige.Draw(true, simple, delegate { setParametersReflection(view); });
            crtezi.Draw(true, simple, delegate { setParametersReflection(view); });
            trava.Draw(true, simple, delegate { setParametersReflection(view); });
            meta.Draw(true, simple, delegate { setParametersReflection(view); });
            dvorackroviste.Draw(true, simple, delegate { setParametersReflection(view); });
            dvoraccesta.Draw(true, simple, delegate { setParametersReflection(view); });
            dvoracpalaca.Draw(true, simple, delegate { setParametersReflection(view); });
            dvoracostatak.Draw(true, simple, delegate { setParametersReflection(view); });
            PlatnoOne.Draw(true, simple, delegate { setParametersReflection(view); });
            PlatnoTwo.Draw(true, simple, delegate { setParametersReflection(view); });
            PlatnoThree.Draw(true, simple, delegate { setParametersReflection(view); });
            PlatnoFour.Draw(true, simple, delegate { setParametersReflection(view); });
            PlatnoFive.Draw(true, simple, delegate { setParametersReflection(view); });
            PlatnoSix.Draw(true, simple, delegate { setParametersReflection(view); });
            PlatnoSeven.Draw(true, simple, delegate { setParametersReflection(view); });
            PlatnoEight.Draw(true, simple, delegate { setParametersReflection(view); });
            dvoracrest.Draw(true, simple, delegate { setParametersReflection(view); });

            KnjigaOne.Draw(true, simple, delegate { setParametersReflection(view); });
            KnjigaTwo.Draw(true, simple, delegate { setParametersReflection(view); });
            KnjigaThree.Draw(true, simple, delegate { setParametersReflection(view); });
            KnjigaFour.Draw(true, simple, delegate { setParametersReflection(view); });
            KnjigaFive.Draw(true, simple, delegate { setParametersReflection(view); });
            KnjigaSix.Draw(true, simple, delegate { setParametersReflection(view); });
            KnjigaSeven.Draw(true, simple, delegate { setParametersReflection(view); });
            KnjigaEight.Draw(true, simple, delegate { setParametersReflection(view); });


            ArsenalOne.Draw(true, simple, delegate { setParametersReflection(view); });
            ArsenalTwo.Draw(true, simple, delegate { setParametersReflection(view); });
            groblje.Draw(true, simple, delegate { setParametersReflection(view); });
            seloOne.Draw(true, simple, delegate { setParametersReflection(view); });
            seloTwo.Draw(true, simple, delegate { setParametersReflection(view); });

            selojedan.Draw(true, simple, delegate { setParametersReflection(view); });
            selodva.Draw(true, simple, delegate { setParametersReflection(view); });
            papir.Draw(true, simple, delegate { setParametersReflection(view); });
 
               
           
        }
        private void RemapModel(Game game, Model model, Effect effect)
        {

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Effect i_effect = effect.Clone(game.GraphicsDevice);
                    Texture2D texture = ((BasicEffect)part.Effect).Texture;
                    i_effect.Parameters["BaseTexture"].SetValue(((BasicEffect)part.Effect).Texture);
                    part.Effect = i_effect;

                   
                }
            }
        }

        private void setParametersReflection(Matrix view)
        {
            simple.Parameters["View"].SetValue(view);
            simple.Parameters["Projection"].SetValue(camera.Projection);
        }

        private void setParameters()
        {
            simple.Parameters["View"].SetValue(camera.View);
            simple.Parameters["Projection"].SetValue(camera.Projection);
        }
    }
}
