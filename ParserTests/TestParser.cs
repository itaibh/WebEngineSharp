using System;
using System.IO;
using NUnit.Framework;
using WebEngineSharp.DOM;
using WebEngineSharp.Parser;
using WebEngineSharp.Tokenizer;

namespace ParserTests
{
    [TestFixture]
    public class TestParser
    {
        [Test]
        public void BasicStructure_HTML4()
        {
            Stream stream = File.OpenRead(Path.Combine("Resources", "BasicStructure_HTML4.html"));

            //TreeConstruction.Instance.

            HtmlTokenizer tokenizer = new HtmlTokenizer();
            IDocument doc = tokenizer.Tokenize(stream);

            //IDocument doc = HtmlParser.Parse(stream); 
        }

        public void BasicStructure_HTML5()
        {

        }
    }
}

