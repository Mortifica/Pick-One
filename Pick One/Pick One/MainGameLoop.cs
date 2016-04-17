using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pick_One.BasicClasses;
using System.Collections.Generic;
using Pick_One.Camera;
using Pick_One.Input;
using Pick_One.Character;
using Pick_One.Levels;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Pick_One
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>a
    public class MainGameLoop : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameState CurrentState { get; set; }
        private Camera2D Camera;
        public KeyboardListener PlayStateKeyListener { get; set; }
        public Player Player { get; set; }
        public List<PlayerSpriteContainer> PlayerSpriteContainers { get; set; }

        public MainGameLoop()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameManager.Content = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            // Set inital level
            GameManager.Instance.SetLevel(@"TestLevel");

            PlayStateKeyListener = new KeyboardListener();
            Player = new Player(GameManager.Instance.GetPlayerStartingLocation(), PlayerSpriteContainers);
            GameManager.Player = Player;
            PlayStateKeyListener.AddSubscriber(new KeyboardSubscriber()
            {
                Subscriber = Player,
                WatchedKeys = Player.GetWatchedKeys(),
                IsPaused = false
            });
            Camera.Focus = Player;
            Camera.Zoom = 2;
            Camera.FocusOffest = new Vector3(graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 4, 0);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentState = new StartState(this);
            Camera = new Camera2D(CurrentState);

            //Normal
            var standingPlayer = Content.Load<Texture2D>(@"test_Circle_Standing_Animation");
            var movingPlayer = Content.Load<Texture2D>(@"test_Circle_Standing_Animation");
            var movingPlayerLeft = Content.Load<Texture2D>(@"test_Circle_Moving_Left_Animation");
            var movingPlayerRight = Content.Load<Texture2D>(@"test_Circle_Moving_Right_Animation");


            //Hover
            var hoverStanding = Content.Load<Texture2D>(@"test_Hover_Standing_Animation");
            var hoverMovingLeft = Content.Load<Texture2D>(@"test_Hover_Moving_Left_Animation");
            var hoverMovingRight = Content.Load<Texture2D>(@"test_Hover_Moving_Right_Animation");

            //
            var fallingPlayer = Content.Load<Texture2D>(@"test_Circle_Falling_Animation");
            var climbingPlayer = Content.Load<Texture2D>(@"test_Circle_WallClimb_Animation");

            PlayerSpriteContainers = new List<PlayerSpriteContainer>();
            PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Normal
            {
                StandingSprite = new Sprite(standingPlayer,1,4,7),
                MovingLeftSprite = new Sprite(movingPlayerLeft, 1, 4, 7),
                MovingRightSprite = new Sprite(movingPlayerRight, 1, 4, 7),
                JumpingSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbUpSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbDownSprite = new Sprite(standingPlayer, 1, 4, 7)
            });
            PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Speed
            {
                StandingSprite = new Sprite(fallingPlayer, 1, 4, 7),
                MovingLeftSprite = new Sprite(climbingPlayer, 1,4, 7),
                MovingRightSprite = new Sprite(standingPlayer, 1, 4, 7),
                JumpingSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbUpSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbDownSprite = new Sprite(standingPlayer, 1, 4, 7)
            });
            PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Stretch
            {
                StandingSprite = new Sprite(hoverStanding, 1, 4, 7),
                MovingLeftSprite = new Sprite(hoverMovingLeft, 1, 4, 7),
                MovingRightSprite = new Sprite(hoverMovingRight, 1, 4, 7),
                JumpingSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbUpSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbDownSprite = new Sprite(standingPlayer, 1, 4, 7)
            });
            PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Vertical
            {
                StandingSprite = new Sprite(standingPlayer, 1, 4, 7),
                MovingLeftSprite = new Sprite(standingPlayer, 1, 4, 7),
                MovingRightSprite = new Sprite(standingPlayer, 1, 4, 7),
                JumpingSprite = new Sprite(fallingPlayer, 1, 4, 7),
                WallClimbUpSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbDownSprite = new Sprite(standingPlayer, 1, 4, 7)
            });
            PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Climbing
            {
                StandingSprite = new Sprite(standingPlayer, 1, 4, 7),
                MovingLeftSprite = new Sprite(standingPlayer, 1, 4, 7),
                MovingRightSprite = new Sprite(standingPlayer, 1, 4, 7),
                JumpingSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbUpSprite = new Sprite(standingPlayer, 1, 4, 7),
                WallClimbDownSprite = new Sprite(standingPlayer, 1, 4, 7)
            });

            // load sounds
            var deathSound = Content.Load<Song>("death");
            var countdownSound = Content.Load<Song>("countdown");
            var goSound = Content.Load<Song>("go");
            var finishSound = Content.Load<Song>("finish");
            var hoverSound = Content.Load<Song>("hover");
            var squishOneSound = Content.Load<Song>("squish_one");
            var squishTwoSound = Content.Load<Song>("squish_two");
            var squishThreeSound = Content.Load<Song>("squish_three");
            var jumpSound = Content.Load<Song>("jump");
            var poofSound = Content.Load<Song>("poof");
            var menuOptionChangeSound = Content.Load<Song>("MenuSelect");
            var menuSelectSound = Content.Load<Song>("select");
            var tutorialTheme = Content.Load<Song>("tutorial");
            var levelTheme = Content.Load<Song>("LevelTheme");

            

            //MediaPlayer.IsRepeating = true;
           // MediaLibrary mediaLibrary = new MediaLibrary();
            

            //SongCollection songs = mediaLibrary.Songs;
            //Song song = songs[0];

            MediaPlayer.Play(jumpSound);
            //MediaPlayer.Volume = 2;
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

            // TODO: Unload any non ContentManager content here
        }

        // Frame counts for keeping an eye on performace
        private int ticks = 0;
        private int frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            PlayStateKeyListener.Update(Keyboard.GetState(), gameTime);
            // TODO: Add your update logic here
            CurrentState.Update(gameTime);
            Player.Update();
            base.Update(gameTime);


            // Update the title bar so we can see FPS easier.
            ticks++;
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                Window.Title = $"Pick One   FPS:{frameRate}   Ticks:{ticks}";
                frameCounter = 0;
                ticks = 0;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            frameCounter++;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin(
                                SpriteSortMode.BackToFront,
                                BlendState.AlphaBlend,
                                SamplerState.PointClamp,
                                null,
                                null,
                                null,
                                Camera.GetMatrixTransformation(GraphicsDevice)
                            );

            CurrentState.Draw(spriteBatch);

            GameManager.Instance.DrawLevel(spriteBatch);
            Player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Start State
        /// </summary>
        public class StartState : GameState, IInputSubscriber 
        {

            public StartState(MainGameLoop game)
                : base(game)
            {
                init();
            }
            private void init()
            {
                
            }

            public override void Update(GameTime gameTime)
            {
                //Listener.Update(Keyboard.GetState(), gameTime);

            }

            public override void Draw(SpriteBatch spriteBatch)
            {

            }
            public void NextState()
            {
                game.CurrentState = new PlayState(game);
            }

            public void NotifyOfChange(List<KeyAction> actions, GameTime gameTime)
            {
                foreach (var action in actions)
                {

                    if (action.HasNoAction)
                    {
                        continue;
                    }

                    if (action.WasPressed)
                    {

                    }

                    if (action.WasReleased)
                    {

                    }

                    if (action.IsBeingHeld)
                    {

                    }
                }

            }
        }

        /// <summary>
        /// Contains the Encapslated Game Logic
        /// </summary>
        public class PlayState : GameState, IInputSubscriber
        {
            

            public PlayState(MainGameLoop game)
                : base(game)
            {
                
                init(game);
            }
            private void init(MainGameLoop game)
            {
                

            }

            public override void Update(GameTime gameTime)
            {
                //Listener.Update(Keyboard.GetState(), gameTime);

            }
            public override void Draw(SpriteBatch spriteBatch)
            {

            }
            public void NotifyOfChange(List<KeyAction> actions, GameTime gameTime)
            {
                foreach (var action in actions)
                {

                    if (action.HasNoAction)
                    {
                        continue;
                    }

                    if (action.WasPressed)
                    {

                    }

                    if (action.WasReleased)
                    {

                    }

                    if (action.IsBeingHeld)
                    {

                    }
                }
            }
        }

    }
}
