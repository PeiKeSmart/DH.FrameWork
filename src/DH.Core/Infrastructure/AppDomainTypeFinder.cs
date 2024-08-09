using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DH.Core.Infrastructure;

/// <summary>
/// 一个类，通过循环当前执行的AppDomain中的程序集来查找DH所需的类型。只调查名称与特定模式匹配的程序集，并始终调查<see cref="AssemblyNames"/>引用的程序集的可选列表。
/// </summary>
public partial class AppDomainTypeFinder : ITypeFinder
{
    #region Fields

    private bool _ignoreReflectionErrors = true;
    protected IDHFileProvider _fileProvider;

    #endregion

    #region Ctor

    public AppDomainTypeFinder(IDHFileProvider fileProvider = null)
    {
        _fileProvider = fileProvider ?? CommonHelper.DefaultFileProvider;
    }

    #endregion

    #region Utilities

    /// <summary>
    /// 迭代AppDomain中的所有程序集，如果其名称与配置的模式匹配，则将其添加到我们的列表中。
    /// </summary>
    /// <param name="addedAssemblyNames"></param>
    /// <param name="assemblies"></param>
    private void AddAssembliesInAppDomain(List<string> addedAssemblyNames, List<Assembly> assemblies)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (!Matches(assembly.FullName))
                continue;

            if (addedAssemblyNames.Contains(assembly.FullName))
                continue;

            assemblies.Add(assembly);
            addedAssemblyNames.Add(assembly.FullName);
        }
    }

    /// <summary>
    /// 添加特定配置的程序集。
    /// </summary>
    /// <param name="addedAssemblyNames"></param>
    /// <param name="assemblies"></param>
    protected virtual void AddConfiguredAssemblies(List<string> addedAssemblyNames, List<Assembly> assemblies)
    {
        foreach (var assemblyName in AssemblyNames)
        {
            var assembly = Assembly.Load(assemblyName);
            if (addedAssemblyNames.Contains(assembly.FullName))
                continue;

            assemblies.Add(assembly);
            addedAssemblyNames.Add(assembly.FullName);
        }
    }

    /// <summary>
    /// 检查dll是否是我们知道不需要分析的dll之一。
    /// </summary>
    /// <param name="assemblyFullName">
    /// 要检查的程序集的名称。
    /// </param>
    /// <returns>
    /// 如果程序集应加载到DH中，则为True。
    /// </returns>
    protected virtual bool Matches(string assemblyFullName)
    {
        return !Matches(assemblyFullName, AssemblySkipLoadingPattern)
               && Matches(assemblyFullName, AssemblyRestrictToLoadingPattern);
    }

    /// <summary>
    /// 检查dll是否是我们知道不需要分析的dll之一。
    /// </summary>
    /// <param name="assemblyFullName">
    /// 要匹配的程序集名称。
    /// </param>
    /// <param name="pattern">
    /// 要与程序集名称匹配的正则表达式模式。
    /// </param>
    /// <returns>
    /// 如果模式与程序集名称匹配，则为True。
    /// </returns>
    protected virtual bool Matches(string assemblyFullName, string pattern)
    {
        return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }

    /// <summary>
    /// 确保在应用程序域中加载了提供的文件夹中的匹配程序集。
    /// </summary>
    /// <param name="directoryPath">
    /// 包含要在应用程序域中加载的dll的目录的物理路径。
    /// </param>
    protected virtual void LoadMatchingAssemblies(string directoryPath)
    {
        var loadedAssemblyNames = new List<string>();

        foreach (var a in GetAssemblies())
        {
            loadedAssemblyNames.Add(a.FullName);
        }

        if (!_fileProvider.DirectoryExists(directoryPath))
        {
            return;
        }

        foreach (var dllPath in _fileProvider.GetFiles(directoryPath, "*.dll"))
        {
            try
            {
                var an = AssemblyName.GetAssemblyName(dllPath);
                if (Matches(an.FullName) && !loadedAssemblyNames.Contains(an.FullName))
                {
                    App.Load(an);
                }

                //old loading stuff
                //Assembly a = Assembly.ReflectionOnlyLoadFrom(dllPath);
                //if (Matches(a.FullName) && !loadedAssemblyNames.Contains(a.FullName))
                //{
                //    App.Load(a.FullName);
                //}
            }
            catch (BadImageFormatException ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }
    }

    /// <summary>
    /// 类型是否实现泛型？
    /// </summary>
    /// <param name="type"></param>
    /// <param name="openGeneric"></param>
    /// <returns></returns>
    protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
    {
        try
        {
            var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                    continue;

                if (genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
                    return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 查找类型的类
    /// </summary>
    /// <param name="assignTypeFrom">分配类型来源</param>
    /// <param name="assemblies">程序集</param>
    /// <param name="onlyConcreteClasses">指示是否仅查找具体类的值</param>
    /// <returns>结果</returns>
    public virtual IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
    {
        var result = new List<Type>();
        try
        {
            foreach (var a in assemblies)
            {
                Type[] types = null;
                try
                {
                    types = a.GetTypes();
                }
                catch
                {
                    //Entity Framework 6不允许获取类型（引发异常）
                    if (!_ignoreReflectionErrors)
                    {
                        throw;
                    }
                }

                if (types == null)
                    continue;

                foreach (var t in types)
                {
                    if (!assignTypeFrom.IsAssignableFrom(t) && (!assignTypeFrom.IsGenericTypeDefinition || !DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                        continue;

                    if (t.IsInterface)
                        continue;

                    if (onlyConcreteClasses)
                    {
                        if (t.IsClass && !t.IsAbstract)
                        {
                            result.Add(t);
                        }
                    }
                    else
                    {
                        result.Add(t);
                    }
                }
            }
        }
        catch (ReflectionTypeLoadException ex)
        {
            var msg = string.Empty;
            foreach (var e in ex.LoaderExceptions)
                msg += e.Message + Environment.NewLine;

            var fail = new Exception(msg, ex);
            Debug.WriteLine(fail.Message, fail);

            throw fail;
        }

        return result;
    }

    #endregion

    #region 方法

    /// <summary>
    /// 查找类型的类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="onlyConcreteClasses">指示是否仅查找具体类的值</param>
    /// <returns>结果</returns>
    public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
    {
        return FindClassesOfType(typeof(T), onlyConcreteClasses);
    }

    /// <summary>
    /// 查找类型的类
    /// </summary>
    /// <param name="assignTypeFrom">分配类型来源</param>
    /// <param name="onlyConcreteClasses">指示是否仅查找具体类的值</param>
    /// <returns>结果</returns>
    /// <returns></returns>
    public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
    {
        return FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
    }

    /// <summary>
    /// 查找类型的类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="assemblies">程序集</param>
    /// <param name="onlyConcreteClasses">一个值，指示是否仅查找具体的类</param>
    /// <returns>结果</returns>
    public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
    {
        return FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
    }

    /// <summary>
    /// 获取与当前实现相关的程序集。
    /// </summary>
    /// <returns>程序集列表</returns>
    public virtual IList<Assembly> GetAssemblies()
    {
        var addedAssemblyNames = new List<string>();
        var assemblies = new List<Assembly>();

        if (LoadAppDomainAssemblies)
            AddAssembliesInAppDomain(addedAssemblyNames, assemblies);
        AddConfiguredAssemblies(addedAssemblyNames, assemblies);

        return assemblies;
    }

    #endregion

    #region Properties

    /// <summary>要在其中查找类型的应用程序域。</summary>
    public virtual AppDomain App => AppDomain.CurrentDomain;

    /// <summary>获取或设置加载DH类型时DH是否应迭代应用程序域中的程序集。加载这些程序集时将应用加载模式。</summary>
    public bool LoadAppDomainAssemblies { get; set; } = true;

    /// <summary>获取或设置启动时加载的程序集以及AppDomain中加载的程序。</summary>
    public IList<string> AssemblyNames { get; set; } = new List<string>();

    /// <summary>获取我们知道不需要分析的dll的模式。</summary>
    public string AssemblySkipLoadingPattern { get; set; } = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease|^NewLife|^XCode|^Azure|^Nito|^Polly|^Swashbuckle|^ToolGood|^WebMarkupMin|^Xfrogcn|^WebOptimizer|^TimeZoneConverter|^StackExchange|^SkiaSharp|^SharpCompress|^NUglify|^Pipelines|^IdentityModel|^JetBrains|^Humanizer|^HtmlSanitizer|^AngleSharp|^aliyun|^BouncyCastle|^FluentFTP|^Google|^Magicodes|^MailKit|^MimeKit|^OpenXmlPowerTools|^SixLabors|^Quartz|^MySqlConnector|^MQTTnet|^Grpc|^Flurl|^DocumentFormat|^WebApiClient|^TinyPinyin|^Segmenter|^Parlot|^NPOI|^MySqlBackupNet|^MurmurHash|^MessagePack|^MathNet|^MathExpressions|^Lucene|^Jering|^J2N|^IP2Region|^ICSharpCode|^Fluid|^Enums|^dotnet-bundle|^ChineseConverter|^AdvancedStringBuilder|^ZstdSharp|^Rougamo|^Certes|^DynamicExpresso|^ExtendedNumerics|^Scrutor";

    /// <summary>获取或设置将被调查的dll的模式。为了易于使用，此默认值将匹配所有内容，但为了提高性能，您可能需要配置一个包含程序集和您自己的模式。</summary>
    /// <remarks>如果您更改此设置以便不调查DH程序集（例如，不包括"^DH|…"之类的内容），则可能会破坏核心功能。</remarks>
    public string AssemblyRestrictToLoadingPattern { get; set; } = ".*";

    #endregion
}
