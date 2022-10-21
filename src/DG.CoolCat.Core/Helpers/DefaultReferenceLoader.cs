﻿using DG.CoolCat.Core.Contracts;

using System.Reflection;

namespace DG.CoolCat.Core.Helpers
{
    public class DefaultReferenceLoader : IReferenceLoader
    {
        private readonly IReferenceContainer _referenceContainer = null;

        public DefaultReferenceLoader(IReferenceContainer referenceContainer)
        {
            _referenceContainer = referenceContainer;
        }

        public void LoadStreamsIntoContext(CollectibleAssemblyLoadContext context, string moduleFolder, Assembly assembly)
        {
        }
    }
}
