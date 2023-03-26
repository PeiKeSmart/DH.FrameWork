﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NewLife;
using NewLife.Log;
using XCode.Code;
using XCode.DataAccessLayer;

namespace XCode;

/// <summary>魔方页面生成器</summary>
public class CubeBuilder : ClassBuilder
{
    #region 属性
    /// <summary>区域名</summary>
    public String AreaName { get; set; }

    /// <summary>根命名空间</summary>
    public String RootNamespace { get; set; }

    /// <summary>菜单排序</summary>
    public Int32 Sort { get; set; }

    /// <summary>区域模版</summary>
    public String AreaTemplate { get; set; } = @"using System.ComponentModel;
using NewLife;
using NewLife.Cube;

namespace {RootNamespace}.Areas.{Name}
{
    [DisplayName(""{DisplayName}"")]
    public class {Name}Area : AreaBase
    {
        public {Name}Area() : base(nameof({Name}Area).TrimEnd(""Area"")) { }
    }
}";

    /// <summary>控制器模版</summary>
    public String ControllerTemplate { get; set; } = @"using {EntityNamespace};
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Extensions;
using NewLife.Cube.ViewModels;
using NewLife.Web;
using XCode.Membership;

namespace {RootNamespace}.Areas.{Name}.Controllers
{
    /// <summary>{DisplayName}</summary>
    [Menu({Sort}, true, Icon = ""fa-table"")]
    [{Name}Area]
    public class {ClassName} : {BaseClass}<{EntityName}>
    {
        static {ClassName}()
        {
            //LogOnChange = true;

            //ListFields.RemoveField(""Id"", ""Creator"");
            ListFields.RemoveCreateField().RemoveRemarkField();

            //{
            //    var df = ListFields.GetField(""Code"") as ListField;
            //    df.Url = ""?code={Code}"";
            //}
            //{
            //    var df = ListFields.AddListField(""devices"", null, ""Onlines"");
            //    df.DisplayName = ""查看设备"";
            //    df.Url = ""Device?groupId={Id}"";
            //    df.DataVisible = e => (e as {EntityName}).Devices > 0;
            //}
            //{
            //    var df = ListFields.GetField(""Kind"") as ListField;
            //    df.GetValue = e => ((Int32)(e as {EntityName}).Kind).ToString(""X4"");
            //}
            //ListFields.TraceUrl(""TraceId"");
        }

        /// <summary>高级搜索。列表页查询、导出Excel、导出Json、分享页等使用</summary>
        /// <param name=""p"">分页器。包含分页排序参数，以及Http请求参数</param>
        /// <returns></returns>
        protected override IEnumerable<{EntityName}> Search(Pager p)
        {
            //var deviceId = p[""deviceId""].ToInt(-1);

            var start = p[""dtStart""].ToDateTime();
            var end = p[""dtEnd""].ToDateTime();

            return {EntityName}.Search(start, end, p[""Q""], p);
        }
    }
}";
    #endregion

    #region 静态
    /// <summary>生成魔方区域</summary>
    /// <param name="option">可选项</param>
    /// <returns></returns>
    public static Int32 BuildArea(BuilderOption option)
    {
        if (option == null)
            option = new BuilderOption();
        else
            option = option.Clone();

        // 自动识别并修正区域名（主要是大小写）
        var areaName = FindAreaName(option.Output);
        if (!areaName.IsNullOrEmpty()) return 0;

        // 优先使用路径最后一段作为区域名，其次再用连接名
        areaName = Path.GetFileNameWithoutExtension(option.Output);
        if (areaName.IsNullOrEmpty())
            areaName = option.ConnName;

        var file = $"{areaName}Area.cs";
        file = option.Output.CombinePath(file);
        file = file.GetBasePath();

        // 文件已存在，不要覆盖
        if (File.Exists(file)) return 0;

        // 根命名空间
        var root = FindProjectRootNamespace(option.Output);
        if (root.IsNullOrEmpty()) root = option.ConnName + "Web";

        if (Debug) XTrace.WriteLine("生成魔方区域 {0} {1}", areaName, file);

        var builder = new CubeBuilder
        {
            RootNamespace = root
        };

        var code = builder.AreaTemplate;

        //code = code.Replace("{Namespace}", option.Namespace);
        code = code.Replace("{RootNamespace}", builder.RootNamespace);
        code = code.Replace("{Name}", areaName);
        code = code.Replace("{DisplayName}", option.DisplayName);

        // 输出到文件
        file.EnsureDirectory(true);
        File.WriteAllText(file, code, Encoding.UTF8);

        return 1;
    }

    /// <summary>生成控制器</summary>
    /// <param name="tables">表集合</param>
    /// <param name="option">可选项</param>
    /// <returns></returns>
    public static Int32 BuildControllers(IList<IDataTable> tables, BuilderOption option = null)
    {
        if (option == null)
            option = new BuilderOption();
        else
            option = option.Clone();

        // 根命名空间
        var root = FindProjectRootNamespace(option.Output);
        if (root.IsNullOrEmpty()) root = option.ConnName + "Web";

        // 自动识别并修正区域名（主要是大小写）
        var areaName = FindAreaName(option.Output);
        if (areaName.IsNullOrEmpty()) areaName = option.ConnName;

        if (option.ClassNameTemplate.IsNullOrEmpty()) option.ClassNameTemplate = "{name}Controller";

        option.Output = option.Output.CombinePath("Controllers");

        if (Debug) XTrace.WriteLine("生成控制器 {0}", option.Output.GetBasePath());

        var count = 0;
        var n = tables.Count;
        foreach (var item in tables)
        {
            // 跳过排除项
            if (option.Excludes.Contains(item.Name)) continue;
            if (option.Excludes.Contains(item.TableName)) continue;

            var builder = new CubeBuilder
            {
                Table = item,
                Option = option.Clone(),

                AreaName = areaName,
                RootNamespace = root,
                Sort = n * 10,
            };
            if (Debug) builder.Log = XTrace.Log;

            if (builder.Option.BaseClass.IsNullOrEmpty())
            {
                if (item.InsertOnly)
                    builder.Option.BaseClass = "ReadOnlyEntityController";
                else
                    builder.Option.BaseClass = "EntityController";
            }

            builder.Load(item);

            builder.Execute();
            builder.Save(null, false, false);

            count++;
            n--;
        }

        return count;
    }

    static String FindAreaName(String dir)
    {
        var di = dir.AsDirectory();
        if (!di.Exists) return null;

        foreach (var fi in di.GetFiles("*Area.cs"))
        {
            var txt = File.ReadAllText(fi.FullName);
            var str = txt.Substring("public class", "AreaBase")?.Trim(' ', ':');
            if (!str.IsNullOrEmpty())
            {
                return str.TrimEnd("Area");
            }
        }

        return null;
    }

    /// <summary>在指定目录中查找项目名</summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    static String FindProjectRootNamespace(String dir)
    {
        var di = dir.AsDirectory();
        if (di.Exists)
        {
            foreach (var fi in di.GetFiles("*.csproj", SearchOption.TopDirectoryOnly))
            {
                var ns = Path.GetFileNameWithoutExtension(fi.FullName);

                var xml = File.ReadAllText(fi.FullName);
                if (!xml.IsNullOrEmpty())
                {
                    var str = xml.Substring("<RootNamespace>", "</RootNamespace>");
                    if (!str.IsNullOrEmpty()) ns = str;
                }

                if (!ns.IsNullOrEmpty()) return ns;
            }
        }

        di = di.Parent;
        if (di.Exists)
        {
            foreach (var fi in di.GetFiles("*.csproj", SearchOption.TopDirectoryOnly))
            {
                var ns = Path.GetFileNameWithoutExtension(fi.FullName);

                var xml = File.ReadAllText(fi.FullName);
                if (!xml.IsNullOrEmpty())
                {
                    var str = xml.Substring("<RootNamespace>", "</RootNamespace>");
                    if (!str.IsNullOrEmpty()) ns = str;
                }

                if (!ns.IsNullOrEmpty()) return ns;
            }
        }

        di = di.Parent;
        if (di.Exists)
        {
            foreach (var fi in di.GetFiles("*.csproj", SearchOption.TopDirectoryOnly))
            {
                var ns = Path.GetFileNameWithoutExtension(fi.FullName);

                var xml = File.ReadAllText(fi.FullName);
                if (!xml.IsNullOrEmpty())
                {
                    var str = xml.Substring("<RootNamespace>", "</RootNamespace>");
                    if (!str.IsNullOrEmpty()) ns = str;
                }

                if (!ns.IsNullOrEmpty()) return ns;
            }
        }

        return null;
    }
    #endregion

    #region 方法
    /// <summary>生成前</summary>
    protected override void OnExecuting()
    {
        var opt = Option;
        var code = ControllerTemplate;

        code = code.Replace("{EntityNamespace}", opt.Namespace);
        code = code.Replace("{ClassName}", ClassName);
        code = code.Replace("{EntityName}", Table.Name);
        code = code.Replace("{RootNamespace}", RootNamespace);
        code = code.Replace("{Name}", AreaName);
        code = code.Replace("{DisplayName}", Table.Description);
        code = code.Replace("{Sort}", Sort + "");

        code = code.Replace("{BaseClass}", GetBaseClass());

        if (Table.Columns.Any(c => c.Name.EqualIgnoreCase("TraceId")))
            code = code.Replace("//ListFields.TraceUrl(", "ListFields.TraceUrl(");

        Writer.Write(code);
    }

    /// <summary>生成后</summary>
    protected override void OnExecuted() { }

    /// <summary>生成主体</summary>
    protected override void BuildItems() { }
    #endregion

    #region 辅助
    ///// <summary>写入</summary>
    ///// <param name="value"></param>
    //protected override void WriteLine(String value = null)
    //{
    //    if (!value.IsNullOrEmpty() && value.Length > 2 && value[0] == '<' && value[1] == '/') SetIndent(false);

    //    base.WriteLine(value);

    //    if (!value.IsNullOrEmpty() && value.Length > 2 && value[0] == '<' && value[1] != '/' && !value.Contains("</")) SetIndent(true);
    //}
    #endregion
}