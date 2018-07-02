using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace SGame
{
    public class ProcessingSystemManager : IHaveContext
    {
        private List<IProcessingSystem> processingSystems = new List<IProcessingSystem>();

        public ISystemContext Context { get; set; }

        public ProcessingSystemManager Register(ProcessingSystem processingSystem)
        {
            if (!processingSystems.Contains(processingSystem))
            {
                processingSystem.EntityComponentSystem = Context.EntityComponentSystem;
                processingSystems.Add(processingSystem);
            }

            return this;
        }

        public void ProcessSystems(GameTime gameTime, ProcessType type)
        {
            processingSystems
                .Where(ps => ps.Type == type)
                .ForEach(ps => ps.Process(gameTime));
        }
    }
}