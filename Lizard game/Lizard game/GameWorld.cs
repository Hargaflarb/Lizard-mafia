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

        public GameObject PlayerObject { get; private set; }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();

            activeGameObjects = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();

            GameObject wallObject = new GameObject();
            wallObject.AddComponent<SpriteRenderer>();
            wallObject.AddComponent<Collider>();
            wallObject.AddComponent<Wall>(new Vector2(2000, 700));
            AddObject(wallObject);
            GameObject wallObject2 = new GameObject();
            wallObject2.AddComponent<SpriteRenderer>();
            wallObject2.AddComponent<Collider>();
            wallObject2.AddComponent<Wall>(new Vector2(1000, 1000));
            AddObject(wallObject2);


            //feel free to edit starting position
            PlayerObject = CreatePlayer(new Vector2(100, 100));
            InputHandler.AddHeldKeyBind(Keys.D, new MoveCommand((Player)PlayerObject.GetComponent<Player>(), 1));
            InputHandler.AddHeldKeyBind(Keys.A, new MoveCommand((Player)PlayerObject.GetComponent<Player>(), -1));
            InputHandler.AddHeldKeyBind(Keys.LeftShift, new SprintCommand((Player)PlayerObject.GetComponent<Player>()));
            InputHandler.AddClickedKeyBind(Keys.Space, new JumpCommand((Player)PlayerObject.GetComponent<Player>()));
            InputHandler.AddClickedKeyBind(Keys.R, new ResetCommand((Player)PlayerObject.GetComponent<Player>()));
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Pixel = Content.Load<Texture2D>("Pixel");

            foreach (GameObject gameObject in activeGameObjects)
            {
                gameObject.Start();
            }
            //add animations to the player (made here to load the textures)
            Texture2D idleSprite = Content.Load<Texture2D>("playerIdle");
            ((Animator)PlayerObject.GetComponent<Animator>()).AddAnimation(new Animation("Idle", new Texture2D[] { idleSprite }, 1));
            Texture2D[] walkAnim = new Texture2D[8];
            for (int i = 0; i < 8; i++)
            {
                walkAnim[i] = Content.Load<Texture2D>("playerWalk/walk anim frame " + (i + 1));
            }
            ((Animator)PlayerObject.GetComponent<Animator>()).AddAnimation(new Animation("Walk", walkAnim, 12));
            ((SpriteRenderer)PlayerObject.GetComponent<SpriteRenderer>()).SetSprite(idleSprite);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            CleanUpGameObjects();

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (GameObject gameObject in activeGameObjects)
            {
                gameObject.Update();
            }

            CheckCollision();


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

        public void CleanUpGameObjects()
        {
            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                gameObject.Start();
                activeGameObjects.Add(gameObject);
            }
            gameObjectsToAdd.Clear();

            foreach (GameObject gameObject in gameObjectsToRemove)
            {
                activeGameObjects.Remove(gameObject);
            }
            gameObjectsToRemove.Clear();
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

        protected void CheckCollision()
        {
            foreach (GameObject gameObject1 in activeGameObjects)
            {
                foreach (GameObject gameObject2 in activeGameObjects)
                {
                    if (gameObject1 == gameObject2)
                    {
                        continue;
                    }

                    Collider colider1 = (Collider)gameObject1.GetComponent<Collider>();
                    Collider colider2 = (Collider)gameObject2.GetComponent<Collider>();


                    if (colider1 is not null && colider2 is not null && colider1.IsColliding(colider2))
                    {
                        gameObject1.OnCollision(colider2);
                        gameObject2.OnCollision(colider1);
                    }
                }
            }
        }


        private GameObject CreatePlayer(Vector2 position)
        {
            GameObject newPlayer = new GameObject();
            newPlayer.AddComponent<Player>();
            newPlayer.AddComponent<Collider>();
            newPlayer.AddComponent<SpriteRenderer>();
            newPlayer.AddComponent<Animator>();
            newPlayer.AddComponent<Gravity>();
            newPlayer.Transform.Position = position;
            AddObject(newPlayer);
            return newPlayer;
        }
    }
}
