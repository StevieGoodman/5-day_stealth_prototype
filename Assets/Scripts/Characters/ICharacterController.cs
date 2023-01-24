namespace StealthPrototype.Characters
{
    /// <summary>
    /// Provides functionality for character controllers to implement.
    /// </summary>
    public interface ICharacterController
    {
        /// <summary>
        /// Increments the transform's position towards a direction by 1 frame of distance.
        /// </summary>
        public void IncrementPosition();
        
        /// <summary>
        /// Increments the transform's rotation towards a direction by 1 frame of rotation.
        /// </summary>
        public void IncrementRotation();
    }
}