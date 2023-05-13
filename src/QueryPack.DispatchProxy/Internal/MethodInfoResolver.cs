namespace QueryPack.DispatchProxy.Internal
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class MethodInfoResolver : ExpressionVisitor
    {
        private MethodInfo _methodInfo;

        private readonly Type _targetType;

        protected MethodInfoResolver(Type targetType)
        {
            _targetType = targetType;
        }

        public MethodInfo Resolve(Expression method)
        {
            Visit(method);
            return _methodInfo;
        }

        public static MethodInfo Resolve<T>(Expression method) where T : class
             => new MethodInfoResolver(typeof(T)).Resolve(method);

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (_methodInfo == null)
            {
                if (node.Value is MethodInfo methodInfo)
                {
                    if (methodInfo.DeclaringType == _targetType)
                    {
                        _methodInfo = methodInfo;
                    }
                }
            }

            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (_methodInfo == null)
            {
                if (node.Method?.DeclaringType == _targetType)
                {
                    _methodInfo = node.Method;
                }
                else
                    Visit(node.Object);
            }

            return base.VisitMethodCall(node);
        }
    }
}