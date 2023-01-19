using Cocos2D;
using CocosDenshion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LondonSewersRPG.Source;

namespace LondonSewersRPG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static Environment platforms;
        static Environment bossRoom;

        static Texture2D charRatRight;
        static Texture2D charRatLeft;
        static Rectangle charRect;
        static Rectangle bossRoomCharRect;

        static Texture2D bossRight;
        static Texture2D bossLeft;
        static Rectangle bossRect;
        static Rectangle bossHealthRect;

        static double bossActualY;

        static bool bossFacingRight;
        static bool bossFalling;
        static bool bossCrouching;
        static double bossHealth;
        static double bossYSpeed;
        static int bossAttackTimer;
        static int currentBossAttack;

        static double bossRoomActualX;
        static double bossRoomActualY;

        static Rectangle[] cannonBallRects;
        static Texture2D cannonBall;

        static Texture2D wingedRat;
        static Rectangle wingedRatRect;
        static double wingedRatActualX;
        static double wingedRatActualY;
        static int wingedRatFlyTimer;
        static double wingedRatYSpeed;
        static double wingedRatJumpSpeed;

        static Rectangle charHealthRect;
        static Texture2D healthBar;

        static Rectangle charAttackRightRect;
        static Rectangle charAttackLeftRect;
        static Texture2D attackRight;
        static Texture2D attackLeft;

        static Rectangle buffRatRect;
        static double buffRatHealth;
        static double buffRatDamage;
        static double buffRatSpeed;
        static int buffRatAttackTimer;
        static bool buffRatFacingRight;
        static double buffRatActualX;
        static double buffRatActualY;
        static bool buffRatAlive;

        static bool attackingState;
        static bool falling;
        static bool crouching;
        static bool facingRight;

        static double gravity;
        static double ySpeed;
        static double xSpeed;
        static double jumpSpeed;
        static double charMaxHealth;
        static double charHealth;
        static double charDamage;

        static int attackTimer;

        static SpriteFont font;

        static bool gameActiveState;
        static bool gameBossState;
        static bool gameMenuState;

        static Rectangle menuRect;
        static Texture2D menu;

        static System.Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            charRect = new Rectangle(355, 200, 90, 90);
            bossRoomCharRect = new Rectangle(42, 402, 36, 36);

            cannonBallRects = new Rectangle[]
            {
                new Rectangle(800, 360, 20, 20),
                new Rectangle(1600, 400, 20, 20)
            };

            charHealthRect = new Rectangle(355, 175, 90, 10);

            charAttackRightRect = new Rectangle(445, 215, 50, 70);
            charAttackLeftRect = new Rectangle(305, 215, 50, 70);

            falling = false;
            crouching = false;
            attackingState = false;
            facingRight = true;

            gravity = .5;
            ySpeed = 0;
            xSpeed = 7;
            jumpSpeed = -12;
            charMaxHealth = 10.0;
            charHealth = charMaxHealth;
            charDamage = 5.0;

            bossRoomActualX = bossRoomCharRect.X;
            bossRoomActualY = bossRoomCharRect.Y;

            bossRect = new Rectangle(340, 30, 120, 100);
            bossHealthRect = new Rectangle(30, 10, 740, 20);

            bossActualY = bossRect.Y;

            bossFacingRight = true;
            bossHealth = 500.0;
            bossFalling = true;
            bossAttackTimer = -300;
            bossYSpeed = 0.0;
            currentBossAttack = 1;

            buffRatRect = new Rectangle(555, 140, 150, 150);

            buffRatHealth = 20.0;
            buffRatSpeed = 1.5;
            buffRatDamage = 5.0;
            buffRatAttackTimer = 0;
            buffRatFacingRight = false;
            buffRatAlive = true;

            buffRatActualX = buffRatRect.X;
            buffRatActualY = buffRatRect.Y;

            wingedRatRect = new Rectangle(2550, 290, 100, 100);

            wingedRatFlyTimer = 0;
            wingedRatJumpSpeed = -15;
            wingedRatYSpeed = 0.0;

            wingedRatActualY = wingedRatRect.Y;
            wingedRatActualX = wingedRatRect.X;

            attackTimer = 0;

            gameActiveState = true;
            gameBossState = false;
            gameMenuState = false;

            menuRect = new Rectangle(0, 0, 800, 480);

            random = new System.Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            platforms = new Environment(
                new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                },
                new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 0 },
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                },
                150, //startX
                -310, //startY
                100,
                new Texture2D[] {
                    this.Content.Load<Texture2D>("blank"),
                    this.Content.Load<Texture2D>("wall"),
                    this.Content.Load<Texture2D>("door")
                }
            );

            bossRoom = new Environment(
                new int[,] {
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                },
                new int[,] {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                },
                0, //startX
                0, //startY
                40,
                new Texture2D[] {
                    this.Content.Load<Texture2D>("blank"),
                    this.Content.Load<Texture2D>("wall"),
                }
            );

            charRatRight = this.Content.Load<Texture2D>("charRatRight");
            charRatLeft = this.Content.Load<Texture2D>("charRatLeft");
            wingedRat = this.Content.Load<Texture2D>("wingedRat");
            cannonBall = this.Content.Load<Texture2D>("cannonBall");

            healthBar = this.Content.Load<Texture2D>("healthBar");

            attackRight = this.Content.Load<Texture2D>("attackRight");
            attackLeft = this.Content.Load<Texture2D>("attackleft");
            
            menu = this.Content.Load<Texture2D>("menu");

            font = this.Content.Load<SpriteFont>("scoreFont");

            bossRight = this.Content.Load<Texture2D>("charRatRight");
            bossLeft = this.Content.Load<Texture2D>("charRatLeft");

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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ExitGame();
            }

            // TODO: Add your update logic here

            if (gameActiveState)
            {
                gameActiveStateUpdate();
            }
            else if (gameBossState)
            {
                gameBossStateUpdate();
            }
            else if (gameMenuState)
            {
                gameMenuStateUpdate();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (gameActiveState)
            {
                gameActiveStateDraw(spriteBatch);
            }
            else if (gameBossState)
            {
                gameBossStateDraw(spriteBatch);
            }
            else if (gameMenuState)
            {
                gameMenuStateDraw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ExitGame()
        {
            // TODO: add your exit code here to restore the device to its per-game environment.
            CCSimpleAudioEngine.SharedEngine.RestoreMediaState();
            Exit();
        }
        private void gameActiveStateUpdate()
        {
            KeyboardState keys = Keyboard.GetState();

            // falling starts when we a) jump, b) walk off an edge
            // falling stops when we land on the ground from above
            // gravity should be in effect
            if (falling)
            {
                crouching = false;

                ySpeed += gravity;
                platforms.move(0, -ySpeed);
                buffRatActualY -= ySpeed;
                wingedRatActualY -= ySpeed;



                if (charRect.Intersects(wingedRatRect) && ySpeed > 0)
                {
                    ySpeed = jumpSpeed;
                }

                bool intersected = false;
                while (platforms.collides(charRect)[0] != -1 && platforms.textureMap[platforms.collides(charRect)[0], platforms.collides(charRect)[1]] == 1)
                {
                    intersected = true;
                    if (ySpeed < 0) // on the way up, hit our head
                    {
                        platforms.move(0, -1);
                        buffRatActualY--;
                        wingedRatActualY--;
                    }
                    else if (ySpeed > 0) // on the way down, landed on ground
                    {
                        platforms.move(0, 1);
                        buffRatActualY++;
                        wingedRatActualY++;
                        falling = false;
                    }

                }
                if (intersected)
                {
                    ySpeed = 0;
                }

            }
            else
            {
                bool collided = false;
                charRect.Y++;
                if (platforms.collides(charRect)[0] != -1 && platforms.textureMap[platforms.collides(charRect)[0], platforms.collides(charRect)[1]] == 1)
                {
                    collided = true;
                }
                charRect.Y--;

                if (!collided)
                {
                    falling = true;
                }

                if (keys.IsKeyDown(Keys.W))
                {
                    if (crouching)
                    {
                        ySpeed = jumpSpeed * 1.2;
                    } else
                    {
                        ySpeed = jumpSpeed;
                    }
                    falling = true;
                }

                if (keys.IsKeyDown(Keys.LeftControl))
                {
                    crouching = true;
                }
                else
                {
                    crouching = false;
                }

            }

            if (crouching)
            {
                xSpeed = 3;
            }
            else
            {
                xSpeed = 7;
            }

            if (keys.IsKeyDown(Keys.A))
            {
                platforms.move(xSpeed, 0);
                buffRatActualX += xSpeed;
                wingedRatActualX += xSpeed;
                while (platforms.collides(charRect)[0] != -1 && platforms.textureMap[platforms.collides(charRect)[0], platforms.collides(charRect)[1]] == 1)
                {
                    platforms.move(-1, 0);
                    buffRatActualX--;
                    wingedRatActualX--;
                }
                facingRight = false;
            }

            if (keys.IsKeyDown(Keys.D))
            {
                platforms.move(-xSpeed, 0);
                buffRatActualX -= xSpeed;
                wingedRatActualX -= xSpeed;
                while (platforms.collides(charRect)[0] != -1 && platforms.textureMap[platforms.collides(charRect)[0], platforms.collides(charRect)[1]] == 1)
                {
                    platforms.move(1, 0);
                    buffRatActualX++;
                    wingedRatActualX++;
                }
                facingRight = true;
            }

            if (buffRatAlive)
            {
                if (buffRatRect.X < 1200 && buffRatRect.X > 365)
                {
                    if (platforms.collides(buffRatRect)[0] == -1)
                    {
                        buffRatRect.Y++;
                        buffRatRect.X -= buffRatRect.Width;
                        if (platforms.collides(buffRatRect)[0] != -1)
                        {
                            buffRatActualX -= buffRatSpeed;
                        }
                    }

                    buffRatFacingRight = false;

                }
                else if (buffRatRect.X > -600 && buffRatRect.X < 300)
                {
                    if (platforms.collides(buffRatRect)[0] == -1)
                    {
                        buffRatRect.Y++;
                        buffRatRect.X += buffRatRect.Width;
                        if (platforms.collides(buffRatRect)[0] != -1)
                        {
                            buffRatActualX += buffRatSpeed;
                        }
                    }

                    buffRatFacingRight = true;

                }

                buffRatRect.X = (int)buffRatActualX;
                buffRatRect.Y = (int)buffRatActualY;
                while (platforms.collides(buffRatRect)[0] != -1)
                {
                    if (buffRatFacingRight)
                    {
                        buffRatRect.X--;
                    }
                    else
                    {
                        buffRatRect.X++;
                    }
                    buffRatActualX = buffRatRect.X;
                }

                if (buffRatRect.Intersects(charRect) && buffRatAttackTimer >= 60)
                {
                    buffRatAttackTimer = 0;
                    charHealth -= buffRatDamage;
                    if (charHealth < 0)
                    {
                        charHealth = 0.0;
                    }
                }

                if (buffRatAttackTimer < 60)
                {
                    buffRatAttackTimer++;
                }
            }

            if (attackTimer < 30)
            {
                attackTimer++;

                if (attackTimer == 1)
                {
                    if (facingRight && charAttackRightRect.Intersects(buffRatRect))
                    {
                        buffRatHealth -= charDamage;
                        buffRatActualX += 15;
                    }

                    else if (!facingRight && charAttackLeftRect.Intersects(buffRatRect))
                    {
                        buffRatHealth -= charDamage;
                        buffRatActualX -= 15;
                    }
                }

                if (attackTimer >= 20)
                {
                    attackingState = false;
                }

            }
            else if (keys.IsKeyDown(Keys.Space))
            {
                attackingState = true;
                attackTimer = 0;
            }

            if (buffRatHealth <= 0)
            {
                buffRatAlive = false;
            }

            if (platforms.rectangles[0, 0].Y < -500)
            {
                charHealth -= 0.1;
            }

            charHealthRect.Width = (int)(90 / (charMaxHealth / charHealth));

            if (charHealth <= 0)
            {
                gameActiveState = false;
                gameMenuState = true;
            }

            wingedRatYSpeed += gravity;
            wingedRatFlyTimer++;

            if (wingedRatFlyTimer > 60)
            {
                wingedRatYSpeed = wingedRatJumpSpeed;
                wingedRatFlyTimer = 0;
                wingedRatActualY = platforms.rectangles[6, 0].Y;
            }

            wingedRatActualY += wingedRatYSpeed;

            wingedRatRect.X = (int)wingedRatActualX;
            wingedRatRect.Y = (int)wingedRatActualY;

            if (platforms.collides(charRect)[0] != -1 && platforms.textureMap[platforms.collides(charRect)[0], platforms.collides(charRect)[1]] == 2)
            {
                gameActiveState = false;
                gameBossState = true;
            }

        }

        private void gameActiveStateDraw(SpriteBatch spriteBatch)
        {

            if (buffRatAlive)
            {
                if (buffRatFacingRight)
                {
                    spriteBatch.Draw(charRatRight, buffRatRect, Color.DarkGray);
                }
                else
                {
                    spriteBatch.Draw(charRatLeft, buffRatRect, Color.DarkGray);
                }
            }

            spriteBatch.Draw(wingedRat, wingedRatRect, Color.DarkGray);


            if (facingRight)
            {
                if (attackingState)
                {
                    spriteBatch.Draw(attackRight, charAttackRightRect, Color.White);
                }
                if (crouching && !falling)
                {
                    spriteBatch.Draw(charRatRight, new Rectangle (charRect.X, charRect.Y + charRect.Height / 2, charRect.Width, charRect.Height / 2), Color.White);
                }
                else
                {
                    spriteBatch.Draw(charRatRight, charRect, Color.White);
                }
            }
            else
            {
                if (attackingState)
                {
                    spriteBatch.Draw(attackLeft, charAttackLeftRect, Color.White);
                }
                if (crouching && !falling)
                {
                    spriteBatch.Draw(charRatLeft, new Rectangle (charRect.X, charRect.Y + charRect.Height / 2, charRect.Width, charRect.Height / 2), Color.White);
                }
                else
                {
                    spriteBatch.Draw(charRatLeft, charRect, Color.White);
                }
            }

            platforms.draw(spriteBatch);
            spriteBatch.Draw(healthBar, charHealthRect, Color.White);
            spriteBatch.DrawString(font, "Space to Attack!", new Vector2(platforms.rectangles[4, 1].X, platforms.rectangles[4, 1].Y), Color.Black);
        }

        private void gameBossStateUpdate()
        {
            KeyboardState keys = Keyboard.GetState();

            if (falling)
            {
                crouching = false;

                ySpeed += (gravity * 0.4);
                bossRoomActualY += ySpeed;
                bossRoomCharRect.Y = (int)bossRoomActualY;

                bool intersected = false;
                while (bossRoom.collides(bossRoomCharRect)[0] != -1)
                {
                    intersected = true;
                    if (ySpeed < 0) // on the way up, hit our head
                    {
                        bossRoomActualY++;
                        bossRoomCharRect.Y = (int)bossRoomActualY;
                    }
                    else if (ySpeed > 0) // on the way down, landed on ground
                    {
                        bossRoomActualY--;
                        bossRoomCharRect.Y = (int)bossRoomActualY;
                        falling = false;
                    }

                }
                if (intersected)
                {
                    ySpeed = 0;
                }

            }
            else
            {
                bool collided = false;
                bossRoomCharRect.Y++;
                if (bossRoom.collides(bossRoomCharRect)[0] != -1)
                {
                    collided = true;
                }
                bossRoomCharRect.Y--;

                if (!collided)
                {
                    falling = true;
                }

                if (keys.IsKeyDown(Keys.W))
                {
                    if (crouching)
                    {
                        ySpeed = ((jumpSpeed * 1.3) * 0.4);
                    }
                    else
                    {
                        ySpeed = (jumpSpeed * 0.4);
                    }
                    falling = true;
                }

                if (keys.IsKeyDown(Keys.LeftControl))
                {
                    crouching = true;
                }
                else
                {
                    crouching = false;
                }

            }

            if (crouching)
            {
                xSpeed = 3;
            }
            else
            {
                xSpeed = 7;
            }

            if (keys.IsKeyDown(Keys.A))
            {
                facingRight = false;
                bossRoomActualX -= (xSpeed * 0.4);
                bossRoomCharRect.X = (int)bossRoomActualX;
                while (bossRoom.collides(bossRoomCharRect)[0] != -1)
                {
                    bossRoomActualX++;
                    bossRoomCharRect.X = (int)bossRoomActualX;
                }
            }

            if (keys.IsKeyDown(Keys.D))
            {
                facingRight = true;
                bossRoomActualX += (xSpeed * 0.4);
                bossRoomCharRect.X = (int)bossRoomActualX;
                while (bossRoom.collides(bossRoomCharRect)[0] != -1)
                {
                    bossRoomActualX--;
                    bossRoomCharRect.X = (int)bossRoomActualX;
                }
            }

            if (attackTimer < 30)
            {
                attackTimer++;

                if (attackTimer == 1)
                {
                    if (facingRight && new Rectangle(bossRoomCharRect.X + bossRoomCharRect.Width, bossRoomCharRect.Y, 20, 30).Intersects(bossRect))
                    {
                        bossHealth -= charDamage;
                    }
                    else if (!facingRight && new Rectangle(bossRoomCharRect.X - 20, bossRoomCharRect.Y, 20, 30).Intersects(bossRect))
                    {
                        bossHealth -= charDamage;
                    }
                }

                if (attackTimer >= 20)
                {
                    attackingState = false;
                }

            }
            else if (keys.IsKeyDown(Keys.Space))
            {
                attackingState = true;
                attackTimer = 0;
            }

            for (int i = 0; i < cannonBallRects.Length; i++)
            {
                cannonBallRects[i].X-= 3;

                if (cannonBallRects[i].X < 0)
                {
                    cannonBallRects[i].X = 1600;
                    cannonBallRects[i].Y = random.Next(360, 420);
                }

                if (crouching)
                {
                    if (cannonBallRects[i].Intersects(new Rectangle(bossRoomCharRect.X, bossRoomCharRect.Y + bossRoomCharRect.Height / 2, bossRoomCharRect.Width, bossRoomCharRect.Height / 2)))
                    {
                        gameMenuState = true;
                        gameBossState = false;
                    }
                } 
                else
                {
                    if (cannonBallRects[i].Intersects(bossRoomCharRect))
                    {
                        gameMenuState = true;
                        gameBossState = false;
                    }
                }
            }



            // Boss Stuff \\

            if (bossFalling)
            {
                bossYSpeed += (gravity * 0.4);
                if (bossYSpeed > 8)
                {
                    bossYSpeed = 8;
                }
                bossActualY += bossYSpeed;
                bossRect.Y = (int)bossActualY;

                bool intersected = false;
                while (bossRoom.collides(bossRect)[0] != -1 && bossYSpeed > 0)// on the way down, landed on ground
                {
                    bossActualY--;
                    bossRect.Y = (int)bossActualY;
                    bossFalling = false;
                    intersected = true;

                }
                if (intersected)
                {
                    bossYSpeed = 0;
                }

            }

            bossAttackTimer++;

            if (bossAttackTimer > 0)
            {
                if (currentBossAttack == 0)
                {
                    currentBossAttack = random.Next(1, 2);
                } 
                else
                {
                    currentBossAttack = 0;
                }

                if (currentBossAttack == 0)
                {
                    bossAttackTimer = -180;
                }
                else if (currentBossAttack == 1)
                {
                    bossAttackTimer = -300;
                }
                else if (currentBossAttack == 2)
                {
                    bossAttackTimer = -600;
                }
            }

            // Boss attacks \\
            if (currentBossAttack == 1)
            {
                if (bossAttackTimer < -240)
                {
                    bossCrouching = true;
                }
                else if (bossAttackTimer > -240 && bossAttackTimer < -100)
                {
                    bossCrouching = false;
                    bossActualY -= 10;
                }
                else if (bossAttackTimer > -80 && bossAttackTimer < -75)
                {
                    bossRect.X = bossRoomCharRect.X - 40;

                    if (bossRect.X < 50)
                    {
                        bossRect.X = 50;
                    }
                    else if (bossRect.X > 750 - bossRect.Width)
                    {
                        bossRect.X = 750 - bossRect.Width;
                    }

                    bossActualY = -150;
                    bossYSpeed = 0.0;

                    bossFalling = true;
                }
            }

            bossRect.Y = (int)bossActualY;

            if ((bossFalling && bossRect.Intersects(bossRoomCharRect)) || bossHealth <= 0)
            {
                gameBossState = false;
                gameMenuState = true;
            }

            bossHealthRect.Width = (int)(740 / (500 / bossHealth));


            // End boss \\



        }

        private void gameBossStateDraw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(cannonBall, cannonBallRects[0], Color.White);
            spriteBatch.Draw(cannonBall, cannonBallRects[1], Color.White);

            bossRoom.draw(spriteBatch);

            if (facingRight)
            {
                if (attackingState)
                {
                    spriteBatch.Draw(attackRight, new Rectangle(bossRoomCharRect.X + bossRoomCharRect.Width, bossRoomCharRect.Y, 20, 30), Color.White);
                }
                if (crouching && !falling)
                {
                    spriteBatch.Draw(charRatRight, new Rectangle(bossRoomCharRect.X, bossRoomCharRect.Y + bossRoomCharRect.Height / 2, bossRoomCharRect.Width, bossRoomCharRect.Height / 2), Color.White);
                }
                else
                {
                    spriteBatch.Draw(charRatRight, bossRoomCharRect, Color.White);
                }
            }
            else
            {
                if (attackingState)
                {
                    spriteBatch.Draw(attackLeft, new Rectangle(bossRoomCharRect.X - 20, bossRoomCharRect.Y, 20, 30), Color.White);
                }
                if (crouching && !falling)
                {
                    spriteBatch.Draw(charRatLeft, new Rectangle(bossRoomCharRect.X, bossRoomCharRect.Y + bossRoomCharRect.Height / 2, bossRoomCharRect.Width, bossRoomCharRect.Height / 2), Color.White);
                }
                else
                {
                    spriteBatch.Draw(charRatLeft, bossRoomCharRect, Color.White);
                }
            }

            spriteBatch.Draw(healthBar, bossHealthRect, Color.White);
            
            if (bossFacingRight)
            {
                if (!bossCrouching)
                {
                    spriteBatch.Draw(bossRight, bossRect, Color.DarkGray);
                }
                else
                {
                    spriteBatch.Draw(bossRight, new Rectangle(bossRect.X, bossRect.Y + bossRect.Height / 2, bossRect.Width, bossRect.Height / 2), Color.DarkGray);
                }
            } 
            else
            {
                if (!bossCrouching)
                {
                    spriteBatch.Draw(bossLeft, bossRect, Color.DarkGray);
                }
                else
                {
                    spriteBatch.Draw(bossLeft, new Rectangle(bossRect.X, bossRect.Y + bossRect.Height / 2, bossRect.Width, bossRect.Height / 2), Color.DarkGray);
                }
            }

        }

        private void gameMenuStateUpdate()
        {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.R))
            {
                gameMenuState = false;
                gameActiveState = true;
                reInitialize();
            }
        }

        private void gameMenuStateDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menu, menuRect, Color.White);
        }

        private void reInitialize()
        {
            charRect = new Rectangle(355, 200, 90, 90);
            bossRoomCharRect = new Rectangle(42, 402, 36, 36);

            cannonBallRects = new Rectangle[]
            {
                new Rectangle(800, 360, 20, 20),
                new Rectangle(1600, 400, 20, 20)
            };

            charHealthRect = new Rectangle(355, 175, 90, 10);

            charAttackRightRect = new Rectangle(445, 215, 50, 70);
            charAttackLeftRect = new Rectangle(305, 215, 50, 70);

            falling = false;
            crouching = false;
            attackingState = false;
            facingRight = true;

            gravity = .5;
            ySpeed = 0;
            xSpeed = 7;
            jumpSpeed = -12;
            charMaxHealth = 10.0;
            charHealth = charMaxHealth;
            charDamage = 5.0;

            bossRoomActualX = bossRoomCharRect.X;
            bossRoomActualY = bossRoomCharRect.Y;

            bossRect = new Rectangle(340, 30, 120, 100);
            bossHealthRect = new Rectangle(30, 10, 740, 20);

            bossActualY = bossRect.Y;

            bossFacingRight = true;
            bossHealth = 500.0;
            bossFalling = true;
            bossAttackTimer = -300;
            bossYSpeed = 0.0;
            currentBossAttack = 1;

            buffRatRect = new Rectangle(555, 140, 150, 150);

            buffRatHealth = 20.0;
            buffRatSpeed = 1.5;
            buffRatDamage = 5.0;
            buffRatAttackTimer = 0;
            buffRatFacingRight = false;
            buffRatAlive = true;

            buffRatActualX = buffRatRect.X;
            buffRatActualY = buffRatRect.Y;

            wingedRatRect = new Rectangle(2550, 290, 100, 100);

            wingedRatFlyTimer = 0;
            wingedRatJumpSpeed = -15;
            wingedRatYSpeed = 0.0;

            wingedRatActualY = wingedRatRect.Y;
            wingedRatActualX = wingedRatRect.X;

            attackTimer = 0;

            platforms.tp(platforms.startX, platforms.startY);
        }
    }

}
