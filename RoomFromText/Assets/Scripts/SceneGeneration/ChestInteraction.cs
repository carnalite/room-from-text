using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public bool isLocked = true;

    private void OnMouseDown()
    {
        if (isLocked)
        {
            isLocked = false;
            Debug.Log(gameObject.name + " is now UNLOCKED.");
        }
    }
}