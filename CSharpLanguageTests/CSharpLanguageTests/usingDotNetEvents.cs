using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpLanguageTests
{
    public class usingDotNetEvents
    {
        [TestFixture]
        public class Testing
        {
            [Test]
            public void tryingToRaiseAnEvent()
            {
                var bus = new EventBus();

                bus.LightningBolt += subscribingMethod;

                var @event = new MyEvent();
                bus.publish(@event);

                @event.SubscriberWasCalled.Should().BeTrue();
            }

            public void subscribingMethod(MyEvent @event)
            {
                @event.SubscriberWasCalled = true;
            }
        }

        // Special EventArgs class to hold info about Shapes. 
        public class MyEvent
        {
            public bool SubscriberWasCalled { get; set; }
        }

        // Base class event publisher 
        public class EventBus
        {
            public event Action<MyEvent> LightningBolt;

            public void publish(MyEvent e)
            {
                LightningBolt(e);
            }
        } 
    }
}