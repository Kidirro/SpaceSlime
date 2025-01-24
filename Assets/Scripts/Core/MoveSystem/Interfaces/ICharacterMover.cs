using UnityEngine;

public interface ICharacterMover
{
    void Move(Vector2 move);
    void Sprint(bool isSprint); 
}
