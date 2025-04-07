using ECSTest.Components;

namespace ECSTest;
public class ECS(int maxEntities)
{
    private readonly IComponent[][] _systems =
    [
        new PhysicsComponent[maxEntities],
        new RenderComponent[maxEntities]
    ];

    private static int runningID;

    public void Update()
    {
        foreach(var system in _systems)
            for (var i = 0; i < maxEntities; i++)
                system[i].Update(i);
    }
    public Entity AddEntity(params IComponent[] components)
    {
        foreach (var c in components)
            _systems[c.ToID()][runningID] = c;
        return new Entity(runningID++);
    }
}