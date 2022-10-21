using System.Reflection;

namespace DG.CoolCat.Core.Contracts
{
    public interface IReferenceLoader
    {
        public void LoadStreamsIntoContext(CollectibleAssemblyLoadContext context, string moduleFolder, Assembly assembly);
    }
}
