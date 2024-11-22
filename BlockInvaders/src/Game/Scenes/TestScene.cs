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
        public Actor blockQueen = new Actor();

        float timeAlive;
        float waveCountDown;
        float waveTimer;
        public static int waveCount;

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
            MathLibrary.Vector2 blockQueenPos = new MathLibrary.Vector2(Raylib.GetScreenWidth() / 2, 100);
            blockQueen = Actor.Instantiate(new BlockQueenActor(), null, blockQueenPos, 0);
            //Draw blockqueen's collider
            blockQueen.Collider = new CircleCollider(blockQueen, 40);

            timeAlive = 0;
            waveCountDown = 20;
            waveTimer = 20;
            waveCount = 1;
        }


        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            waveCountDown -= (float)deltaTime;
            timeAlive += (float)deltaTime;

            //Draw Waves Completed
            Raylib.DrawText("Wave Count: " + waveCount,
            10,
            10,
            10,
            Color.Red);

            //Draw Time Alive
            Raylib.DrawText("Time Alive: " + timeAlive,
            10,
            20,
            10,
            Color.Red);

            //Draw Time Till Next Wave
            Raylib.DrawText("Next wave in: " + waveCountDown,
            10,
            30,
            10,
            Color.Red);

            //Draw Time Till Next Wave
            Raylib.DrawText("Current Time Per Wave: " + waveTimer,
            10,
            40,
            10,
            Color.Red);


            if (Game.devTools == true)
            {
                Raylib.DrawText("DEV TOOLS ACTIVATED",
                10,
                75,
                10,
                Color.DarkPurple);
            }


            if (waveCountDown <= 0 || Raylib.IsKeyPressed(KeyboardKey.Enter) && Game.devTools == true)
            {
                waveTimer = 20 - waveCount / 5;
                if (waveTimer < 5)
                {
                    waveTimer = 5;
                }
                waveCountDown = waveTimer;
                waveCount++;

                //Draw blockQueen
                MathLibrary.Vector2 blockQueenPos = new MathLibrary.Vector2(Raylib.GetScreenWidth() / 2, 100);
                blockQueen = Actor.Instantiate(new BlockQueenActor(), null, blockQueenPos, 0);
                //Draw blockqueen's collider
                blockQueen.Collider = new CircleCollider(blockQueen, 40);
            }
        }

        public override void End()
        {
            base.End();
            
            
        }
    }
}
