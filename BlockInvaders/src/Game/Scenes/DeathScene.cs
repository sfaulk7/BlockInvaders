using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace BlockInvaders
{
    internal class DeathScene : Scene
    {
        public override void Start()
        {
            base.Start();

        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            Raylib.DrawText("GAME OVER \n \n \n \n \n \n \n \nPRESS ENTER TO TRY AGAIN \n \n \n \n \n \n \n \nPRESS ESC TO EXIT",
            Raylib.GetScreenWidth() / 6,
            Raylib.GetScreenHeight() / 4,
            50,
            Color.Red);

            //Restarts game if enter is pressed
            if (Raylib.IsKeyDown(KeyboardKey.Enter))
            {
                Game.CurrentScene = Game.GetScene(2);
            }
        }

        public override void End()
        {
            base.End();
        }
    }
}
