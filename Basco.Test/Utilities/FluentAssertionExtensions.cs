namespace Basco.Test.Utilities
{
    using System;
    using System.Reflection;
    using FluentAssertions;
    using FluentAssertions.Events;
    using Moq;

    public static class FluentAssertionExtensions
    {
        public static EventRecorder ShouldRaise<T>(this T value, Action<T> eventCallback)
            where T : class
        {
            EventInfo eventInfo = eventCallback.GetEventInfo();
            return value.ShouldRaise(eventInfo.Name);
        }

        public static EventRecorder ShouldRaise<T>(this T value, Action<T> eventCallback, params object[] args)
            where T : class
        {
            EventInfo eventInfo = eventCallback.GetEventInfo(args);
            return value.ShouldRaise(eventInfo.Name);
        }

        public static void ShouldNotRaise<T>(this T value, Action<T> eventCallback)
            where T : class
        {
            EventInfo eventInfo = eventCallback.GetEventInfo();
            value.ShouldNotRaise(eventInfo.Name);
        }

        public static void ShouldNotRaise<T>(this T value, Action<T> eventCallback, params object[] args)
            where T : class
        {
            EventInfo eventInfo = eventCallback.GetEventInfo(args);
            value.ShouldNotRaise(eventInfo.Name);
        }

        private static EventInfo GetEventInfo<T>(this Action<T> eventCallback)
            where T : class
        {
            var m = new Mock<T>();
            return GetEventInfo(eventCallback, m);
        }

        private static EventInfo GetEventInfo<T>(this Action<T> eventCallback, params object[] args)
            where T : class
        {
            var m = new Mock<T>(args);
            return GetEventInfo(eventCallback, m);
        }

        private static EventInfo GetEventInfo<T>(Action<T> eventCallback, Mock<T> m)
            where T : class
        {
            var moqExtensionType = typeof(Mock).Assembly.GetType("Moq.Extensions");
            var getEventMethodInfo = moqExtensionType.GetMethod("GetEvent").MakeGenericMethod(typeof(T));
            return (EventInfo)getEventMethodInfo.Invoke(null, new object[] { eventCallback, m.Object });
        }
    }
}