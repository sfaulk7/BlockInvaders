using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class DrawComponent : Component
    {
        float _size = 0f;
        Color _color = Color.Blank;

        public DrawComponent(Actor owner, float size, Color color) : base(owner)
        {
            _size = size;
            _color = color;
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            Raylib.DrawCircleV(Owner.Transform.GlobalPosition, _size, _color);
        }
    }
}
