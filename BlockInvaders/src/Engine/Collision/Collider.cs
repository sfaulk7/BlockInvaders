using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{

    internal class Collider
    {
        public Actor Owner { get; protected set; }

        public Collider(Actor owner)
        {
            Owner = owner;
        }

        public bool CheckCollision(Actor other)
        {
            if (other.Collider != null && other.Collider is CircleCollider)
            {
                return CheckCollisionCircle((CircleCollider)other.Collider);
            }
            return false;
        }

        public virtual bool CheckCollisionCircle(CircleCollider collider)
        {
            return false;
        }



        public virtual void Draw() { }
    }
}
