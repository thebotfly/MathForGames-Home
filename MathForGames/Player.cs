using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    /// <summary>
    /// An actor that moves based on input given by the user
    /// </summary>
    class Player : Actor
    {
        private float _speed = 1;
        private Sprite _sprite;
        private bool _canMove = true;

        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
        }

        /// <summary>
        /// Disable all player controls including movement and shooting.  
        /// </summary>
        public void DisableControls()
        {
            _canMove = false;
        }


        public override void Start()
        {
            ///
            /// The function DisableControls() is responsible for stopping all player input.
            /// The player shouldn't be able to control the ship after the game is won.
            /// This will make it easier for the player to focus on any menu options that
            /// may appear on the win screen. 
            /// This function needs to be called when the player wins the game, 
            /// so it is added to the GameManager's onWin delegate.
            ///
            GameManager.onWin += DisableControls;
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            //If the player can't move, don't ask for input.
            if (!_canMove)
                return;

            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_S));

            //Set the actors current velocity to be the a vector with the direction found scaled by the speed
            Acceleration = new Vector2(xDirection, yDirection);
            Velocity = Velocity.Normalized * Speed;
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            _sprite.Draw(_globalTransform);
            base.Draw();
        }
    }
}
