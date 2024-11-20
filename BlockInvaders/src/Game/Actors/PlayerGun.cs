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

        private Color _color = Color.Black;

        //This offset moves playerGun from the center of playerActor to the top
        MathLibrary.Vector2 offset = new MathLibrary.Vector2(0, -15);

        public override void Start()
        {
            base.Start();
            AddComponent<DrawComponent>(new DrawComponent(this, (Transform.GlobalScale.x / 8) * 100, _color, offset));
            AddComponent<PlayerShootComponent>(new PlayerShootComponent(this));
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

        }



        //PlayerGun will not have a Collision behavior
        //I will be leaving this here though in case i want to change that
        public override void OnCollision(Actor other)
        {
            //Do nothing
        }
    }
}
