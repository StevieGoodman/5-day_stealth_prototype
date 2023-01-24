namespace StealthPrototype.Characters.Movement {

/// <summary>
/// Provides functionality for character controllers to implement.
/// </summary>
public interface IMovementController {

    /// <summary>
    /// Increments the transform's position towards a direction by 1 frame of distance.
    /// </summary>
    public void increment_position();

    /// <summary>
    /// Increments the transform's rotation towards a direction by 1 frame of rotation.
    /// </summary>
    public void increment_rotation();

}

}