using System.Collections.Generic;

namespace SGame
{
    public interface IComponentsPool
    {
        T Register<T>(T component) where T : INewComponent;
        T GetComponent<T>() where T : INewComponent;
        IEnumerable<INewComponent> GetComponentsByProcessType(ProcessType type);
    }
}