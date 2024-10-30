﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{

    internal class Scene
    {
        private List<Actor> _actors;

        public void AddActor(Actor actor)
        {
            if (!_actors.Contains(actor))
            {
                _actors.Add(actor);
            }
        }

        public bool RemoveActor(Actor actor)
        {
            return _actors.Remove(actor);
        }

        public virtual void Start()
        {
            _actors = new List<Actor>();
        }

        public virtual void Update(double deltaTime)
        {
            //Update Actors
            foreach (Actor actor in _actors)
            {
                if (!actor.Started)
                {
                    actor.Start();
                }
                actor.Update(deltaTime);
            }
        }

        public virtual void End()
        {
            //Update Actors
            foreach (Actor actor in _actors)
            {
                actor.End();
            }
        }
    }
}
