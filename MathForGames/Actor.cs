﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    /// <summary>
    /// This is the base class for all objects that will 
    /// be moved or interacted with in the game.
    /// </summary>
    class Actor
    {
        protected char _icon = ' ';
        private Vector2 _velocity = new Vector2();
        private Vector2 acceleration = new Vector2();
        protected Matrix3 _globalTransform = new Matrix3();
        protected Matrix3 _localTransform = new Matrix3();
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected float _rotationAngle;
        protected float _collisionRadius;
        private float _maxSpeed = 5;

        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get 
            {
                return new Vector2(_globalTransform.m11, _globalTransform.m21);
            }
            set
            {
                Vector2 lookPosition = LocalPosition + value.Normalized;
                LookAt(lookPosition);
            }
        }

        public Vector2 WorldPosition
        {
            get
            {
                return new Vector2(_globalTransform.m13, _globalTransform.m23);
            }
        }

        public Vector2 LocalPosition
        {
            get
            {
                return new Vector2(_localTransform.m13, _localTransform.m23);
            }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }

        public Vector2 Acceleration 
        { 
            get 
            { 
                return acceleration;
            }
            set
            {
                acceleration = value;
            }
        }

        public float MaxSpeed
        {
            get
            {
                return _maxSpeed;
            }
            set
            {
                _maxSpeed = value;
            }
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            _localTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x,y,icon,color)
        {
            _localTransform = new Matrix3();
            _rayColor = rayColor;
        }

        /// <summary>
        /// Adds a child to the actor. The child's transform is concatenated and
        /// updated with the actor's transform.
        /// </summary>
        /// <param name="child">The reference to the new child</param>
        public void AddChild(Actor child)
        {
            Actor[] tempArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
        }

        /// <summary>
        /// Removes the child from the actors list of children. The child will no longer
        /// be updated or concatenated with this actor's transform.
        /// </summary>
        /// <param name="child">The reference to the child to remove</param>
        /// <returns>Returns if the removal was successful. Can be unsuccessful
        /// if the actor isn't in the list of children.</returns>
        public bool RemoveChild(Actor child)
        {
            bool childRemoved = false;

            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length - 1];

            int j = 0;
            for (int i = 0; i <_children.Length; i++)
            {
                if (child != _children[i])
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                {
                    childRemoved = true;
                }
            }

            _children = tempArray;
            child._parent = null;
            return childRemoved;
        }

        /// <summary>
        /// Sets the position of the actor.
        /// </summary>
        /// <param name="position">The new position of the actor.</param>
        public void SetTranslation(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
        }

        /// <summary>
        /// Set the rotaion of the actor.
        /// </summary>
        /// <param name="radians">The angle of the new roation in radians.</param>
        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Adds a roation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in radians to turn.</param>
        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Sets the scale of the actor.
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(new Vector2(x, y));
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be facing</param>
        public void LookAt(Vector2 position)
        {
            //Find the direction that the actor should look in
            Vector2 direction = (position - LocalPosition).Normalized;

            //Use the dotproduct to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProduct(Forward, direction);
            if (Math.Abs(dotProd) > 1)
                return;
            float angle = (float)Math.Acos(dotProd);

            //Find a perpindicular vector to the direction
            Vector2 perp = new Vector2(direction.Y, -direction.X);

            //Find the dot product of the perpindicular vector and the current forward
            float perpDot = Vector2.DotProduct(perp, Forward);

            //If the result isn't 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }

        /// <summary>
        /// Checks to see if this actor overlaps another.
        /// </summary>
        /// <param name="other">The actor that this actor is checking collision against</param>
        /// <returns></returns>
        public bool CheckCollision(Actor other)
        {
            float distance = (other.WorldPosition - WorldPosition).Magnitude;
            return distance <= other._collisionRadius + _collisionRadius;
        }

        /// <summary>
        /// Called whenever a collision occurs between this actor and another.
        /// Use this to define game logic for this actors collision.
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(Actor other)
        {

        }

        /// <summary>
        /// Sets the local transform to be the concatenation of the translation, rotation, and
        /// scale matrices. Sets the global transform to be the concatenation of the
        /// parent transform and local transform, or to the local transform and the world transform
        /// if the actor has no parent.
        /// </summary>
        private void UpdateTransforms()
        {
            _localTransform = _translation * _rotation * _scale;

            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Engine.GetCurrentScene().World * _localTransform;
        }

        /// <summary>
        /// Updates the actors forward vector to be
        /// the last direction it moved in
        /// </summary>
        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;
            Forward = Velocity.Normalized;
        }

        public virtual void Start()
        {
            Started = true;
        }
        
        public virtual void Update(float deltaTime)
        {
            UpdateTransforms();

            //Before the actor is moved, update the direction it's facing
            UpdateFacing();

            Velocity += Acceleration;

            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.Normalized * MaxSpeed;

            //Increase position by the current velocity
            LocalPosition += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window.
            //Scaled to match console movement
            Raylib.DrawLine(
                (int)(WorldPosition.X * 32),
                (int)(WorldPosition.Y * 32),
                (int)((WorldPosition.X + Forward.X) * 32),
                (int)((WorldPosition.Y + Forward.Y) * 32),
                Color.WHITE
            );

            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;

            //Only draws the actor on the console if it is within the bounds of the window
            if (WorldPosition.X >= 0 && WorldPosition.X < Console.WindowWidth 
                && WorldPosition.Y >= 0  && WorldPosition.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)WorldPosition.X, (int)WorldPosition.Y);
                Console.Write(_icon);
            }
            
            //Reset console text color to be default color
            Console.ForegroundColor = Engine.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
