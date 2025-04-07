using ECSTest.Components;

namespace ECSTest;
public interface IComponent
{
    public void Update(int id);
}

internal static class ComponentExtensions
{
    public static int ToID(this IComponent component) => 
        component switch
        {
            PhysicsComponent    => 0,
            RenderComponent     => 1,
            _ => throw new NotImplementedException()
        };
}