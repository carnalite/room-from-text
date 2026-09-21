using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Dictionary<string, InteractionRule> interactions =
        new Dictionary<string, InteractionRule>();

    public void SetupInteractions(List<InteractionRule> sceneInteractions)
    {
        interactions.Clear();

        if (sceneInteractions == null)
        {
            Debug.Log("No interactions defined for this scene.");
            return;
        }

        foreach (InteractionRule interaction in sceneInteractions)
        {
            if (string.IsNullOrEmpty(interaction.subject))
            {
                continue;
            }

            interactions[interaction.subject] = interaction;

            GameObject subject = GameObject.Find(interaction.subject);

            if (subject != null)
            {
                Interactable interactable =
                    subject.GetComponent<Interactable>();

                if (interactable == null)
                {
                    interactable = subject.AddComponent<Interactable>();
                }

                interactable.manager = this;
                interactable.objectId = interaction.subject;
            }
        }
    }

    public void ExecuteInteraction(string objectId)
    {
        if (!interactions.ContainsKey(objectId))
        {
            Debug.Log("No interaction defined for " + objectId);
            return;
        }

        InteractionRule interaction = interactions[objectId];

        Debug.Log(
            "Interaction: " +
            interaction.trigger + " " +
            interaction.subject + " " +
            interaction.action + " " +
            interaction.target
        );

        if (interaction.trigger == "collect")
        {
            ExecuteCollect(interaction);
        }
    }

    private void ExecuteCollect(InteractionRule interaction)
    {
        GameObject subject = GameObject.Find(interaction.subject);

        if (subject == null)
        {
            Debug.LogWarning(
                "Could not find interaction subject: " +
                interaction.subject
            );

            return;
        }

        if (interaction.action == "unlock")
        {
            GameObject target = GameObject.Find(interaction.target);

            if (target == null)
            {
                Debug.LogWarning(
                    "Could not find interaction target: " +
                    interaction.target
                );

                return;
            }

            ChestInteraction chest =
                target.GetComponent<ChestInteraction>();

            if (chest != null)
            {
                chest.isLocked = false;

                Debug.Log(
                    target.name +
                    " unlocked by interaction."
                );
            }
        }

        Destroy(subject);

        Debug.Log(
            interaction.subject +
            " collected."
        );
    }
}