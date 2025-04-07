namespace ECSTest;

public class ECS
{
    private List<IComponent[]> systems = [];

    private readonly int _maxEntities;
    public ECS(int maxEntities)
    {
        _maxEntities = maxEntities;
        systems.Add(new PhysicsComponent[maxEntities]);
    }

    public void Update()
    {
        foreach(var system in systems)
            for (var i = 0; i < _maxEntities; i++)
                system[i].Update(i);
    }
}