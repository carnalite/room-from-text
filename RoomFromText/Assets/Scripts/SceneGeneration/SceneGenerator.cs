using Unity.VisualScripting;
using UnityEngine;
 
public class SceneGenerator : MonoBehaviour
{

    void Start()
    {
        GenerateScene();
    }

    public string testJson = "{\r\n  \"objects\": [\r\n    {\r\n      \"id\": \"table_1\",\r\n      \"type\": \"table\",\r\n      \"size\": \"large\"\r\n    },\r\n    {\r\n      \"id\": \"candle_1\",\r\n      \"type\": \"candle\",\r\n      \"size\": \"small\"\r\n    }\r\n  ],\r\n  \"relationships\": [\r\n    {\r\n      \"subject\": \"candle_1\",\r\n      \"relation\": \"on\",\r\n      \"target\": \"table_1\"\r\n    }\r\n  ]\r\n}";
    public void GenerateScene()
    {
        SceneData sceneData = JsonUtility.FromJson<SceneData>(testJson);
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
            if (generatedObject != null)
            {
                generatedObject.name = obj.id;
                if(obj.size=="small")
                {
                    generatedObject.transform.localScale = Vector3.one * 0.5f;
                }
                else if (obj.size == "medium")
                {
                    generatedObject.transform.localScale = Vector3.one;
                }
                else if (obj.size == "large")
                {
                    generatedObject.transform.localScale = Vector3.one* 2f;
                }

                if (obj.type == "table")
                {
                    generatedObject.transform.position = new Vector3(0, 0, 0);
                }

            }
        }

        foreach (SceneRelationship relationship in sceneData.relationships)
        {
            GameObject subject = GameObject.Find(relationship.subject);
            GameObject target = GameObject.Find(relationship.target);
            Debug.Log(relationship.subject + " - " + relationship.relation + " - " + relationship.target);

            if (relationship.relation == "on")
            {
                float targetHeight = target.GetComponent<Renderer>().bounds.size.y;
                float subjectHeight = subject.GetComponent<Renderer>().bounds.size.y;

                subject.transform.position=target.transform.position + new Vector3(0,(targetHeight/2f)+ (subjectHeight/2f),0);
            }

        }
    }
    
}