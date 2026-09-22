using System.Collections.Generic;
using UnityEngine;

public static class SceneValidator
{
    public static bool Validate(SceneData sceneData)
    {
        if (sceneData == null)
        {
            Debug.LogError("Scene data is null.");
            return false;
        }

        if (sceneData.objects == null)
        {
            Debug.LogError("Objects list is missing.");
            return false;
        }

        HashSet<string> objectIds = new HashSet<string>();

        // Check objects
        foreach (SceneObject obj in sceneData.objects)
        {
            if (string.IsNullOrEmpty(obj.id))
            {
                Debug.LogError("An object has no ID.");
                return false;
            }

            if (!objectIds.Add(obj.id))
            {
                Debug.LogError(
                    "Duplicate object ID: " + obj.id
                );

                return false;
            }
        }

        // Check relationships
        if (sceneData.relationships != null)
        {
            foreach (SceneRelationship relationship in sceneData.relationships)
            {
                if (!objectIds.Contains(relationship.subject))
                {
                    Debug.LogError(
                        "Relationship references missing subject: " +
                        relationship.subject
                    );

                    return false;
                }

                if (!objectIds.Contains(relationship.target))
                {
                    Debug.LogError(
                        "Relationship references missing target: " +
                        relationship.target
                    );

                    return false;
                }
            }
        }

        // Check interactions
        if (sceneData.interactions != null)
        {
            foreach (InteractionRule interaction in sceneData.interactions)
            {
                if (!objectIds.Contains(interaction.subject))
                {
                    Debug.LogError(
                        "Interaction references missing subject: " +
                        interaction.subject
                    );

                    return false;
                }

                if (!string.IsNullOrEmpty(interaction.target) &&
                    !objectIds.Contains(interaction.target))
                {
                    Debug.LogError(
                        "Interaction references missing target: " +
                        interaction.target
                    );

                    return false;
                }
            }
        }

        Debug.Log("Scene validation passed.");

        return true;
    }
}