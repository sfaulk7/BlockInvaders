using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class PlayerProjectileActor : Actor
    {
        public float Speed { get; set; } = 1000;

        private Color _color = Color.Brown;

        public int projectileCharge;
        public int projectileLifetime;
        public float projectileSize;

        //Make W shoot projectiles

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            projectileSize = (Transform.GlobalScale.x / 12) * 100;

            //Movement
            MathLibrary.Vector2 movement = new MathLibrary.Vector2();
            movement.y -= 1;
            MathLibrary.Vector2 deltaMovement = movement.Normalized * Speed * (float)deltaTime;
            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }

            //Draw Projectile
            Raylib.DrawCircleV(Transform.GlobalPosition, projectileSize, _color);

            if (Transform.LocalPosition.y <= 0)
            {
                projectileLifetime = 0;
                projectileCharge = 0;
            }
        }



        public override void OnCollision(Actor other)
        {
            //Only do collision behavior if the colliding Actor isn't playerGun or player
            if (other.ToString() != "playerGun" && (other.ToString() != "BlockInvaders.PlayerActor"))
            {
                MathLibrary.Vector2 Gut = new MathLibrary.Vector2(9999, 9999);
                Transform.LocalPosition = Gut;
                projectileSize = 9999f;
            }
        }
    }
}
