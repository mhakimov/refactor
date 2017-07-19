using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    // I would rename this class to CsvReaderWriter
    public class CSVReaderWriter
    {
        private StreamReader _readerStream;
        private StreamWriter _writerStream;

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        //currently there is no scenario for the Default case to be executed but we'll keep it in case Mode enum 
        //will get changed
        public void Open(string fileName, Mode mode)
        {
            switch (mode)
            {
                case Mode.Read:
                    _readerStream = File.OpenText(fileName);
                    break;
                case Mode.Write:
                    FileInfo fileInfo = new FileInfo(fileName);
                    _writerStream = fileInfo.CreateText();
                    break;
                default:
                    throw new Exception($"Unknown file mode for {fileName}");
            }
        }


        public void Write(params string[] columns)
        {
            string outPut = string.Join("\t", columns);          
            WriteLine(outPut);
        }


        public bool Read(params string[] inputColumns)
        {
            var line = ReadLine();
            if (line == null)
                return false;

            var columns = line.Split('\t');

            if (columns.Length < inputColumns.Length) return false;

            for (var i = 0; i < inputColumns.Length; i++)
            {
                inputColumns[i] = columns[i];
            }
            return true;
        }


        public bool Read(out string column1, out string column2)
        {           
            string[] columns = { null, null };

           var read = Read(columns);

            column1 = columns[0];
            column2 = columns[1];
            return read;
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
            _writerStream?.Close();          
            _readerStream?.Close();
        }


    }
}
