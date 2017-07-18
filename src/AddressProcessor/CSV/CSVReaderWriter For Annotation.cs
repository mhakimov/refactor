using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        1) List three to five key concerns with this implementation that you would discuss with the junior developer. 

        Please leave the rest of this file as it is so we can discuss your concerns during the next stage of the interview process.
        
        *) Regarding Write() method: I wouldn't include if statement in the loop, as it can affect performance if columns
        is a long array. It would be better to decrease amount of iterations by 1, remove if statement, and after 
        we exit the loop simply add the last column without tabulation at the end.
        However, this loop is of a variable length, therefore using StringBuilder is advised. Even if columns
        will contain 3 strings max most of the time, it is a good practice to plan for edge cases (e.g. 1000 strings).
        Alternatively use String.Join to make code more readable.

        *) Regarding Read() methods: It is unclear why to have two Read() methods?
        The first Read method has incorrect implementation. Omitting out keywords in the parameters means column1 and
        column2 cannot be overwritten by this method. Therefore there is no point of giving these parameters any values 
        within the method.
        Also there is no need for else statement in the end of the method. Return ture can be taken out of it.

        *) To make method Read() more flexible it would make sense to change its parameters from two strings to an array
        of strings. This way we can use Read() even for any amount of columns (not just two)

        *) Current implementation of method Read() lacks verification that columns.length is not equal 1.
        Assume that columns contains just one string. Then the line "column2 = columns[1];" will throw an
        IndexOutOfRangeException.

        *) Regarding Open() method: will it ever get to else condition???

    */

    public class CSVReaderWriterForAnnotation
    {
        private StreamReader _readerStream = null;
        private StreamWriter _writerStream = null;

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                _readerStream = File.OpenText(fileName);
            }
            else if (mode == Mode.Write)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                _writerStream = fileInfo.CreateText();
            }
            else
            {
                throw new Exception("Unknown file mode for " + fileName);
            }
        }

        public void Write(params string[] columns)
        {
            string outPut = "";

            for (int i = 0; i < columns.Length; i++)
            {
                outPut += columns[i];
                if ((columns.Length - 1) != i)
                {
                    outPut += "\t";
                }
            }

            WriteLine(outPut);
        }

        public bool Read(string column1, string column2)
        {
            const int FIRST_COLUMN = 0;
            const int SECOND_COLUMN = 1;

            string line;
            string[] columns;

            char[] separator = { '\t' };

            line = ReadLine();
            columns = line.Split(separator);

            if (columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            }
            else
            {
                column1 = columns[FIRST_COLUMN];
                column2 = columns[SECOND_COLUMN];

                return true;
            }
        }

        public bool Read(out string column1, out string column2)
        {
            const int FIRST_COLUMN = 0;
            const int SECOND_COLUMN = 1;

            string line;
            string[] columns;

            char[] separator = { '\t' };

            line = ReadLine();

            if (line == null)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            columns = line.Split(separator);

            if (columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            }
            else
            {
                column1 = columns[FIRST_COLUMN];
                column2 = columns[SECOND_COLUMN];

                return true;
            }
        }

        private void WriteLine(string line)
        {
            _writerStream.WriteLine(line);
        }

        private string ReadLine()
        {
            return _readerStream.ReadLine();
        }

        public void Close()
        {
            if (_writerStream != null)
            {
                _writerStream.Close();
            }

            if (_readerStream != null)
            {
                _readerStream.Close();
            }
        }
    }
}
