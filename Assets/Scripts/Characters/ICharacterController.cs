using System;
using UnityEngine;

namespace StealthPrototype.Characters
{
    /// <summary>
    /// Provides functionality for character controllers to implement.
    /// </summary>
    public interface ICharacterController
    {
        /// <summary>
        /// Instructs a character to move to a location in world space.
        /// </summary>
        /// <param name="transform">Transform to move towards destination</param>
        /// <param name="destination">Vector2 representing destination of movement</param>
        /// <param name="speed">Speed of the character</param>
        public void MoveTo(Transform transform, Vector2 destination, float speed)
        {
            throw new NotImplementedException();
        }
    }
}