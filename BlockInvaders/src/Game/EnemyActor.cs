using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class EnemyActor : Actor
    {
        public float Speed { get; set; } = 150;

        private Color _color = Color.Red;

        bool enemyHit = false;
        bool goingLeft = false;

        public override void Start()
        {
            base.Start();
            AddComponent<DrawComponent>(new DrawComponent(this, (Transform.GlobalScale.x / 4) * 100, _color, new MathLibrary.Vector2(0, 0)));
            AddComponent<HealthComponent>(new HealthComponent(this, 5));
            AddComponent<EnemyShootComponent>(new EnemyShootComponent(this));
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);


        }

        public override void OnCollision(Actor other)
        {
            if (other.ToString() == "BlockInvaders.PlayerProjectileActor" && enemyHit == false)
            {
                //enemyHit = true;
                (this).GetComponent<HealthComponent>().Health--;
            }
        }
    }
}
