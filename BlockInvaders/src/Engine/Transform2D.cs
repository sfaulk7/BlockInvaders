using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MathLibrary;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlockInvaders
{
    internal class Transform2D
    {
        private Matrix3 _localMatrix = Matrix3.Identity;
        private Matrix3 _globalMatrix = Matrix3.Identity;

        private Matrix3 _localTranslation = Matrix3.Identity;
        private Matrix3 _localRotation = Matrix3.Identity;
        private Matrix3 _localScale = Matrix3.Identity;

        private Actor _owner;

        private Transform2D _parent;
        private Transform2D[] _children;

        private float _localRotationAngle;

        //Gets and sets LocalRotation
        public Matrix3 LocalRotation
        {
            get { return _localRotation; }
            set
            {
                //Set _localRotation
                _localRotation = value;
                //Set _localRotationAngle
                _localRotationAngle = -(float)Math.Atan2(_localRotation.m01, _localRotation.m00);
                UpdateTransforms();
            }
        }

        //Gets and sets LocalPosition
        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTranslation.m02, _localTranslation.m12); }
            set
            {
                _localTranslation.m02 = value.x;
                _localTranslation.m12 = value.y;
                UpdateTransforms();
            }
        }

        //Gets and sets LocalScale
        public Vector2 LocalScale
        {
            get { return new Vector2(_localMatrix.m00, _localMatrix.m11); }
            set
            {
                _localScale.m00 = value.x;
                _localScale.m11 = value.y;
                UpdateTransforms();
            }
        }

        //Gets GlobalPosition
        public Vector2 GlobalPosition
        {
            get { return new Vector2(_globalMatrix.m02, _globalMatrix.m12); }
        }

        //Gets GlobalScale
        public Vector2 GlobalScale
        {
            get
            {
                Vector2 xAxis = new Vector2(_globalMatrix.m00, _globalMatrix.m10);
                Vector2 yAxis = new Vector2(_globalMatrix.m01, _globalMatrix.m11);

                return new Vector2(xAxis.Magnitude, yAxis.Magnitude);
            }
        }

        public Actor Owner
        {
            get { return _owner; }
        }

        //Gets forward (x)
        public Vector2 Forward
        {
            get { return new Vector2(_globalMatrix.m00, _globalMatrix.m10).Normalized; }
        }

        //Gets right (y)
        public Vector2 Right
        {
            get { return new Vector2(_globalMatrix.m01, _globalMatrix.m11).Normalized; }
        }

        //Gets LocalRotationAngle
        public float LocalRotationAngle
        {
            get { return _localRotationAngle; }
        }

        //Gets GlobalRotationAngle
        public float GlobalRotationAngle
        {
            get { return (float)Math.Atan2(_globalMatrix.m01, _globalMatrix.m00); }
        }

        public Transform2D Parent
        {
            get => _parent;
        }

        public Transform2D[] Children 
        { 
            get => _children; 
        }

        //
        public Transform2D(Actor owner)
        {
            _owner = owner;
            _children = new Transform2D[0];
        }

        //Translates LocalPosition using a Vector2 called direction
        public void Translate(Vector2 direction)
        {
            LocalPosition += direction;
        }

        //Translates LocalPosition using two floats x and y
        public void Translate(float x, float y)
        {
            LocalPosition += new Vector2(x, y);
        }

        //
        public void Rotate(float radians)
        {
            LocalRotation = Matrix3.CreateRotation(_localRotationAngle + radians);
        }

        //
        public void AddChild(Transform2D child)
        {
            //old array is _children

            // Do not add the child if it is this transfor's parents
            if (child == _parent)
            {
                return;
            }

            //arr tempArray set to new array[old.length + 1]
            Transform2D[] temp = new Transform2D[_children.Length + 1];

            //for each child in old array
            for (int i = 0; i < _children.Length; i++)
            {
                //copy child to new array
                temp[i] = _children[i];
            }

            //tempArray[old.length] set to new child
            temp[_children.Length] = child;

            //set child parent to this instance
            child._parent = this;

            //set old array to new array
            _children = temp;

        }

        //
        public bool RemoveChild(Transform2D child)
        {
            bool childRemoved = false;

            //if no children
            if (_children.Length <= 0)
            {
                return false;
            }

            //Make temp array (it is after the no children check to garentee that there is at least one child)
            Transform2D[] temp = new Transform2D[_children.Length - 1];

            //if only one child and that child is the corrent child
            if (_children.Length == 1 && _children[0] == child)
            {
                childRemoved = true;
            }

            //j is basically i for the temp array
            int j = 0;
            for (int i = 0; j < _children.Length - 1; i++)
            {
                //If the current child isn't the one that is being removed; copy
                if (_children[i] != child)
                {
                    temp[j] = _children[i];
                    j++;
                }
                //If the current child is the one being removed
                else
                {
                    childRemoved = true;
                }
            }

            //Copy the temp array into as the new _children array
            if (childRemoved)
            {
                _children = temp;
                child._parent = null;
            }

            return childRemoved;
        }

        //
        public void UpdateTransforms()
        {
            _localMatrix = _localTranslation * _localRotation * _localScale;

            // if parent is not null
            if (_parent != null)
            {
                // Global transform = parent global transform * local transform
                _globalMatrix = _parent._globalMatrix * _localMatrix;
            }
            //else
            else
            {
                //global transform - local transform
                _globalMatrix = _localMatrix;
            }

            //Update Children
            foreach (Transform2D child in _children)
            {
                child.UpdateTransforms();
            }
        }
    }
}
