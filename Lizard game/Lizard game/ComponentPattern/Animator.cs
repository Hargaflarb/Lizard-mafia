using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class Animator : Component
    {
        #region Fields
        private int currentIndex;
        private float elapsedTime;
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations;
        private Animation currentAnimation;
        #endregion
        #region Properties
        public int CurrentIndex { get => currentIndex; set => currentIndex = value; }
        public float ElapsedTime { get => elapsedTime; set => elapsedTime = value; }
        #endregion
        #region Constructors
        public Animator(GameObject gameObject) : base(gameObject)
        {
        }
        #endregion
        #region Methods
        public void Animation(GameObject gameObject)
        {
            spriteRenderer = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
        }
        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += GameWorld.Instance.DeltaTime;
            currentIndex = (int)(elapsedTime * currentAnimation.Fps);
            if (currentIndex > currentAnimation.Sprites.Length - 1)
            {
                elapsedTime = 0;
                CurrentIndex = 0;
            }
            spriteRenderer.Sprite=currentAnimation.Sprites[currentIndex];
        }

        public void AddAnimation(Animation animation)
        {
            if (!animations.TryGetValue(animation.Name, out var anim))
            {
                animations.Add(animation.Name, animation);
            }
        }

        public void PlayAnimation(string name)
        {
            currentAnimation = animations[name];
        }
        #endregion
    }
}
