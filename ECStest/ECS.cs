using ECSTest.Components;

namespace ECSTest;
public class ECS
{
    private readonly List<IComponent[]> _systems = [];

    private readonly int _maxEntities;
    private static int runningID;
    public ECS(int maxEntities)
    {
        _maxEntities = maxEntities;
        _systems.Add(new PhysicsComponent[maxEntities]);
        _systems.Add(new RenderComponent[maxEntities]);
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
            _systems[c.ToID()][runningID] = c;
        return new Entity(runningID++);
    }
}