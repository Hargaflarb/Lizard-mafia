using Lizard_game.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.Factory
{
    public class EnemyFactory : Factory
    {
        private static EnemyFactory instance;

        public static EnemyFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyFactory();
                }
                return instance;
            }
        }

        public override GameObject Create()
        {
            throw new NotImplementedException();
        }

        public GameObject CreateEnemy(Vector2 position)
        {
            GameObject enemyObject = new GameObject();
            SpriteRenderer enemyRenderer = enemyObject.AddComponent<SpriteRenderer>();
            enemyObject.AddComponent<Enemy>();
            enemyObject.AddComponent<Collider>();
            enemyRenderer.SetSprite("wasp");
            enemyObject.Transform.Position = position;
            enemyObject.Transform.Scale = 0.25f;
            return enemyObject;
        }
    }
}
