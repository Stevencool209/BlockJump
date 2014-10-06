#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace BlockJump
{
    public class Game1 : Game
    {
        #region Variablen
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int Elapsed;
        int delay = 150;
        
        #region Blockvariablen
        Vector2[,] BlockPos = new Vector2[21, 11]; //Gibt die Position des Blockes an [x,y]
        Texture2D[,] BlockTex = new Texture2D[50,10]; //Gibt die Textur des Blockes an [BlockID, Frame]
        int[] BlockFrm = new int[50]; //Gibt den Frame des Blockes an [BlockID]
        int[] BlockinsgFrm = new int[50]; //Gibt die Gesamtanzahl der Frames für diesen Block an [BlockID]
        int[,] BlockID = new int[21, 11]; //Gibt die ID des Blockes an einer bestimmten Stelle an [x,y]
        bool[] BlockHart = new bool[50]; //Gibt an ob man durch den Block gehen kann [BlockID]
        #endregion
        #region Mapvariablen
        Texture2D Hintergrund;
        int TotalMaps = 3;
        int MapID = 0;
        Texture2D[] MapTex = new Texture2D[3];
        #endregion
        #region Spielervariablen
        Texture2D SpielerLinks1;
        Texture2D SpielerLinks2;

        Texture2D SpielerRechts1;
        Texture2D SpielerRechts2;

        Texture2D SpielerLinksSprung;
        Texture2D SpielerRechtsSprung;

        Rectangle SpielerRect;
        Vector2 SpielerPos;
        Vector2 SpielerPosOld;
        Vector2 SpielerSpawnPos;

        String SpielerTex = "Rechts1";
        int SpielerFrame;
        int SpielerinsgFrame = 2;

        bool Sprungerlaubt;
        bool springt;
        float Sprunggeschwindigkeit;
        #endregion

        String[] MapArray = new String[1];

        Rectangle SS = new Rectangle(0, 0, 1048, 720);
        #endregion

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            #region Maps laden
            MapTex[0] = Content.Load<Texture2D>("Maps/Einstieg");
            MapTex[1] = Content.Load<Texture2D>("Maps/Lava");
            MapTex[2] = Content.Load<Texture2D>("Maps/Wasser");
            #endregion
            Hintergrund = Content.Load<Texture2D>("Hintergrund");
            #region Spieler laden
            SpielerLinks1 = Content.Load<Texture2D>("Person/Links1");
            SpielerLinks2 = Content.Load<Texture2D>("Person/Links2");
            SpielerLinksSprung = Content.Load<Texture2D>("Person/LinksSpringen");
            SpielerRechts1 = Content.Load<Texture2D>("Person/Rechts1");
            SpielerRechts2 = Content.Load<Texture2D>("Person/Rechts2");
            SpielerRechtsSprung = Content.Load<Texture2D>("Person/RechtsSpringen");
            #endregion
            #region Blöcke laden
            //EIGENSCHAFTEN ALLER BLÖCKE:
            #region Ziel(0)
            BlockHart[0] = false;   //ID 0 = Ziel
            BlockinsgFrm[0] = 0;
            BlockFrm[0] = 0;
            #endregion
            #region Spawn(1)
            BlockHart[1] = false;   //ID 1 = Spawn
            BlockinsgFrm[1] = 0;
            BlockFrm[1] = 0;
            #endregion
            #region Luft(2)
            BlockHart[2] = false;    //ID 2 = Luft
            BlockinsgFrm[2] = 0;
            BlockFrm[2] = 0;
            #endregion
            #region Stein(3)
            BlockTex[3,1] = Content.Load<Texture2D>("Stein");  //ID 3 = SteinHart
            BlockHart[3] = true;
            BlockinsgFrm[3] = 1;
            BlockFrm[3] = 1;
            #endregion
            #region Erde(4)
            BlockTex[4,1] = Content.Load<Texture2D>("Erde");  //ID 4 = Erde Hart
            BlockHart[4] = true;
            BlockinsgFrm[4] = 1;
            BlockFrm[4] = 1;
            #endregion
            #region Gras(5)
            BlockTex[5, 1] = Content.Load<Texture2D>("Gras/Gras1"); //ID 5 = Gras Hart
            BlockTex[5, 2] = Content.Load<Texture2D>("Gras/Gras2");
            BlockTex[5, 3] = Content.Load<Texture2D>("Gras/Gras3");
            BlockHart[5] = true;
            BlockinsgFrm[5] = 3;
            BlockFrm[5] = 1;
            #endregion
            #region Holz(6)
            BlockTex[6,1] = Content.Load<Texture2D>("Holz"); //ID 6 = Holz Hart
            BlockHart[6] = true;
            BlockinsgFrm[6] = 1;
            BlockFrm[6] = 1;
            #endregion
            #region Blatt(7)
            BlockTex[7, 1] = Content.Load<Texture2D>("Blatt/Blatt1");  //ID 7 = Blatt Hart
            BlockTex[7, 2] = Content.Load<Texture2D>("Blatt/Blatt2");
            BlockTex[7, 3] = Content.Load<Texture2D>("Blatt/Blatt3");
            BlockHart[7] = true;
            BlockinsgFrm[7] = 3;
            BlockFrm[7] = 1;
            #endregion
            #region MohnBlume(8)
            BlockTex[8, 1] = Content.Load<Texture2D>("MohnBlume");  //ID 8 = MohnBlume NichtHart
            BlockHart[8] = false;
            BlockinsgFrm[8] = 1;
            BlockFrm[8] = 1;
            #endregion
            #region Steinmauer(9)
            BlockTex[9, 1] = Content.Load<Texture2D>("Steinmauer");  //ID 9 = Steinmauer Hart
            BlockHart[9] = true;
            BlockinsgFrm[9] = 1;
            BlockFrm[9] = 1;
            #endregion
            #region Wasser(10)
            BlockTex[10, 1] = Content.Load<Texture2D>("Wasser/Wasser1");  //ID 9 = Wasser NichtHart
            BlockTex[10, 2] = Content.Load<Texture2D>("Wasser/Wasser2");
            BlockTex[10, 3] = Content.Load<Texture2D>("Wasser/Wasser3");
            BlockTex[10, 4] = Content.Load<Texture2D>("Wasser/Wasser4");
            BlockTex[10, 5] = Content.Load<Texture2D>("Wasser/Wasser5");
            BlockTex[10, 6] = Content.Load<Texture2D>("Wasser/Wasser6");
            BlockTex[10, 7] = Content.Load<Texture2D>("Wasser/Wasser7");
            BlockHart[10] = false;
            BlockinsgFrm[10] = 7;
            BlockFrm[10] = 1;
            #endregion
            #endregion

            SS.Width = graphics.PreferredBackBufferWidth;
            SS.Height = graphics.PreferredBackBufferHeight;
            
            MapLoad(MapID);
            Spawn();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #region Tick
            Elapsed += (int)gameTime.ElapsedGameTime.Milliseconds;
            if(Elapsed >= delay)
            {
                Ticking();
                Elapsed = 0;
            }
            #endregion

            #region Skalierung
            SS.Width = graphics.PreferredBackBufferWidth;
            SS.Height = graphics.PreferredBackBufferHeight;
            for (int y = 0; y <= 10; y++)
            {
                for (int x = 0; x <= 20; x++)
                {
                    BlockPos[x, y].X = x * (SS.Width / MapTex[MapID].Width);
                    BlockPos[x, y].Y = y * (SS.Height / MapTex[MapID].Height);
                }
            }

            SpielerRect.Width = SS.Width / MapTex[MapID].Width - 10;
            SpielerRect.Height = SS.Height / MapTex[MapID].Height - 10;
            #endregion
            
            SpielerRect.X = (int)SpielerPos.X;
            SpielerRect.Y = (int)SpielerPos.Y;

            SpielerSteuerung();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(Hintergrund, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

            #region Map zeichnen
            for (int y = 0; y <= 10; y++)
            {
                for (int x = 0; x <= 20; x++)
                {
                    if (BlockID[x,y] == 3)          //Stein
                    {
                        spriteBatch.Draw(BlockTex[3, BlockFrm[3]], new Rectangle((int)BlockPos[x, y].X, (int)BlockPos[x, y].Y, SS.Width / MapTex[MapID].Width, SS.Height / MapTex[MapID].Height), Color.White);
                    }
                    else if (BlockID[x,y] == 4)     
                    {
                        spriteBatch.Draw(BlockTex[4, BlockFrm[4]], new Rectangle((int)BlockPos[x, y].X, (int)BlockPos[x, y].Y, SS.Width / MapTex[MapID].Width, SS.Height / MapTex[MapID].Height), Color.White);
                    }
                    else if (BlockID[x,y] == 5)
                    {
                        spriteBatch.Draw(BlockTex[5, BlockFrm[5]], new Rectangle((int)BlockPos[x, y].X, (int)BlockPos[x, y].Y, SS.Width / MapTex[MapID].Width, SS.Height / MapTex[MapID].Height), Color.White);
                    }
                    else if (BlockID[x,y] == 6)
                    {
                        spriteBatch.Draw(BlockTex[6, BlockFrm[6]], new Rectangle((int)BlockPos[x, y].X, (int)BlockPos[x, y].Y, SS.Width / MapTex[MapID].Width, SS.Height / MapTex[MapID].Height), Color.White);
                    }
                    else if (BlockID[x, y] == 7)
                    {
                        spriteBatch.Draw(BlockTex[7, BlockFrm[7]], new Rectangle((int)BlockPos[x, y].X, (int)BlockPos[x, y].Y, SS.Width / MapTex[MapID].Width, SS.Height / MapTex[MapID].Height), Color.White);
                    }
                    else if (BlockID[x, y] == 8)
                    {
                        spriteBatch.Draw(BlockTex[8, BlockFrm[8]], new Rectangle((int)BlockPos[x, y].X, (int)BlockPos[x, y].Y, SS.Width / MapTex[MapID].Width, SS.Height / MapTex[MapID].Height), Color.White);
                    }
                    else if (BlockID[x, y] == 9)
                    {
                        spriteBatch.Draw(BlockTex[9, BlockFrm[9]], new Rectangle((int)BlockPos[x, y].X, (int)BlockPos[x, y].Y, SS.Width / MapTex[MapID].Width, SS.Height / MapTex[MapID].Height), Color.White);
                    }
                    else if (BlockID[x, y] == 10)
                    {
                        spriteBatch.Draw(BlockTex[10, BlockFrm[10]], new Rectangle((int)BlockPos[x, y].X, (int)BlockPos[x, y].Y, SS.Width / MapTex[MapID].Width, SS.Height / MapTex[MapID].Height), Color.White);
                    }
                    else
                    {}
                }
            }
            #endregion

            #region Spieler zeichnen
            if(SpielerTex == "Rechts1")
            {
                spriteBatch.Draw(SpielerRechts1, SpielerRect, Color.White);
            }
            else if (SpielerTex == "Rechts2")
            {
                spriteBatch.Draw(SpielerRechts2, SpielerRect, Color.White);
            }
            else if (SpielerTex == "Links1")
            {
                spriteBatch.Draw(SpielerLinks1, SpielerRect, Color.White);
            }
            else if (SpielerTex == "Links2")
            {
                spriteBatch.Draw(SpielerLinks2, SpielerRect, Color.White);
            }
            else if (SpielerTex == "SpielerRechtsSprung")
            {
                spriteBatch.Draw(SpielerRechtsSprung, SpielerRect, Color.White);
            }
            else if (SpielerTex == "SpielerLinksSprung")
            {
                spriteBatch.Draw(SpielerLinksSprung, SpielerRect, Color.White);
            }
            else
            {}
            #endregion

            if(springt == true)
            {
                Sprunggeschwindigkeit += 1;
                SpielerPos.Y += Sprunggeschwindigkeit;
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void MapLoad (int Map)
        {
            Color[] colors1D = new Color[MapTex[MapID].Width * MapTex[MapID].Height];
            MapTex[MapID].GetData(colors1D);
            Color[,] colors2D = new Color[MapTex[MapID].Width, MapTex[MapID].Height];
            for (int y = 0; y <= 9; y++)
            {
                for (int x = 0; x <= 19; x++)
                {
                    colors2D[x, y] = colors1D[x + y * MapTex[MapID].Width];
                    if (colors2D[x, y].G == 0 && colors2D[x, y].R == 255 && colors2D[x, y].B == 220)
                    {
                        //SPAWN
                        SpielerSpawnPos.X = x * (SS.Width / 20);
                        SpielerSpawnPos.Y = y * (SS.Height / 10);
                        BlockID[x,y] = 1;
                    }
                    else if (colors2D[x, y].G == 128 && colors2D[x, y].R == 128 && colors2D[x, y].B == 128)
                    {
                        //STEIN
                        BlockID[x,y] = 3;
                    }
                    else if (colors2D[x, y].G == 0 && colors2D[x, y].R == 127 && colors2D[x, y].B == 0)
                    {
                        //ERDE
                        BlockID[x,y] = 4;
                    }
                    else if (colors2D[x, y].G == 255 && colors2D[x, y].R == 182 && colors2D[x, y].B == 0)
                    {
                        //GRAS
                        BlockID[x,y] = 5;
                    }
                    else if (colors2D[x, y].G == 0 && colors2D[x, y].R == 255 && colors2D[x, y].B == 0)
                    {
                        //HOLZ
                        BlockID[x,y] = 6;
                    }
                    else if (colors2D[x, y].G == 255 && colors2D[x, y].R == 76 && colors2D[x, y].B == 0)
                    {
                        //BLATT
                        BlockID[x,y] = 7;
                    }
                    else if (colors2D[x, y].R == 255 && colors2D[x, y].G == 100 && colors2D[x, y].B == 0)
                    {
                        //MohnBlume
                        BlockID[x, y] = 8;
                    }
                    else if (colors2D[x, y].R == 160 && colors2D[x, y].G == 160 && colors2D[x, y].B == 160)
                    {
                        //Steinmauer
                        BlockID[x, y] = 9;
                    }
                    else if (colors2D[x, y].R == 0 && colors2D[x, y].G == 0 && colors2D[x, y].B == 255)
                    {
                        //Wasser
                        BlockID[x, y] = 10;
                    }
                    
                    else
                    {
                        BlockID[x,y] = 0;
                    }
                }
            }
        }

        protected void Ticking ()
        {
            if (SpielerFrame <= SpielerinsgFrame)
            {

            }
            else
            {
                SpielerFrame = 1;
            }
            Animation();
        }

        protected void Spawn ()
        {
            SpielerPos.X = (int)SpielerSpawnPos.X;
            SpielerPos.Y = (int)SpielerSpawnPos.Y;
            SpielerPosOld = SpielerPos;
        }

        protected void SpielerSteuerung()
        {
            KeyboardState kState = Keyboard.GetState();
            #region Kollision
            for (int y = 0; y <= 9; y++)
            {
                for (int x = 0; x <= 19; x++)
                {
                    try
                    {

                        if (SpielerPos.X >= BlockPos[x,y].X && SpielerPos.X <= BlockPos[x,y].X + SS.Width / MapTex[MapID].Width && BlockHart[BlockID[x,y]] == true)
                        {
                            SpielerPos.X = (int)SpielerPosOld.X;
                        }
                        else
                        {

                        }

                        if (SpielerPos.Y >= BlockPos[x, y].Y && SpielerPos.Y <= BlockPos[x, y].Y + SS.Height / MapTex[MapID].Height && BlockHart[BlockID[x, y]] == true)
                        {
                            SpielerPos.Y = SpielerPosOld.Y;
                            springt = false;
                            Sprungerlaubt = true;
                        }
                        else
                        {

                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }
                }
            }
            #endregion
            #region Tastenbelegung
            SpielerPosOld = SpielerPos;
            //Springen:
            if (kState.IsKeyDown(Keys.W) && Sprungerlaubt == true)
            {
                Sprunggeschwindigkeit = -19;
                springt = true;
                Sprungerlaubt = false;
            }
            //rechts
            if(kState.IsKeyDown(Keys.D))
            {
                SpielerPos.X += 1.5f;
                SpielerTex = "Rechts1";
            }
            //links
            else if (kState.IsKeyDown(Keys.A))
            {
                SpielerPos.X -= 1.5f;
                SpielerTex = "Links1";
            }
            #endregion
            SpielerPos.Y += 2;
        }

        protected void Animation()
        {
            for (int i = 3; i <= 20; i++)
            {
                if (BlockFrm[i] < BlockinsgFrm[i])
                {
                    BlockFrm[i] += 1;
                }
                else
                {
                    BlockFrm[i] = 1;
                }
            }
        }
    }
}
