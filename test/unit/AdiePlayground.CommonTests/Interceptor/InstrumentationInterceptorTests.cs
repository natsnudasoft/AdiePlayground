// <copyright file="InstrumentationInterceptorTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Interceptor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Common.Interceptor;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class InstrumentationInterceptorTests
    {
        private const int ReturnValue = 6;
        private const int AsyncReturnValue = 12;
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorInvocationCounterParam = "invocationCounter";
        private const string ConstructorInvocationTimerParam = "invocationTimer";
        private const string ConstructorRegistrarsParam = "registrars";
        private const string ConstructorGuidProviderParam = "guidProvider";
#pragma warning restore CC0021 // Use nameof
        private static readonly Guid Guid = new Guid("{75DB5BE4-366D-4425-8086-F25F8E175475}");

        private MethodInvocationCounter invocationCounter;
        private MethodInvocationTimer invocationTimer;
        private Mock<IRegistrar> registrarMock;
        private Mock<IGuidProvider> guidProviderMock;

        [SetUp]
        public void BeforeTest()
        {
            this.invocationCounter = new MethodInvocationCounter();
            this.invocationTimer = new MethodInvocationTimer();
            this.registrarMock = new Mock<IRegistrar>();
            this.guidProviderMock = new Mock<IGuidProvider>();
            this.guidProviderMock
                .Setup(g => g.NewGuid())
                .Returns(Guid);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1702:CompoundWordsShouldBeCasedCorrectly",
            MessageId = "CounterArgument",
            Justification = "This is not a casing exception.")]
        [Test]
        public void Constructor_NullInvocationCounter_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new InstrumentationInterceptor(
                null,
                this.invocationTimer,
                new[] { this.registrarMock.Object },
                this.guidProviderMock.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorInvocationCounterParam));
        }

        [Test]
        public void Constructor_NullInvocationTimer_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new InstrumentationInterceptor(
                this.invocationCounter,
                null,
                new[] { this.registrarMock.Object },
                this.guidProviderMock.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorInvocationTimerParam));
        }

        [Test]
        public void Constructor_NullRegistrars_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new InstrumentationInterceptor(
                this.invocationCounter,
                this.invocationTimer,
                null,
                this.guidProviderMock.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorRegistrarsParam));
        }

        [Test]
        public void Constructor_NullGuidProvider_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new InstrumentationInterceptor(
                this.invocationCounter,
                this.invocationTimer,
                new[] { this.registrarMock.Object },
                null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorGuidProviderParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => this.CreateInstrumentationInterceptor());
        }

        [Test]
        public void Intercept_VoidReturnInvocation()
        {
            var instrumentationInterceptor = this.CreateInstrumentationInterceptor();
            var invocationMock = InvocationMockHelper.MockVoidReturnInvocation();

            instrumentationInterceptor.Intercept(invocationMock.Object);

            this.registrarMock.Verify(r => r.Register(Guid, It.IsAny<string>()), Times.Exactly(2));
            this.guidProviderMock.Verify(g => g.NewGuid(), Times.Once());
            invocationMock.Verify(i => i.Proceed(), Times.Once());
            invocationMock.VerifyGet(i => i.ReturnValue, Times.Never());
            Assert.That(invocationMock.Object.ReturnValue, Is.Null);
        }

        [Test]
        public void Intercept_IntReturnInvocation()
        {
            var instrumentationInterceptor = this.CreateInstrumentationInterceptor();
            var invocationMock = InvocationMockHelper.MockIntReturnInvocation(ReturnValue);

            instrumentationInterceptor.Intercept(invocationMock.Object);

            this.registrarMock.Verify(r => r.Register(Guid, It.IsAny<string>()), Times.Exactly(2));
            this.guidProviderMock.Verify(g => g.NewGuid(), Times.Once());
            invocationMock.Verify(i => i.Proceed(), Times.Once());
            invocationMock.VerifyGet(i => i.ReturnValue, Times.Exactly(2));
            Assert.That(invocationMock.Object.ReturnValue, Is.EqualTo(ReturnValue));
        }

        [Test]
        public void Intercept_AsyncVoidReturnInvocation()
        {
            var instrumentationInterceptor = this.CreateInstrumentationInterceptor();
            var invocationMock = InvocationMockHelper.MockAsyncVoidReturnInvocation();

            using (var resetEvent = new ManualResetEventSlim())
            {
                RegistrarMockCountResetEvent(this.registrarMock, resetEvent, 3);
                instrumentationInterceptor.Intercept(invocationMock.Object);
                resetEvent.Wait();
            }

            this.registrarMock.Verify(r => r.Register(Guid, It.IsAny<string>()), Times.Exactly(3));
            this.guidProviderMock.Verify(g => g.NewGuid(), Times.Once());
            invocationMock.Verify(i => i.Proceed(), Times.Once());
            invocationMock.VerifyGet(i => i.ReturnValue, Times.Once());
            var returnValue = invocationMock.Object.ReturnValue;
            Assert.That(returnValue, Is.AssignableTo<Task>());
            Assert.That(((Task)returnValue).Status, Is.EqualTo(TaskStatus.RanToCompletion));
        }

        [Test]
        public void Intercept_AsyncVoidReturnCanceledInvocation()
        {
            var instrumentationInterceptor = this.CreateInstrumentationInterceptor();
            var invocationMock = InvocationMockHelper.MockAsyncVoidReturnCanceledInvocation();

            using (var resetEvent = new ManualResetEventSlim())
            {
                RegistrarMockCountResetEvent(this.registrarMock, resetEvent, 3);
                instrumentationInterceptor.Intercept(invocationMock.Object);
                resetEvent.Wait();
            }

            this.registrarMock.Verify(r => r.Register(Guid, It.IsAny<string>()), Times.Exactly(3));
            this.guidProviderMock.Verify(g => g.NewGuid(), Times.Once());
            invocationMock.Verify(i => i.Proceed(), Times.Once());
            invocationMock.VerifyGet(i => i.ReturnValue, Times.Once());
            var returnValue = invocationMock.Object.ReturnValue;
            Assert.That(returnValue, Is.AssignableTo<Task>());
            Assert.That(((Task)returnValue).Status, Is.EqualTo(TaskStatus.Canceled));
        }

        [Test]
        public void Intercept_AsyncVoidReturnFaultedInvocation()
        {
            var instrumentationInterceptor = this.CreateInstrumentationInterceptor();
            var invocationMock = InvocationMockHelper.MockAsyncVoidReturnFaultedInvocation();

            using (var resetEvent = new ManualResetEventSlim())
            {
                RegistrarMockCountResetEvent(this.registrarMock, resetEvent, 3);
                instrumentationInterceptor.Intercept(invocationMock.Object);
                resetEvent.Wait();
            }

            this.registrarMock.Verify(r => r.Register(Guid, It.IsAny<string>()), Times.Exactly(3));
            this.guidProviderMock.Verify(g => g.NewGuid(), Times.Once());
            invocationMock.Verify(i => i.Proceed(), Times.Once());
            invocationMock.VerifyGet(i => i.ReturnValue, Times.Once());
            var returnValue = invocationMock.Object.ReturnValue;
            Assert.That(returnValue, Is.AssignableTo<Task>());
            Assert.That(((Task)returnValue).Status, Is.EqualTo(TaskStatus.Faulted));
        }

        [Test]
        public void Intercept_AsyncIntReturnInvocation()
        {
            var instrumentationInterceptor = this.CreateInstrumentationInterceptor();
            var invocationMock =
                InvocationMockHelper.MockAsyncIntReturnInvocation(AsyncReturnValue);

            using (var resetEvent = new ManualResetEventSlim())
            {
                RegistrarMockCountResetEvent(this.registrarMock, resetEvent, 3);
                instrumentationInterceptor.Intercept(invocationMock.Object);
                resetEvent.Wait();
            }

            this.registrarMock.Verify(r => r.Register(Guid, It.IsAny<string>()), Times.Exactly(3));
            this.guidProviderMock.Verify(g => g.NewGuid(), Times.Once());
            invocationMock.Verify(i => i.Proceed(), Times.Once());
            invocationMock.VerifyGet(i => i.ReturnValue, Times.Once());
            var returnValue = invocationMock.Object.ReturnValue;
            Assert.That(returnValue, Is.TypeOf<Task<int>>());
            Assert.That(((Task<int>)returnValue).Result, Is.EqualTo(AsyncReturnValue));
        }

        private static void RegistrarMockCountResetEvent(
            Mock<IRegistrar> registrarMock,
            ManualResetEventSlim resetEvent,
            int count)
        {
            var currentCount = 0;
            registrarMock
                .Setup(r => r.Register(Guid, It.IsAny<string>()))
                .Callback(() =>
                {
                    if (Interlocked.Increment(ref currentCount) >= count)
                    {
                        resetEvent.Set();
                    }
                });
        }

        private InstrumentationInterceptor CreateInstrumentationInterceptor()
        {
            return new InstrumentationInterceptor(
                this.invocationCounter,
                this.invocationTimer,
                new[] { this.registrarMock.Object },
                this.guidProviderMock.Object);
        }
    }
}
