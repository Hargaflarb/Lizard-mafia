﻿using Lizard_game.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.Factory
{
    public class BugFactory : Factory
    {
        private static BugFactory instance;

        public static BugFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BugFactory();
                }
                return instance;
            }   
        }

        public override GameObject Create()
        {
            throw new NotImplementedException();
        }

        public GameObject CreateBug(Vector2 position)
        {
            GameObject bugObject = new GameObject();
            SpriteRenderer bugSpriteRenderer = bugObject.AddComponent<SpriteRenderer>();
            bugObject.AddComponent<Bug>();
            bugSpriteRenderer.SetSprite("bug");
            bugObject.Transform.Position = position;
            bugObject.Transform.Scale = 0.25f;
            return bugObject;
        }
    }
}
