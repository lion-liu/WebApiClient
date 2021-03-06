﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.Defaults.KeyValueFormates
{
    /// <summary>
    /// 表示类型描述
    /// </summary>
    public sealed class TypeDescriptor
    {
        /// <summary>
        /// 描述缓存
        /// </summary>
        private static readonly ConcurrentDictionary<Type, TypeDescriptor> descriptorCache;

        /// <summary>
        /// 获取类型
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 获取类型是否为简单类型
        /// </summary>
        public bool IsSimpleType { get; private set; }

        /// <summary>
        /// 获取类型是否为可列举类型
        /// </summary>
        public bool IsEnumerable { get; private set; }

        /// <summary>
        /// 获取类型是否为IEnumerable(KeyValuePair(string, object))
        /// </summary>
        public bool IsEnumerableKeyValueOfObject { get; private set; }

        /// <summary>
        /// 获取类型是否为IEnumerable(KeyValuePair(string, string))
        /// </summary>
        public bool IsEnumerableKeyValueOfString { get; private set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        /// <param name="type">类型</param>
        private TypeDescriptor(Type type)
        {
            this.Type = type;
            this.IsSimpleType = type.IsSimple();
            this.IsEnumerable = type.IsInheritFrom<IEnumerable>();
            this.IsEnumerableKeyValueOfObject = type.IsInheritFrom<IEnumerable<KeyValuePair<string, object>>>();
            this.IsEnumerableKeyValueOfString = type.IsInheritFrom<IEnumerable<KeyValuePair<string, string>>>();
        }

        /// <summary>
        /// 静态构造器
        /// </summary>
        static TypeDescriptor()
        {
            descriptorCache = new ConcurrentDictionary<Type, TypeDescriptor>();
        }

        /// <summary>
        /// 获取类型的描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static TypeDescriptor GetDescriptor(Type type)
        {
            if (type == null)
            {
                return null;
            }
            return descriptorCache.GetOrAdd(type, (t) => new TypeDescriptor(t));
        }
    }
}
