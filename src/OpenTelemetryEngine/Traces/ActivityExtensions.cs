//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace OpenTelemetryEngine.Traces
{
        internal static class ActivityExtensions
    {

        private static readonly Action<Activity, string>? s_setSpanId;
        private static readonly Action<Activity, string>? s_setId;
        private static readonly Action<Activity, string>? s_setTraceId;
        private static readonly Action<Activity, string>? s_setRootId;

        static ActivityExtensions()
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            s_setSpanId = typeof(Activity).GetField("_spanId", flags)?.CreateSetter<Activity, string>();
            s_setId = typeof(Activity).GetField("_id", flags)?.CreateSetter<Activity, string>();
            s_setRootId = typeof(Activity).GetField("_rootId", flags)?.CreateSetter<Activity, string>();
            s_setTraceId = typeof(Activity).GetField("_traceId", flags)?.CreateSetter<Activity, string>();
        }

        public static void SetId(this Activity activity, string id)
        {
            if (s_setId is not null) 
            {
                s_setId(activity, id);
            }
        }

        public static void SetSpanId(this Activity activity, string spanId)
        {
            if (s_setSpanId is not null) 
            {
                s_setSpanId(activity, spanId);
            }
        }

        public static void SetRootId(this Activity activity, string rootId)
        {
            if (s_setRootId is not null) 
            {
                s_setRootId(activity, rootId);
            }
        }

        public static void SetTraceId(this Activity activity, string traceId)
        {
            if (s_setTraceId is not null) 
            {
                s_setTraceId(activity, traceId);
            }
        }
    }

    internal static class FieldInfoExtensionMethods
    {
        /// <summary>
        /// Create a re-usable setter for a <see cref="FieldInfo"/>.
        /// When cached and reused, This is quicker than using <see cref="FieldInfo.SetValue(object, object)"/>.
        /// </summary>
        /// <typeparam name="TTarget">The target type of the object.</typeparam>
        /// <typeparam name="TValue">The value type of the field.</typeparam>
        /// <param name="fieldInfo">The field info.</param>
        /// <returns>A re-usable action to set the field.</returns>
        internal static Action<TTarget, TValue> CreateSetter<TTarget, TValue>(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                throw new ArgumentNullException(nameof(fieldInfo));
            }

            ParameterExpression targetExp = Expression.Parameter(typeof(TTarget), "target");
            Expression source = targetExp;

            if (typeof(TTarget) != fieldInfo.DeclaringType && fieldInfo.DeclaringType is not null)
            {
                source = Expression.Convert(targetExp, fieldInfo.DeclaringType);
            }

            // Creating the setter to set the value to the field
            ParameterExpression valueExp = Expression.Parameter(typeof(TValue), "value");
            MemberExpression fieldExp = Expression.Field(source, fieldInfo);
            BinaryExpression assignExp = Expression.Assign(fieldExp, valueExp);
            return Expression.Lambda<Action<TTarget, TValue>>(assignExp, targetExp, valueExp).Compile();
        }
    }
}
