﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class GameObject
    {
        private List<Component> components = new List<Component>();
        private Transform transform;
        private Vector2 velocity;


        public Transform Transform { get; set; } = new Transform();
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public float XVelocity { get => velocity.X; set => velocity = new Vector2(value, velocity.Y); }
        public float YVelocity { get => velocity.Y; set => velocity = new Vector2(velocity.X, value); }


        public GameObject()
        {
            components = new List<Component>();
        }

        public T AddComponent<T>(params object[] additionalParameters) where T : Component 
        {
            Type componentType = typeof(T);
            try
            {
                object[] allParameters = new object[1 + additionalParameters.Length];
                allParameters[0] = this;
                Array.Copy(additionalParameters, 0, allParameters, 1, additionalParameters.Length);

                T component = (T)Activator.CreateInstance(componentType, allParameters);
                components.Add(component);
                return component;
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Klassen {componentType.Name} har ikke en konstruktør der matcher de leverede parametre.");
            }
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)components.Find(x => x.GetType() == typeof(T));
        }

        public void Awake()
        {
            foreach (var component in components)
            {
                component.Awake();
            }
        }

        public void Start()
        {
            foreach (var component in components)
            {
                component.Start();
            }
        }

        public void Update()
        {
            foreach (var component in components)
            {
                component.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        public void OnCollision(Collider collider)
        {
            foreach (var component in components)
            {
                component.OnCollision(collider);
            }
        }

        public Component AddComponentWithExistingValues(Component component)
        {
            components.Add(component);
            return component;
        }
    }
}
