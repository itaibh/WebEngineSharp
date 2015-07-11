using System;
using System.Collections.Generic;
using WebEngineSharp.DOM;
using WebEngineSharp.DOM.Impl;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    // 8.2.5.4.1 http://www.w3.org/TR/html5/syntax.html#the-initial-insertion-mode
    public class InitialInsertionModeState : BaseInsertionModeState
    {
        private static InitialInsertionModeState s_Instance = new InitialInsertionModeState();

        private InitialInsertionModeState()
        {
        }

        public static InitialInsertionModeState Instance
        {
            get { return s_Instance; }
        }

        private static readonly HashSet<string> s_allowedDoctypeSets = new HashSet<string>() {
            @"<-//W3C//DTD HTML 4.0//EN>-<>",
            @"<-//W3C//DTD HTML 4.0//EN>-<http://www.w3.org/TR/REC-html40/strict.dtd>",
            @"<-//W3C//DTD HTML 4.01//EN>-<>",
            @"<-//W3C//DTD HTML 4.01//EN>-<http://www.w3.org/TR/html4/strict.dtd>",
            @"<-//W3C//DTD XHTML 1.0 Strict//EN>-<http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd>",
            @"<-//W3C//DTD XHTML 1.1//EN>-<http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd>"
        };

        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            if (IsWhitespace(token))
            {
                return this;
            }

            CommentToken commentToken = token as CommentToken;
            if (commentToken != null)
            {
                IComment commentNode = new Comment(doc, commentToken.Comment);
                doc.appendChild(commentNode);
                return this;
            }

            DocTypeToken docTypeToken = token as DocTypeToken;
            if (docTypeToken != null)
            {
                if (docTypeToken.Name != "html" ||
                    docTypeToken.PublicIdentifier != null ||
                    (docTypeToken.SystemIdentifier != null && docTypeToken.SystemIdentifier != "about:legacy-compat"))
                {
                    if (docTypeToken.Name == "html")
                    {
                        string pair = string.Concat("<", docTypeToken.PublicIdentifier, ">-<", docTypeToken.SystemIdentifier, ">");
                        if (!s_allowedDoctypeSets.Contains(pair))
                        {
                            ReportParseError();
                            return this;
                        }
                    }
                }

                IDocumentType docTypeNode = new DocumentType(doc, docTypeToken.Name, docTypeToken.PublicIdentifier, docTypeToken.SystemIdentifier);
                doc.appendChild(docTypeNode);

                // Append a DocumentType node to the Document node, 
                // with the name attribute set to the name given in the DOCTYPE token,
                //      or the empty string if the name was missing;
                // the publicId attribute set to the public identifier given in the DOCTYPE token,
                //      or the empty string if the public identifier was missing;
                // the systemId attribute set to the system identifier given in the DOCTYPE token,
                //      or the empty string if the system identifier was missing;

                // TODO - and the other attributes specific to DocumentType objects set to null and empty lists as appropriate.

                if (doc.nodeName != "iframe")
                {
                    if (docTypeToken.ForceQuirks ||
                        docTypeToken.Name != "html" ||
                        docTypeToken.PublicIdentifier.StartsWith(@"+//Silmaril//dtd html Pro v0r11 19970101//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//AdvaSoft Ltd//DTD HTML 3.0 asWedit + extensions//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//AS//DTD HTML 3.0 asWedit + extensions//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 2.0 Level 1//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 2.0 Level 2//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 2.0 Strict Level 1//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 2.0 Strict Level 2//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 2.0 Strict//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 2.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 2.1E//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 3.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 3.2 Final//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 3.2//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML 3//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Level 0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Level 1//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Level 2//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Level 3//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Strict Level 0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Strict Level 1//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Strict Level 2//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Strict Level 3//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML Strict//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//IETF//DTD HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Metrius//DTD Metrius Presentational//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Microsoft//DTD Internet Explorer 2.0 HTML Strict//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Microsoft//DTD Internet Explorer 2.0 HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Microsoft//DTD Internet Explorer 2.0 Tables//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Microsoft//DTD Internet Explorer 3.0 HTML Strict//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Microsoft//DTD Internet Explorer 3.0 HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Microsoft//DTD Internet Explorer 3.0 Tables//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Netscape Comm. Corp.//DTD HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Netscape Comm. Corp.//DTD Strict HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//O'Reilly and Associates//DTD HTML 2.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//O'Reilly and Associates//DTD HTML Extended 1.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//O'Reilly and Associates//DTD HTML Extended Relaxed 1.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//SoftQuad Software//DTD HoTMetaL PRO 6.0::19990601::extensions to HTML 4.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//SoftQuad//DTD HoTMetaL PRO 4.0::19971010::extensions to HTML 4.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Spyglass//DTD HTML 2.0 Extended//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//SQ//DTD HTML 2.0 HoTMetaL + extensions//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Sun Microsystems Corp.//DTD HotJava HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//Sun Microsystems Corp.//DTD HotJava Strict HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 3 1995-03-24//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 3.2 Draft//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 3.2 Final//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 3.2//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 3.2S Draft//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 4.0 Frameset//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 4.0 Transitional//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML Experimental 19960712//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML Experimental 970421//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD W3 HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3O//DTD W3 HTML 3.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.Equals(@"-//W3O//DTD W3 HTML Strict 3.0//EN//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//WebTechs//DTD Mozilla HTML 2.0//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//WebTechs//DTD Mozilla HTML//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.Equals(@"-/W3C/DTD HTML 4.0 Transitional/EN", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.Equals(@"HTML", StringComparison.Ordinal) ||
                        docTypeToken.SystemIdentifier.Equals(@"http://www.ibm.com/data/dtd/v11/ibmxhtml1-transitional.dtd", StringComparison.Ordinal) ||
                        (docTypeToken.SystemIdentifier == null && docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 4.01 Frameset//", StringComparison.Ordinal)) ||
                        (docTypeToken.SystemIdentifier == null && docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 4.01 Transitional//", StringComparison.Ordinal)))
                    {
                        // TODO - set the Document to quirks mode
                    } else if (
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD XHTML 1.0 Frameset//", StringComparison.Ordinal) ||
                        docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD XHTML 1.0 Transitional//", StringComparison.Ordinal) ||
                        (docTypeToken.SystemIdentifier != null && docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 4.01 Frameset//", StringComparison.Ordinal)) ||
                        (docTypeToken.SystemIdentifier != null && docTypeToken.PublicIdentifier.StartsWith(@"-//W3C//DTD HTML 4.01 Transitional//", StringComparison.Ordinal)))
                    {
                        // TODO - set the Document to limited-quirks mode
                    }
                    return BeforeHTMLInsertionModeState.Instance;
                }
            }

            // TODO: If the document is not an iframe srcdoc document, then this is a parse error; set the Document to quirks mode.
            //if (this.Document.Type != "srcdoc")
            //{
            // TODO - set the Document to quirks mode
            //}

            // In any case, switch the insertion mode to "before html", then reprocess the token.
            queue.EnqueueTokenForReprocessing(token);
            return BeforeHTMLInsertionModeState.Instance;
        }
    }
}