namespace QueryPack.DispatchProxy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base method invoker abstraction
    /// </summary>
    public interface IMethodInvoker { }

    /// <summary>
    /// Typed method invoker
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IMethodInvoker<TResult> : IMethodInvoker
    {
        /// <summary>
        /// Invoke target method
        /// </summary>
        TResult Invoke();
        /// <summary>
        /// Method name
        /// </summary>
        string MethodName { get; }
        /// <summary>
        /// Get method custom attributes collection
        /// </summary>
        IEnumerable<Attribute> CustomAttributes { get; }
    }
}