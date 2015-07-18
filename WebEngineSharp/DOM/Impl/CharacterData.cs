using System;
using System.Text;

namespace WebEngineSharp.DOM.Impl
{
    public class CharacterData : Node, ICharacterData
    {
        public CharacterData(IDocument doc)
            : base(doc)
        {
            m_Data = new StringBuilder();
        }

        public CharacterData(IDocument doc, string data)
            : base(doc)
        {
            m_Data = new StringBuilder(data);
        }

        private StringBuilder m_Data;

        #region ICharacterData implementation

        public string substringData(ulong offset, ulong count)
        {
            return m_Data.ToString((int)offset, (int)count);
        }

        public void appendData(string data)
        {
            m_Data.Append(data);
        }

        public void appendData(char data)
        {
            m_Data.Append(data);
        }

        public void insertData(ulong offset, string data)
        {
            m_Data.Insert((int)offset, data);
        }

        public void insertData(ulong offset, char data)
        {
            m_Data.Insert((int)offset, data);
        }

        public void deleteData(ulong offset, ulong count)
        {
            m_Data.Remove((int)offset, (int)count);
        }

        public void replaceData(ulong offset, ulong count, string data)
        {
            //TODO - PERFORMANCE!
            m_Data.Remove((int)offset, (int)count);
            m_Data.Insert((int)offset, data);
        }

        public string data
        {
            get { return m_Data.ToString(); }
            set { m_Data.Clear().Append(value); }
        }

        public ulong length
        {
            get { return (ulong)m_Data.Length; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("[CharacterData: data={0}, length={1}]", data, length);
        }
    }
}

