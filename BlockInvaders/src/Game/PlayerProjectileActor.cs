using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class PlayerProjectileActor : PlayerGun
    {
        public float Speed { get; set; } = 1000;

        private Color _color = Color.Blue;


        //Make W shoot projectiles

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //Movement
            MathLibrary.Vector2 movement = new MathLibrary.Vector2();
            movement.y -= 1;

            MathLibrary.Vector2 deltaMovement = movement.Normalized * Speed * (float)deltaTime;

            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }

            Raylib.DrawCircleV(Transform.GlobalPosition, projectileSize, _color);

            if (Transform.LocalPosition.y <= 0)
            {
                projectileLifetime = 0;
                projectileCharge = 0;
            }
        }

        public override void OnCollision(Actor other)
        {
            
        }
    }
}
