using System;

namespace WebEngineSharp.DOM
{
	public interface IEvent
	{
        string type { get; }
        IEventTarget target { get; }
        IEventTarget currentTarget { get; }

        ushort eventPhase { get; }

        void stopPropagation();
        void stopImmediatePropagation();

        bool bubbles { get; }
        bool cancelable { get; }
        void preventDefault();
        bool defaultPrevented { get; }

        //[Unforgeable] 
        bool isTrusted { get; }
        DOMTimeStamp timeStamp { get; }

        void initEvent(string type, bool bubbles, bool cancelable);
	}


}

