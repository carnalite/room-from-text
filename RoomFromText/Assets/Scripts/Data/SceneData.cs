using System;
using System.Collections.Generic;
[Serializable]
    public class SceneData
{

    public List<SceneObject> objects;
    public List<SceneRelationship> relationships;

}
[Serializable]
public class SceneObject
{
    public string id;
    public string type;
    public string size;

}
[Serializable]
public class SceneRelationship
{
    public string subject;
    public string relation;
    public string target;
}