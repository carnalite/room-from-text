using System.Drawing;
using UnityEngine;
 
public class SceneGenerator : MonoBehaviour
{

   

    public string testJson = "{\r\n  \"objects\": [\r\n    {\r\n      \"id\": \"table_1\",\r\n      \"type\": \"table\",\r\n      \"size\": \"large\"\r\n    },\r\n    {\r\n      \"id\": \"candle_1\",\r\n      \"type\": \"candle\",\r\n      \"size\": \"small\"\r\n    }\r\n  ],\r\n  \"relationships\": [\r\n    {\r\n      \"subject\": \"candle_1\",\r\n      \"relation\": \"near\",\r\n      \"target\": \"table_1\"\r\n    }\r\n  ]\r\n}";
    public void GenerateScene(string json)
    {
        SceneData sceneData = JsonUtility.FromJson<SceneData>(json);
        Debug.Log(sceneData.objects.Count);
        Debug.Log(sceneData.relationships.Count);
        foreach (SceneObject obj in sceneData.objects)
        {
            GameObject generatedObject = null;
            if(obj.type == "table")
            {
                generatedObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
            else if (obj.type == "candle")
            {
                generatedObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            }
            else if (obj.type == "chest")
            {
                generatedObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
            else if (obj.type=="key")
            {
                generatedObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            }

            if (generatedObject != null)
            {
                generatedObject.name = obj.id;
                if (obj.type == "chest")
                {
                    ChestInteraction chestInteraction =
                        generatedObject.AddComponent<ChestInteraction>();

                    chestInteraction.isLocked = (obj.state == "locked");
                }

                // COLOR
                if (!string.IsNullOrEmpty(obj.color))
                {
                    UnityEngine.Color parsedColor;
                    if (ColorUtility.TryParseHtmlString(obj.color, out parsedColor))
                    {
                        generatedObject.GetComponent<Renderer>().material.color = parsedColor;
                    }
                }

                // MATERIAL
                if (obj.material == "wooden")
                {
                    generatedObject.GetComponent<Renderer>().material.color =
                        new UnityEngine.Color(0.55f, 0.27f, 0.07f);
                }

                // STATE
                if (!string.IsNullOrEmpty(obj.state))
                {
                    Debug.Log(obj.id + " state: " + obj.state);
                }

                // SIZE
                if (obj.size == "small")
                {
                    generatedObject.transform.localScale = Vector3.one * 0.5f;
                }
                else if (obj.size == "medium")
                {
                    generatedObject.transform.localScale = Vector3.one;
                }
                else if (obj.size == "large")
                {
                    generatedObject.transform.localScale = Vector3.one * 2f;
                }


                // POSITION
                if (obj.type == "table")
                {
                    generatedObject.transform.position = new Vector3(0, 0, 0);
                }
                else if (obj.type == "chest")
                {
                    generatedObject.transform.position = new Vector3(3, 0, 0);
                }


            }
        }

        // RELATIONSHIP
        foreach (SceneRelationship relationship in sceneData.relationships)
        {
            GameObject subject = GameObject.Find(relationship.subject);
            GameObject target = GameObject.Find(relationship.target);

            Debug.Log(relationship.subject + " - " + relationship.relation + " - " + relationship.target);

            if (subject == null || target == null)
            {
                Debug.LogWarning("Could not find relationship objects.");
                continue;
            }

            if (relationship.relation == "on")
            {
                Renderer targetRenderer = target.GetComponent<Renderer>();
                Renderer subjectRenderer = subject.GetComponent<Renderer>();
                Bounds targetBounds = targetRenderer.bounds;
                Bounds subjectBounds = subjectRenderer.bounds;

                float x = targetBounds.center.x;
                float z = targetBounds.center.z;
                float y = targetBounds.max.y + subjectBounds.extents.y;

                subject.transform.position = new Vector3(x, y, z);
            }
            else if (relationship.relation == "near")
            {
                Debug.Log("Near relationship detected: " +
                          relationship.subject + " is near " +
                          relationship.target);
            }
            else if (relationship.relation == "inside")
            {
                Vector3 targetPosition = target.transform.position;

                subject.transform.position =
                    targetPosition + new Vector3(0, 0.5f, 0);

                Debug.Log(subject.name + " placed inside " + target.name);
            }
        }
    }
    
}