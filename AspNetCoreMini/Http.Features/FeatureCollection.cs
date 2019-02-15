using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreMini.Http.Features
{
    public class FeatureCollection : IFeatureCollection
    {
        //private static KeyComparer FeatureKeyComparer = new KeyComparer();
        private readonly IFeatureCollection _defaults;
        private IDictionary<Type, object> _features;
        private volatile int _containerRevision;

        public FeatureCollection() { }

        public FeatureCollection(IFeatureCollection defaults) => _defaults = defaults;

        public bool IsReadOnly => false;

        public int Revision => _containerRevision + (_defaults?.Revision ?? 0);

        public object this[Type key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                return _features != null && _features.TryGetValue(key, out object result) ? result : _defaults?[key];
            }
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (value == null)
                {
                    if (_features != null && _features.Remove(key))
                    {
                        _containerRevision++;
                    }
                    return;
                }

                if (_features == null)
                {
                    _features = new Dictionary<Type, object>();
                }
                _features[key] = value;
                _containerRevision++;
            }
        }

        public TFeature Get<TFeature>()
        {
            return (TFeature)this[typeof(TFeature)];
        }

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
        {
            if (_features != null)
            {
                foreach (var pair in _features)
                {
                    yield return pair;
                }
            }

            if (_defaults != null)
            {
                // Don't return features masked by the wrapper.
                foreach (var pair in _features == null ? _defaults : _defaults.Except(_features, FeatureKeyComparer))
                {
                    yield return pair;
                }
            }
        }

        public void Set<TFeature>(TFeature instance)
        {
            this[typeof(TFeature)] = instance;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
