﻿namespace Mvvmicro
{
    using System;
    using System.Linq;

    /// <summary>
    /// A helper for parsing a given url and extracting each components and query fluently.
    /// </summary>
    public class NavigationUrlBuilder
    {
        private NavigationUrlBuilder(NavigationUrl url)
        {
            this.Root = url.Segments.FirstOrDefault()?.Value;
            this.url = url;
        }

        public NavigationUrlBuilder(string root)
        {
            this.Root = root;
            this.url = new NavigationUrl(root);
        }

        public String Root { get; }

        private NavigationUrl url;

        public NavigationUrlBuilder WithSegment(string segment)
        {
            var newUrl = new NavigationUrl(url.Segments.Concat(new[] { new NavigationUrlSegment(segment) }).ToArray());
            return new NavigationUrlBuilder(newUrl);
        }

        public NavigationUrl Build(Action<NavigationUrlQuery> query = null)
        {
            var newUrl = new NavigationUrl(this.url.Segments);
            query?.Invoke(newUrl.Segments.Last().Query);
            return newUrl;
        }
    }
}