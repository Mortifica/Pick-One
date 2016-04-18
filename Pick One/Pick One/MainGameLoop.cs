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
        private const string FONT_FOLDER = @"SpriteFonts\";

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Player Player { get; set; }
        private GameState CurrentState { get; set; }
        public Camera2D Camera;
        private Texture2D standingPlayer;
        private Texture2D fallingPlayer;
        private Texture2D movingPlayer;
        private Texture2D movingPlayerLeft;
        private Texture2D movingPlayerRight;
        private Texture2D hoverStanding;
        private Texture2D hoverMovingLeft;
        private Texture2D hoverMovingRight;

        private Texture2D standingVertical;
        private Texture2D jumpingVertical;
        private Texture2D midJumpVertical;

        private Texture2D landingVertical;
        private Texture2D fallingVertical;
        private Texture2D standingClimb;
        private Texture2D movingLeftClimb;
        private Texture2D movingRightClimb;
        private Texture2D poof;

        public SpriteFont timerFont;


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
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = (float)0.10;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.


            //Normal
            standingPlayer = Content.Load<Texture2D>(@"test_Circle_Standing_Animation");
            fallingPlayer = Content.Load<Texture2D>(@"test_Circle_Falling_Animation");
            movingPlayer = Content.Load<Texture2D>(@"test_Circle_Standing_Animation");
            movingPlayerLeft = Content.Load<Texture2D>(@"test_Circle_Moving_Left_Animation");
            movingPlayerRight = Content.Load<Texture2D>(@"test_Circle_Moving_Right_Animation");


            //Hover
            hoverStanding = Content.Load<Texture2D>(@"test_Hover_Standing_Animation");
            hoverMovingLeft = Content.Load<Texture2D>(@"test_Hover_Moving_Left_Animation");
            hoverMovingRight = Content.Load<Texture2D>(@"test_Hover_Moving_Right_Animation");

            //Vertical
            standingVertical = Content.Load<Texture2D>(@"test_Triangle_Standing_Animation");
            jumpingVertical = Content.Load<Texture2D>(@"test_Triangle_Jumping_Animation");
            midJumpVertical = Content.Load<Texture2D>(@"test_Triangle_MidJump_Animation");
            landingVertical = Content.Load<Texture2D>(@"test_Triangle_JLanding_Animation");
            fallingVertical = Content.Load<Texture2D>(@"test_Triangle_Falling_Animation");
            //var standingVertical = Content.Load<Texture2D>(@"test_Circle_Moving_Right_Animation");

            //WallClimb
            standingClimb = Content.Load<Texture2D>(@"test_Square_Standing_Animation");
            movingLeftClimb = Content.Load<Texture2D>(@"test_Square_Moving_Left_Animation");
            movingRightClimb = Content.Load<Texture2D>(@"test_Square_Moving_Right_Animation");

            poof = Content.Load<Texture2D>(@"test_Transition_Poof");

            timerFont = Content.Load<SpriteFont>(FONT_FOLDER + "timer_Font");
            // Sound Effects
            SoundContainer.Instance.Death = Content.Load<SoundEffect>(@"SoundEffects\death");
            SoundContainer.Instance.CountDown = Content.Load<SoundEffect>(@"SoundEffects\countdown");
            SoundContainer.Instance.Go = Content.Load<SoundEffect>(@"SoundEffects\go");
            SoundContainer.Instance.Finish = Content.Load<SoundEffect>(@"SoundEffects\finish");
            SoundContainer.Instance.Hover = Content.Load<SoundEffect>(@"SoundEffects\hover");
            SoundContainer.Instance.Squish1 = Content.Load<SoundEffect>(@"SoundEffects\squish_one");
            SoundContainer.Instance.Squish2 = Content.Load<SoundEffect>(@"SoundEffects\squish_two");
            //SoundContainer.Instance.Squish3 = Content.Load<SoundEffect>("squish_three");
            SoundContainer.Instance.Jump = Content.Load<SoundEffect>(@"SoundEffects\jump");
            SoundContainer.Instance.Poof = Content.Load<SoundEffect>(@"SoundEffects\poof");
            SoundContainer.Instance.MenuSelect = Content.Load<SoundEffect>(@"SoundEffects\MenuSelect");
            SoundContainer.Instance.Select = Content.Load<SoundEffect>(@"SoundEffects\select");

            // Songs
            SoundContainer.Instance.Tutorial = Content.Load<Song>(@"Songs\tutorial");
            SoundContainer.Instance.LevelTheme = Content.Load<Song>(@"Songs\LevelTheme");

            MediaPlayer.Play(SoundContainer.Instance.Tutorial);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentState = new StartState(this);
            Camera = new Camera2D(CurrentState);
            //MediaPlayer.IsRepeating = true;
            // MediaLibrary mediaLibrary = new MediaLibrary();


            //SongCollection songs = mediaLibrary.Songs;
            //Song song = songs[0];

            //MediaPlayer.Play(tutorialTheme);
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

            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin(
                                SpriteSortMode.Immediate,
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
            
            private SpriteFont titleFont;
            private SpriteFont optionFont;
            private Sprite circle;
            private Sprite hover;
            private Sprite square;
            private Sprite triangle;
            private Options[] MenuOptions = new Options[2]
            {
                Options.StartGame,
                Options.Credits
            };
            private Vector2 MenuLocation = new Vector2(100, 100);
            private int currentOption = 1;
            private TimeSpan elapseTime = TimeSpan.Zero;
            private int menuSpeed = 100;
            private bool isLoading = true;
            private TimeSpan loadingTime = TimeSpan.Zero;
            public StartState(MainGameLoop game)
                : base(game)
            {
                init();
            }
            private void init()
            {
                titleFont = game.Content.Load<SpriteFont>(FONT_FOLDER + "mainMenuFont");
                optionFont = game.Content.Load<SpriteFont>(FONT_FOLDER + "menu_Options_Font");
                
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
                circle = new Sprite(game.standingPlayer, 1, 4, 7);
                hover = new Sprite(game.hoverStanding, 1, 4, 7);
                square = new Sprite(game.standingClimb, 1, 4, 7);
                triangle = new Sprite(game.standingVertical, 1, 4, 7);
            }

            public override void Update(GameTime gameTime)
            {
                circle.Update();
                hover.Update();
                square.Update();
                triangle.Update();
                if (isLoading)
                {
                    loadingTime += gameTime.ElapsedGameTime;
                    if (loadingTime > TimeSpan.FromMilliseconds(1000))
                    {
                        isLoading = false;
                    }
                    return;
                }


                Listener.Update(Keyboard.GetState(), gameTime);

            }

            public override void Draw(SpriteBatch spriteBatch)
            {



                Color color = Color.White;
                Color topColor = Color.DarkOrange;
                Random rand = new Random();
                var windowHeight = game.GraphicsDevice.Viewport.Height;
                var windowWidth = game.GraphicsDevice.Viewport.Width;
                var lineup = windowWidth / 2 -150;
                string title = "\"Pick One\"";
                Vector2 menu = new Vector2(windowWidth / 2 - titleFont.MeasureString(title).X / 2 + 100, titleFont.LineSpacing + 200);
                circle.Draw(spriteBatch, new Vector2(250, 250), 4);
                hover.Draw(spriteBatch, new Vector2(windowWidth - 500, 250),4);
                square.Draw(spriteBatch, new Vector2(windowWidth - 500, windowHeight - 250), 4);
                triangle.Draw(spriteBatch, new Vector2(250, windowHeight - 250), 4);
                spriteBatch.DrawString(titleFont, title, new Vector2(windowWidth/2 - titleFont.MeasureString(title).X / 2 + rand.Next(5), 0 + rand.Next(10)) , topColor);
                spriteBatch.DrawString(optionFont, "Press \"A\" to select an option.", new Vector2(lineup, windowHeight - 80), topColor);
                spriteBatch.DrawString(optionFont, "Navigate Menu with W,S,UP,Down.", new Vector2(lineup, windowHeight - 40), topColor);

                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    if (currentOption == i + 1)
                    {
                        color = Color.Red;
                    }
                    else
                    {
                        color = Color.White;
                    }
                    spriteBatch.DrawString(optionFont, MenuOptions[i].ToString(), menu += new Vector2(0, optionFont.LineSpacing * i), color);
                }
            }
            public override void NextState()
            {
                game.CurrentState = new HelpState(game);
                
            }
            private void CreditsState()
            {
                game.CurrentState = new DeadState(game);
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
                        if (action.Key == Keys.A && MenuOptions[currentOption - 1].Equals(Options.Credits))
                        {
                            CreditsState();
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
                            if (currentOption > 2) currentOption = 2;
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
                        if (action.Key == Keys.A && MenuOptions[currentOption - 1].Equals(Options.Credits))
                        {
                            CreditsState();
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
                            if (currentOption > 2) currentOption = 2;
                            continue;
                        }

                    }
                }

            }
            private enum Options
            {
                StartGame,
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
                var font = game.Content.Load<SpriteFont>(FONT_FOLDER + "game_Hud_Font");
                var arrow = game.Content.Load<Texture2D>("directional_Arrow");
                var hudBackground = game.Content.Load<Texture2D>("hud_Background");
                hud = new Hud(game, this, font, arrow, hudBackground);


                //MiscTesting
                var climbingPlayer = game.Content.Load<Texture2D>(@"test_Circle_Moving_Animation");
                PlayerSpriteContainers = new List<PlayerSpriteContainer>();
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Normal
                {
                    StandingSprite = new Sprite(game.standingPlayer, 1, 4, 7),
                    MovingLeftSprite = new Sprite(game.movingPlayerLeft, 1, 4, 7),
                    MidJumpSprite = new Sprite(game.standingPlayer, 1, 4, 7),
                    LandingSprite = new Sprite(game.standingPlayer, 1, 6, 7),
                    FallingSprite = new Sprite(game.standingPlayer, 1, 4, 7),
                    MovingRightSprite = new Sprite(game.movingPlayerRight, 1, 4, 7),
                    JumpingSprite = new Sprite(game.standingPlayer, 1, 4, 7),
                    WallClimbLeft = new Sprite(game.standingPlayer, 1, 4, 7),
                    WallClimbRight = new Sprite(game.standingPlayer, 1, 4, 7),
                    Poof = new Sprite(game.poof, 1, 13, 2)
                });
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Speed
                {
                    StandingSprite = new Sprite(game.standingPlayer, 1, 4, 7),
                    MovingLeftSprite = new Sprite(game.movingPlayerLeft, 1, 4, 7),
                    MidJumpSprite = new Sprite(game.standingPlayer, 1, 4, 7),
                    LandingSprite = new Sprite(game.standingPlayer, 1, 6, 7),
                    FallingSprite = new Sprite(game.standingPlayer, 1, 4, 7),
                    MovingRightSprite = new Sprite(game.movingPlayerRight, 1, 4, 7),
                    JumpingSprite = new Sprite(game.standingPlayer, 1, 4, 7),
                    WallClimbLeft = new Sprite(game.standingPlayer, 1, 4, 7),
                    WallClimbRight = new Sprite(game.standingPlayer, 1, 4, 7),
                    Poof = new Sprite(game.poof, 1, 13, 2)
                });
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Stretch
                {
                    StandingSprite = new Sprite(game.hoverStanding, 1, 4, 7),
                    MovingLeftSprite = new Sprite(game.hoverMovingLeft, 1, 4, 7),
                    FallingSprite = new Sprite(game.hoverStanding, 1, 4, 7),
                    MidJumpSprite = new Sprite(game.hoverStanding, 1, 4, 7),
                    LandingSprite = new Sprite(game.hoverStanding, 1, 6, 7),
                    MovingRightSprite = new Sprite(game.hoverMovingRight, 1, 4, 7),
                    JumpingSprite = new Sprite(game.hoverStanding, 1, 4, 7),
                    WallClimbLeft = new Sprite(game.hoverStanding, 1, 4, 7),
                    WallClimbRight = new Sprite(game.hoverStanding, 1, 4, 7),
                    Poof = new Sprite(game.poof, 1, 13, 2)
                });
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Vertical
                {
                    StandingSprite = new Sprite(game.standingVertical, 1, 4, 7),
                    MovingLeftSprite = new Sprite(game.standingVertical, 1, 4, 7),
                    MovingRightSprite = new Sprite(game.standingVertical, 1, 4, 7),
                    JumpingSprite = new Sprite(game.jumpingVertical, 1, 6, 7),
                    MidJumpSprite = new Sprite(game.midJumpVertical, 1, 4, 7),
                    LandingSprite = new Sprite(game.landingVertical, 1, 6, 7),
                    FallingSprite = new Sprite(game.fallingVertical, 1, 6, 7),
                    WallClimbLeft = new Sprite(game.standingVertical, 1, 4, 7),
                    WallClimbRight = new Sprite(game.standingVertical, 1, 4, 7),
                    Poof = new Sprite(game.poof, 1, 13, 2)
                });
                PlayerSpriteContainers.Add(new PlayerSpriteContainer() // Climbing
                {
                    StandingSprite = new Sprite(game.standingClimb, 1, 4, 7),
                    MovingLeftSprite = new Sprite(game.movingLeftClimb, 1, 7, 7),
                    FallingSprite = new Sprite(game.standingClimb, 1, 4, 7),
                    MovingRightSprite = new Sprite(game.movingRightClimb, 1, 7, 7),
                    MidJumpSprite = new Sprite(game.standingClimb, 1, 4, 7),
                    LandingSprite = new Sprite(game.standingClimb, 1, 6, 7),
                    JumpingSprite = new Sprite(game.standingClimb, 1, 4, 7),
                    WallClimbLeft = new Sprite(game.movingLeftClimb, 1, 7, 7),
                    WallClimbRight = new Sprite(game.movingRightClimb, 1, 7, 7),
                    Poof = new Sprite(game.poof, 1, 13, 2)
                });


                LevelManager.Instance.SetLevel(@"Level1");
                
                PlayStateKeyListener = new KeyboardListener();
                game.Player = new Player(LevelManager.Instance.GetStartingPosition(), PlayerSpriteContainers);
                LevelManager.Player = game.Player;
                PlayStateKeyListener.AddSubscriber(new KeyboardSubscriber()
                {
                    Subscriber = game.Player,
                    WatchedKeys = game.Player.GetWatchedKeys(),
                    IsPaused = false
                });
                game.Camera.Focus = game.Player;
                game.Camera.Zoom = 1f;
                game.Camera.FocusOffest = new Vector3(game.graphics.PreferredBackBufferWidth / 2, game.graphics.PreferredBackBufferHeight / 2, 0);

            }

            public override void Update(GameTime gameTime)
            {
                //Listener.Update(Keyboard.GetState(), gameTime);
                PlayStateKeyListener.Update(Keyboard.GetState(), gameTime);
                game.Player.Update(gameTime);
                hud.Update(gameTime);
            }
            public override void Draw(SpriteBatch spriteBatch)
            {
                game.Player.Draw(spriteBatch);
                LevelManager.Instance.DrawLevel(spriteBatch);
                
                hud.Draw(spriteBatch);
            }
            public override void NextState()
            {
                game.Camera.Focus = null;
                game.Camera.FocusOffest = Vector3.Zero;
                game.CurrentState = new DeadState(game);

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
        /// Start State
        /// </summary>
        public class DeadState : GameState, IInputSubscriber
        {

            private SpriteFont font;
            private SpriteFont textFont;
            private Options[] MenuOptions = new Options[1]
            {
                Options.MainMenu
            };
            private Vector2 MenuLocation = new Vector2(100, 100);
            private int currentOption = 1;
            private TimeSpan elapseTime = TimeSpan.Zero;
            private int menuSpeed = 100;
            private int currentColor = 0;
            private bool isLoading = true;
            private TimeSpan loadingTime = TimeSpan.Zero;
            public DeadState(MainGameLoop game)
                : base(game)
            {
                init();
            }
            private void init()
            {
                font = game.Content.Load<SpriteFont>(FONT_FOLDER + "mainMenuFont");
                textFont = game.Content.Load<SpriteFont>(FONT_FOLDER + "menu_Options_Font");

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

                if (isLoading)
                {
                    loadingTime += gameTime.ElapsedGameTime;
                    if (loadingTime > TimeSpan.FromMilliseconds(1000))
                    {
                        isLoading = false;
                    }
                    return;
                }
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
                spriteBatch.DrawString(font, "Credits", new Vector2(200, 0), Color.Orange);
                spriteBatch.DrawString(textFont, "Thanks For Playing", new Vector2(200, 150), topColor);
                spriteBatch.DrawString(textFont, "Please leave some comments", new Vector2(200, textFont.LineSpacing + 150), topColor);
                spriteBatch.DrawString(textFont, "And Sorry for the Seizures", new Vector2(200, textFont.LineSpacing * 2 + 150), topColor);
                spriteBatch.DrawString(textFont, "Hankenstien: Art", new Vector2(200, textFont.LineSpacing * 5 + 150), Color.Orange);
                spriteBatch.DrawString(textFont, "ViperD: Coding", new Vector2(200, textFont.LineSpacing * 6 + 150), Color.Orange);
                spriteBatch.DrawString(textFont, "Tombz26: Coding", new Vector2(200, textFont.LineSpacing * 7 + 150), Color.Orange);
                spriteBatch.DrawString(textFont, "KoukiiMonster: Music", new Vector2(200, textFont.LineSpacing * 8 + 150), Color.Orange);


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
                    spriteBatch.DrawString(textFont,"Press \"A\" for the " + MenuOptions[i].ToString(), new Vector2(200, textFont.LineSpacing * 12 + 150), color);
                }
            }
            public override void NextState()
            {
                game.CurrentState = new StartState(game);
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
                        if (action.Key == Keys.A && MenuOptions[currentOption - 1].Equals(Options.MainMenu))
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
                        //if (action.Key == Keys.A && MenuOptions[currentOption - 1].Equals(Options.MainMenu))
                        //{
                        //    NextState();
                        //    continue;
                        //}
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
                MainMenu
            }
        }
        /// <summary>
        /// Start State
        /// </summary>
        public class HelpState : GameState, IInputSubscriber
        {

            private SpriteFont titleFont;
            private SpriteFont optionFont;
            private Sprite circle;
            private Sprite hover;
            private Sprite square;
            private Sprite triangle;
            private Options[] MenuOptions = new Options[1]
            {
                Options.Start
            };
            private Vector2 MenuLocation = new Vector2(100, 100);
            private int currentOption = 1;
            private TimeSpan elapseTime = TimeSpan.Zero;
            private int menuSpeed = 100;
            private int currentColor = 0;
            private bool isLoading = true;
            private TimeSpan loadingTime = TimeSpan.Zero;
            public HelpState(MainGameLoop game)
                : base(game)
            {
                init();
            }
            private void init()
            {
                titleFont = game.Content.Load<SpriteFont>(FONT_FOLDER + "mainMenuFont");
                optionFont = game.Content.Load<SpriteFont>(FONT_FOLDER + "menu_Options_Font");
                circle = new Sprite(game.standingPlayer, 1, 4, 7);
                hover = new Sprite(game.hoverStanding, 1, 4, 7);
                square = new Sprite(game.standingClimb, 1, 4, 7);
                triangle = new Sprite(game.standingVertical, 1, 4, 7);
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
                circle.Update();
                hover.Update();
                square.Update();
                triangle.Update();
                if (isLoading)
                {
                    loadingTime += gameTime.ElapsedGameTime;
                    if (loadingTime > TimeSpan.FromMilliseconds(1000))
                    {
                        isLoading = false;
                    }
                    return;
                }
                Listener.Update(Keyboard.GetState(), gameTime);

            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                Color color = Color.White;
                Color topColor = Color.White;

                
                circle.Draw(spriteBatch, new Vector2(100, 300), 2);
                spriteBatch.DrawString(optionFont, "Rolli is fast left and right, but cannot jump.", new Vector2(200, 300), Color.Orange);
                spriteBatch.DrawString(optionFont, "Press 1 to ShapeShift to him", new Vector2(200, 300 + optionFont.LineSpacing), Color.Orange);
                hover.Draw(spriteBatch, new Vector2(100, 400), 2);
                spriteBatch.DrawString(optionFont, "Hovi is slow left and right. He cannot jump, and is not effected by gravity.", new Vector2(200, 400), Color.Orange);
                spriteBatch.DrawString(optionFont, "Press 2 to ShapeShift to him", new Vector2(200, 400 + optionFont.LineSpacing), Color.Orange);
                square.Draw(spriteBatch, new Vector2(100, 500), 2);
                spriteBatch.DrawString(optionFont, "Climbi is slow left and right. He cannot jump, but can climb some walls", new Vector2(200, 500), Color.Orange);
                spriteBatch.DrawString(optionFont, "Press 3 to ShapeShift to him", new Vector2(200, 500 + optionFont.LineSpacing), Color.Orange);
                triangle.Draw(spriteBatch, new Vector2(100, 600), 2);
                spriteBatch.DrawString(optionFont, "Jumpi cannot move left and right, but can he can jump up levels", new Vector2(200, 600), Color.Orange);
                spriteBatch.DrawString(optionFont, "Press 4 to ShapeShift to him", new Vector2(200, 600 + optionFont.LineSpacing), Color.Orange);

                spriteBatch.DrawString(titleFont, "How to Play", new Vector2(200, 0), topColor);
                spriteBatch.DrawString(optionFont, "A,D for movement and Space to Jump", new Vector2(200, titleFont.LineSpacing + 5), topColor);
                spriteBatch.DrawString(optionFont, "The number keys will ShapeShift you into other characters.", new Vector2(200, titleFont.LineSpacing + 5 + optionFont.LineSpacing), topColor);
                spriteBatch.DrawString(optionFont, "Goal: get to the end of the level in the given time.  The Green arrow points at the Finish Zone.", new Vector2(200, titleFont.LineSpacing + 5 + optionFont.LineSpacing*2), topColor);
                Vector2 menu = new Vector2(300, titleFont.LineSpacing + 5 + (optionFont.LineSpacing*3));
                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    if (currentOption == i + 1)
                    {
                        color = Color.Red;
                    }
                    else
                    {
                        color = Color.White;
                    }
                    spriteBatch.DrawString(optionFont, "Press \"A\" to continue" + MenuOptions[i].ToString(), menu, color);
                }
            }
            public override void NextState()
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
                        if (action.Key == Keys.A && MenuOptions[currentOption - 1].Equals(Options.Start))
                        {
                            NextState();
                            continue;
                        }
                        //if ((action.Key == Keys.W || action.Key == Keys.Up) && elapseTime >= TimeSpan.FromMilliseconds(menuSpeed))
                        //{
                        //    currentOption = currentOption - 1;
                        //    if (currentOption < 1) currentOption = 1;
                        //    continue;
                        //}
                        //if ((action.Key == Keys.S || action.Key == Keys.Down) && elapseTime >= TimeSpan.FromMilliseconds(menuSpeed))
                        //{
                        //    currentOption = currentOption + 1;
                        //    if (currentOption > 3) currentOption = 3;
                        //    continue;
                        //}

                    }

                    if (action.WasReleased)
                    {
                        //no action
                    }

                    if (action.IsBeingHeld)
                    {
                        if (action.Key == Keys.A && MenuOptions[currentOption - 1].Equals(Options.Start))
                        {
                            //NextState();
                            continue;
                        }
                        //if ((action.Key == Keys.W || action.Key == Keys.Up) && elapseTime >= TimeSpan.FromMilliseconds(menuSpeed))
                        //{
                        //    currentOption = currentOption - 1;
                        //    if (currentOption < 1) currentOption = 1;
                        //    continue;
                        //}
                        //if ((action.Key == Keys.S || action.Key == Keys.Down) && elapseTime >= TimeSpan.FromMilliseconds(menuSpeed))
                        //{
                        //    currentOption = currentOption + 1;
                        //    if (currentOption > 3) currentOption = 3;
                        //    continue;
                        //}

                    }
                }

            }

            private enum Options
            {
                Start
            }
        }
    }
}
