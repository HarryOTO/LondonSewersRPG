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

        static Texture2D charRatRight;
        static Texture2D charRatLeft;
        static Rectangle charRect;
        static Rectangle charCrouchRect;

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

        static Texture2D buffRat;
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
        static bool gameOverState;
        static bool gameMenuState;

        static Rectangle menuRect;
        static Texture2D menu;

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
            charCrouchRect = new Rectangle(355, 240, 90, 50);

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
            gameOverState = false;
            gameMenuState = false;

            menuRect = new Rectangle(0, 0, 800, 480);

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
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                },
                new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                },
                150, //startX
                -310, //startY
                100,
                new Texture2D[] {
                    this.Content.Load<Texture2D>("blank"),
                    this.Content.Load<Texture2D>("wall"),
                }
            );

            charRatRight = this.Content.Load<Texture2D>("charRatRight");
            charRatLeft = this.Content.Load<Texture2D>("charRatLeft");
            wingedRat = this.Content.Load<Texture2D>("wingedRat");

            healthBar = this.Content.Load<Texture2D>("healthBar");

            attackRight = this.Content.Load<Texture2D>("attackRight");
            attackLeft = this.Content.Load<Texture2D>("attackleft");
            
            menu = this.Content.Load<Texture2D>("menu");

            font = this.Content.Load<SpriteFont>("scoreFont");

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
            else if (gameOverState)
            {
                gameOverStateUpdate();
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
            else if (gameOverState)
            {
                gameOverStateDraw(spriteBatch);
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

                ySpeed += gravity;
                platforms.move(0, -ySpeed);
                buffRatActualY -= ySpeed;
                wingedRatActualY -= ySpeed;



                if (charRect.Intersects(wingedRatRect) && ySpeed > 0)
                {
                    ySpeed = jumpSpeed;
                }

                bool intersected = false;
                while (platforms.collides(charRect)[0] != -1)
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
                if (platforms.collides(charRect)[0] != -1)
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

            }

            if (keys.IsKeyDown(Keys.LeftControl))
            {
                crouching = true;
            } else
            {
                crouching = false;
            }

            if (keys.IsKeyDown(Keys.A))
            {
                platforms.move(xSpeed, 0);
                buffRatActualX += xSpeed;
                wingedRatActualX += xSpeed;
                if (crouching)
                {
                    while (platforms.collides(charCrouchRect)[0] != -1)
                    {
                        platforms.move(-1, 0);
                        buffRatActualX--;
                        wingedRatActualX--;
                    }
                } else
                {
                    while (platforms.collides(charRect)[0] != -1)
                    {
                        platforms.move(-1, 0);
                        buffRatActualX--;
                        wingedRatActualX--;
                    }
                }
                facingRight = false;
            }

            if (keys.IsKeyDown(Keys.D))
            {
                platforms.move(-xSpeed, 0);
                buffRatActualX -= xSpeed;
                wingedRatActualX -= xSpeed;
                if (crouching)
                {
                    while (platforms.collides(charCrouchRect)[0] != -1)
                    {
                        platforms.move(1, 0);
                        buffRatActualX++;
                        wingedRatActualX++;
                    }
                }
                else
                {
                    while (platforms.collides(charRect)[0] != -1)
                    {
                        platforms.move(1, 0);
                        buffRatActualX++;
                        wingedRatActualX++;
                    }
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

            if (attackTimer < 45)
            {
                attackTimer++;

                if (attackTimer == 1)
                {
                    if (facingRight && charAttackRightRect.Intersects(buffRatRect))
                    {
                        buffRatHealth -= charDamage;
                    }
                    else if (!facingRight && charAttackLeftRect.Intersects(buffRatRect))
                    {
                        buffRatHealth -= charDamage;
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
                    spriteBatch.Draw(charRatRight, charCrouchRect, Color.White);
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
                    spriteBatch.Draw(charRatLeft, charCrouchRect, Color.White);
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

        private void gameOverStateUpdate()
        {

        }

        private void gameOverStateDraw(SpriteBatch spriteBatch)
        {

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
            charCrouchRect = new Rectangle(355, 240, 90, 50);

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
            platforms.collisionMap =
                new int[,] {
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                    {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                };
            platforms.textureMap = platforms.collisionMap;
        }
    }

}
