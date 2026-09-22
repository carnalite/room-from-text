using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public bool isLocked = true;

    private void OnMouseDown()
    {
        if (isLocked)
        {
            if (KeyInteraction.keyCollected)
            {
                isLocked = false;

                Debug.Log(
                    gameObject.name +
                    " is now unlocked!"
                );
            }
            else
            {
                Debug.Log(
                    gameObject.name +
                    " is locked. Find the key first."
                );
            }
        }
        else
        {
            Debug.Log(
                gameObject.name +
                " is already unlocked."
            );
        }
    }
}