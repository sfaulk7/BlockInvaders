using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace BlockInvaders
{
    internal class TestScene : Scene
    {

        public override void Start()
        {
            base.Start();

            //Add Player actor
            MathLibrary.Vector2 playerStartPosition = new MathLibrary.Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() * .95f);
            Actor player = Actor.Instantiate(new PlayerActor(), null, playerStartPosition, 0);
            //Draw player's collider
            player.Collider = new CircleCollider(player, 30);

            //Draw Player gun
            Actor playerGun = Actor.Instantiate(new PlayerGun(), player.Transform, default, 0);

            //Draw blockQueen
            MathLibrary.Vector2 blockQueenPos = new MathLibrary.Vector2(100, 100);
            Actor blockQueen = Actor.Instantiate(new BlockQueenActor(), null, blockQueenPos, 0);
            //Draw blockqueen's collider
            blockQueen.Collider = new CircleCollider(blockQueen, 60);

        }


        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
