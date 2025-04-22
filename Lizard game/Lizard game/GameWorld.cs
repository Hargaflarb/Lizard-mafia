using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lizard_game.Command;
using Lizard_game.ComponentPattern;

namespace Lizard_game
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static GameWorld instance;
        private float deltaTime;

        public float DeltaTime { get; set; }
        public GraphicsDeviceManager Graphics { get { return _graphics; } }

        public static GameWorld Instance 
        { 
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            } 
        }

        private GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();

            GameObject playerObject = new GameObject();
            playerObject.AddComponent<Player>();
            InputHandler.AddHeldKeyBind(Keys.D, new MoveCommand((Player)playerObject.GetComponent<Player>(), new Vector2(1, 0)));
            InputHandler.AddHeldKeyBind(Keys.A, new MoveCommand((Player)playerObject.GetComponent<Player>(), new Vector2(-1, 0)));
            InputHandler.AddHeldKeyBind(Keys.LeftShift, new SprintCommand((Player)playerObject.GetComponent<Player>()));
            InputHandler.AddClickedKeyBind(Keys.Space, new JumpCommand((Player)playerObject.GetComponent<Player>()));
            InputHandler.AddClickedKeyBind(Keys.R, new ResetCommand((Player)playerObject.GetComponent<Player>()));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            InputHandler.HandleInput();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
