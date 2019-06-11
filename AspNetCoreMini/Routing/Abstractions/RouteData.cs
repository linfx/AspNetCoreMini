using AspNetCoreMini.Http;
using System;
using System.Collections.Generic;

namespace AspNetCoreMini.Routing
{
    public class RouteData
    {
        private List<IRouter> _routers;
        private RouteValueDictionary _dataTokens;
        private RouteValueDictionary _values;

        public RouteData()
        {
            // Perf: Avoid allocating collections unless needed.
        }

        public RouteData(RouteData other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            // Perf: Avoid allocating collections unless we need to make a copy.
            if (other._routers != null)
            {
                _routers = new List<IRouter>(other.Routers);
            }

            if (other._dataTokens != null)
            {
                _dataTokens = new RouteValueDictionary(other._dataTokens);
            }

            if (other._values != null)
            {
                _values = new RouteValueDictionary(other._values);
            }
        }

        public RouteData(RouteValueDictionary values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            _values = values;
        }

        /// <summary>
        /// Gets the data tokens produced by routes on the current routing path.
        /// </summary>
        public RouteValueDictionary DataTokens
        {
            get
            {
                if (_dataTokens == null)
                {
                    _dataTokens = new RouteValueDictionary();
                }

                return _dataTokens;
            }
        }

        /// <summary>
        /// Gets the list of <see cref="IRouter"/> instances on the current routing path.
        /// </summary>
        public IList<IRouter> Routers
        {
            get
            {
                if (_routers == null)
                {
                    _routers = new List<IRouter>();
                }

                return _routers;
            }
        }

        /// <summary>
        /// Gets the values produced by routes on the current routing path.
        /// </summary>
        public RouteValueDictionary Values
        {
            get
            {
                if (_values == null)
                {
                    _values = new RouteValueDictionary();
                }

                return _values;
            }
        }
    }
}