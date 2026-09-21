using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractionManager manager;
    public string objectId;

    private void OnMouseDown()
    {
        if (manager != null)
        {
            manager.ExecuteInteraction(objectId);
        }
    }
}