﻿using Castle.DynamicProxy;
using Core.Aspect.Autofac.Performance;
using System;
using System.Linq;
using System.Reflection;
using static Core.Utilities.Interceptors.Class1;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
           
           // classAttributes.Add(new PerformanceAspect(typeof()));

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
