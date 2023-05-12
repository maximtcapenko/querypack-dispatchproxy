namespace QueryPack.DispatchProxy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class MethodFactory
    {
        public static Func<object, object[], T> CreateGenericMethod<T>(MethodInfo method)
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

        private static List<Expression> GetSetterExpressionTree(MemberExpression memberExpression)
        {
            var tree = new Stack<Expression>();
            do
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string))
                {
                    var checkExpression = Expression.Equal(memberExpression, Expression.Constant(null));
                    var expression = Expression.IfThen(checkExpression, Expression.Assign(memberExpression, Expression.MemberInit(Expression.New(propertyInfo.PropertyType))));
                    tree.Push(expression);
                }

                memberExpression = memberExpression.Expression as MemberExpression;
            }
            while (memberExpression != null);

            return tree.ToList();
        }

        private static List<Expression> GetGetterExpressionTree(MemberExpression memberExpression)
        {
            var type = (memberExpression.Member as PropertyInfo).PropertyType;
            var returnTarget = Expression.Label(type);
            var steps = new Stack<Expression>();

            steps.Push(Expression.Label(returnTarget, Expression.Default(type)));
            steps.Push(Expression.Return(returnTarget, memberExpression, type));

            do
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string))
                {
                    var checkExpression = Expression.Equal(memberExpression, Expression.Constant(null));
                    var expression = Expression.IfThen(checkExpression, Expression.Return(returnTarget, Expression.Default(type), type));
                    steps.Push(expression);
                }

                memberExpression = memberExpression.Expression as MemberExpression;
            }
            while (memberExpression != null);

            return steps.ToList();
        }
    }

}