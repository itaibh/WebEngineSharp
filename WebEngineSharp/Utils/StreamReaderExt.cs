using System;
using System.IO;

namespace WebEngineSharp.Utils
{
    public class StreamReaderExt
    {
        public StreamReaderExt(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            BaseStream = stream;
            m_DataSize = stream.Read(m_Array, 0, m_Array.Length);
            //StreamReader
        }

        private byte[] m_Array = new byte[8192];
        private int m_DataSize;

        public Stream BaseStream { get; private set;}

        public int Position { get; set;}

        public int Peek()
        {
            return m_Array[Position];
        }

    }
}

