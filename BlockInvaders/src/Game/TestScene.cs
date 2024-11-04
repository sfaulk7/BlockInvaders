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
        Actor _blockQueen;

        public override void Start()
        {
            base.Start();

            //Add Player actor
            Actor player = new PlayerActor();
            player.Transform.LocalPosition = new MathLibrary.Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() * .95f);
            AddActor(player);
            player.Collider = new CircleCollider(player, 30);

            Actor blockQueen = new BlockQueenActor();
            blockQueen.Transform.LocalPosition = new MathLibrary.Vector2(100, 100);
            AddActor(blockQueen);
            blockQueen.Collider = new CircleCollider(blockQueen, 60);
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
