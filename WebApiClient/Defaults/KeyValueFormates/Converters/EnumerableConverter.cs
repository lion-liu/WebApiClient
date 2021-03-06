﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.Defaults.KeyValueFormates.Converters
{
    /// <summary>
    /// 表示集合转换器
    /// </summary>
    public class EnumerableConverter : ConverterBase
    {
        /// <summary>
        /// 执行转换
        /// </summary>
        /// <param name="context">转换上下文</param>
        /// <returns></returns>
        public override IEnumerable<KeyValuePair<string, string>> Invoke(ConvertContext context)
        {
            if (context.Descriptor.IsEnumerable == true)
            {
                var array = context.Value as IEnumerable;
                return array.Cast<object>().SelectMany(item => this.SerializeByFormatter(context.Name, item, context.Options));

            }
            return this.Next.Invoke(context);
        }
    }
}
