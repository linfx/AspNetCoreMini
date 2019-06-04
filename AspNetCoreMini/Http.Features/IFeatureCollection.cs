using System;
using System.Collections.Generic;

namespace AspNetCoreMini.Http.Features
{
    /// <summary>
    /// Represents a collection of HTTP features.
    /// </summary>
    public interface IFeatureCollection : IDictionary<Type, object>
    {
        /// <summary>
        /// Incremented for each modification and can be used to verify cached results.
        /// </summary>
        int Revision { get; }

        /// <summary>
        /// Retrieves the requested feature from the collection.
        /// </summary>
        /// <typeparam name="TFeature">The feature key.</typeparam>
        /// <returns>The requested feature, or null if it is not present.</returns>
        TFeature Get<TFeature>();

        /// <summary>
        /// Sets the given feature in the collection.
        /// </summary>
        /// <typeparam name="TFeature">The feature key.</typeparam>
        /// <param name="instance">The feature value.</param>
        void Set<TFeature>(TFeature instance);
    }
}
