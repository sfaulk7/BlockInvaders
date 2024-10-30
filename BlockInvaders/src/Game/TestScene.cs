using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class TestScene : Scene
    {
        public override void Start()
        {
            base.Start();

            //Add sick af actor
            Actor actor = new TestActor();
            actor.Transform.LocalPosition = new MathLibrary.Vector2(200, 200);
            AddActor(actor);

        }
    }
}
