﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockInvaders
{
    internal class Component
    {
        public Actor _owner;
        private bool _enabled;
        private bool _started;

        public Actor Owner { get => _owner; set => _owner = value; }
        public bool Enabled
        {
            get => _enabled;
            set
            {
                //if enables would not change, do nothing
                if (_enabled == value) return;

                _enabled = value;
                //If value is true, call OnEnable
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

        public bool Started { get => _started; }

        public Component(Actor owner = null)
        {
            
            _owner = owner;
            _enabled = true;
            _started = false;
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
            if (Owner == null)
            {
                throw new NullReferenceException();
            }
        }

        public virtual void End()
        {

        }
    }
}
