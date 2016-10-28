// <copyright file="ResourceTypeResolverTests.cs" company="natsnudasoft">
// Copyright (c) Adrian John Dunstan. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace AdiePlaygroundTests.Common
{
    using System;
    using AdiePlayground.Common;
    using NUnit.Framework;
    using Properties;

    [TestFixture]
    public sealed class ResourceTypeResolverTests
    {
        private const string ResolveResourceResourceTypeParam = "resourceType";
        private const string ResolveResourceResourceNameParam = "resourceName";
        private const string ResourceName = "TestResource";

        [Test]
        public void ResolveResource_NullResourceType_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                ResourceTypeResolver.ResolveResource<string>(null, ResourceName));
            Assert.That(ex.ParamName, Is.EqualTo(ResolveResourceResourceTypeParam));
        }

        [Test]
        public void ResolveResource_NullResourceName_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                ResourceTypeResolver.ResolveResource<string>(typeof(Resources), null));
            Assert.That(ex.ParamName, Is.EqualTo(ResolveResourceResourceNameParam));
        }

        [Test]
        public void ResolveResource_EmptyResourceName_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                ResourceTypeResolver.ResolveResource<string>(typeof(Resources), string.Empty));
            Assert.That(ex.ParamName, Is.EqualTo(ResolveResourceResourceNameParam));
        }

        [Test]
        public void ResolveResource_ResourceNotFound()
        {
            Assert.Throws<InvalidOperationException>(() =>
                ResourceTypeResolver.ResolveResource<string>(typeof(Resources), "InvalidResource"));
        }

        [Test]
        public void ResolveResource_ResourceFound()
        {
            var resource =
                ResourceTypeResolver.ResolveResource<string>(typeof(Resources), ResourceName);
            Assert.That(resource, Is.Not.Null);
            Assert.That(resource, Is.EqualTo("This is a test resource."));
        }
    }
}
