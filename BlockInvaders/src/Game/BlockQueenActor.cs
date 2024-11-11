using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class BlockQueenActor : Actor
    {
        public float Speed { get; set; } = 250;

        private Color _color = Color.Red;

        bool enemyHit = false;

        public override void Start()
        {
            base.Start();
            AddComponent<DrawComponent>(new DrawComponent(this, (Transform.GlobalScale.x / 4) * 100, _color));
            AddComponent<HealthComponent>(new HealthComponent(this, 5));
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //Movement
            MathLibrary.Vector2 movement = new MathLibrary.Vector2();

            if (Transform.GlobalPosition.y > 960)
            {
                MathLibrary.Vector2 subtract = new MathLibrary.Vector2(0, -500);
                Transform.LocalPosition += (subtract);
            }
            else
            {
                movement.y += 1;
            }

            MathLibrary.Vector2 deltaMovement = movement.Normalized * Speed * (float)deltaTime;

            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }

        }

        public override void OnCollision(Actor other)
        {
            if (other.ToString() == "BlockInvaders.PlayerProjectileActor" && enemyHit == false)
            {
                enemyHit = true;
                (this).GetComponent<HealthComponent>().Health--;
            }
        }
    }
}
