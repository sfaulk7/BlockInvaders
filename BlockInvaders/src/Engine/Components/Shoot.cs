using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class Shoot : Component
    {
        public int projectileCharge;
        public int projectileLifetime;
        public float projectileSize;

        private Color _color = Color.Brown;

        public Shoot(Actor owner) : base(owner)
        {

        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //If W is held down charge a projectile
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                if (projectileLifetime > 50)
                {
                    _color = Color.Red;
                    projectileSize = (Transform.GlobalScale.x / 12) * 100;
                    projectileCharge = 0;
                }
                if (projectileLifetime > 125)
                {
                    _color = Color.Red;
                    projectileSize = (Transform.GlobalScale.x / 12) * 100;
                    Raylib.DrawCircleV(Transform.GlobalPosition + offset, projectileSize, _color);
                    projectileCharge = 1;
                }

                if (projectileLifetime > 2000)
                {
                    _color = Color.Orange;
                    projectileSize = (Transform.GlobalScale.x / 11) * 100;
                    Raylib.DrawCircleV(Transform.GlobalPosition + offset, projectileSize, _color);
                    projectileCharge = 2;
                }

                if (projectileLifetime > 5000)
                {
                    _color = Color.White;
                    projectileSize = (Transform.GlobalScale.x / 10) * 100;
                    Raylib.DrawCircleV(Transform.GlobalPosition + offset, projectileSize, _color);
                    projectileCharge = 3;
                }

                if (projectileLifetime > 10000)
                {
                    _color = Color.Blue;
                    projectileSize = (Transform.GlobalScale.x / 9) * 100;
                    Raylib.DrawCircleV(Transform.GlobalPosition + offset, projectileSize, _color);
                    projectileCharge = 4;
                }

                if (projectileLifetime > 25000)
                {
                    _color = Color.Violet;
                    projectileSize = (Transform.GlobalScale.x / 8) * 100;
                    Raylib.DrawCircleV(Transform.GlobalPosition + offset, projectileSize, _color);
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

                Actor.Instantiate(new PlayerProjectileActor(), null, Owner.Transform.GlobalPosition, 0);

                projectileCharge = 0;
                projectileLifetime = 0;
            }

            if (Raylib.IsKeyDown(KeyboardKey.W) && !hasFired)
            {
                hasFired = true;
                
            }

            if (Raylib.IsKeyUp(KeyboardKey.W))
            {
                hasFired = false;
            }
        }
    }
}
