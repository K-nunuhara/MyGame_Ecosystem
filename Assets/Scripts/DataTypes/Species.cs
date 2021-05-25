using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Species : MonoBehaviour
{
    public enum Type
    {
        Flower,
        Bush,
        Grass,
        Sheep,
    }

    public static T GetClassByName<T>(Species.Type type)
    {
        switch (type)
        {
            case Type.Flower:
                return (T)(object)new Flower().GetType();
            /*
            case Type.Bush:
                return (T)(object)new Bush().GetType();
            case Type.Grass:
                return (T)(object)new Grass().GetType();
            */
            case Type.Sheep:
                return (T)(object)new Sheep().GetType();
            default:
                return default(T);
        }
    }

    public static Component GetScriptComponent(GameObject target)
    {
        Component component = null;
        foreach (string name in Enum.GetNames(typeof(Species.Type)))
        {
            if (target.GetComponent(name) != null)
            {
                component = target.GetComponent(name);
            }
        }
        return component;
    }
}
