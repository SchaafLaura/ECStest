using ECSTest.Components;

namespace ECSTest;
internal class ECS
{
    private static int runningID;
    private readonly int _maxEntities;
    private readonly IComponent[][] _systems;
    
    public ECS(int maxEntities)
    {
        _maxEntities = maxEntities;
        _systems = new IComponent[ComponentExtensions.NumTypes][];
        foreach (var (type, id) in ComponentExtensions.TypesToID)
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
            _systems[c.GetID()][runningID] = c;
        return new Entity(runningID++);
    }
}