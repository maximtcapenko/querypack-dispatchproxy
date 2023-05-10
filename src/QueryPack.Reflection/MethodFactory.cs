namespace QueryPack.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class MethodFactory
    {
        public static Func<object, object[], T> CreateGenericMethod<T>(MethodInfo method)
        {
            var instanceParameter = Expression.Parameter(typeof(object), "target");
            var argumentsParameter = Expression.Parameter(typeof(object[]), "arguments");

            var call = Expression.Call(
                Expression.Convert(instanceParameter, method.DeclaringType),
                method,
                CreateParameterExpressions(method, argumentsParameter));

            var lambda = Expression.Lambda<Func<object, object[], T>>(
                Expression.Convert(call, typeof(T)),
                instanceParameter,
                argumentsParameter);

            return lambda.Compile();
        }

        public static Action<TEntity, TProperty> CreateSetter<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> selector)
        {
            var param = Expression.Parameter(typeof(TProperty));
            var tree = GetSetterExpressionTree(selector.Body as MemberExpression);

            tree.Add(Expression.Invoke(CreateSetterInternal(selector), selector.Parameters.First(), param));

            var block = Expression.Block(tree);

            return Expression.Lambda<Action<TEntity, TProperty>>(block, selector.Parameters.First(), param).Compile();
        }

        public static Func<TEntity, TProperty> CreateGetter<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> selector)
        {
            var tree = GetGetterExpressionTree(selector.Body as MemberExpression);
            var block = Expression.Block(tree);

            return Expression.Lambda<Func<TEntity, TProperty>>(block, selector.Parameters.First()).Compile();
        }

        private static Expression<Action<TEntity, TProperty>> CreateSetterInternal<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> selector)
        {
            var valueParam = Expression.Parameter(typeof(TProperty));
            var body = Expression.Assign(selector.Body, valueParam);
            return Expression.Lambda<Action<TEntity, TProperty>>(body, selector.Parameters.Single(), valueParam);
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