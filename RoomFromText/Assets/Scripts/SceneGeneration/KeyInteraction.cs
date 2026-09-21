using UnityEngine;

public class KeyInteraction : MonoBehaviour
{
    public static bool keyCollected = false;

    private void OnMouseDown()
    {
        keyCollected = true;

        Debug.Log("Key collected!");

        Destroy(gameObject);
    }
}