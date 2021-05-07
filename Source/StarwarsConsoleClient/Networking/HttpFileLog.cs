using Newtonsoft.Json;
using System;
using System.IO;

namespace StarwarsConsoleClient.Networking
{
    public class HttpFileLog
    {
        public HttpFileLog(string filePath)
        {
            FilePath = filePath;
            if (!File.Exists(FilePath))
            {
                File.WriteAllLines(filePath,
                    new string[]
                    {
                        "HTTP-FILE-LOG-ENABLED"
                    });
            }
        }
        public string FilePath { get; private set; }
        private static string JsonFormat(object obj) {
          string json = JsonConvert.SerializeObject(obj);
            return json.Replace(",", "," + _n).Replace("{", "{" + _n).Replace("\\", "").Replace("}", _n + "}");
        }
        private void Append(string line) => File.AppendAllLines(FilePath, new[] { line });
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
        private static string _n = Environment.NewLine;
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
            Append($"{data.RequestMethod} {data.RequestUri}");
            Append("Response Code: " + data.StatusCode);
            Append("Response string: " + JsonFormat(data.ResponseContentString));
            Append("Response Headers: " + _n + data.ResponseHeaders.ToString());
            Append($"Request string: {JsonFormat(data.RequestContentString)}");
            Append("Request Headers: " + JsonFormat(data.RequestHeaders));
            Delimiter();
            NewLines(2);
        }
    }
}


