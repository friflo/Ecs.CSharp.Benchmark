﻿using System;
using BenchmarkDotNet.Attributes;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Ecs.CSharp.Benchmark
{
    public partial class CreateEntity
    {
        private class SveltoECSContext : IDisposable
        {
            public sealed class Entity : IEntityDescriptor
            {
                public IComponentBuilder[] componentsToBuild => Array.Empty<IComponentBuilder>();
            }

            public static ExclusiveGroup Group { get; } = new();

            public SimpleEntitiesSubmissionScheduler Scheduler { get; }
            public EnginesRoot Root { get; }
            public IEntityFactory Factory { get; }

            public SveltoECSContext()
            {
                Scheduler = new SimpleEntitiesSubmissionScheduler();
                Root = new EnginesRoot(Scheduler);
                Factory = Root.GenerateEntityFactory();
            }

            public void Dispose()
            {
                Root.Dispose();
                Scheduler.Dispose();
            }
        }

        private SveltoECSContext _sveltoECS;

        [Benchmark]
        public void SveltoECS()
        {
            for (int i = 0; i < EntityCount; ++i)
            {
                _sveltoECS.Factory.BuildEntity<SveltoECSContext.Entity>((uint)i, SveltoECSContext.Group);
            }

            _sveltoECS.Scheduler.SubmitEntities();
        }
    }
}
