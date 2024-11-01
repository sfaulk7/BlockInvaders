using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace BlockInvaders
{
    internal class TestScene : Scene
    {
        Actor _thatOne;

        public override void Start()
        {
            base.Start();

            //Add sick af actor
            Actor actor = new TestActor();
            actor.Transform.LocalPosition = new MathLibrary.Vector2(200, 200);
            AddActor(actor);
            actor.Collider = new CircleCollider(actor, 60);

            _thatOne = Actor.Instantiate(new Actor("That One"), null, new MathLibrary.Vector2(100, 100), 0);
            _thatOne.Collider = new CircleCollider(_thatOne, 50);


        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            Raylib.DrawCircleV(_thatOne.Transform.GlobalPosition, 50, Color.Green);
        }
    }
}
