using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Lizard_game.Command;
using Lizard_game.ComponentPattern;
using Lizard_game.Factory;


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
        private GameObject playerObject;
        private GameObject bugObject;
        private GameObject shadows;
        private Effect effect;
        private bool isAlive = true;


        public GameObject BugObject { get => bugObject; set => bugObject = value; }
        public GameObject PlayerObject { get => playerObject; private set => playerObject = value; }
        public float DeltaTime { get => deltaTime; set => deltaTime = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
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


        protected override void Initialize()
        {
            IsAlive = true;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();
            InputHandler.Reset();
            

            activeGameObjects = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            
            AddObject(WallFactory.Instance.CreateWall(new Rectangle(400, 900, 400, 300)));
            AddObject(WallFactory.Instance.CreateWall(new Rectangle(800, 800, 600, 400)));
            AddObject(WallFactory.Instance.CreateWall(new Rectangle(200, 800, 200, 300)));
            AddObject(WallFactory.Instance.CreateWall(new Rectangle(1600, 500, 200, 600)));
            AddObject(WallFactory.Instance.CreateWall(new Rectangle(1400, 650, 200, 600)));
            AddObject(WallFactory.Instance.CreateWall(new Rectangle(500, 350, 600, 150)));

            ShadowMap.SetSprite();

            //feel free to edit starting position
            PlayerObject = CreatePlayer(new Vector2(1000, 500));

            BugObject = BugFactory.Instance.CreateBug(new Vector2(1000, 900));
            AddObject(BugObject);

            //feel free to edit starting position
            GameObject enemyObject = EnemyFactory.Instance.CreateEnemy(new Vector2(100, 700));
            AddObject(enemyObject);
            

            InputHandler.AddHeldKeyBind(Keys.D, new MoveCommand(PlayerObject.GetComponent<Player>(), 1));
            InputHandler.AddHeldKeyBind(Keys.A, new MoveCommand(PlayerObject.GetComponent<Player>(), -1));
            InputHandler.AddHeldKeyBind(Keys.LeftShift, new SprintCommand(PlayerObject.GetComponent<Player>()));
            InputHandler.AddClickedKeyBind(Keys.Space, new JumpCommand(PlayerObject.GetComponent<Player>()));
            InputHandler.AddClickedKeyBind(Keys.R, new ResetCommand(PlayerObject.GetComponent<Player>()));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            effect = Content.Load<Effect>("TestShader");
            Pixel = Content.Load<Texture2D>("Pixel");
            //add animations to the player (made here to load the textures)
            Texture2D idleSprite = Content.Load<Texture2D>("playerIdle");
            playerObject.GetComponent<Animator>().AddAnimation(new Animation("Idle", new Texture2D[] { idleSprite }, 1));
            Texture2D[] walkAnim = new Texture2D[8];
            for (int i = 0; i < 8; i++)
            {
                walkAnim[i] = Content.Load<Texture2D>("playerWalk/walk anim frame " + (i + 1));
            }
            PlayerObject.GetComponent<Animator>().AddAnimation(new Animation("Walk", walkAnim, 12));
            PlayerObject.GetComponent<SpriteRenderer>().SetSprite(idleSprite);
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

            if (IsAlive == false)
            {
                GameOver();
                Initialize();
            }

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
            ShadowMap.PrepareShadows(_spriteBatch);

            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            foreach (GameObject gameObject in activeGameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }
            ShadowMap.Draw(_spriteBatch);
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

                    Collider colider1 = gameObject1.GetComponent<Collider>();
                    Collider colider2 = gameObject2.GetComponent<Collider>();


                    if (colider1 is not null && colider2 is not null && colider1.IsColliding(colider2))
                    {
                        gameObject1.OnCollision(colider2);
                        gameObject2.OnCollision(colider1);
                    }
                }
            }
        }

        public void GameOver()
        {
            activeGameObjects.Clear();
        }

        private GameObject CreatePlayer(Vector2 position)
        {
            GameObject newPlayer = new GameObject();
            newPlayer.AddComponent<Player>();
            newPlayer.AddComponent<Collider>();
            newPlayer.AddComponent<Gravity>();
            newPlayer.AddComponent<SpriteRenderer>();
            newPlayer.AddComponent<Animator>();
            newPlayer.AddComponent<LightEmitter>(0.15f);
            newPlayer.Transform.Position = position;
            AddObject(newPlayer);
            return newPlayer;
        }

        public (List<LightEmitter> lightEmitters, List<ShadowInterval> shadowIntervals) GetShaderData()
        {
            List<LightEmitter> lightEmitters = new List<LightEmitter>();
            List<ShadowInterval> shadowCasters = new List<ShadowInterval>(); 
            foreach (GameObject gameObject in activeGameObjects)
            {
                Component shaderComponent;
                if ((shaderComponent = gameObject.GetComponent<LightEmitter>()) is not null)
                {
                    lightEmitters.Add((LightEmitter)shaderComponent);
                }
                else if ((shaderComponent = gameObject.GetComponent<ShadowCaster>()) is not null)
                {

                    //shadowCasters.Add((ShadowInterval)shaderComponent);
                }
            }
            return (lightEmitters, shadowCasters);
        }
    }
}
