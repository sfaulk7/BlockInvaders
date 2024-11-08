using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class PlayerGun : Actor
    {
        public float Speed { get; set; } = 1000;

        private Color _color = Color.Blue;

        public int parryLifetime;

        


        //Make W shoot projectiles

        public override void Start()
        {
            base.Start();
            AddComponent<Shoot>(new Shoot(this));
        }

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

            MathLibrary.Vector2 offset = new MathLibrary.Vector2(0, -15);

            Raylib.DrawCircleV(Transform.GlobalPosition + offset, (Transform.GlobalScale.x / 8) * 100, Color.Black);

            
            


        }

        public override void OnCollision(Actor other)
        {
            //_color = Color.Red;
        }
    }
}
