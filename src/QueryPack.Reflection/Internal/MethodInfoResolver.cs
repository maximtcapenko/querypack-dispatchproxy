namespace QueryPack.Reflection.Internal
{
    using System.Linq.Expressions;
    using System.Reflection;
    
    internal class MethodInfoResolver : ExpressionVisitor
    {
        private MethodInfo _methodInfo;

        public MethodInfo Resolve(Expression method)
        {
            Visit(method);
            return _methodInfo;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var @const = node.Object as ConstantExpression;
            var value = @const.Value;
            if (value != null)
            {
                _methodInfo = value as MethodInfo;
            }

            return base.VisitMethodCall(node);
        }
    }
}