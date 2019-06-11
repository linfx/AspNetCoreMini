namespace AspNetCoreMini.Http.Features
{
    /// <summary>
    /// Feature对象引用，用来保存对应的Feature实例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct FeatureReference<T>
    {
        private T _feature;
        private int _revision;

        private FeatureReference(T feature, int revision)
        {
            _feature = feature;
            _revision = revision;
        }

        public static readonly FeatureReference<T> Default = new FeatureReference<T>(default, -1);

        public T Fetch(IFeatureCollection features)
        {
            if (_revision == features.Revision)
            {
                return _feature;
            }
            _feature = (T)features[typeof(T)];
            _revision = features.Revision;
            return _feature;
        }

        public T Update(IFeatureCollection features, T feature)
        {
            features[typeof(T)] = feature;
            _feature = feature;
            _revision = features.Revision;
            return feature;
        }
    }
}
