using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class EnemyShootComponent : Component
    {
        Random rand = new Random();

        private Color _color = Color.Brown;
        public float shootTimer = 0f;
        public int projectileCharge;
        public int projectileLifetime;
        public float projectileSize;

        public float ProjectileSize
        {
            get => projectileSize;
            set
            {
                projectileSize = value;
                return;
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                return;
            }
        }

        public EnemyShootComponent(Actor owner) : base(owner)
        {

        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            shootTimer += (float)deltaTime;

            if (shootTimer >= 5)
            {
                shootTimer = 0;
                int fire = rand.Next(1, 10);

                if (fire == 7)
                {
                    Actor enemyProjectile = Actor.Instantiate(new EnemyProjectileActor(), null, Owner.Transform.GlobalPosition, 0);
                    enemyProjectile.Collider = new CircleCollider(enemyProjectile, projectileSize);
                }

                Console.WriteLine(fire);
            }
        }
    }
}
