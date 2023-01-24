using UnityEngine;

namespace StealthPrototype.Characters {

[CreateAssetMenu(fileName = "Movement Configuration", menuName = "Characters/Movement Configuration", order = 0)]
public class MovementConfig : ScriptableObject {

    public float moveSpeed;
    public float rotateSpeed;

}

}