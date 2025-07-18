// <copyright file="AssemblyHelper.cs" company="Skd">
// Copyright (c) Skd. All rights reserved.
// </copyright>

namespace FisherYatestTests.Helpers
{
    using System.Reflection;

    /// <summary>
    /// Provides utility methods for working with assemblies and types.
    /// </summary>
    /// <remarks>This class includes methods for discovering and instantiating implementations of interfaces
    /// within the same assembly as the interface type. It is designed to simplify reflection-based operations for
    /// scenarios where interface implementations need to be dynamically resolved.</remarks>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Retrieves all concrete implementations of the specified interface type within its assembly.
        /// </summary>
        /// <remarks>This method searches the assembly containing <typeparamref name="TInterface"/> for
        /// all non-abstract, concrete classes that implement the interface and have a parameterless constructor.
        /// Instances of these classes are created and returned.</remarks>
        /// <typeparam name="TInterface">The interface type for which implementations are to be retrieved. Must be a class type.</typeparam>
        /// <returns>An enumerable collection of object arrays, where each array contains a single instance of a concrete
        /// implementation of <typeparamref name="TInterface"/>. The collection will be empty if no implementations are
        /// found.</returns>
        /// <exception cref="ArgumentException">Thrown if <typeparamref name="TInterface"/> is not an interface type.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the assembly containing <typeparamref name="TInterface"/> cannot be found.</exception>
        public static IEnumerable<object[]> GetAllImplementations<TInterface>()
            where TInterface : class
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new ArgumentException($"{typeof(TInterface).Name} must be an interface.");
            }

            var interfaceType = typeof(TInterface);
            var assembly = Assembly.GetAssembly(interfaceType)
                ?? throw new InvalidOperationException($"Assembly for {interfaceType.Name} not found.");

            var types = assembly.GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null);

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type) as TInterface;
                if (instance != null)
                {
                    yield return new object[] { instance };
                }
            }
        }
    }
}
