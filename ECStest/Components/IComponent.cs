using System.Collections.Frozen;
using System.Reflection;

namespace ECSTest.Components;
public interface IComponent
{
    public void Update(int id);
}

internal static class ComponentExtensions
{
    public static readonly FrozenDictionary<Type, int> TypesToID;
    public static int NumTypes => TypesToID.Count;
    static ComponentExtensions()
    {
        var componentType = typeof(IComponent);
        var subclassTypes = Assembly
            .GetAssembly(typeof(IComponent))!
            .GetTypes()
            .Where(t => componentType.IsAssignableFrom(t) && t.IsClass);
        var dict = new Dictionary<Type, int>();
        var k = 0;
        foreach (var t in subclassTypes)
            dict.Add(t, k++);
        TypesToID = dict.ToFrozenDictionary();
    }

    public static int GetID(this IComponent c) => TypesToID[c.GetType()];
}