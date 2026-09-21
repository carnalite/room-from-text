using System;
using System.Collections.Generic;

[Serializable]
public class SceneData
{
    public List<SceneObject> objects;
    public List<SceneRelationship> relationships;
    public List<InteractionRule> interactions;
}

[Serializable]
public class SceneObject
{
    public string id;
    public string type;
    public string size;
    public string color;
    public string material;
    public string state;
}

[Serializable]
public class SceneRelationship
{
    public string subject;
    public string relation;
    public string target;
}

[Serializable]
public class InteractionRule
{
    public string trigger;
    public string subject;
    public string action;
    public string target;
}