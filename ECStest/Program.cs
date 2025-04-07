using ECSTest;
using ECSTest.Components;

using System.Reflection;

var ecs = new ECS(1000);
var e0 = ecs.AddEntity(
    new PhysicsComponent());
var e1 = ecs.AddEntity(
    new PhysicsComponent(),
    new RenderComponent());



var x = 5;

