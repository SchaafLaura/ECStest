using System.Collections.Frozen;
using System.Reflection;

namespace ECSTest.Components;
internal interface IComponent
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
        TypesToID = Assembly
            .GetAssembly(componentType)!
            .GetTypes()
            .Where(t => componentType.IsAssignableFrom(t) && t.IsClass)
            .Select((type, index) => new { type, index })
            .ToFrozenDictionary(x => x.type, x => x.index);
    }
    public static int GetID(this IComponent c) => TypesToID[c.GetType()];
}