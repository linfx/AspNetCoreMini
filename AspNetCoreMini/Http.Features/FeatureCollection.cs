using System;
using System.Collections.Generic;

namespace AspNetCoreMini.Http.Features
{
    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection
    {
        private readonly IFeatureCollection _defaults;
        private volatile int _containerRevision;

        public FeatureCollection() { }

        public FeatureCollection(IFeatureCollection defaults) => _defaults = defaults;

        public int Revision => _containerRevision + (_defaults?.Revision ?? 0);

        public TFeature Get<TFeature>()
        {
            return (TFeature)this[typeof(TFeature)];
        }

        public void Set<TFeature>(TFeature instance)
        {
            this[typeof(TFeature)] = instance;
        }
    }
}
