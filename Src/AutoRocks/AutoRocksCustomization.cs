using System;
using System.Collections;
using System.Collections.Generic;
using AutoFixture.Kernel;
using Rocks;

namespace AutoFixture.AutoRocks
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

            if (this.ConfigureMembers)
            {
                mockBuilder = new Postprocessor(
                    builder: mockBuilder,
                    command: new CompositeSpecimenCommand(
                        new StubPropertiesCommand(),
                        new MockVirtualMethodsCommand(),
                        new AutoMockPropertiesCommand()));

            }

            fixture.Customizations.Add(mockBuilder);
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
        private ISpecimenBuilder Builder { get; }

        public MockPostprocessor(ISpecimenBuilder builder)
        {
            this.Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public object Create(object request, ISpecimenContext context)
        {
            var t = request as Type;
            if (!t.IsMock())
            {
                return new NoSpecimen();
            }

            var specimen = this.Builder.Create(request, context);
            if (specimen is NoSpecimen || specimen is OmitSpecimen || specimen == null)
                return specimen;

            Rock m = specimen as Rock;
            if (m == null)
            {
                return new NoSpecimen();
            }

            var mockType = t.GetMockedType();
            if (m.GetType().GetMockedType() != mockType)
            {
                return new NoSpecimen();
            }

            var configurator = (IMockConfigurator)Activator.CreateInstance(typeof(MockConfigurator<>).MakeGenericType(mockType));
            configurator.Configure(m);

            return m;
            ;
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