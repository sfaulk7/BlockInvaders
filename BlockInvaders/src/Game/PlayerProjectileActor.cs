﻿using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class PlayerProjectileActor : Actor
    {
        public float Speed { get; set; } = 1000;

        private Color _color = Color.Brown;

        public int projectileCharge;
        public int projectileLifetime;
        public float projectileSize;

        public override void Start()
        {
            base.Start();
            projectileSize = 10;
            AddComponent<DrawComponent>(new DrawComponent(this, projectileSize, _color, new MathLibrary.Vector2(0, 0)));
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //Movement
            MathLibrary.Vector2 movement = new MathLibrary.Vector2();
            movement.y -= 1;
            MathLibrary.Vector2 deltaMovement = movement.Normalized * Speed * (float)deltaTime;
            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }

            if (Transform.LocalPosition.y <= 0)
            {
                Game.CurrentScene.RemoveActor(this);
            }
        }

        public override void OnCollision(Actor other)
        {
            //Only do collision behavior if the colliding Actor isn't playerGun or player
            if (other.ToString() != "BlockInvaders.PlayerGun" && (other.ToString() != "BlockInvaders.PlayerActor"))
            {
                Game.CurrentScene.RemoveActor(this);
            }
        }
    }
}
