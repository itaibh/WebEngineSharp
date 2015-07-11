using System;

namespace WebEngineSharp.DOM.Impl
{
    public class Event : IEvent
    {
        public Event()
        {
        }

        public const ushort NONE = 0;
        public const ushort CAPTURING_PHASE = 1;
        public const ushort AT_TARGET = 2;
        public const ushort BUBBLING_PHASE = 3;

        #region IEvent implementation

        public void stopPropagation()
        {
            throw new NotImplementedException();
        }

        public void stopImmediatePropagation()
        {
            throw new NotImplementedException();
        }

        public void preventDefault()
        {
            throw new NotImplementedException();
        }

        public void initEvent(string type, bool bubbles, bool cancelable)
        {
            throw new NotImplementedException();
        }

        public string type {
            get {
                throw new NotImplementedException();
            }
        }

        public IEventTarget target {
            get {
                throw new NotImplementedException();
            }
        }

        public IEventTarget currentTarget {
            get {
                throw new NotImplementedException();
            }
        }

        public ushort eventPhase {
            get {
                throw new NotImplementedException();
            }
        }

        public bool bubbles {
            get {
                throw new NotImplementedException();
            }
        }

        public bool cancelable {
            get {
                throw new NotImplementedException();
            }
        }

        public bool defaultPrevented {
            get {
                throw new NotImplementedException();
            }
        }

        public bool isTrusted {
            get {
                throw new NotImplementedException();
            }
        }

        public DOMTimeStamp timeStamp {
            get {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

