﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Export
{
    /// <summary>
    /// 批量导出类接口提供批量导出轨迹数据功能，该类接口将一段时间内的轨迹数据打包成文件，生成下载链接供开发者下载。该类接口包括三个 API：
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/trackexport">批量导出类</a>
    /// </summary>
    public partial class ExportApi
    {
        private YingyanApi framework;
        private const string url = "export/";

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportApi"/> class.
        /// </summary>
        /// <param name="framework">The framework.</param>
        public ExportApi(YingyanApi framework)
        {
            this.framework = framework;
        }

        /// <summary>
        /// 	创建一个任务，该任务完成后将返回文件下载地址，供开发者下载
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<ExportCreatJobResult> createjob(ExportCreatJobParam param)
        {
            return framework.post<ExportCreatJobResult>(url + "createjob", param);
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="job_id">任务id</param>
        /// <returns></returns>
        public Task<CommonResult> deletejob(string job_id)
        {
            return framework.post<CommonResult>(url + "deletejob", new Dictionary<string, string> { ["job_id"] = job_id });
        }

        /// <summary>
        /// 查询任务，将返回任务状态和文件下载地址
        /// </summary>
        /// <returns></returns>
        public Task<ExportGetJobResult> getjob()
        {
            return framework.get<ExportGetJobResult>(url + "getjob");
        }
    }
}
