using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{ 
    internal class PlayerActor : Actor
    {
        public float Speed { get; set; } = 1000;

        private Color _color = Color.White;

        public int parryLifetime;

        bool playerHit = false;
        bool hitFlash = false;
        int hitFlashCount = 0;
        int hitFlashTimer = 0;

        //Make W shoot projectiles
        //Make S a parry button
        //Enemy projectiles will be red
        //Parryable enemy projectiles will be yellow

        

        public override void Start()
        {
            base.Start();
            AddComponent<DrawComponent>(new DrawComponent(this, (Transform.GlobalScale.x / 4) * 100, _color));
            AddComponent<HealthComponent>(new HealthComponent(this, 3));
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            

            //Define player attributes
            float playerSize = (Transform.GlobalScale.x / 4) * 100;
            MathLibrary.Vector2 playerPosition = Transform.GlobalPosition;

            //Movement
            MathLibrary.Vector2 movementInput = new MathLibrary.Vector2();
            //movementInput.y -= Raylib.IsKeyDown(KeyboardKey.W);
            movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
            //movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);
            movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);
            MathLibrary.Vector2 deltaMovement = movementInput.Normalized * Speed * (float)deltaTime;

            //Moves player if deltaMovement has a value
            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }
            
            //Draw Player
            //Raylib.DrawCircleV(playerPosition, playerSize, _color);

            //Flash Between red and _color when hit (to display iframes)
            if (hitFlash == true)
            {
                _color = Color.Red;
                hitFlashTimer++;
                if (hitFlashTimer >= 1250)
                {
                    hitFlash = false;
                    hitFlashTimer = 0;
                    hitFlashCount++;
                }
            }
            else if (hitFlash == false)
            {
                _color = Color.White;

                if (hitFlashCount > 0 && hitFlashCount < 5)
                {
                    hitFlashTimer++;
                }

                if (hitFlashCount >= 5)
                {
                    hitFlashCount = 0;
                    playerHit = false;
                }

                if (hitFlashTimer >= 1250)
                {
                    
                    hitFlash = true;
                    hitFlashTimer = 0;
                }
            }
        }

        public override void OnCollision(Actor other)
        {
            //Only do collision behavior if the colliding Actor isn't playerGun or playerProjectile
            if (other.ToString() != "playerGun" && (other.ToString() != "BlockInvaders.PlayerProjectileActor") && playerHit == false)
            {
                playerHit = true;
                hitFlash = true;
                (this).GetComponent<HealthComponent>().Health--;
            }
        }
    }
}
