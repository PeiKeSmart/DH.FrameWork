namespace DH.CoolCat.Core.Contracts
{
    public interface IMigration
    {
        DomainModel.Version Version { get; }

        void MigrateUp(String pluginId);

        void MigrateDown(Guid pluginId);
    }
}
