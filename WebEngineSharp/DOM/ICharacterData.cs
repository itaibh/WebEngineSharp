using System;

namespace WebEngineSharp.DOM
{
    // http://www.w3.org/TR/dom/#characterdata
    public interface ICharacterData : INode
    {
        //[TreatNullAs=EmptyString] attribute
        string data {get;set;}
        ulong length {get;}
        string substringData(ulong offset, ulong count);
        void appendData(string data);
        void appendData(char data);
        void insertData(ulong offset, string data);
        void insertData(ulong offset, char data);
        void deleteData(ulong offset, ulong count);
        void replaceData(ulong offset, ulong count, string data);
    }
}

