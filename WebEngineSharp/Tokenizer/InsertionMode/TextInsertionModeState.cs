using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    //8.2.5.4.8 http://www.w3.org/TR/html5/syntax.html#parsing-main-incdata
    class TextInsertionModeState :BaseInsertionModeState
    {
        #region singleton

        private TextInsertionModeState()
        {
        }

        private static TextInsertionModeState s_Instance = new TextInsertionModeState();

        public static TextInsertionModeState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            CharacterToken characterToken = token as CharacterToken;
            if (characterToken != null)
            {
                InsertCharacter(characterToken, doc);
                return this;
            }

            if (token is EndOfFileToken)
            {
                ReportParseError();
                //TODO - If the current node is a script element, mark the script element as "already started".
                //TODO - Pop the current node off the stack of open elements.
                return TreeConstruction.Instance.GetOriginalInsertionModeState();
            }

            EndTagToken endTagToken = token as EndTagToken;
            if (endTagToken != null && endTagToken.TagName == "script")
            {
                /* TODO:
                Perform a microtask checkpoint. (http://www.w3.org/TR/html5/webappapis.html#perform-a-microtask-checkpoint)
                Provide a stable state. (http://www.w3.org/TR/html5/webappapis.html#provide-a-stable-state)
                Let script be the current node (which will be a script element).
                Pop the current node off the stack of open elements.
              v Switch the insertion mode to the original insertion mode.
                Let the old insertion point have the same value as the current insertion point. Let the insertion point be just before the next input character.
                Increment the parser's script nesting level (http://www.w3.org/TR/html5/syntax.html#script-nesting-level) by one.
                Prepare the script. This might cause some script to execute, which might cause new characters to be inserted into the tokenizer, and might cause the tokenizer to output more tokens, resulting in a reentrant invocation of the parser.
                Decrement the parser's script nesting level by one. If the parser's script nesting level is zero, then set the parser pause flag to false.
                Let the insertion point have the value of the old insertion point. (In other words, restore the insertion point to its previous value. This value might be the "undefined" value.)
                At this stage, if there is a pending parsing-blocking script, then:

                    If the script nesting level is not zero:
                        Set the parser pause flag to true, and abort the processing of any nested invocations of the tokenizer, yielding control back to the caller. (Tokenization will resume when the caller returns to the "outer" tree construction stage.)
                        NOTE: The tree construction stage of this particular parser is being called reentrantly, say from a call to document.write().

                    Otherwise:
                        Run these steps:
                            1. Let the script be the pending parsing-blocking script. There is no longer a pending parsing-blocking script.
                            2. Block the tokenizer for this instance of the HTML parser, such that the event loop will not run tasks that invoke the tokenizer.
                            3. If the parser's Document has a style sheet that is blocking scripts or the script's "ready to be parser-executed" flag is not set: spin the event loop until the parser's Document has no style sheet that is blocking scripts and the script's "ready to be parser-executed" flag is set.
                            4. If this parser has been aborted in the meantime, abort these steps.
                               NOTE: This could happen if, e.g., while the spin the event loop algorithm is running, the browsing context gets closed, or the document.open() method gets invoked on the Document.
                            5. Unblock the tokenizer for this instance of the HTML parser, such that tasks that invoke the tokenizer can again be run.
                            6. Let the insertion point be just before the next input character.
                            7. Increment the parser's script nesting level by one (it should be zero before this step, so this sets it to one).
                            8. Execute the script.
                            9. Decrement the parser's script nesting level by one. If the parser's script nesting level is zero (which it always should be at this point), then set the parser pause flag to false.
                            10. Let the insertion point be undefined again.
                            11. If there is once again a pending parsing-blocking script, then repeat these steps from step 1.
                    */
                return TreeConstruction.Instance.GetOriginalInsertionModeState();
            }

            TreeConstruction.Instance.StackOfOpenElements.Pop();
            return TreeConstruction.Instance.GetOriginalInsertionModeState();
        }
    }
}
