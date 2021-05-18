﻿using System;
using BenchmarkDotNet.Attributes;
using Entitas;

namespace Ecs.CSharp.Benchmark
{
    public partial class CreateEntity
    {
        private class EntitasContext : IDisposable
        {
            public Context<Entity> Context { get; }

            public EntitasContext()
            {
                Context = new Context<Entity>(0, () => new Entity());
            }

            public void Dispose()
            {
                Context.DestroyAllEntities();
            }
        }

        private EntitasContext _entitas;

        [Benchmark]
        public void Unity()
        {
            for (int i = 0; i < EntityCount; ++i)
            {
                _entitas.Context.CreateEntity();
            }
        }
    }
}
