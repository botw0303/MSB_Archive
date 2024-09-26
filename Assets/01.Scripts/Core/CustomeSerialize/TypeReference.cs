using UnityEngine;
using System;

[Serializable]
public class TypeReference : ISerializationCallbackReceiver
{
    [SerializeField]
    private string typeName;

    [NonSerialized]
    private Type type;

    public Type Type
    {
        get => type;
        set
        {
            type = value;
            typeName = value?.AssemblyQualifiedName;
        }
    }

    public void OnBeforeSerialize()
    {
        typeName = type != null ? type.AssemblyQualifiedName : string.Empty;
    }

    public void OnAfterDeserialize()
    {
        if (!string.IsNullOrEmpty(typeName))
        {
            type = Type.GetType(typeName);
        }
    }
}
