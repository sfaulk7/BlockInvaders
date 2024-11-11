﻿using Raylib_cs;
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
        private Color _color = Color.Brown;
        public int projectileCharge;
        public int projectileLifetime;
        public float projectileSize;
        MathLibrary.Vector2 _offset = new MathLibrary.Vector2(0, -15);


        public PlayerShootComponent(Actor owner, int projectileCharge) : base(owner)
        {

        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //If W is held increment lifetime
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                projectileLifetime++;
            }

            //Shoot a projectile with different effects depenting on the charge
            if (!(Raylib.IsKeyDown(KeyboardKey.W)) && projectileLifetime > 0)
            {
                //1 damage small projectile
                if (projectileCharge == 0)
                {
                    _color = Color.Red;
                    projectileSize = (Owner.Transform.GlobalScale.x / 12) * 100;
                }

                //2 damage small projectile
                if (projectileCharge == 1)
                {
                    _color = Color.Red;
                    projectileSize = (Owner.Transform.GlobalScale.x / 12) * 100;
                    Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                }

                //4 damage small projectile
                if (projectileCharge == 2)
                {
                    _color = Color.Orange;
                    projectileSize = (Owner.Transform.GlobalScale.x / 11) * 100;
                    Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                }

                //6 damage small projectile
                if (projectileCharge == 3)
                {
                    _color = Color.White;
                    projectileSize = (Owner.Transform.GlobalScale.x / 10) * 100;
                    Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                }

                //10 damage small projectile
                if (projectileCharge == 4)
                {
                    _color = Color.Blue;
                    projectileSize = (Owner.Transform.GlobalScale.x / 9) * 100;
                    Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                }

                //20 damage big projectile
                if (projectileCharge == 5)
                {
                    _color = Color.Violet;
                    projectileSize = (Owner.Transform.GlobalScale.x / 8) * 100;
                    Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                }

                Actor playerProjectile = Actor.Instantiate(new PlayerProjectileActor(), null, Owner.Transform.GlobalPosition, 0);
                playerProjectile.Collider = new CircleCollider(playerProjectile, projectileSize * 4);


                projectileCharge = 0;
                projectileLifetime = 0;
            }

            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
            }

            if (Raylib.IsKeyUp(KeyboardKey.W))
            {

            }
        }
    }
}
