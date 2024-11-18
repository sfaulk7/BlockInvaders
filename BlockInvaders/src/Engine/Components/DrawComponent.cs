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
        MathLibrary.Vector2 _offset = new MathLibrary.Vector2 (0, 0);

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                return;
            }
        }

        public DrawComponent(Actor owner, float size, Color color, MathLibrary.Vector2 offset) : base(owner)
        {
            _size = size;
            _color = color;
            _offset = offset;
        }

        public override void Update(double deltaTime)
        {
            if (Enabled)
            {
                base.Update(deltaTime);

                Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, _size, _color);
            }
        }

        public override void End()
        {
            base.End();
            Enabled = false;
        }
    }
}
