using System;

namespace WebEngineSharp.Parser
{

    public enum HtmlToken
    {
        START,
        OpenTag,
        OpenMetaTag,
        OpenDoctypeTag,
        CloseTag,
        CloseInlineTag,
        TagName,
        Namespace,
        AttributeName,
        AttributeValue,
        OpenComment,
        CloseComment,
        OpenCDATA,
        CloseCDATA,
        Text
    }

    /* State Machine:
     * 
     * START -> OpenTag -> OpenMetaTag -> Text -> CloseTag ==> generate metadata (doctype, most likely)
     *           ^     \-> OpenMetaTag -> OpenComment -> Text -> CloseComment ==> ignore
     *            \    \-> TagName -> AttributeName -> '=' -> AttibuteValue -> CloseInlineTag
     *             \                           ^------------------/  /
     *              \-----------------------------------------------/
     */
}
