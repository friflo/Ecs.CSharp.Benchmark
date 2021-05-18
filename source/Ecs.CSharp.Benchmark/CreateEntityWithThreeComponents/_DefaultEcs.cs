﻿using System;
using BenchmarkDotNet.Attributes;
using DefaultEcs;

namespace Ecs.CSharp.Benchmark
{
    public partial class CreateEntityWithThreeComponents
    {
        private class DefaultEcsContext : IDisposable
        {
#pragma warning disable CS0649
            public struct Component1
            {
                public int Value;
            }

            public struct Component2
            {
                public int Value;
            }

            public struct Component3
            {
                public int Value;
            }

            public World World { get; }

            public DefaultEcsContext()
            {
                World = new World();
            }

            public void Dispose()
            {
                World.Dispose();
            }
        }

        private DefaultEcsContext _defaultEcs;

        [Benchmark]
        public void DefaultEcs()
        {
            for (int i = 0; i < EntityCount; ++i)
            {
                Entity entity = _defaultEcs.World.CreateEntity();
                entity.Set<DefaultEcsContext.Component1>();
                entity.Set<DefaultEcsContext.Component2>();
                entity.Set<DefaultEcsContext.Component3>();
            }
        }
    }
}