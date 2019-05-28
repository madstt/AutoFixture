using System;
using System.Linq;
using System.Reflection;
using Rocks;

namespace AutoFixture.AutoRocks
{
    public static class MockType
    {
        internal static bool IsMock(this Type type)
        {
            return type != null
                   && type.GetTypeInfo().IsGenericType
                   && typeof(Rock).IsAssignableFrom(type.GetGenericTypeDefinition())
                   && !type.GetMockedType().IsGenericParameter;
        }

        internal static Type GetMockedType(this Type type)
        {
            return type.GetTypeInfo().GetGenericArguments().Single();
        }

    }
}