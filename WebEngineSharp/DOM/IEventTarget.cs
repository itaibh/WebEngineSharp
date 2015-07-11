using System;

namespace WebEngineSharp.DOM
{
    // 4.6 http://www.w3.org/TR/dom/#interface-eventtarget
    //[Exposed=Window,Worker]
    public interface IEventTarget {
        void addEventListener(string type, IEventListener callback, bool capture = false);
        void removeEventListener(string type, IEventListener callback, bool capture = false);
        bool dispatchEvent(IEvent @event);
    }
}

