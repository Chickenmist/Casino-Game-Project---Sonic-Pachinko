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

        Ball ball;
        Texture2D ballTexture;
        Rectangle ballRect;
        Vector2 ballSpeed;
        Point ballPostionHolder;
        bool ballLaunched;

        bool regBumperHit;
        bool leftBumperHit;
        bool rightBumperHit;

        float launchTime;

        Rectangle wallRect;
        Texture2D wallTexture;

        Texture2D backgroundTexture;
        Rectangle backgroundRect;

        Texture2D curveTexture;
        Texture2D bottomCurveTexture;
        Rectangle roofCurveLeftRect;
        //Left roof curve movement rectangles
        Rectangle roofCurveLeftMoveRectOne;
        Rectangle roofCurveLeftMoveRectTwo;
        Rectangle roofCurveLeftMoveRectThree;
        //

        Rectangle roofCurveRightRect;
        Rectangle floorCurveLeftRect;
        Rectangle floorCurveRightRect;

        Texture2D bumperTexture;

        Texture2D tubeTexture;
        Rectangle tubeRect;

        Texture2D catcherTexture;
        Rectangle centreCatcherRect;
        Rectangle leftCatcherRect;
        Rectangle rightCatcherRect;

        Texture2D slopeTexture;
        Rectangle slopeRightRect;
        Rectangle slopeLeftRect;

        Texture2D floorTexture;
        Rectangle floorLeftRect;
        Rectangle floorRightRect;

        List<Rectangle> bumpers;
        List<Rectangle> leftWallBumpers;
        List<Rectangle> rightWallBumpers;

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
            launchTime = 0;

            wallRect = new Rectangle(45, 185, 40, 800);
            
            backgroundRect = new Rectangle(0, 0, 800, 985);

            roofCurveLeftRect = new Rectangle(-1, 0, 130, 160);

            //Left roof curve movement rectangles
            roofCurveLeftMoveRectOne = new Rectangle(0, 107, 5, 5);
            roofCurveLeftMoveRectTwo = new Rectangle(0, 29, 50, 5);
            roofCurveLeftMoveRectThree = new Rectangle(0, 0, 106, 5);
            //

            roofCurveRightRect = new Rectangle(671, 0, 130, 160);
            floorCurveLeftRect = new Rectangle(85, 762, 130, 160);
            floorCurveRightRect = new Rectangle(670, 762, 130, 160);

            floorLeftRect = new Rectangle(85, 922, 130, 64);
            floorRightRect = new Rectangle(670, 923, 130, 64);

            tubeRect = new Rectangle(400, 982, 84, 63);
            
            slopeLeftRect = new Rectangle(215, 921, 185, 64);
            slopeRightRect = new Rectangle(485, 921, 185, 64);

            ballRect = new Rectangle(0, 945, 40, 40);

            centreCatcherRect = new Rectangle(400, 250, 51, 52);

            leftCatcherRect = new Rectangle(200, 550, 51, 52);

            rightCatcherRect = new Rectangle(600, 550, 51, 52);

            ballSpeed = new Vector2(0, -10);

            bumpers = new List<Rectangle>();
            
            //Row one
            bumpers.Add(new Rectangle(150, 100, 52, 52));
            bumpers.Add(new Rectangle(250, 100, 52, 52));
            bumpers.Add(new Rectangle(350, 100, 52, 52));
            bumpers.Add(new Rectangle(450, 100, 52, 52));
            bumpers.Add(new Rectangle(550, 100, 52, 52));
            bumpers.Add(new Rectangle(650, 100, 52, 52));
            //

            //Row two
            bumpers.Add(new Rectangle(200, 250, 52, 52));
            bumpers.Add(new Rectangle(300, 250, 52, 52));
            bumpers.Add(new Rectangle(500, 250, 52, 52));
            bumpers.Add(new Rectangle(600, 250, 52, 52));
            //

            //Row three
            bumpers.Add(new Rectangle(150, 400, 52, 52));
            bumpers.Add(new Rectangle(250, 400, 52, 52));
            bumpers.Add(new Rectangle(350, 400, 52, 52));
            bumpers.Add(new Rectangle(450, 400, 52, 52));
            bumpers.Add(new Rectangle(550, 400, 52, 52));
            bumpers.Add(new Rectangle(650, 400, 52, 52));
            //

            //Row four
            bumpers.Add(new Rectangle(300, 550, 52, 52));
            bumpers.Add(new Rectangle(400, 550, 52, 52));
            bumpers.Add(new Rectangle(500, 550, 52, 52));
            //

            //Row five
            bumpers.Add(new Rectangle(150, 700, 52, 52));
            bumpers.Add(new Rectangle(250, 700, 52, 52));
            bumpers.Add(new Rectangle(350, 700, 52, 52));
            bumpers.Add(new Rectangle(450, 700, 52, 52));
            bumpers.Add(new Rectangle(550, 700, 52, 52));
            bumpers.Add(new Rectangle(650, 700, 52, 52));
            //
            
            //Left wall bumpers
            leftWallBumpers = new List<Rectangle>();
            leftWallBumpers.Add(new Rectangle(90, 250, 52, 52));
            leftWallBumpers.Add(new Rectangle(90, 550, 52, 52));
            //

            //Right wall bumpers
            rightWallBumpers = new List<Rectangle>();
            rightWallBumpers.Add(new Rectangle(700, 250, 52, 52));
            rightWallBumpers.Add(new Rectangle(700, 550, 52, 52));
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
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Window.Title = mouseState.Position.ToString();

            if (currentScreen == Screen.WaitScreen)
            {
                //Input bets

                currentScreen = Screen.PlayScreen;
            }
            else 
            {
                if (!ballLaunched)
                {
                    if (keyboardState.IsKeyDown(Keys.Space)) // Initiate launch
                    {

                        firstBumper = new Random().Next(1, 7);

                        ballSpeed = new Vector2(0, -10);

                        ballLaunched = true;
                    }
                }
                else
                {
                    ballRect.Offset(ballSpeed);
                    if (ballRect.Intersects(roofCurveLeftMoveRectOne))
                    {
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
                            if (ballRect.X >= 150 && ballRect.X <= 202)
                            {
                                ballSpeed = new Vector2(0, 5);
                                dropSide = new Random().Next(1,3);
                                dropStarted = true;
                            }
                        }
                        else if (firstBumper == 2)
                        {
                            if (ballRect.X >= 250 && ballRect.X <= 302)
                            {
                                ballSpeed = new Vector2(0, 5);
                                dropSide = new Random().Next(1, 3);
                                dropStarted = true;
                            }
                        }
                        else if (firstBumper == 3)
                        {
                            if (ballRect.X >= 350 && ballRect.X <= 402)
                            {
                                ballSpeed = new Vector2(0, 5);
                                dropSide = new Random().Next(1, 3);
                                dropStarted = true;
                            }
                        }
                        else if (firstBumper == 4)
                        {
                            if (ballRect.X >= 450 && ballRect.X <= 502)
                            {
                                ballSpeed = new Vector2(0, 5);
                                dropSide = new Random().Next(1, 3);
                                dropStarted = true;
                            }
                        }
                        else if (firstBumper == 5)
                        {
                            if (ballRect.X >= 550 && ballRect.X <= 602)
                            {
                                ballSpeed = new Vector2(0, 5);
                                dropSide = new Random().Next(1, 3);
                                dropStarted = true;
                            }
                        }
                        else if (firstBumper == 6)
                        {
                            if (ballRect.X >= 650 && ballRect.X <= 702)
                            {
                                ballSpeed = new Vector2(0, 5);
                                dropSide = new Random().Next(1, 3);
                                dropStarted = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (Rectangle bumper in bumpers)
                        {
                            if (ballRect.Intersects(bumper))
                            {
                                regBumperHit = false;
                                ballSpeed = new Vector2(0, 0);
                                ballRect.Y = bumper.Top - ballRect.Height - 5;
                                ballPostionHolder = ballRect.Location;
                                dropSide = new Random().Next(1, 3);
                                regBumperHit = true;
                            }
                            
                            if (regBumperHit)
                            {
                                if (dropSide == 1) //Drop to the left
                                {
                                    ballSpeed = new Vector2(-5, 0);

                                    if (ballRect.X >= ballPostionHolder.X - 100 && ballRect.X <= ballPostionHolder.X - 50)
                                    {
                                        ballSpeed = new Vector2(0, 5);
                                    }
                                }
                                else if (dropSide == 2) //Drop to the right
                                {
                                    ballSpeed = new Vector2(5, 0);

                                    if (ballRect.X <= ballPostionHolder.X + 100 && ballRect.X >= ballPostionHolder.X + 50)
                                    {
                                        ballSpeed = new Vector2(0, 5);
                                    }
                                }
                            }
                        }

                        foreach (Rectangle leftbumper in leftWallBumpers)
                        {
                            if (ballRect.Intersects(leftbumper))
                            {
                                leftBumperHit = false;
                                ballSpeed = new Vector2(0, 0);
                                ballRect.Y = leftbumper.Top - ballRect.Height - 5;
                                dropSide = 2;
                                leftBumperHit = true;
                            }

                            if (leftBumperHit)
                            {
                                ballSpeed = new Vector2(5, 0);

                                if (ballRect.X <= ballPostionHolder.X + 100 && ballRect.X >= ballPostionHolder.X + 50)
                                {
                                    ballSpeed = new Vector2(0, 5);
                                }
                            }
                        }

                        foreach (Rectangle rightbumper in rightWallBumpers)
                        {
                            if (ballRect.Intersects(rightbumper))
                            {
                                rightBumperHit = false;
                                ballSpeed = new Vector2(0, 0);
                                ballRect.Y = rightbumper.Top - ballRect.Height - 5;
                                dropSide = 1;
                                rightBumperHit = true;
                            }

                            if (rightBumperHit)
                            {
                                ballSpeed = new Vector2(-5, 0);

                                if (ballRect.X >= ballPostionHolder.X - 100 && ballRect.X <= ballPostionHolder.X - 50)
                                {
                                    ballSpeed = new Vector2(0, 5);
                                }
                            }
                        }
                       
                    }
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
            _spriteBatch.Draw(tubeTexture, tubeRect, Color.White);
            _spriteBatch.Draw(slopeTexture, slopeLeftRect, Color.White);
            _spriteBatch.Draw(slopeTexture, slopeRightRect, new Rectangle(0, 0, 127, 64), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
            _spriteBatch.Draw(floorTexture, floorLeftRect, Color.White);
            _spriteBatch.Draw(floorTexture, floorRightRect, Color.White);
            _spriteBatch.Draw(ballTexture, ballRect, Color.White);
            _spriteBatch.Draw(catcherTexture, centreCatcherRect, Color.White);
            _spriteBatch.Draw(catcherTexture, leftCatcherRect, Color.White);
            _spriteBatch.Draw(catcherTexture, rightCatcherRect, Color.White);
            
            _spriteBatch.Draw(boxTexture, roofCurveLeftMoveRectOne, new Color(Color.White, 0.5f));
            _spriteBatch.Draw(boxTexture, roofCurveLeftMoveRectTwo, new Color(Color.White, 0.5f));
            _spriteBatch.Draw(boxTexture, roofCurveLeftMoveRectThree, new Color(Color.White, 0.5f));

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
