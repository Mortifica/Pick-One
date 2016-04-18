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
        public Player Player { get; set; }
        private GameState CurrentState { get; set; }
        public Camera2D Camera;
        


        public MainGameLoop()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1440;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
            LevelManager.Content = Content;
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

            // TODO: Add your update logic here
            CurrentState.Update(gameTime);
            
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

            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Start State
        /// </summary>
        public class StartState : GameState, IInputSubscriber 
        {
            
            private SpriteFont font;
            private Sprite background;
            private Options[] MenuOptions = new Options[3]
            {
                Options.StartGame,
                Options.Help,
                Options.Credits
            };
            private Vector2 MenuLocation = new Vector2(100, 100);
            private int currentOption = 1;
            private TimeSpan elapseTime = TimeSpan.Zero;
            private int menuSpeed = 100;
            private int currentColor = 0;
            public StartState(MainGameLoop game)
                : base(game)
            {
                init();
            }
            private void init()
            {
                font = game.Content.Load<SpriteFont>("mainMenuFont");
                
                var tempSubscriber = new KeyboardSubscriber()
                {
                    Subscriber = this,
                    IsPaused = false,
                    WatchedKeys = new List<Keys>()
                    {
                        Keys.A,
                        Keys.S,
                        Keys.W,
                        Keys.Up,
                        Keys.Down
            }
                };

                Listener = new KeyboardListener();
                Listener.AddSubscriber(tempSubscriber);
            }

            public override void Update(GameTime gameTime)
            {
                Listener.Update(Keyboard.GetState(), gameTime);

            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                Color color = Color.Black;
                Color topColor = Color.Black;
                currentColor += 1;
                if (currentColor % 1 == 0)
                {
                    topColor = Color.Black;
                }
                if (currentColor % 2 == 0)
                {
                    topColor = Color.Pink;
                }
                if (currentColor % 3 == 0)
                {
                    topColor = Color.Blue;
                }
                if (currentColor >= 1000)
                {
                    currentColor = 0;
                }


                Vector2 menu = MenuLocation;
                spriteBatch.DrawString(font, "Press \"A\" to select an option.", new Vector2(50, 40), topColor);
                spriteBatch.DrawString(font, "Navigate Menu with W,S,UP,Down.", new Vector2(50, 60), topColor);

                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    if (currentOption == i + 1)
                    {
                        color = Color.Red;
                    }
                    else
                    {
                        color = Color.Black;
                    }
                    spriteBatch.DrawString(font, MenuOptions[i].ToString(), menu += new Vector2(0, font.LineSpacing), color);
                }
            }
            public void NextState()
            {
                game.CurrentState = new PlayState(game);
            }

            public void NotifyOfChange(List<KeyAction> actions, GameTime gameTime)
            {
                if (elapseTime > TimeSpan.FromMilliseconds(menuSpeed))
                {
                    elapseTime = TimeSpan.Zero;
                }
                elapseTime += gameTime.ElapsedGameTime;
                foreach (var action in actions)
                {

                    if (action.HasNoAction)
                    {
                        continue;
                    }

                    if (action.WasPressed)
                    {
                        if (action.Key == Keys.A && MenuOptions[currentOption - 1].Equals(Options.StartGame))
                        {
                            NextState();
                            continue;
                        }
                        if ((action.Key == Keys.W || action.Key == Keys.Up) && elapseTime >= TimeSpan.FromMilliseconds(menuSpeed))
                        {
                            currentOption = currentOption - 1;
                            if (currentOption < 1) currentOption = 1;
                            continue;
                        }
                        if ((action.Key == Keys.S || action.Key == Keys.Down) && elapseTime >= TimeSpan.FromMilliseconds(menuSpeed))
                        {
                            currentOption = currentOption + 1;
                            if (currentOption > 3) currentOption = 3;
                            continue;
                        }

                    }

                    if (action.WasReleased)
                    {
                        //no action
                    }

                    if (action.IsBeingHeld)
                    {
                        if (action.Key == Keys.A && MenuOptions[currentOption - 1].Equals(Options.StartGame))
                        {
                            NextState();
                            continue;
                        }
                        if ((action.Key == Keys.W || action.Key == Keys.Up) && elapseTime >= TimeSpan.FromMilliseconds(menuSpeed))
                        {
                            currentOption = currentOption - 1;
                            if (currentOption < 1) currentOption = 1;
                            continue;
                        }
                        if ((action.Key == Keys.S || action.Key == Keys.Down) && elapseTime >= TimeSpan.FromMilliseconds(menuSpeed))
                        {
                            currentOption = currentOption + 1;
                            if (currentOption > 3) currentOption = 3;
                            continue;
                        }

                    }
                }

            }
            private enum Options
            {
                StartGame,
                Help,
                Credits
            }
        }

        /// <summary>
        /// Contains the Encapslated Game Logic
        /// </summary>
        public class PlayState : GameState, IInputSubscriber
        {
            private Hud hud;
            public KeyboardListener PlayStateKeyListener { get; set; }
            public List<PlayerSpriteContainer> PlayerSpriteContainers { get; set; }

            public PlayState(MainGameLoop game)
                : base(game)
            {
                
                init(game);
            }
            private void init(MainGameLoop game)
            {
                var font = game.Content.Load<SpriteFont>("mainMenuFont");
                var arrow = game.Content.Load<Texture2D>("directional_Arrow");
                hud = new Hud(game, font, arrow);
                //Normal
                var standingPlayer = game.Content.Load<Texture2D>(@"test_Circle_Standing_Animation");
                var fallingPlayer = game.Content.Load<Texture2D>(@"test_Circle_Falling_Animation");
                var movingPlayer = game.Content.Load<Texture2D>(@"test_Circle_Standing_Animation");
                var movingPlayerLeft = game.Content.Load<Texture2D>(@"test_Circle_Moving_Left_Animation");
                var movingPlayerRight = game.Content.Load<Texture2D>(@"test_Circle_Moving_Right_Animation");


                //Hover
                var hoverStanding = game.Content.Load<Texture2D>(@"test_Hover_Standing_Animation");
                var hoverMovingLeft = game.Content.Load<Texture2D>(@"test_Hover_Moving_Left_Animation");
                var hoverMovingRight = game.Content.Load<Texture2D>(@"test_Hover_Moving_Right_Animation");

                //Vertical
                var standingVertical = game.Content.Load<Texture2D>(@"test_Triangle_Standing_Animation");
                var jumpingVertical = game.Content.Load<Texture2D>(@"test_Triangle_Jumping_Animation");
                var midJumpVertical = game.Content.Load<Texture2D>(@"test_Triangle_MidJump_Animation");
                var landingVertical = game.Content.Load<Texture2D>(@"test_Triangle_JLanding_Animation");
                var fallingVertical = game.Content.Load<Texture2D>(@"test_Triangle_Falling_Animation");
                //var standingVertical = Content.Load<Texture2D>(@"test_Circle_Moving_Right_Animation");

                //WallClimb
                var standingClimb = game.Content.Load<Texture2D>(@"test_Square_Standing_Animation");
                var movingLeftClimb = game.Content.Load<Texture2D>(@"test_Square_Moving_Left_Animation");
                var movingRightClimb = game.Content.Load<Texture2D>(@"test_Square_Moving_Right_Animation");

                var poof = game.Content.Load<Texture2D>(@"test_Transition_Poof");

                //MiscTesting
                var climbingPlayer = game.Content.Load<Texture2D>(@"test_Circle_Moving_Animation");
                PlayerSpriteContainers = new List<PlayerSpriteContainer>();
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Normal
                {
                    StandingSprite = new Sprite(standingPlayer, 1, 4, 7),
                    MovingLeftSprite = new Sprite(movingPlayerLeft, 1, 4, 7),
                    MidJumpSprite = new Sprite(standingPlayer, 1, 4, 7),
                    LandingSprite = new Sprite(standingPlayer, 1, 6, 7),
                    FallingSprite = new Sprite(fallingPlayer, 1, 4, 7),
                    MovingRightSprite = new Sprite(movingPlayerRight, 1, 4, 7),
                    JumpingSprite = new Sprite(standingPlayer, 1, 4, 7),
                    WallClimbLeft = new Sprite(standingPlayer, 1, 4, 7),
                    WallClimbRight = new Sprite(standingPlayer, 1, 4, 7),
                    Poof = new Sprite(poof, 1, 13, 2)
                });
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Speed
                {
                    StandingSprite = new Sprite(fallingPlayer, 1, 4, 7),
                    MovingLeftSprite = new Sprite(climbingPlayer, 1, 4, 7),
                    MidJumpSprite = new Sprite(fallingPlayer, 1, 4, 7),
                    LandingSprite = new Sprite(fallingPlayer, 1, 6, 7),
                    FallingSprite = new Sprite(fallingPlayer, 1, 4, 7),
                    MovingRightSprite = new Sprite(standingPlayer, 1, 4, 7),
                    JumpingSprite = new Sprite(fallingPlayer, 1, 4, 7),
                    WallClimbLeft = new Sprite(fallingPlayer, 1, 4, 7),
                    WallClimbRight = new Sprite(fallingPlayer, 1, 4, 7),
                    Poof = new Sprite(poof, 1, 13, 2)
                });
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Stretch
                {
                    StandingSprite = new Sprite(hoverStanding, 1, 4, 7),
                    MovingLeftSprite = new Sprite(hoverMovingLeft, 1, 4, 7),
                    FallingSprite = new Sprite(hoverStanding, 1, 4, 7),
                    MidJumpSprite = new Sprite(hoverStanding, 1, 4, 7),
                    LandingSprite = new Sprite(hoverStanding, 1, 6, 7),
                    MovingRightSprite = new Sprite(hoverMovingRight, 1, 4, 7),
                    JumpingSprite = new Sprite(hoverStanding, 1, 4, 7),
                    WallClimbLeft = new Sprite(hoverStanding, 1, 4, 7),
                    WallClimbRight = new Sprite(hoverStanding, 1, 4, 7),
                    Poof = new Sprite(poof, 1, 13, 2)
                });
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Vertical
                {
                    StandingSprite = new Sprite(standingVertical, 1, 4, 7),
                    MovingLeftSprite = new Sprite(standingVertical, 1, 4, 7),
                    MovingRightSprite = new Sprite(standingVertical, 1, 4, 7),
                    JumpingSprite = new Sprite(jumpingVertical, 1, 6, 7),
                    MidJumpSprite = new Sprite(midJumpVertical, 1, 4, 7),
                    LandingSprite = new Sprite(landingVertical, 1, 6, 7),
                    FallingSprite = new Sprite(fallingVertical, 1, 6, 7),
                    WallClimbLeft = new Sprite(standingVertical, 1, 4, 7),
                    WallClimbRight = new Sprite(standingVertical, 1, 4, 7),
                    Poof = new Sprite(poof, 1, 13, 2)
                });
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Climbing
                {
                    StandingSprite = new Sprite(standingClimb, 1, 4, 7),
                    MovingLeftSprite = new Sprite(movingLeftClimb, 1, 7, 7),
                    FallingSprite = new Sprite(standingClimb, 1, 4, 7),
                    MovingRightSprite = new Sprite(movingRightClimb, 1, 7, 7),
                    MidJumpSprite = new Sprite(standingClimb, 1, 4, 7),
                    LandingSprite = new Sprite(standingClimb, 1, 6, 7),
                    JumpingSprite = new Sprite(standingClimb, 1, 4, 7),
                    WallClimbLeft = new Sprite(movingLeftClimb, 1, 7, 7),
                    WallClimbRight = new Sprite(movingRightClimb, 1, 7, 7),
                    Poof = new Sprite(poof, 1, 13, 2)
                });


                LevelManager.Instance.SetLevel(@"TestLevel");
                //GameManager.Instance.SetLevel(@"TestLevel2");
                
                PlayStateKeyListener = new KeyboardListener();
                game.Player = new Player(LevelManager.Instance.GetPlayerStartingLocation(), PlayerSpriteContainers);
                LevelManager.Player = game.Player;
                PlayStateKeyListener.AddSubscriber(new KeyboardSubscriber()
                {
                    Subscriber = game.Player,
                    WatchedKeys = game.Player.GetWatchedKeys(),
                    IsPaused = false
                });
                game.Camera.Focus = game.Player;
                game.Camera.Zoom = 1;
                game.Camera.FocusOffest = new Vector3(game.graphics.PreferredBackBufferWidth / 4, game.graphics.PreferredBackBufferHeight / 4, 0);

            }

            public override void Update(GameTime gameTime)
            {
                //Listener.Update(Keyboard.GetState(), gameTime);
                PlayStateKeyListener.Update(Keyboard.GetState(), gameTime);
                game.Player.Update(gameTime);
            }
            public override void Draw(SpriteBatch spriteBatch)
            {

                LevelManager.Instance.DrawLevel(spriteBatch);
                game.Player.Draw(spriteBatch);
                hud.Draw(spriteBatch);
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
