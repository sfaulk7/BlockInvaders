using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace BlockInvaders
{
    internal class TitleScene : Scene
    {
        public override void Start()
        {
            base.Start();

        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            Raylib.DrawText("B L O C K I N V A D E R S",
            Raylib.GetScreenWidth() / 6,
            Raylib.GetScreenHeight() / 4,
            75,
            Color.Red);

            Raylib.DrawText(" \n \n \n \n \n \n \n \n \n \n \n \n \n \n \n \nPRESS ENTER TO START \n \n \n \nPRESS ESC TO EXIT",
            Raylib.GetScreenWidth() / 6,
            Raylib.GetScreenHeight() / 4,
            50,
            Color.Red);

            if (Game.devTools == true)
            {
                Raylib.DrawText("DEV TOOLS ACTIVATED \n \nIn game press ENTER to skip the current wave and DELETE to damage player",
                10,
                10,
                25,
                Color.DarkPurple);
            }

            //Activates devTools
            if (Raylib.IsKeyDown(KeyboardKey.Seven) && Game.devTools == false)
            {
                Game.devTools = true;
            }

            //Starts game if enter is pressed
            if (Raylib.IsKeyDown(KeyboardKey.Enter))
            {
                Game.CurrentScene = Game.GetScene(0);
            }
        }

        public override void End()
        {
            base.End();
        }
    }
}
