using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace WebEngineSharp.Tokenizer.States
{
    public abstract class BaseState
    {
        public BaseState()
        {
            LastConsumedCharacters = new Queue<char>();
        }

        public Queue<char> LastConsumedCharacters { get; private set; }

        protected int Read(StreamReader reader)
        {
            if (LastConsumedCharacters.Count > 0)
            {
                return LastConsumedCharacters.Dequeue();
            }
            return reader.Read();
        }

        protected int ReadBlock(StreamReader reader, char[] buffer, int index, int count)
        {
            if (LastConsumedCharacters.Count > 0)
            {
                if (count >= LastConsumedCharacters.Count)
                {
                    LastConsumedCharacters.CopyTo(buffer, index);
                    index += LastConsumedCharacters.Count;
                    count -= LastConsumedCharacters.Count;
                    LastConsumedCharacters.Clear();
                } else
                {
                    int pos = index;
                    for (int i = 0; i < count; ++i, ++pos)
                    {
                        buffer[pos] = LastConsumedCharacters.Dequeue();
                    }
                    return count;
                }
            }
            return reader.ReadBlock(buffer, index, count);
        }

        protected int Peek(StreamReader reader)
        {
            if (LastConsumedCharacters.Count > 0)
            {
                return LastConsumedCharacters.Peek();
            }
            return reader.Peek();
        }

        public abstract BaseState Process(HtmlTokenizer tokenizer, StreamReader reader);

        protected string ConsumeCharacters(StreamReader reader, HashSet<char> allowedCharacters, char delimiter)
        {
            StringBuilder sb = new StringBuilder();
            int c = 0;
            while (c != -1)
            {
                c = Read(reader);
                if (!allowedCharacters.Contains((char)c))
                {
                    break;
                }
                sb.Append((char)c);
            }

            if (c == delimiter)
            {
                return sb.ToString();
            }

            return null;
        }

        protected bool IsWhitespace(char c)
        {
            return (c == 0x09 || c == 0x0A || c == 0x0C || c == ' ');            
        }

        protected bool IsWhitespace(int c)
        {
            return (c == 0x09 || c == 0x0A || c == 0x0C || c == ' ');            
        }

        public bool IsUppercaseAsciiLetter(int c)
        {
            return (c >= 'A' && c <= 'Z');
        }

        public bool IsLowercaseAsciiLetter(int c)
        {
            return (c >= 'a' && c <= 'z');
        }

        protected void ReportParseError()
        {
        }
    }
}
