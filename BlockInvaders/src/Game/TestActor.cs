﻿using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{ 
    internal class TestActor : Actor
    {
        public float Speed { get; set; } = 100;


        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //Movement
            MathLibrary.Vector2 movementInput = new MathLibrary.Vector2();
            movementInput.y -= Raylib.IsKeyDown(KeyboardKey.W);
            movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
            movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);
            movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);
            MathLibrary.Vector2 deltaMovement = movementInput.Normalized * Speed * (float)deltaTime;

            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }
            

            Raylib.DrawRectangleV(Transform.GlobalPosition, Transform.GlobalScale * 100, Color.Blue);
        }
    }
}