using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class HealthComponent : Component
    {
       private int _health = 0;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                return;
            }
        }

        public HealthComponent(Actor owner, int health) : base(owner)
        {
            _health = health;
        }

        public override void Update(double deltaTime)
        {
            if (Enabled)
            {
                base.Update(deltaTime);


                Raylib.DrawText(_health.ToString(),
                    (int)Owner.Transform.GlobalPosition.x,
                    (int)Owner.Transform.GlobalPosition.y,
                    (int)Owner.Transform.LocalScale.x * 25,
                    Color.Green);

                if (Health <= 0)
                {
                    End();
                }

                if (_health <= 0)
                {
                    End();
                }
            }
        }

        public override void End()
        {
            base.End();
            Enabled = false;

            Actor.Destroy(Owner);
        }
    }
}
