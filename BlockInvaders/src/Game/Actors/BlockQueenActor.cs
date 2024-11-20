using Raylib_cs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        bool goingLeft = false;

        public override void Start()
        {
            base.Start();
            AddComponent<DrawComponent>(new DrawComponent(this, (Transform.GlobalScale.x / 4) * 100, _color, new MathLibrary.Vector2(0, 0)));
            AddComponent<EnemySpawnComponent>(new EnemySpawnComponent(this));
            AddComponent<HealthComponent>(new HealthComponent(this, 5 * TestScene.waveCount));
            AddComponent<EnemyShootComponent>(new EnemyShootComponent(this));
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            MathLibrary.Vector2 screenDimensions = new MathLibrary.Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

            //Movement
            MathLibrary.Vector2 movement = new MathLibrary.Vector2();

            //If enemy goes off the right of screen turn left and shift down
            if (Transform.GlobalPosition.x >= screenDimensions.x - 75)
            {
                goingLeft = true;
                MathLibrary.Vector2 subtract = new MathLibrary.Vector2(-5, 25);
                Transform.LocalPosition += (subtract);
            }
            //If enemy goes off the left of screen turn right and shift down
            if (Transform.GlobalPosition.x <= 0 + 75)
            {
                goingLeft = false;
                MathLibrary.Vector2 subtract = new MathLibrary.Vector2(5, 25);
                Transform.LocalPosition += (subtract);
            }
            //Go right
            if (goingLeft == false)
            {
                movement.x += 1;
            }
            //Go Left
            if (goingLeft == true)
            {
                movement.x -= 1;
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
                //enemyHit = true;
                (this).GetComponent<HealthComponent>().Health -= PlayerShootComponent.projectileDamage;
            }
        }
    }
}
