using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcTurbine.ComponentModel;

namespace StatefulT4Processor.Shared
{
    /// <summary>
    ///   Provides derived classes the functionality to register concrete types of interfaces in a <see cref = "IServiceLocator" />.
    /// </summary>
    /// <remarks>
    ///   Interfaces with multiple implementations or abstract classes will not be registered.
    /// </remarks>
    public abstract class InterfaceToSingleImplementationRegistrationConvention : IServiceRegistration
    {
        /// <summary>
        ///   Registers the types in the <see cref = "IServiceLocator" />.
        /// </summary>
        /// <param name = "locator">The service locator that the types will be registered with.</param>
        public void Register(IServiceLocator locator)
        {
            var assemblies = GetAssembliesToScan();
            foreach (var assembly in assemblies)
                RegisterEveryInterfaceWithTheImplementerWhenOnlyOneImplementerExistsInTheAssembly(assembly, locator);
        }

        private static void RegisterEveryInterfaceWithTheImplementerWhenOnlyOneImplementerExistsInTheAssembly(Assembly assembly, IServiceLocator locator)
        {
            var interfaceTypes = GetAllInterfacesInThisAssembly(assembly);

            foreach (var interfaceType in interfaceTypes)
            {
                var implementers = GetAllTypesThatImplementThisInterface(assembly, interfaceType);

                if (OnlyOneImplementationExists(implementers))
                    RegisterThisImplementationWithTheInterface(locator, interfaceType, implementers.Single());
            }
        }

        private static void RegisterThisImplementationWithTheInterface(IServiceLocator locator, Type interfaceType, Type implType)
        {
            locator.Register(interfaceType, implType);
        }

        private static bool OnlyOneImplementationExists(IEnumerable<Type> implementers)
        {
            return implementers.Count() == 1;
        }

        private static IEnumerable<Type> GetAllTypesThatImplementThisInterface(Assembly assembly, Type interfaceType)
        {
            return assembly.GetTypes()
                .Where(TypeIsNotAnInterfaceAndIsNotAbstract)
                .Where(type => TypeImplementsInterface(type, interfaceType));
        }

        private static IEnumerable<Type> GetAllInterfacesInThisAssembly(Assembly assembly)
        {
            return assembly.GetTypes().Where(TypeIsAnInterface);
        }

        /// <summary>
        ///   When overriden in a derived class, returns the assemblies to scan for types to register.
        /// </summary>
        /// <returns>The assemblies that will be scanned for types to register.</returns>
        protected abstract IEnumerable<Assembly> GetAssembliesToScan();

        private static bool TypeIsAnInterface(Type type)
        {
            return type.IsInterface;
        }

        private static bool TypeIsNotAnInterfaceAndIsNotAbstract(Type type)
        {
            return type.IsInterface == false && type.IsAbstract == false;
        }

        private static bool TypeImplementsInterface(Type type, Type interfaceType)
        {
            return type.GetInterfaces().Contains(interfaceType);
        }
    }
}