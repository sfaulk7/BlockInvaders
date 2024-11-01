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

                if(actor.Collider != null)
                {
                    actor.Collider.Draw();
                }
            }

            //Check for collision
            for (int row = 0; row < _actors.Count; row++)
            {
                for (int column = row; column < _actors.Count; column++)
                {
                    //Don't check collider against self
                    if (row == column)
                    {
                        continue;
                    }

                    //if both colliders are valid
                    if (_actors[row].Collider != null && _actors[column].Collider != null)
                    {
                        //Check collision
                        if (_actors[row].Collider.CheckCollision(_actors[column]))
                        {
                            _actors[row].OnCollision(_actors[column]);
                            _actors[column].OnCollision(_actors[row]);
                        }
                    }
                }
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
