using System;
using System.Collections;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Kernel;

namespace AutoRocks
{
    /// <summary>
    /// Enables auto-mocking using Rocks.
    /// </summary>
    public class AutoRocksCustomization : ICustomization
    {
        /// <summary>
        /// Customizes an <see cref="IFixture"/> to enable auto-mocking with Rocks.
        /// </summary>
        /// <param name="fixture">The fixture upon which to enable auto-mocking.</param>
        public void Customize(IFixture fixture)
        {
            if (fixture == null) throw new ArgumentNullException(nameof(fixture));

            ISpecimenBuilder mockBuilder = new MockPostprocessor(
                new MethodInvoker(
                    new MockConstructorQuery()));

            if (ConfigureMembers)
            {
                mockBuilder = new Postprocessor(
                    builder: mockBuilder,
                    command: new CompositeSpecimenCommand(
                        new StubPropertiesCommand(),
                        new MockVirtualMethodsCommand(),
                        new AutoMockPropertiesCommand()));

            }
        }

        public bool ConfigureMembers { get; set; }

    }

    public class AutoMockPropertiesCommand : ISpecimenCommand
    {
        public void Execute(object specimen, ISpecimenContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class MockVirtualMethodsCommand : ISpecimenCommand
    {
        public void Execute(object specimen, ISpecimenContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class StubPropertiesCommand : ISpecimenCommand
    {
        public void Execute(object specimen, ISpecimenContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class MockConstructorQuery : IMethodQuery
    {
        public IEnumerable<IMethod> SelectMethods(Type type)
        {
            throw new NotImplementedException();
        }
    }

    public class MockPostprocessor : ISpecimenBuilderNode
    {
        public MockPostprocessor(ISpecimenBuilder builder)
        {
            throw new NotImplementedException();
        }

        public object Create(object request, ISpecimenContext context)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ISpecimenBuilder> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public ISpecimenBuilderNode Compose(IEnumerable<ISpecimenBuilder> builders)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}