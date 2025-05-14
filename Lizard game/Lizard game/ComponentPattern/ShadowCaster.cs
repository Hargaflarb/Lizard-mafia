using Microsoft.Xna.Framework;
using SharpDX.X3DAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public struct ShadowInterval
    {
        private float upperAngle;
        private float lowerAngle;
        private float angleOffset;
        private float distance;

        public ShadowInterval(ShadowCaster shadowCaster, LightEmitter light)
        {
            distance = shadowCaster.CalculateDistanceToLight(light);
            float BaseAngle = shadowCaster.CalculateLightToShadowAngle(light);
            float AngleIntervalSize = shadowCaster.CalculateAngle(distance);
            if (BaseAngle + AngleIntervalSize > MathF.PI * 2)
            {
                angleOffset = -((MathF.PI * 2) % (BaseAngle + AngleIntervalSize));
            }
            else if(BaseAngle - AngleIntervalSize < 0)
            {
                // double negativity
                angleOffset = -(BaseAngle - AngleIntervalSize);
            }
            else
            {
                angleOffset = 0;
            }
            upperAngle = BaseAngle + AngleIntervalSize + angleOffset;
            lowerAngle = BaseAngle - AngleIntervalSize + angleOffset;
        }
    }

    public class ShadowCaster : Component
    {
        private float objectRadius;

        public ShadowCaster(GameObject gameObject, float Radius) : base(gameObject)
        {
            objectRadius = Radius;
        }

        public float CalculateLightToShadowAngle(LightEmitter light)
        {
            Vector2 dif = GameObject.Transform.Position - light.GameObject.Transform.Position;
            return MathF.Atan2(dif.Y, dif.X);
        }
        public float CalculateDistanceToLight(LightEmitter light)
        {
            return (light.GameObject.Transform.Position - GameObject.Transform.Position).Length();
        }
        public float CalculateAngle(float lightDistance)
        {
            // i can use Asin here because the angle with never go above 90 degrees.
            // it would have to be inside the shadowcaster's radius.
            return MathF.Asin(objectRadius / lightDistance);
        }
    }
}
