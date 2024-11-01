using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLibrary;

namespace BlockInvaders
{
    internal class Actor
    {
        private bool _started = false;
        private bool _enabled = true;
        
        public bool Started { get => _started; }
        public bool Enabled
        {
            get => _enabled;
            set
            {
                //if enables would not change, do nothing
                if (_enabled == value) return;

                _enabled = value;
                //If value ifs true, call OnEnable
                if (_enabled)
                {
                    OnEnable();
                }
                //If value is false, call on OnDisable
                else
                {
                    OnDisable();
                }
            }
        } 

        public Collider Collider { get; set; }

        public string Name { get; set; }

        public Transform2D Transform { get; protected set; }

        public Actor(string name = "Actor")
        {
            Name = name;
            Transform = new Transform2D(this);
        }

        public static Actor Instantiate(
            Actor actor, 
            Transform2D parent = null,
            Vector2 position = new Vector2(), 
            float rotation = 0,
            string name = "Actor") 
        {
            //Set actor transform values
            actor.Transform.LocalPosition = position;
            actor.Transform.Rotate(rotation);
            actor.Name = name;
            if (parent != null)
            {
                parent.AddChild(actor.Transform);
            }

            //Add actor to current scene
            Game.CurrentScene.AddActor(actor);

            return actor;
        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(double deltaTime)
        {

        }

        public virtual void End()
        {

        }
        
        public virtual void OnCollision(Actor other)
        {

        }
    }
}
