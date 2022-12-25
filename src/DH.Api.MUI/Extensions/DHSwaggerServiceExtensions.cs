namespace DH.Api.MUI;

public class DHSwaggerServiceExtensions
{

    /// <summary>
    /// 所有xml默认当作swagger文档注入swagger
    /// </summary>
    /// <returns></returns>
    private static List<string> GetXmlComments()
    {
        //var pattern = $"^{netProOption.ProjectPrefix}.*({netProOption.ProjectSuffix}|Domain)$";
        //List<string> assemblyNames = ReflectionHelper.GetAssemblyNames(pattern);
        List<string> assemblyNames = AppDomain.CurrentDomain.GetAssemblies().Select(s => s.GetName().Name).ToList();
        List<string> xmlFiles = new List<string>();
        assemblyNames.ForEach(r =>
        {
            string fileName = $"{r}.xml";
            xmlFiles.Add(fileName);
        });
        return xmlFiles;
    }

}
