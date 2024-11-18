using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class EnemySpawnComponent : Component
    {
        public bool waveStarted = false;
        public bool waveFinished = false;

        float _size = 0f;
        Color _color = Color.Blank;
        MathLibrary.Vector2 _offset = new MathLibrary.Vector2(0, 0);

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                return;
            }
        }

        public EnemySpawnComponent(Actor owner) : base(owner)
        {

        }

        public override void Update(double deltaTime)
        {
            if (Enabled)
            {
                base.Update(deltaTime);

                if (waveStarted == false)
                {
                    //First Line of defense
                    for (int i = -1; i <= 1; i++)
                    {
                        MathLibrary.Vector2 offset = new MathLibrary.Vector2(50 * i, 100);
                        Actor enemy = Actor.Instantiate(new EnemyActor(), Owner.Transform, offset);
                        enemy.Collider = new CircleCollider(enemy, 25);
                    }

                    for (int i = -1; i <= 1; i++)
                    {
                        MathLibrary.Vector2 offset = new MathLibrary.Vector2(50 * i, 150);
                        Actor enemy = Actor.Instantiate(new EnemyActor(), Owner.Transform, offset);
                        enemy.Collider = new CircleCollider(enemy, 25);
                    }

                    waveStarted = true;
                }

                if (waveFinished == true)
                {
                    waveFinished = false;
                }
            }
        }

        public override void End()
        {
            base.End();
            Enabled = false;
        }
    }
}
