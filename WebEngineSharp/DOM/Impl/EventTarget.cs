using System;

namespace WebEngineSharp.DOM.Impl
{
    public class EventTarget : IEventTarget
	{
        #region IEventTarget implementation
        public void addEventListener(string type, IEventListener callback, bool capture = false)
        {
            throw new NotImplementedException();
        }
        public void removeEventListener(string type, IEventListener callback, bool capture = false)
        {
            throw new NotImplementedException();
        }
        public bool dispatchEvent(IEvent @event)
        {
            throw new NotImplementedException();
        }
        #endregion
	}

}

