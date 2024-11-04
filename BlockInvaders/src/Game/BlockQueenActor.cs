using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class BlockQueenActor : Actor
    {
        public float Speed { get; set; } = 100;

        private Color _color = Color.Red;


        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //Movement
            MathLibrary.Vector2 movement = new MathLibrary.Vector2();
            movement.y += 1;

            MathLibrary.Vector2 deltaMovement = movement.Normalized * Speed * (float)deltaTime;

            if (deltaMovement.Magnitude != 0)
            {
                Transform.LocalPosition += (deltaMovement);
            }

            Raylib.DrawCircleV(Transform.GlobalPosition, (Transform.GlobalScale.x / 2) * 100, _color);

        }

        public override void OnCollision(Actor other)
        {
            _color = Color.Blue;
        }
    }
}
