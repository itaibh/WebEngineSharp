using System;

namespace WebEngineSharp.DOM
{
    // http://dev.w3.org/csswg/cssom/#stylesheet
	public class StyleSheet
    { 
        public string type { get; private set; }
        public string href { get; private set; }

        //(Element or ProcessingInstruction)
        public object ownerNode { get; private set; }

        public StyleSheet parentStyleSheet { get; private set; }
        public string title { get; private set; }

        //[SameObject, PutForwards=mediaText] 
        public MediaList media { get; private set; }

        public bool disabled { get; set; }
	}


}

