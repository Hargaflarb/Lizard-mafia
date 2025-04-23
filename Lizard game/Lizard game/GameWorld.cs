using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
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
        private List<GameObject> activeGameObjects;
        private List<GameObject> gameObjectsToAdd;
        private List<GameObject> gameObjectsToRemove;

        public float DeltaTime { get; set; }
        public GraphicsDeviceManager Graphics { get { return _graphics; } }

        public Texture2D Pixel;

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

        public GameObject Player { get; private set; }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
             _graphics.ApplyChanges();
             
            activeGameObjects = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();

            GameObject playerObject = CreatePlayer(new Vector2(100, 100));
            InputHandler.AddHeldKeyBind(Keys.D, new MoveCommand((Player)playerObject.GetComponent<Player>(), new Vector2(1, 0)));
            InputHandler.AddHeldKeyBind(Keys.A, new MoveCommand((Player)playerObject.GetComponent<Player>(), new Vector2(-1, 0)));
            InputHandler.AddHeldKeyBind(Keys.D, new MoveCommand((Player)playerObject.GetComponent<Player>(), new Vector2(1, 0)));
            InputHandler.AddHeldKeyBind(Keys.LeftShift, new SprintCommand((Player)playerObject.GetComponent<Player>()));
            InputHandler.AddClickedKeyBind(Keys.Space, new JumpCommand((Player)playerObject.GetComponent<Player>()));
            InputHandler.AddClickedKeyBind(Keys.R, new ResetCommand((Player)playerObject.GetComponent<Player>()));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Pixel = Content.Load<Texture2D>("Pixel");

            foreach (var gameObject in activeGameObjects)
            {
                gameObject.Start();
            }
            //feel free to edit starting position

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (GameObject gameObject in activeGameObjects) 
            { 
                gameObject.Update();
            }

            foreach (GameObject gameObject in gameObjectsToAdd) 
            { 
                activeGameObjects.Add(gameObject);
            }
            gameObjectsToAdd.Clear();

            foreach (GameObject gameObject in gameObjectsToRemove)
            {
                activeGameObjects.Remove(gameObject);
            }
            gameObjectsToRemove.Clear();

            InputHandler.HandleInput();
            base.Update(gameTime);
        }

        public void AddObject(GameObject gameObject)
        {
            gameObject.Awake();
            
            gameObjectsToAdd.Add(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            gameObjectsToRemove.Add(gameObject);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            foreach (GameObject gameObject in activeGameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private GameObject CreatePlayer(Vector2 position)
        {
            Player = new GameObject();
            Player.AddComponent<Player>();
            Player.AddComponent<Collider>();
            Player.AddComponent<SpriteRenderer>();
            ((SpriteRenderer)Player.GetComponent<SpriteRenderer>()).SetSprite(Content.Load<Texture2D>("playerIdle"));
            Player.Transform.Position = position;
            AddObject(Player);
            return Player;
        }
    }
}
