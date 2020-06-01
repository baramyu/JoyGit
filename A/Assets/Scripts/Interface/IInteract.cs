using UnityEngine;

public interface IInteract
{
    void Interact();
    bool IsInteractAble();
    string GetInteractText();
    Vector3 GetPosition();
}