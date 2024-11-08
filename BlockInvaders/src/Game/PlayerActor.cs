﻿using Raylib_cs;
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

        public int projectileCharge;
        public int projectileLifetime;

        //Make W shoot projectiles
        //Make S a parry button
        //Enemy projectiles will be red
        //Parryable enemy projectiles will be yellow

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
            Raylib.DrawCircleV(playerPosition, playerSize, _color);

            


        }

        public override void OnCollision(Actor other)
        {
            //_color = Color.Red;
        }
    }
}
