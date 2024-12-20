﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MathLibrary;

namespace BlockInvaders
{
    internal class Actor
    {
        private bool _started = false;
        private bool _enabled = true;

        private Component[] _components;
        private Component[] _componentsToRemove;

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
            _components = new Component[0];
            _componentsToRemove = new Component[0];
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

        public static void Destroy(Actor actor, float delay = 0)
        {
            //Remove all children
            foreach (Transform2D child in actor.Transform.Children)
            {
                actor.Transform.RemoveChild(child);
            }

            //Unchild from parent
            if (actor.Transform.Parent != null)
            {
                actor.Transform.Parent.RemoveChild(actor.Transform);
            }

            Game.CurrentScene.RemoveActor(actor);

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
            //Update Component
            foreach (Component component in _components)
            {
                if (!component.Started)
                {
                    component.Start();
                }

                component.Update(deltaTime);
            }

            //Remove componets that should be removed
            RemoveComponentsToBeRemoved();

        }

        public virtual void End()
        {
            foreach (Component component in _components)
            {
                component.End();
            }
        }
        
        public virtual void OnCollision(Actor other)
        {

        }

        //Add Component
        public T AddComponent<T>(T component) where T : Component
        {
            //Create temorary array one bigger than _componets
            Component[] temp = new Component[_components.Length + 1];

            //Deep copy _components into temp
            for (int i = 0; i < _components.Length; i++)
            {
                temp[i] = _components[i];

            }

            //Set the last index in temp to the component we wish to have
            temp[temp.Length - 1] = component;

            //Store temp in _components
            _components = temp;

            return component;
        }

        public T AddComponent<T>() where T : Component, new()
        {
            T component = (T)new Component(this);
            component.Owner = this;
            return AddComponent(component);
        }

        //Remove Component
        public bool RemoveComponent<T>(T component) where T : Component
        {
            //Edge case for empty component array
            if (_components.Length <= 0)
            {
                return false;
            }

            if (_components.Length <= 1 && _components[0] == component)
            {
                //Add component to _componentsToRemove
                AddComponentToRemove(component);

            }

            //Loop through _components
            foreach ( Component comp in _components)
            {
                if (comp == component)
                {
                    // Add component to _componentsToRemove
                    AddComponentToRemove(comp);

                    //Stop the loop when a component is removed
                    return true;
                }
            }

            return false;

            ////Edge case for only one component array
            //if (_components.Length <= 1 && _components[0] == component)
            //{
            //    _components = new Component[0];
            //    return true;
            //}

                ////Create temorary array one smaller than _componets
                //Component[] temp = new Component[_components.Length - 1];
                //bool componentRemoved = false;

                ////Deep copy _components into temp minus one component
                //int j = 0;
                //for (int i = 0; j < _components.Length - 1; i++)
                //{
                //    //If the current component isn't the one that is being removed; copy
                //    if (_components[i] != component)
                //    {
                //        temp[j] = _components[i];
                //        j++;
                //    }
                //    //If the current component is the one being removed
                //    else
                //    {
                //        componentRemoved = true;
                //    }
                //}

                ////Copy the temp array into as the new _components array
                //if (componentRemoved)
                //{
                //    _components = temp;
                //}

                //return componentRemoved;
        }

        public bool RemoveComponent<T>() where T: Component
        {
            T component = GetComponent<T>();
            if (component != null)
            {
                return RemoveComponent(component);
            }
            return false;
        }

        //Get Component
        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            return null;
        }

        //Get Components
        public T[] GetComponents<T>() where T : Component
        {
            //Create a temp array of the same size as _componets
            T[] temp = new T[_components.Length];


            //Copy all elemets that are type T into temp
            int count = 0;
            for (int i = 0; i < _components.Length; i++)
            {
                if (_components[i] is T)
                {
                    temp[count] = (T)_components[i];
                    count++;
                }

            }

            //Trim the array
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = temp[i];
            }

            return result;

        }

        private void AddComponentToRemove(Component comp)
        {
            //Ensure component is not already being removed
            foreach (Component component in _componentsToRemove)
            {
                if (component == comp)
                {
                    return;
                }
            }

            //Create temorary array one bigger than _componetsToRemove
            Component[] temp = new Component[_componentsToRemove.Length + 1];

            //Deep copy _componentsToRemove into temp
            for (int i = 0; i < _componentsToRemove.Length; i++)
            {
                temp[i] = _componentsToRemove[i];

            }

            //Set the last index in temp to the component we wish to have
            temp[temp.Length - 1] = comp;

            //Store temp in _componentsToRemove
            _componentsToRemove = temp;
        }

        private void RemoveComponentsToBeRemoved()
        {
            //If there are no components to remove, return
            if (_componentsToRemove.Length <= 0)
            {
                return;
            }

            //Create temp array for _components
            Component[] tempComponents = new Component[_components.Length];

            //Deep copy array
            for (int i = 0; i < _components.Length; i++)
            {
                tempComponents[i] = _components[i];
            }

            //Deep copy the array, removeing the elemets in _componentsToRemove
            int j = 0;
            for (int i = 0; i < _components.Length; i++)
            {
                //Loop through components to check if any of them is equal to this one
                bool removed = false;
                foreach (Component component in _componentsToRemove)
                {
                    if (_components[i] == component)
                    {
                        removed = true;
                        component.End();
                        break;
                    }
                }

                //If we did not find one to remove, copy the item and increment the temp array
                if (!removed)
                {
                    tempComponents[j] = _components[i];
                    j++;
                }
            }

            //Trim the array
            Component[] result = new Component[_components.Length - _componentsToRemove.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = tempComponents[i];
            }

            //Set _components
            _components = result;

        }
    }
}
