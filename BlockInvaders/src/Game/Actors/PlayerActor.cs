using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BlockInvaders
{ 
    internal class PlayerActor : Actor
    {
        public float Speed { get; set; } = 1000;

        private Color _color = Color.White;

        public int parryLifetime;

        //False will be basic shots, true will be charged shots
        public static bool firingMode = true;

        bool playerHit = false;
        bool hitFlash = false;
        int hitFlashCount = 0;
        int hitFlashTimer = 0;
        int afterHitIframeTimer = 0;

        //Make W shoot projectiles
        //Make S a parry button
        //Enemy projectiles will be red
        //Parryable enemy projectiles will be yellow

        

        public override void Start()
        {
            base.Start();
            AddComponent<DrawComponent>(new DrawComponent(this, (Transform.GlobalScale.x / 4) * 100, _color, new MathLibrary.Vector2(0, 0)));
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
            //movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);

            //Stop player if going to far left
            if (playerPosition.x >= 0 + 25)
            {
                movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
            }

            //Stop player if going to far right
            if (playerPosition.x <= Raylib.GetScreenWidth() - 25)
            {
                movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);
            }
            MathLibrary.Vector2 deltaMovement = movementInput.Normalized * Speed * (float)deltaTime;

            //Swap Firing Mode
            if (Raylib.IsKeyPressed(KeyboardKey.Q) || Raylib.IsKeyPressed(KeyboardKey.E))
            {
                firingMode = !(firingMode);
            }
            //Display firingMode
            if (firingMode == true)
            {
                Raylib.DrawText("Current Shot Mode: Charge",
                (Raylib.GetScreenWidth() / 2) - 75,
                Raylib.GetScreenHeight() - 25,
                10,
                Color.Red);
            }
            if (firingMode == false)
            {
                Raylib.DrawText("Current Shot Mode: Rapid",
                (Raylib.GetScreenWidth() / 2) - 75,
                Raylib.GetScreenHeight() - 25,
                10,
                Color.Red);
            }
            //Mode Swap Instructions
            Raylib.DrawText("Q/E to swap",
            (Raylib.GetScreenWidth() / 2) - 75,
            Raylib.GetScreenHeight() - 15,
            10,
            Color.Red);

            //Moves player if deltaMovement has a value
            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }

            //Flash Between red and _color when hit (to display iframes)
            if (hitFlash == true)
            {
                (this).GetComponent<DrawComponent>().Color = Color.Red;
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
                (this).GetComponent<DrawComponent>().Color = Color.White;

                if (hitFlashCount > 0 && hitFlashCount < 5)
                {
                    hitFlashTimer++;
                }

                if (hitFlashCount >= 5)
                {
                    hitFlashCount = 0;
                }

                if (hitFlashTimer >= 1250)
                {
                    
                    hitFlash = true;
                    hitFlashTimer = 0;
                }
            }

            //I-frames between taking damage
            if (playerHit == true)
            {
                afterHitIframeTimer++;
                if (afterHitIframeTimer >= 5000)
                {
                    afterHitIframeTimer = 0;
                    playerHit = false;
                }
            }




            //DEV KILL BIND
            if (Raylib.IsKeyPressed(KeyboardKey.Delete))
            {
                (this).GetComponent<HealthComponent>().Health--;
                if ((this).GetComponent<HealthComponent>().Health <= 0)
                {

                    //Go to death scene
                    Game.CurrentScene = Game.GetScene(1);
                }
            }
        }

        public override void OnCollision(Actor other)
        {
            //Only do collision behavior if the colliding Actor isn't playerGun or playerProjectile
            if (other.ToString() == "BlockInvaders.BlockQueenActor" || other.ToString() == "BlockInvaders.EnemyActor" || other.ToString() == "BlockInvaders.EnemyProjectileActor")
            {
                if (playerHit == false)
                {
                    playerHit = true;
                    hitFlash = true;
                    (this).GetComponent<HealthComponent>().Health--;
                    if ((this).GetComponent<HealthComponent>().Health <= 0)
                    {
                        //Go to death scene
                        Game.CurrentScene = Game.GetScene(1);
                    }
                }
            }
        }
    }
}
