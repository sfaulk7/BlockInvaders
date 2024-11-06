using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class Game
    {
        private static List<Scene> _scenes;
        private static Scene _currentScene;

        public static Color backgroundColor = Color.Black;

        public static Scene CurrentScene 
        { 
            get => _currentScene;
            set
            {
                if (_currentScene != null)
                {
                    _currentScene.End();
                }
                _currentScene = value;
                _currentScene.Start();
            }
        }

        public Game()
        {
            _scenes = new List<Scene>();
        }

        public static void AddScene(Scene scene)
        {
            if (!_scenes.Contains(scene))
            {
                _scenes.Add(scene);
            }

            if (_currentScene == null)
            {
                CurrentScene = scene;
            }
        }

        public static bool RemoveScene(Scene scene)
        {
            bool removed = _scenes.Remove(scene);

            if (_currentScene == scene)
            {
                CurrentScene = GetScene(0);
            }

            return removed;
        }

        public static Scene GetScene(int index)
        {
            //If scene count is <= 0 or if scene count is <= index or index is < 0
            if (_scenes.Count <= 0 || _scenes.Count <= index || index < 0)
            {
                return null;
            }

            return _scenes[index];
        }

        public void Run()
        {
            Console.WriteLine("HI");

            Raylib.InitWindow(1600, 960, "BlockInvaders");

            //Timing
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long currentTime = 0;
            long lastTime = 0;
            double deltaTime = 1;

            Scene testScene = new TestScene();
            AddScene(testScene);

            //Scene
            //Scene testScene = new Scene();
            testScene.Start();
            testScene.AddActor(new Actor());

            while (!Raylib.WindowShouldClose())
            {
                currentTime = stopwatch.ElapsedMilliseconds;
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(backgroundColor);

                testScene.Update(deltaTime);

                Raylib.EndDrawing();
                deltaTime = (currentTime - lastTime) / 1000.0;
                lastTime = currentTime;
            }

            CurrentScene.End();

            Raylib.CloseWindow();
        }
    }
}
