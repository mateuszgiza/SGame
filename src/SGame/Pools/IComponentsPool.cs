using System.Collections.Generic;

namespace SGame
{
    public interface IComponentsPool
    {
        T Register<T>(T component) where T : IComponent;
        T GetComponent<T>() where T : IComponent;
    }
}