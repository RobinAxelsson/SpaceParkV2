using System;
using System.IO;

namespace StarwarsConsoleClient.Networking
{
    public class HttpFileLog
    {
        public HttpFileLog(string filePath)
        {
            _filePath = filePath;
        }
        private string _filePath;
        private void Append(string line) => File.AppendAllLines(_filePath, new[] { line });
        private void NewLines(int lines)
        {
            string text = string.Empty;
            if (lines < 1) throw new Exception("blanc lines cant be smaller then 1.");

            for (int i = 0; i < lines; i++)
            {
                text += Environment.NewLine;
            }
            Append(text);
        }
        private void Delimiter()
        {
            Append("------------------------------");
        }
        private string TimeStamp() => DateTime.Now.ToString("G");
        public void LogError(string errorMessage)
        {
            Delimiter();
            Append(TimeStamp());
            Append(errorMessage);
            Delimiter();
            NewLines(2);
        }
        public void LogClientData(ClientResponseData data)
        {
            Delimiter();
            Append(data.TimeStamp);
            Append($"Request: {data.RequestMethod} {data.RequestUri}");
            Append("Response Code: " + data.StatusCode);
            Append("Response Headers: " + data.ResponseContentString);
            Append("Response string: " + data.ResponseContentString);
            Append(data.ResponseHeaders);
            NewLines(2);
            Append($"Request string: {data.RequestContentString}");
            Append(data.RequestHeaders);
            Delimiter();
            NewLines(2);
        }
    }
}


