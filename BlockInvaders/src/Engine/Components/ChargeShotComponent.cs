using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class ChargeShotComponent : Component
    {
        private Color _color = Color.Brown;
        public int projectileCharge;
        public int projectileLifetime;
        public float projectileSize;
        MathLibrary.Vector2 _offset = new MathLibrary.Vector2(0, 0);


        public ChargeShotComponent(Actor owner, MathLibrary.Vector2 offset) : base(owner)
        {
            _offset = offset;
        }
        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            if (PlayerActor.firingMode == false)
            {
                Enabled = false;
            }
            if (PlayerActor.firingMode == true)
            {
                Enabled = true;
            }

            if (Enabled == true)
            {
                //If W is held down charge a projectile
                if (Raylib.IsKeyDown(KeyboardKey.W))
                {
                    if (projectileLifetime < 125)
                    {
                        _color = Color.Red;
                        projectileSize = (Owner.Transform.GlobalScale.x / 12) * 100;
                        projectileCharge = 0;
                    }
                    if (projectileLifetime >= 125 && projectileLifetime < 2000)
                    {
                        _color = Color.Red;
                        projectileSize = (Owner.Transform.GlobalScale.x / 12) * 100;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                        projectileCharge = 1;
                    }

                    if (projectileLifetime >= 2000 && projectileLifetime < 5000)
                    {
                        _color = Color.Orange;
                        projectileSize = (Owner.Transform.GlobalScale.x / 11) * 100;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                        projectileCharge = 2;
                    }

                    if (projectileLifetime >= 5000 && projectileLifetime < 10000)
                    {
                        _color = Color.White;
                        projectileSize = (Owner.Transform.GlobalScale.x / 10) * 100;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                        projectileCharge = 3;
                    }

                    if (projectileLifetime >= 10000 && projectileLifetime < 25000)
                    {
                        _color = Color.Blue;
                        projectileSize = (Owner.Transform.GlobalScale.x / 9) * 100;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                        projectileCharge = 4;
                    }

                    if (projectileLifetime >= 25000)
                    {
                        _color = Color.Violet;
                        projectileSize = (Owner.Transform.GlobalScale.x / 8) * 100;
                        Raylib.DrawCircleV(Owner.Transform.GlobalPosition + _offset, projectileSize, _color);
                        projectileCharge = 5;
                    }
                    projectileLifetime++;
                }

                //If w isn't held reset projecileLifetime and projectileCharge
                if (Raylib.IsKeyReleased(KeyboardKey.W))
                {
                    projectileLifetime = 0;
                }
            }
        }
    }
}
