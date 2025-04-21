using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        KeyboardState keyboardState;
        MouseState mouseState;

        Ball ball;
        Texture2D ballTexture;
        Rectangle ballRect;

        Rectangle wallRect;
        Texture2D wallTexture;

        Texture2D backgroundTexture;
        Rectangle backgroundRect;

        Texture2D curveTexture;
        Texture2D bottomCurveTexture;
        Rectangle roofCurveLeftRect;
        Rectangle roofCurveRightRect;
        Rectangle floorCurveLeftRect;
        Rectangle floorCurveRightRect;

        Texture2D bumperTexture; // Needs to be replaced, the right side got cut off slightly

        Texture2D tubeTexture;
        Rectangle tubeRect;

        Texture2D slopeTexture;
        Rectangle slopeRightRect;
        Rectangle slopeLeftRect;

        Texture2D floorTexture;
        Rectangle floorLeftRect;
        Rectangle floorRightRect;

        List<Rectangle> bumpers;

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

            wallRect = new Rectangle(45, 185, 40, 800);
            
            backgroundRect = new Rectangle(0, 0, 800, 985);

            roofCurveLeftRect = new Rectangle(-1, 0, 130, 160);
            roofCurveRightRect = new Rectangle(671, 0, 130, 160);
            floorCurveLeftRect = new Rectangle(85, 762, 130, 160);
            floorCurveRightRect = new Rectangle(670, 762, 130, 160);

            floorLeftRect = new Rectangle(85, 922, 130, 64);
            floorRightRect = new Rectangle(670, 923, 130, 64);

            tubeRect = new Rectangle(400, 982, 84, 63);
            
            slopeLeftRect = new Rectangle(215, 921, 185, 64);
            slopeRightRect = new Rectangle(485, 921, 185, 64);

            ballRect = new Rectangle(0, 945, 40, 40);

            bumpers = new List<Rectangle>();
            //Row one
            bumpers.Add(new Rectangle(150, 100, 51, 52));
            bumpers.Add(new Rectangle(250, 100, 51, 52));
            bumpers.Add(new Rectangle(350, 100, 51, 52));
            bumpers.Add(new Rectangle(450, 100, 51, 52));
            bumpers.Add(new Rectangle(550, 100, 51, 52));
            bumpers.Add(new Rectangle(650, 100, 51, 52));
            //

            //Row two
            bumpers.Add(new Rectangle(90, 250, 51, 52));
            bumpers.Add(new Rectangle(200, 250, 51, 52));
            bumpers.Add(new Rectangle(300, 250, 51, 52));
            bumpers.Add(new Rectangle(400, 250, 51, 52)); // Replace with hole
            bumpers.Add(new Rectangle(500, 250, 51, 52));
            bumpers.Add(new Rectangle(600, 250, 51, 52));
            bumpers.Add(new Rectangle(700, 250, 51, 52));
            //

            //Row three
            bumpers.Add(new Rectangle(150, 400, 51, 52));
            bumpers.Add(new Rectangle(250, 400, 51, 52));
            bumpers.Add(new Rectangle(350, 400, 51, 52));
            bumpers.Add(new Rectangle(450, 400, 51, 52));
            bumpers.Add(new Rectangle(550, 400, 51, 52));
            bumpers.Add(new Rectangle(650, 400, 51, 52));
            //

            //Row four
            bumpers.Add(new Rectangle(90, 550, 51, 52));
            bumpers.Add(new Rectangle(200, 550, 51, 52)); // Replace with hole
            bumpers.Add(new Rectangle(300, 550, 51, 52));
            bumpers.Add(new Rectangle(400, 550, 51, 52));
            bumpers.Add(new Rectangle(500, 550, 51, 52));
            bumpers.Add(new Rectangle(600, 550, 51, 52)); // Replace with hole
            bumpers.Add(new Rectangle(700, 550, 51, 52));
            //

            //Row five
            bumpers.Add(new Rectangle(150, 700, 51, 52));
            bumpers.Add(new Rectangle(250, 700, 51, 52));
            bumpers.Add(new Rectangle(350, 700, 51, 52));
            bumpers.Add(new Rectangle(450, 700, 51, 52));
            bumpers.Add(new Rectangle(550, 700, 51, 52));
            bumpers.Add(new Rectangle(650, 700, 51, 52));
            //


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("sonicBall");

            backgroundTexture = Content.Load<Texture2D>("Casino Night Zone BG3");
            wallTexture = Content.Load<Texture2D>("Casino Night Wall");
            curveTexture = Content.Load<Texture2D>("Casino Night Curve");
            bottomCurveTexture = Content.Load<Texture2D>("Casino Night Curve Bottom");
            bumperTexture = Content.Load<Texture2D>("Casino Night Bumper");
            tubeTexture = Content.Load<Texture2D>("Casino Night Tube");
            slopeTexture = Content.Load<Texture2D>("Casino Night Slope");
            floorTexture = Content.Load<Texture2D>("Casino Night Floor");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Window.Title = mouseState.Position.ToString();

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

            foreach (Rectangle bumper in bumpers)
                _spriteBatch.Draw(bumperTexture, bumper, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
