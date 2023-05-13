namespace QueryPack.DispatchProxy.Internal
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class MethodFactory
    {
        internal static Func<object, object[], T> CreateGenericMethod<T>(MethodInfo method)
        {
            var instanceParameter = Expression.Parameter(typeof(object), "target");
            var argumentsParameter = Expression.Parameter(typeof(object[]), "arguments");

            MethodCallExpression call;

            if (method.IsStatic)
            {
                call = Expression.Call(null,
                              method,
                              CreateParameterExpressions(method, argumentsParameter));
            }
            else
            {
                call = Expression.Call(
                  Expression.Convert(instanceParameter, method.DeclaringType),
                  method,
                  CreateParameterExpressions(method, argumentsParameter));
            }

            var lambda = Expression.Lambda<Func<object, object[], T>>(
                Expression.Convert(call, typeof(T)),
                instanceParameter,
                argumentsParameter);

            return lambda.Compile();
        }

        private static Expression[] CreateParameterExpressions(MethodInfo method, Expression argumentsParameter)
        {
            return method.GetParameters().Select((parameter, index) =>
                Expression.Convert(
                    Expression.ArrayIndex(argumentsParameter, Expression.Constant(index)),
                    parameter.ParameterType)).ToArray();
        }
    }

}