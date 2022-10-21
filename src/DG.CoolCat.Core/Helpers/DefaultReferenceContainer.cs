using DG.CoolCat.Core.Contracts;
using DG.CoolCat.Core.DomainModel;

namespace DG.CoolCat.Core.Helpers
{
    public class DefaultReferenceContainer : IReferenceContainer
    {
        private static readonly Dictionary<CachedReferenceItemKey, Stream> _cachedReferences = new Dictionary<CachedReferenceItemKey, Stream>();

        public bool Exist(string name, string version)
        {
            return _cachedReferences.Keys.Any(p => p.ReferenceName == name
                && p.Version == version);
        }

        public List<CachedReferenceItemKey> GetAll()
        {
            return _cachedReferences.Keys.ToList();
        }

        public Stream GetStream(string name, string version)
        {
            CachedReferenceItemKey key = _cachedReferences.Keys.FirstOrDefault(p => p.ReferenceName == name
                && p.Version == version);

            if (key != null)
            {
                _cachedReferences[key].Position = 0;
                return _cachedReferences[key];
            }

            return null;
        }

        public void SaveStream(string name, string version, Stream stream)
        {
            if (Exist(name, version))
            {
                return;
            }

            _cachedReferences.Add(new CachedReferenceItemKey { ReferenceName = name, Version = version }, stream);
        }
    }
}
