using System;

namespace WebEngineSharp.DOM
{
    // http://dev.w3.org/csswg/cssom/#medialist
    public class MediaList
    {
        //[TreatNullAs=EmptyString] stringifier attribute
        public string mediaText { get; set; }

        public ulong length { get; private set; }

        public string item(ulong index)
        {
            throw new NotImplementedException();
        }

        public string this[ulong index]
        {
            get{ return item(index); }
        }

        public void appendMedium(string medium)
        {
        }

        public void deleteMedium(string medium)
        {
        }
    }
}

