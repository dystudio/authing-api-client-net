﻿using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authing.ApiClient.Mgmt
{
    public partial class ManagementClient
    {
        /// <summary>
        /// 自定义字段管理模块
        /// </summary>
        public UdfManagementClient Udf { get; private set; }

        /// <summary>
        /// 自定义字段管理类
        /// <br />
        /// Udf 是 User Defined Field（用户自定义字段） 的简称。Authing 的数据实体（如用户、角色、分组、组织机构等）可以添加自定义字段，
        /// 你可以配置 Authing 默认不自带的字段，比如你需要创建以一个学校相关的应用，就可以添加一个自定义 \`school\` 字段。
        /// 同时你可以在用户注册完成之后要求用户补充此字段的信息，详细文档请见 https://docs.authing.co/extensibility/user/extend-register-fields.html 。
        /// </summary>
        public class UdfManagementClient
        {
            private readonly ManagementClient client;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="client"></param>
            public UdfManagementClient(ManagementClient client)
            {
                this.client = client;
            }

            /// <summary>
            /// 设置自定义字段元数据，如果字段不存在则会创建，存在会更新
            /// </summary>
            /// <param name="type">自定义字段目标类型， USER 表示用户、ROLE 表示角色。</param>
            /// <param name="key">字段 key</param>
            /// <param name="dataType">数据类型，目前共支持五种数据类型。STRING 为字符串、NUMBER 为数字、DATETIME 为日期、BOOLEAN 为 boolean 值、OBJECT 为对象。</param>
            /// <param name="label">字段 Label，一般是一个 Human Readable 字符串。</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<UserDefinedField> Set(
                UdfTargetType type,
                string key,
                UdfDataType dataType,
                string label,
                CancellationToken cancellationToken = default) 
            {
                var param = new SetUdfParam(type, key, dataType, label);

                await client.GetAccessToken();
                var res = await client.Request<SetUdfResponse>(param.CreateRequest(), cancellationToken);
                return res.Result;
            }

            /// <summary>
            /// 删除自定义字段
            /// </summary>
            /// <param name="type">自定义字段目标类型， USER 表示用户、ROLE 表示角色。</param>
            /// <param name="key">字段 key</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<CommonMessage> Remove(
                UdfTargetType type,
                string key,
                CancellationToken cancellationToken = default)
            {
                var param = new RemoveUdfParam(type, key);

                await client.GetAccessToken();
                var res = await client.Request<RemoveUdfResponse>(param.CreateRequest(), cancellationToken);
                return res.Result;
            }

            /// <summary>
            /// 获取自定义字段列表
            /// </summary>
            /// <param name="type">自定义字段目标类型， USER 表示用户、ROLE 表示角色。</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<IEnumerable<UserDefinedField>> List(
                UdfTargetType type,
                CancellationToken cancellationToken = default)
            {
                var param = new UdfParam(type);

                await client.GetAccessToken();
                var res = await client.Request<UdfResponse>(param.CreateRequest(), cancellationToken);
                return res.Result;
            }
        }
    }
}
