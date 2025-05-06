using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;
using System;
using System.Collections.Generic;

namespace Casino_Game_Project___Sonic_Pachinko
{
    //Wilson

    //WaitScreen will allow bets to be accepted
    enum Screen
    {
        WaitScreen,
        PlayScreen
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Screen currentScreen;

        KeyboardState keyboardState;
        MouseState mouseState;

        Random random = new Random();
        int dropSide;
        int firstBumper;
        bool dropStarted;

        //Ball related
        Texture2D ballTexture;
        Rectangle ballRect;
        Vector2 ballSpeed;
        Point ballPostionHolder;
        bool ballLaunched;
        int remainingBalls;
        //

        bool regBumperHit;
        bool leftBumperHit;
        bool rightBumperHit;

        bool fallComplete;

        bool rollStarted;

        bool fromTop;
        bool fromLeft;
        bool fromRight;
        bool hitTube;

        bool reload;

        float launchTime;
        bool launchable;

        //Wall
        Rectangle wallRect;
        Texture2D wallTexture;
        //

        //Background
        Texture2D backgroundTexture;
        Rectangle backgroundRect;
        //

        //Curve
        Texture2D curveTexture;
        Texture2D bottomCurveTexture; 
        Rectangle roofCurveRightRect;
        Rectangle roofCurveLeftRect;
        
        //Left roof curve movement rectangles
        Rectangle roofCurveLeftMoveRectOne;
        Rectangle roofCurveLeftMoveRectTwo;
        Rectangle roofCurveLeftMoveRectThree;
        //

        Rectangle floorCurveLeftRect;
        
        //Left floor curve movement rectangles
        Rectangle floorCurveLeftMoveRectOne;
        Rectangle floorCurveLeftMoveRectTwo;
        Rectangle floorCurveLeftMoveRectThree;
        //

        Rectangle floorCurveRightRect;

        //Right floor curve movement rectangles
        Rectangle floorCurveRightMoveRectOne;
        Rectangle floorCurveRightMoveRectTwo;
        Rectangle floorCurveRightMoveRectThree;
        //

        //Tube
        Texture2D tubeTexture;
        Rectangle tubeRect;
        //

        //Catchers
        Texture2D catcherTexture;
        Rectangle centreCatcherRect;
        Rectangle leftCatcherRect;
        Rectangle rightCatcherRect;
        //

        //Spring
        Texture2D springTexture;
        Rectangle springRect;
        int springCharge;
        float chargeTimer;
        //

        //Slopes
        Texture2D slopeTexture;
        Rectangle slopeRightRect;
        Rectangle slopeRightMoveRectOne; 
        Rectangle slopeRightMoveRectTwo;

        Rectangle slopeLeftRect;
        Rectangle slopeLeftMoveRectOne;
        Rectangle slopeLeftMoveRectTwo;
        //

        //Floor
        Texture2D floorTexture;
        Rectangle floorLeftRect;
        Rectangle floorRightRect;
        //

        //Bumper
        Texture2D bumperTexture;
        List<Rectangle> bumpers;
        List<Rectangle> leftWallBumpers;
        List<Rectangle> rightWallBumpers;
        //

        Texture2D boxTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 985;
            _graphics.ApplyChanges();

            currentScreen = Screen.WaitScreen;
            ballLaunched = false;
            dropStarted = false;
            regBumperHit = false;
            leftBumperHit = false;
            rightBumperHit = false;
            launchable = false;
            fallComplete = false;
            reload = false;
            fromLeft = false;
            fromRight = false;
            hitTube = false;
            fromTop = true;
            rollStarted = false;

            launchTime = 0;

            wallRect = new Rectangle(45, 185, 45, 800);
            
            springRect = new Rectangle(0, 936, 45, 49);

            backgroundRect = new Rectangle(0, 0, 800, 985);

            roofCurveLeftRect = new Rectangle(-1, 0, 130, 160);

            //Left roof curve movement rectangles
            roofCurveLeftMoveRectOne = new Rectangle(0, 107, 5, 5);
            roofCurveLeftMoveRectTwo = new Rectangle(0, 29, 50, 5);
            roofCurveLeftMoveRectThree = new Rectangle(0, 0, 106, 5);
            //

            roofCurveRightRect = new Rectangle(671, 0, 130, 160);

            floorCurveLeftRect = new Rectangle(90, 762, 130, 160);
            
            //Left floor curve movement rectangles
            floorCurveLeftMoveRectOne = new Rectangle(105, 850, 10, 50);
            floorCurveLeftMoveRectTwo = new Rectangle(145, 895, 10, 50);
            floorCurveLeftMoveRectThree = new Rectangle(205, 920, 10, 50);
            //

            floorCurveRightRect = new Rectangle(670, 762, 130, 160);

            //Right floor curve movement rectangles
            floorCurveRightMoveRectOne = new Rectangle(710, 910, 10, 50);
            floorCurveRightMoveRectTwo = new Rectangle(670, 920, 10, 50);
            //

            floorLeftRect = new Rectangle(85, 922, 130, 64);
            floorRightRect = new Rectangle(670, 923, 130, 64);

            tubeRect = new Rectangle(400, 982, 84, 63);
            
            //Slopes
            slopeLeftRect = new Rectangle(215, 920, 185, 65);
            
            //Left slope movement
            slopeLeftMoveRectOne = new Rectangle(210, 921, 30, 50);
            slopeLeftMoveRectTwo = new Rectangle(310, 950, 30, 50);
            //

            slopeRightRect = new Rectangle(485, 920, 185, 65);
            
            //Right slope movement
            slopeRightMoveRectOne = new Rectangle(630, 931, 50, 50);
            slopeRightMoveRectTwo = new Rectangle(520, 965, 50, 50);
            //

            //Ball
            ballRect = new Rectangle(0, 906, 30, 30);
            //

            //Catchers
            centreCatcherRect = new Rectangle(400, 250, 50, 50);
            leftCatcherRect = new Rectangle(200, 550, 50, 50);
            rightCatcherRect = new Rectangle(600, 550, 50, 50);
            //

            ballSpeed = new Vector2(0, -10);

            bumpers = new List<Rectangle>();

            
            //Row one
            bumpers.Add(new Rectangle(150, 100, 50, 50));
            bumpers.Add(new Rectangle(250, 100, 50, 50));
            bumpers.Add(new Rectangle(350, 100, 50, 50));
            bumpers.Add(new Rectangle(450, 100, 50, 50));
            bumpers.Add(new Rectangle(550, 100, 50, 50));
            bumpers.Add(new Rectangle(650, 100, 50, 50));
            //

            //Row two
            bumpers.Add(new Rectangle(200, 250, 50, 50));
            bumpers.Add(new Rectangle(300, 250, 50, 50));
            bumpers.Add(new Rectangle(500, 250, 50, 50));
            bumpers.Add(new Rectangle(600, 250, 50, 50));
            //

            //Row three
            bumpers.Add(new Rectangle(150, 400, 50, 50));
            bumpers.Add(new Rectangle(250, 400, 50, 50));
            bumpers.Add(new Rectangle(350, 400, 50, 50));
            bumpers.Add(new Rectangle(450, 400, 50, 50));
            bumpers.Add(new Rectangle(550, 400, 50, 50));
            bumpers.Add(new Rectangle(650, 400, 50, 50));
            //

            //Row four
            bumpers.Add(new Rectangle(300, 550, 50, 50));
            bumpers.Add(new Rectangle(400, 550, 50, 50));
            bumpers.Add(new Rectangle(500, 550, 50, 50));
            //

            //Row five
            bumpers.Add(new Rectangle(150, 700, 50, 50));
            bumpers.Add(new Rectangle(250, 700, 50, 50));
            bumpers.Add(new Rectangle(350, 700, 50, 50));
            bumpers.Add(new Rectangle(450, 700, 50, 50));
            bumpers.Add(new Rectangle(550, 700, 50, 50));
            bumpers.Add(new Rectangle(650, 700, 50, 50));
            //
            
            //Left wall bumpers
            leftWallBumpers = new List<Rectangle>();
            leftWallBumpers.Add(new Rectangle(100, 250, 50, 50));
            leftWallBumpers.Add(new Rectangle(100, 550, 50, 50));
            //

            //Right wall bumpers
            rightWallBumpers = new List<Rectangle>();
            rightWallBumpers.Add(new Rectangle(700, 250, 50, 50));
            rightWallBumpers.Add(new Rectangle(700, 550, 50, 50));
            //

            //Bumper variables
            regBumperHit = false;
            leftBumperHit = false;
            rightBumperHit = false;
            //

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("sonicBall");

            boxTexture = Content.Load<Texture2D>("rectangle");

            backgroundTexture = Content.Load<Texture2D>("Casino Night Zone BG3");
            wallTexture = Content.Load<Texture2D>("Casino Night Wall");
            curveTexture = Content.Load<Texture2D>("Casino Night Curve");
            bottomCurveTexture = Content.Load<Texture2D>("Casino Night Curve Bottom");
            bumperTexture = Content.Load<Texture2D>("Casino Night Bumper");
            tubeTexture = Content.Load<Texture2D>("Casino Night Tube");
            slopeTexture = Content.Load<Texture2D>("Casino Night Slope");
            floorTexture = Content.Load<Texture2D>("Casino Night Floor");
            catcherTexture = Content.Load<Texture2D>("Casino Night Catcher");
            springTexture = Content.Load<Texture2D>("Casino Night Spring");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Window.Title = "Sonic Pachinko";

            if (currentScreen == Screen.WaitScreen)
            {
                Window.Title = "Sonic Pachinko - Bet to Play";

                //Input bets
                if (keyboardState.IsKeyDown(Keys.D1) || keyboardState.IsKeyDown(Keys.NumPad1)) //Bet 1
                {
                    remainingBalls = 1;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D2) || keyboardState.IsKeyDown(Keys.NumPad2)) //Bet 2
                {
                    remainingBalls = 2;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D3) || keyboardState.IsKeyDown(Keys.NumPad3)) //Bet 3
                {
                    remainingBalls = 3;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D4) || keyboardState.IsKeyDown(Keys.NumPad4)) //Bet 4
                {
                    remainingBalls = 4;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D5) || keyboardState.IsKeyDown(Keys.NumPad5)) //Bet 5
                {
                    remainingBalls = 5;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D6) || keyboardState.IsKeyDown(Keys.NumPad6)) //Bet 6
                {
                    remainingBalls = 6;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D7) || keyboardState.IsKeyDown(Keys.NumPad7)) //Bet 7
                {
                    remainingBalls = 7;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D8) || keyboardState.IsKeyDown(Keys.NumPad8)) //Bet 8
                {
                    remainingBalls = 8;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D9) || keyboardState.IsKeyDown(Keys.NumPad9)) //Bet 9
                {
                    remainingBalls = 9;
                    currentScreen = Screen.PlayScreen;
                }
                else if (keyboardState.IsKeyDown(Keys.D0) || keyboardState.IsKeyDown(Keys.NumPad0)) //Bet 10
                {
                    remainingBalls = 10;
                    currentScreen = Screen.PlayScreen;
                }
            }
            else 
            {
                Window.Title = "Sonic Pachinko";

                if (remainingBalls > 0)
                {
                    if (!ballLaunched)
                    {
                        if(reload)
                        {
                            ballSpeed.X = 0.5f;
                            
                            ballRect.Offset(ballSpeed);
                            
                            if (ballRect.X < 0)
                            {
                                ballRect.X += 1;
                            }
                            else if (ballRect.X >= 0)
                            {
                                ballSpeed.X = 0;
                                ballRect.X = 0;
                                remainingBalls--;
                                reload = false;
                            }
                        }
                        else
                        {
                            if (keyboardState.IsKeyDown(Keys.Space) && !launchable) //Initiate launch
                            {
                                chargeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                                if (chargeTimer >= 0.5)
                                {
                                    springCharge++;

                                    if (springCharge == 1)
                                    {
                                        springRect.Y += 9;
                                        ballRect.Y += 9;
                                    }
                                    else if (springCharge == 2)
                                    {
                                        springRect.Y += 9;
                                        ballRect.Y += 9;
                                    }
                                    else if (springCharge == 3)
                                    {
                                        springRect.Y += 9;
                                        ballRect.Y += 9;
                                    }
                                    else if (springCharge == 4)
                                    {
                                        springRect.Y += 9;
                                        ballRect.Y += 9;
                                        launchable = true;
                                    }

                                    chargeTimer = 0;
                                }
                            }
                            else if (!launchable && keyboardState.IsKeyUp(Keys.Space))
                            {
                                springCharge = 0;
                                chargeTimer = 0;
                                springRect.Y = 936;
                                ballRect.Y = 906;
                            }
                            else if (launchable && keyboardState.IsKeyUp(Keys.Space))
                            {
                                firstBumper = new Random().Next(1, 7);
                                springRect.Y = 936;
                                ballSpeed = new Vector2(0, -10);

                                ballLaunched = true;
                            }
                        }
                    }
                    else
                    {
                        ballRect.Offset(ballSpeed);

                        if (ballRect.Intersects(roofCurveLeftMoveRectOne))
                        {
                            ballRect.Y = roofCurveLeftMoveRectOne.Bottom;
                            ballSpeed = new Vector2(4.5f, -7.3f);
                        }
                        else if (ballRect.Intersects(roofCurveLeftMoveRectTwo))
                        {
                            ballSpeed = new Vector2(5.6f, -2.9f);
                        }
                        else if (ballRect.Intersects(roofCurveLeftMoveRectThree))
                        {
                            ballSpeed = new Vector2(10, 0);
                        }

                        if (!dropStarted)
                        {
                            if (firstBumper == 1)
                            {
                                if (ballRect.X >= 160)
                                {
                                    ballRect.X = 160;
                                    ballSpeed = new Vector2(0, 5);
                                    dropStarted = true;
                                }
                            }
                            else if (firstBumper == 2)
                            {
                                if (ballRect.X >= 260)
                                {
                                    ballRect.X = 260;
                                    ballSpeed = new Vector2(0, 5);
                                    dropStarted = true;
                                }
                            }
                            else if (firstBumper == 3)
                            {
                                if (ballRect.X >= 360)
                                {
                                    ballRect.X = 360;
                                    ballSpeed = new Vector2(0, 5);
                                    dropStarted = true;
                                }
                            }
                            else if (firstBumper == 4)
                            {
                                if (ballRect.X >= 460)
                                {
                                    ballRect.X = 460;
                                    ballSpeed = new Vector2(0, 5);
                                    dropStarted = true;
                                }
                            }
                            else if (firstBumper == 5)
                            {
                                if (ballRect.X >= 560)
                                {
                                    ballRect.X = 560;
                                    ballSpeed = new Vector2(0, 5);
                                    dropStarted = true;
                                }
                            }
                            else if (firstBumper == 6)
                            {
                                if (ballRect.X >= 660)
                                {
                                    ballRect.X = 660;
                                    ballSpeed = new Vector2(0, 5);
                                    dropStarted = true;
                                }
                            }
                        }
                        else
                        {
                            if (!fallComplete)
                            {
                                ballSpeed.Y += 0.5f;
                            }

                            foreach (Rectangle bumper in bumpers)
                            {
                                if (ballRect.Intersects(bumper))
                                {
                                    ballRect.Y = bumper.Y - ballRect.Height - 5;
                                    ballSpeed.Y = -1;

                                    if (!regBumperHit)
                                    {
                                        ballPostionHolder = new Point(ballRect.X, ballRect.Y);
                                        dropSide = new Random().Next(1, 3);
                                        regBumperHit = true;
                                    }
                                }

                                if (regBumperHit)
                                {
                                    if (dropSide == 1) //Drop to the left
                                    {
                                        if (ballRect.X > ballPostionHolder.X - 50)
                                        {
                                            ballSpeed.X -= 0.005f;
                                        }

                                        if (ballRect.Left < ballPostionHolder.X - 50)
                                        {
                                            ballSpeed.X = 0;
                                            ballRect.X = ballPostionHolder.X - 50;
                                            regBumperHit = false;
                                        }
                                    }
                                    else if (dropSide == 2) //Drop to the right
                                    {
                                        if (ballRect.X < ballPostionHolder.X + 50)
                                        {
                                            ballSpeed.X += 0.005f;
                                        }

                                        if (ballRect.X > ballPostionHolder.X + 50)
                                        {
                                            ballSpeed.X = 0;
                                            ballRect.X = ballPostionHolder.X + 50;
                                            regBumperHit = false;
                                        }
                                    }
                                }

                                foreach (Rectangle rightBumper in rightWallBumpers)
                                {
                                    if (ballRect.Intersects(rightBumper))
                                    {
                                        ballRect.Y = rightBumper.Top - ballRect.Height - 5;
                                        ballSpeed.Y = -1;

                                        if (!rightBumperHit)
                                        {
                                            ballPostionHolder = new Point(ballRect.X, ballRect.Y);
                                            rightBumperHit = true;
                                        }
                                    }

                                    if (rightBumperHit)
                                    {
                                        if (ballRect.X > ballPostionHolder.X - 50)
                                        {
                                            ballSpeed.X -= 0.005f;
                                        }

                                        if (ballRect.Left < ballPostionHolder.X - 50)
                                        {
                                            ballSpeed.X = 0;
                                            ballRect.X = ballPostionHolder.X - 50;
                                            rightBumperHit = false;
                                        }
                                    }
                                }

                                foreach (Rectangle leftBumper in leftWallBumpers)
                                {
                                    if (ballRect.Intersects(leftBumper))
                                    {
                                        ballRect.Y = leftBumper.Top - ballRect.Height - 5;
                                        ballSpeed.Y = -1;

                                        if (!leftBumperHit)
                                        {
                                            ballPostionHolder = new Point(ballRect.X, ballRect.Y);
                                            leftBumperHit = true;
                                        }
                                    }

                                    if (leftBumperHit)
                                    {
                                        if (ballRect.X < ballPostionHolder.X + 50)
                                        {
                                            ballSpeed.X += 0.005f;
                                        }

                                        if (ballRect.X > ballPostionHolder.X + 50)
                                        {
                                            ballSpeed.X = 0;
                                            ballRect.X = ballPostionHolder.X + 50;
                                            leftBumperHit = false;
                                        }
                                    }
                                }
                            }

                            //Curve movement
                            if (ballRect.Intersects(floorCurveLeftMoveRectOne))
                            {
                                ballSpeed = new Vector2(4, 4.5f);
                            }
                            else if (ballRect.Intersects(floorCurveLeftMoveRectTwo))
                            {
                                if (!rollStarted)
                                {
                                    ballRect.Y = floorCurveLeftMoveRectTwo.Top - 30;
                                }

                                rollStarted = true;
                                ballSpeed = new Vector2(6, 2.5f);
                            }

                            if (ballRect.Intersects(floorCurveRightMoveRectOne))
                            {
                                if (!rollStarted)
                                {
                                    ballRect.Y = floorCurveRightMoveRectOne.Top - 30;
                                }

                                rollStarted = true;
                                ballSpeed = new Vector2(-4, 1);
                            }
                            else if (ballRect.Intersects(floorCurveLeftMoveRectTwo))
                            {
                                if (!rollStarted)
                                {
                                    ballRect.Y = floorCurveRightMoveRectTwo.Top - 30;
                                }

                                rollStarted = true;
                                ballSpeed = new Vector2(3.7f, 1.3f);
                            }
                            //

                            //Slope movement
                            if (ballRect.Intersects(slopeLeftMoveRectOne))
                            {
                                fromLeft = true;
                                fromTop = false;
                                
                                if (!fallComplete)
                                {
                                    ballRect.Y = slopeLeftMoveRectOne.Y - 30;
                                }

                                fallComplete = true;
                                ballSpeed = new Vector2(3.7f, 1.3f);
                            }

                            if (ballRect.Intersects(slopeLeftMoveRectTwo))
                            {
                                fromLeft = true;
                                fromTop = false;

                                if (!fallComplete)
                                {
                                    ballRect.Y = slopeLeftMoveRectTwo.Y - 30;
                                }

                                fallComplete = true;
                                ballSpeed = new Vector2(3.7f, 1.3f);
                            }

                            if(ballRect.Intersects(slopeRightMoveRectOne))
                            {
                                if (!fallComplete)
                                {
                                    ballRect.Y = slopeRightMoveRectOne.Y - 30;
                                }

                                fromRight = true;
                                fromTop = false;

                                fallComplete = true;
                                ballSpeed = new Vector2(-3.7f, 1.3f);
                            }

                            if(ballRect.Intersects(slopeRightMoveRectTwo))
                            {
                                fromRight = true;
                                fromTop = false;

                                if (!fallComplete)
                                {
                                    ballRect.Y = slopeRightMoveRectTwo.Y - 30;
                                }

                                fallComplete = true;
                                ballSpeed = new Vector2(-3.7f, 1.3f);
                            }
                            //

                            if (ballRect.Intersects(centreCatcherRect)) //Ball lands in the centre catcher, 2 chips
                            {
                                if (ballRect.Y > centreCatcherRect.Y)
                                {
                                    ballSpeed = new Vector2(0, 0);
                                    ballRect.Location = new Point(-30, 906);
                                    launchable = false;
                                    dropStarted = false;
                                    ballLaunched = false;
                                    fallComplete = false;
                                    hitTube = false;
                                    fromLeft = false;
                                    fromRight = false;
                                    fromTop = true;
                                    rollStarted = false;
                                    reload = true;
                                    remainingBalls--;
                                }

                            }
                            
                            if (ballRect.Intersects(leftCatcherRect)) //Ball lands in the left catcher, 1 chip
                            {
                                if (ballRect.Y > leftCatcherRect.Y)
                                {
                                    ballSpeed = new Vector2(0, 0);
                                    ballRect.Location = new Point(-30, 906);
                                    launchable = false;
                                    dropStarted = false;
                                    ballLaunched = false;
                                    fallComplete = false;
                                    hitTube = false;
                                    fromLeft = false;
                                    fromRight = false;
                                    fromTop = true;
                                    rollStarted = false;
                                    reload = true;
                                }
                            }
                            
                            if (ballRect.Intersects(rightCatcherRect)) //Ball lands in the right catcher, 1 chip
                            {
                                if (ballRect.Y > rightCatcherRect.Y)
                                {
                                    ballSpeed = new Vector2(0, 0);
                                    ballRect.Location = new Point(-30, 906);
                                    launchable = false;
                                    dropStarted = false;
                                    ballLaunched = false;
                                    fallComplete = false;
                                    hitTube = false;
                                    fromLeft = false;
                                    fromRight = false;
                                    fromTop = true;
                                    rollStarted = false;
                                    reload = true;
                                }
                            }

                            if (ballRect.Intersects(tubeRect)) //Ball falls into bottom
                            {
                                if(!hitTube)
                                {
                                    if (!fromTop)
                                    {
                                        ballSpeed = new Vector2(0, 0);
                                        hitTube = true;
                                    }
                                }
                                else
                                {
                                    if (fromLeft && ballRect.X <= 412)
                                    {
                                        ballSpeed.X += 0.5f;
                                    }
                                    else if (fromLeft && ballRect.X >= 412)
                                    {
                                        ballSpeed.X = 0;
                                        ballSpeed.Y += 0.5f;
                                    }

                                    if (fromRight && ballRect.X >= 430)
                                    {
                                        ballSpeed.X -= 0.5f;
                                    }
                                    else if (fromRight && ballRect.X <= 430)
                                    {
                                        ballSpeed.X = 0;
                                        ballSpeed.Y += 0.5f;
                                    }
                                }
                            }

                            if (ballRect.Y > tubeRect.Y) //Reset ball from bottom, Zero Chips
                            {
                                ballSpeed = new Vector2(0, 0);
                                ballRect.Location = new Point(-30, 906);
                                launchable = false;
                                dropStarted = false;
                                ballLaunched = false;
                                fallComplete = false;
                                hitTube = false;
                                fromLeft = false;
                                fromRight = false;
                                fromTop = true;
                                rollStarted = false;
                                reload = true;
                            }
                        }
                    }
                }
                else if (remainingBalls == 0)
                {
                    currentScreen = Screen.WaitScreen;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            _spriteBatch.Draw(wallTexture, wallRect, Color.White);

            _spriteBatch.Draw(curveTexture, roofCurveLeftRect, new Rectangle(0, 0, 94, 95), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
            _spriteBatch.Draw(curveTexture, roofCurveRightRect, Color.White);
            _spriteBatch.Draw(curveTexture, floorCurveRightRect, new Rectangle(0, 0, 94, 95), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0f);
            _spriteBatch.Draw(bottomCurveTexture, floorCurveLeftRect, new Rectangle(0, 0, 94, 95), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);

            _spriteBatch.Draw(slopeTexture, slopeLeftRect, Color.White);
            _spriteBatch.Draw(slopeTexture, slopeRightRect, new Rectangle(0, 0, 127, 64), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
           
            _spriteBatch.Draw(floorTexture, floorLeftRect, Color.White);
            _spriteBatch.Draw(floorTexture, floorRightRect, Color.White);

            _spriteBatch.Draw(springTexture, springRect, Color.White);

            _spriteBatch.Draw(ballTexture, ballRect, Color.White);
            
            _spriteBatch.Draw(catcherTexture, centreCatcherRect, Color.White);
            _spriteBatch.Draw(catcherTexture, leftCatcherRect, Color.White);
            _spriteBatch.Draw(catcherTexture, rightCatcherRect, Color.White);
            _spriteBatch.Draw(tubeTexture, tubeRect, Color.White);

            foreach (Rectangle bumper in bumpers)
                _spriteBatch.Draw(bumperTexture, bumper, Color.White);
            
            foreach (Rectangle bumper in leftWallBumpers)
                _spriteBatch.Draw(bumperTexture, bumper, Color.White);

            foreach (Rectangle bumper in rightWallBumpers)
                _spriteBatch.Draw(bumperTexture, bumper, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
