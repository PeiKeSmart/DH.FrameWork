using DH.Npoi.Configurations;

namespace DH.Npoi
{
    public interface IMappingProfile
    {
    }

    public interface IMappingProfile<T> : IMappingProfile
    {
        public void Configure(IExcelConfiguration<T> configuration);
    }
}
