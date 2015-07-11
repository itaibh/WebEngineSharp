using System;

namespace WebEngineSharp.DOM
{
    // 4.6 http://www.w3.org/TR/dom/#interface-eventtarget
	public interface IEventListener
	{
        void handleEvent(IEvent @event);
	}

}

