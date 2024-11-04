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

        private Color _color = Color.Blue;

        private int projectileCharge;
        private int projectileLifetime;
        private int parryLifetime;

        //Make W shoot projectiles
        //Make S a parry button

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);


            //Movement
            MathLibrary.Vector2 movementInput = new MathLibrary.Vector2();
            //movementInput.y -= Raylib.IsKeyDown(KeyboardKey.W);
            movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
            //movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);
            movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);
            MathLibrary.Vector2 deltaMovement = movementInput.Normalized * Speed * (float)deltaTime;

            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }
            

            Raylib.DrawCircleV(Transform.GlobalPosition, (Transform.GlobalScale.x / 4) * 100, _color);

            //If W is held down charge a projectile
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                if (projectileLifetime < 125)
                {
                    projectileCharge = 0;
                }
                if (projectileLifetime > 125)
                {
                    Raylib.DrawCircleV(Transform.GlobalPosition, (Transform.GlobalScale.x / 12) * 100, Color.Red);
                    projectileCharge = 1;
                }

                if (projectileLifetime > 2000)
                {
                    Raylib.DrawCircleV(Transform.GlobalPosition, (Transform.GlobalScale.x / 10) * 100, Color.Orange);
                    projectileCharge = 2;
                }

                if (projectileLifetime > 5000)
                {
                    Raylib.DrawCircleV(Transform.GlobalPosition, (Transform.GlobalScale.x / 8) * 100, Color.White);
                    projectileCharge = 3;
                }

                if (projectileLifetime > 10000)
                {
                    Raylib.DrawCircleV(Transform.GlobalPosition, (Transform.GlobalScale.x / 6) * 100, Color.DarkBlue);
                    projectileCharge = 4;
                }

                if (projectileLifetime > 25000)
                {
                    Raylib.DrawCircleV(Transform.GlobalPosition, (Transform.GlobalScale.x / 5) * 100, Color.Violet);
                    projectileCharge = 5;
                }
                projectileLifetime++;
            }
            //Shoot a projectile with different effects depenting on the charge
            if (!(Raylib.IsKeyDown(KeyboardKey.W)) && projectileLifetime > 0)
            {
                //1 damage small projectile
                if (projectileCharge == 0)
                {



                }

                //2 damage small projectile
                if (projectileCharge == 1)
                {



                }

                //4 damage small projectile
                if (projectileCharge == 2)
                {



                }

                //6 damage small projectile
                if (projectileCharge == 3)
                {



                }

                //10 damage small projectile
                if (projectileCharge == 4)
                {



                }

                //20 damage big projectile
                if (projectileCharge == 5)
                {



                }

                projectileCharge = 0;
                projectileLifetime = 0;
            }

        }

        public override void OnCollision(Actor other)
        {
            _color = Color.Red;
        }
    }
}
