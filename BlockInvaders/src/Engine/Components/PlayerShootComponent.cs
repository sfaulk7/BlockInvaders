using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class PlayerShootComponent : Component
    {
        public static Color projectileColor = Color.Brown;
        public int projectileCharge;
        public int projectileLifetime;
        public static int projectileDamage;
        public static float projectileSize;
        MathLibrary.Vector2 _offset = new MathLibrary.Vector2(0, -15);

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
            get => projectileColor;
            set
            {
                projectileColor = value;
                return;
            }
        }

        public PlayerShootComponent(Actor owner) : base(owner)
        {

        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //Do Normal Shot
            if (PlayerActor.firingMode == false)
            {
                projectileDamage = 1;
                projectileColor = Color.Brown;
                projectileSize = 10;



                if (Raylib.IsKeyPressed(KeyboardKey.W) || Raylib.IsKeyPressed(KeyboardKey.Space))
                {
                    Actor playerProjectile = Actor.Instantiate(new PlayerProjectileActor(), null, Owner.Transform.GlobalPosition, 0);
                    playerProjectile.Collider = new CircleCollider(playerProjectile, projectileSize);
                    projectileLifetime = 0;
                }

                if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Space))
                {
                    projectileLifetime++;
                    if (projectileLifetime > 1500)
                    {
                        Actor playerProjectile = Actor.Instantiate(new PlayerProjectileActor(), null, Owner.Transform.GlobalPosition, 0);
                        playerProjectile.Collider = new CircleCollider(playerProjectile, projectileSize);
                        projectileLifetime = 0;
                    }
                }
            }

            //Do Charged Shot
            if (PlayerActor.firingMode == true)
            {
                //If W is held down charge a projectile
                if (Raylib.IsKeyDown(KeyboardKey.W))
                {
                    if (projectileLifetime < 500)
                    {
                        projectileCharge = 0;
                    }
                    if (projectileLifetime >= 500 && projectileLifetime < 2000)
                    {
                        projectileColor = Color.Red;
                        projectileSize = (Owner.Transform.GlobalScale.x / 12) * 100;
                        projectileCharge = 1;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, projectileColor);
                    }

                    if (projectileLifetime >= 2000 && projectileLifetime < 5000)
                    {
                        projectileColor = Color.Orange;
                        projectileSize = (Owner.Transform.GlobalScale.x / 11) * 100;
                        projectileCharge = 2;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, projectileColor);
                    }

                    if (projectileLifetime >= 5000 && projectileLifetime < 10000)
                    {
                        projectileColor = Color.White;
                        projectileSize = (Owner.Transform.GlobalScale.x / 10) * 100;
                        projectileCharge = 3;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, projectileColor);
                    }

                    if (projectileLifetime >= 10000 && projectileLifetime < 25000)
                    {
                        projectileColor = Color.Blue;
                        projectileSize = (Owner.Transform.GlobalScale.x / 9) * 100;
                        projectileCharge = 4;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, projectileColor);
                    }

                    if (projectileLifetime >= 25000)
                    {
                        projectileColor = Color.Violet;
                        projectileSize = (Owner.Transform.GlobalScale.x / 8) * 100;
                        projectileCharge = 5;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, projectileColor);
                    }

                    projectileLifetime++;
                }


                //Shoot a projectile with different effects depenting on the charge
                if (!(Raylib.IsKeyDown(KeyboardKey.W)) && projectileLifetime > 0 || Raylib.IsKeyDown(KeyboardKey.Space) && projectileLifetime > 0)
                {
                    //2 damage small projectile
                    if (projectileCharge == 1)
                    {
                        projectileDamage = 2;
                        projectileColor = Color.Red;
                        projectileSize = (Owner.Transform.GlobalScale.x / 12) * 100;
                    }

                    //4 damage small projectile
                    if (projectileCharge == 2)
                    {
                        projectileDamage = 4;
                        projectileColor = Color.Orange;
                        projectileSize = (Owner.Transform.GlobalScale.x / 11) * 100;
                    }

                    //6 damage small projectile
                    if (projectileCharge == 3)
                    {
                        projectileDamage = 6;
                        projectileColor = Color.White;
                        projectileSize = (Owner.Transform.GlobalScale.x / 10) * 100;
                    }

                    //10 damage small projectile
                    if (projectileCharge == 4)
                    {
                        projectileDamage = 10;
                        projectileColor = Color.Blue;
                        projectileSize = (Owner.Transform.GlobalScale.x / 9) * 100;
                    }

                    //20 damage big projectile
                    if (projectileCharge == 5)
                    {
                        projectileDamage = 20;
                        projectileColor = Color.Violet;
                        projectileSize = (Owner.Transform.GlobalScale.x / 8) * 100;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, projectileColor);
                    }

                    //Make a Projectile if charge is greater than 0
                    if (projectileCharge > 0)
                    {
                        Actor playerProjectile = Actor.Instantiate(new PlayerProjectileActor(), null, Owner.Transform.GlobalPosition, 0);
                        playerProjectile.Collider = new CircleCollider(playerProjectile, projectileSize);
                    }

                    //Reset projecileLifetime and projectileCharge
                    projectileCharge = 0;
                    projectileLifetime = 0;
                }
            }
        }
    }
}