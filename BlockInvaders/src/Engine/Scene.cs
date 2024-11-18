using System;
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
        private List<Actor> _actorsToBeRemoved;

        public void AddActor(Actor actor)
        {
            if (!_actors.Contains(actor))
            {
                _actors.Add(actor);
            }
        }

        public bool RemoveActor(Actor actor)
        {
            AddToRemoveActor(actor);
            return true;
        }

        public virtual void Start()
        {
            _actors = new List<Actor>();
            _actorsToBeRemoved = new List<Actor>();
        }

        public virtual void Update(double deltaTime)
        {
            //Update Actors
            for (int i = 0; i < _actors.Count; i++)
            {
                if (!_actors[i].Started)
                {
                    _actors[i].Start();
                }
                _actors[i].Update(deltaTime);

                if (_actors[i].Collider != null)
                {
                    _actors[i].Collider.Draw();
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
            ActorToBeRemoved();
        }

        public virtual void End()
        {
            //Update Actors
            foreach (Actor actor in _actors)
            {
                actor.End();
            }
        }

        private bool AddToRemoveActor(Actor actor)
        {
            _actorsToBeRemoved.Add(actor);
            return true;
        }

        private void ActorToBeRemoved()
        {
            for (int i = 0; i < _actors.Count; i++)
            {
                if (_actorsToBeRemoved.Contains(_actors[i]))
                {
                    _actors.Remove(_actors[i]);
                }
            }
            _actorsToBeRemoved.Clear();
            _actors.TrimExcess();
            _actorsToBeRemoved.TrimExcess();
        }

    }
}
