using System.Collections.Frozen;
using ECSTest.Components;
using System.Reflection;

namespace ECSTest;
public class ECS
{
    private static int runningID;
    private readonly int _maxEntities;
    private readonly IComponent[][] _systems;
    private readonly FrozenDictionary<Type, int> _typesToID;
    public ECS(int maxEntities)
    {
        _maxEntities = maxEntities;
        var componentType = typeof(IComponent);
        var subclassTypes = Assembly
            .GetAssembly(typeof(IComponent))!
            .GetTypes()
            .Where(t => componentType.IsAssignableFrom(t) && t.IsClass);
        var dict = new Dictionary<Type, int>();
        var k = 0;
        foreach (var t in subclassTypes)
            dict.Add(t, k++);
        _typesToID = dict.ToFrozenDictionary();
        
        _systems = new IComponent[_typesToID.Count][];
        foreach (var (type, id) in _typesToID)
            _systems[id] = (IComponent[]) Array.CreateInstance(type, maxEntities);
    }
    public void Update()
    {
        foreach(var system in _systems)
            for (var i = 0; i < _maxEntities; i++)
                system[i].Update(i);
    }
    public Entity AddEntity(params IComponent[] components)
    {
        foreach (var c in components)
            _systems[_typesToID[c.GetType()]][runningID] = c;
        return new Entity(runningID++);
    }
}