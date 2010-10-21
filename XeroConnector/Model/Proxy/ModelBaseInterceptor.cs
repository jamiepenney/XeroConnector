using System;
using System.Linq;
using System.Reflection;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace XeroConnector.Model.Proxy
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    internal sealed class LazyLoadAttribute : Attribute
    {
    }

    public class ModelHook : IProxyGenerationHook
    {
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            if(IsGetterName(methodInfo.Name) || IsSetterName(methodInfo.Name) )
            {
                var propertyInfo = GetProperty(methodInfo);
                var customAttributes = propertyInfo.GetCustomAttributes(typeof(LazyLoadAttribute), true);
                if (customAttributes.Any())
                    return true;
            }
            
            if (methodInfo.Name == "LoadDetailedInformation") 
                return true;

            return false;
        }

        private static PropertyInfo GetProperty(MethodInfo method)
        {
            bool takesArg = method.GetParameters().Length == 1;
            bool hasReturn = method.ReturnType != typeof(void);
            if (takesArg == hasReturn) return null;
            if (takesArg)
            {
                return method.DeclaringType.GetProperties()
                    .Where(prop => prop.GetSetMethod() == method).FirstOrDefault();
            }
            else
            {
                return method.DeclaringType.GetProperties()
                    .Where(prop => prop.GetGetMethod() == method).FirstOrDefault();
            }
        }

        private bool IsGetterName(string name)
        {
            return name.StartsWith("get_", StringComparison.Ordinal);
        }

        private bool IsSetterName(string name)
        {
            return name.StartsWith("set_", StringComparison.Ordinal);
        }

        public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo) { }

        public void MethodsInspected() { }
    }

    public class ModelInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.InvocationTarget is IModel == false)
            {
                invocation.Proceed();
                return;
            }

            var model = (IModel)invocation.InvocationTarget;

            if (model.IsLoaded) 
            {
                invocation.Proceed();
                return; // Nothing to do. Don't re-run the loading.
            }

            if (invocation.Method.Name.StartsWith("set_"))
            {
                // One of the Lazy Loaded properties has been set. All bets are off.
                model.IsLoaded = true;
                invocation.Proceed();
                return;
            }

            if(invocation.Method.Name == "LoadDetailedInformation")
            {
                // This will happen if the user manually calls LoadDetailedInformation()
                model.IsLoaded = true;
                invocation.Proceed();
                return;
            }

            // Do lazy load
            model.IsLoaded = true;
            model.LoadDetailedInformation();
            invocation.Proceed();
        }
    }
}