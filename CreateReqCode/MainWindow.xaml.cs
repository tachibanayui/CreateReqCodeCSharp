using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CreateReqCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            string str = RequestMethod();
            //Get all encodings:
            Type type = typeof(Encoding);
            foreach (var item in type.GetProperties())
            {
                Encodings.Add(item.Name);
            }

            HistoryFileLocation = AppDomain.CurrentDomain.BaseDirectory + "RequestHistories.json";
            if (File.Exists(HistoryFileLocation))
            {
                RequestRecords = JsonConvert.DeserializeObject<ObservableCollection<RequestRecord>>(File.ReadAllText(HistoryFileLocation)) ?? new ObservableCollection<RequestRecord>();
            }
            else
            {
                File.Create(HistoryFileLocation);
            }

            DataContext = this;
        }

        #region Property as ViewModel
        private string _ReqName = "req";
        public string ReqName
        {
            get
            {
                return _ReqName;
            }
            set
            {
                if (_ReqName != value)
                {
                    _ReqName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _RespName = "resp";
        public string RespName
        {
            get
            {
                return _RespName;
            }
            set
            {
                if (_RespName != value)
                {
                    _RespName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _StreamName = "stream";
        public string StreamName
        {
            get
            {
                return _StreamName;
            }
            set
            {
                if (_StreamName != value)
                {
                    _StreamName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _StreamReaderName = "reader";
        public string StreamReaderName
        {
            get
            {
                return _StreamReaderName;
            }
            set
            {
                if (_StreamReaderName != value)
                {
                    _StreamReaderName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ResName = "responeData";
        public string ResName
        {
            get
            {
                return _ResName;
            }
            set
            {
                if (_ResName != value)
                {
                    _ResName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsUseAync = false;
        public bool IsUseAync
        {
            get
            {
                return _IsUseAync;
            }
            set
            {
                if (_IsUseAync != value)
                {
                    _IsUseAync = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsWrapMethod = true;
        public bool IsWrapMethod
        {
            get
            {
                return _IsWrapMethod;
            }
            set
            {
                if (_IsWrapMethod != value)
                {
                    _IsWrapMethod = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _MethodName = "RequestMethod";
        public string MethodName
        {
            get
            {
                return _MethodName;
            }
            set
            {
                if (_MethodName != value)
                {
                    _MethodName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsIOMethod = true;
        public bool IsIOMethod
        {
            get
            {
                return _IsIOMethod;
            }
            set
            {
                if (_IsIOMethod != value)
                {
                    _IsIOMethod = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _TabSize = "4";
        public string TabSize
        {
            get
            {
                return _TabSize;
            }
            set
            {
                if (_TabSize != value)
                {
                    _TabSize = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ReqGeneral;
        public string ReqGeneral
        {
            get
            {
                return _ReqGeneral;
            }
            set
            {
                if (_ReqGeneral != value)
                {
                    _ReqGeneral = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ReqHeaders;
        public string ReqHeaders
        {
            get
            {
                return _ReqHeaders;
            }
            set
            {
                if (_ReqHeaders != value)
                {
                    _ReqHeaders = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ResultCode = string.Empty;
        public string ResultCode
        {
            get
            {
                return _ResultCode;
            }
            set
            {
                if (_ResultCode != value)
                {
                    _ResultCode = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _UseHttpWebRespone;
        public bool UseHttpWebRespone
        {
            get
            {
                return _UseHttpWebRespone;
            }
            set
            {
                if (_UseHttpWebRespone != value)
                {
                    _UseHttpWebRespone = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _TabOffset = "1";
        public string TabOffset
        {
            get
            {
                return _TabOffset;
            }
            set
            {
                if (_TabOffset != value)
                {
                    _TabOffset = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsIgnoreCookie;
        public bool IsIgnoreCookie
        {
            get
            {
                return _IsIgnoreCookie;
            }
            set
            {
                if (_IsIgnoreCookie != value)
                {
                    _IsIgnoreCookie = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ReqStream = "reqStream";
        public string ReqStream
        {
            get
            {
                return _ReqStream;
            }
            set
            {
                if (_ReqStream != value)
                {
                    _ReqStream = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _PayLoadDataName = "payloadData";
        public string PayLoadDataName
        {
            get
            {
                return _PayLoadDataName;
            }
            set
            {
                if (_PayLoadDataName != value)
                {
                    _PayLoadDataName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _PayLoadData;
        public string PayLoadData
        {
            get
            {
                return _PayLoadData;
            }
            set
            {
                if (_PayLoadData != value)
                {
                    _PayLoadData = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _RequestByteName = "bytes";
        public string RequestByteName
        {
            get
            {
                return _RequestByteName;
            }
            set
            {
                if (_RequestByteName != value)
                {
                    _RequestByteName = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Encodings { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<int> FontSizes { get; set; } = new ObservableCollection<int>(new List<int>() { 10, 12, 15, 18, 20, 24, 36, 48, 52, 60, 72, 80, 100 });
        public ObservableCollection<RequestRecord> RequestRecords { get; set; } = new ObservableCollection<RequestRecord>();

        private string _SelectedEncoding = "Default";
        public string SelectedEncoding
        {
            get
            {
                return _SelectedEncoding;
            }
            set
            {
                if (_SelectedEncoding != value)
                {
                    _SelectedEncoding = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _CurrentFontSize;
        public int CurrentFontSize
        {
            get
            {
                return _CurrentFontSize;
            }
            set
            {
                if (_CurrentFontSize != value)
                {
                    _CurrentFontSize = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsUseFile;
        public bool IsUseFile
        {
            get
            {
                return _IsUseFile;
            }
            set
            {
                if (_IsUseFile != value)
                {
                    _IsUseFile = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsRecalcContentLength;
        public bool IsRecalcContentLength
        {
            get
            {
                return _IsRecalcContentLength;
            }
            set
            {
                if (_IsRecalcContentLength != value)
                {
                    _IsRecalcContentLength = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Process Properties and Fields
        public string Result { get; set; }
        public string HistoryFileLocation { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

#if DEBUG
        private void GetCode(object sender, RoutedEventArgs e)
        {
            GenerateCode();
        }
#else
        private void GetCode(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateCode();
            }
            catch
            {
                MessageBox.Show("Some of the request is missing or bad formating", "Invalid data!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

        }
#endif

        private void GenerateCode()
        {

            if (TestForInvalidSetting())
            {
                return;
            }

            Result = string.Empty;

            string methodCreationString = string.Empty;
            Dictionary<string, string> requestGeneralKeyValues = ConvertStringToDictionary(ReqGeneral);

            if (IsWrapMethod)
            {
                methodCreationString += CreateTabAsString(int.Parse(TabOffset)) + "public ";
                if (IsUseAync)
                {
                    methodCreationString += "async ";
                }

                if (IsIOMethod)
                {
                    if (IsUseAync)
                    {
                        methodCreationString += "Task<string> ";
                    }
                    else
                    {
                        methodCreationString += "string ";
                    }
                }
                else
                {
                    if (IsUseAync)
                    {
                        methodCreationString += "Task ";
                    }
                    else
                    {
                        methodCreationString += "void ";
                    }
                }

                methodCreationString += MethodName + "(";

                if (IsIOMethod)
                {
                    methodCreationString += "string url = " + "\"" + requestGeneralKeyValues["Request URL"] + "\"";
                }

                methodCreationString += ")\r\n" + CreateTabAsString(int.Parse(TabOffset)) + "{\r\n";
            }

            string reqCreationString = string.Empty;

            reqCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}HttpWebRequest {ReqName} = (HttpWebRequest)WebRequest.Create(";
            if (IsIOMethod)
            {
                reqCreationString += "url);\r\n";
            }
            else
            {
                reqCreationString += $"\"{requestGeneralKeyValues["Request URL"]}\");\r\n";
            }

            reqCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}{ReqName}.Method = \"{requestGeneralKeyValues["Request Method"]}\";\r\n";
            Dictionary<string, string> reqHeaderKeyValues = ConvertStringToDictionary(ReqHeaders);
            reqCreationString += AddHeaders(reqHeaderKeyValues);

            if (requestGeneralKeyValues["Request Method"] == "POST" && int.Parse(reqHeaderKeyValues["content-length"]) > 0)
            {
                reqCreationString += AddGetRequestStream();
            }

            string getResponeString = string.Empty;
            getResponeString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}using(";
            if (UseHttpWebRespone)
            {
                getResponeString += $"HttpWebResponse ";
            }
            else
            {
                getResponeString += $"WebResponse ";
            }

            getResponeString += RespName + " = ";
            if (UseHttpWebRespone)
            {
                getResponeString += "(HttpWebResponse)";
            }

            if (IsUseAync)
            {
                getResponeString += $"await {ReqName}.GetResponseAsync())\r\n{CreateTabAsString(int.Parse(TabOffset) + 1)}" + "{\r\n";
            }
            else
            {
                getResponeString += $"{ReqName}.GetResponse())\r\n{CreateTabAsString(int.Parse(TabOffset) + 1)}" + "{\r\n";
            }

            getResponeString += $"{CreateTabAsString(int.Parse(TabOffset) + 2)}using(Stream {StreamName} = {RespName}.GetResponseStream())\r\n{CreateTabAsString(int.Parse(TabOffset) + 2)}" + "{\r\n";
            getResponeString += $"{CreateTabAsString(int.Parse(TabOffset) + 3)}StreamReader {StreamReaderName} = new StreamReader({StreamName});\r\n";
            getResponeString += $"{CreateTabAsString(int.Parse(TabOffset) + 3)}string {ResName} = ";
            if (IsUseAync)
            {
                getResponeString += $"await {StreamReaderName}.ReadToEndAsync();\r\n";
            }
            else
            {
                getResponeString += $"{StreamReaderName}.ReadToEnd();\r\n";
            }

            if (IsIOMethod)
            {
                getResponeString += $"{CreateTabAsString(int.Parse(TabOffset) + 3)}return {ResName};\r\n";
            }

            getResponeString += CreateTabAsString(int.Parse(TabOffset) + 2) + "}\r\n" + CreateTabAsString(int.Parse(TabOffset) + 1) + "}\r\n";


            Result = methodCreationString + reqCreationString + getResponeString;
            if (IsWrapMethod)
            {
                Result += CreateTabAsString(int.Parse(TabOffset)) + "}";
            }

            ResultCode = Result;
            AddHistoricalRecord();
        }


        private void AddHistoricalRecord()
        {
            RequestRecord rec = new RequestRecord
            {
                RequestGeneral = ReqGeneral,
                DateRequested = DateTime.Now,
                Options = $"Use Async call: {IsUseAync}, Wrap in method: {IsWrapMethod}, Use HttpWebRespone: {UseHttpWebRespone}, Pass url in method args: {IsIOMethod}, IgnoreCookies: {IsIgnoreCookie}",
                RequestName = ReqName,
                ResponeName = RespName,
                StreamName = StreamName,
                StreamReaderName = StreamReaderName,
                StringResData = ResName,
                UseAsync = IsUseAync,
                WrapInMethod = IsWrapMethod,
                UseHttpWebRespone = UseHttpWebRespone,
                MethodName = MethodName,
                PassUrlAsArg = IsIOMethod,
                TabSize = int.Parse(TabSize),
                TabOffset = int.Parse(TabOffset),
                IgnoreCookie = IsIgnoreCookie,
                GetRequestStreamName = ReqStream,
                DataPayloadName = PayLoadDataName,
                ReqByteName = RequestByteName,
                EncodingType = SelectedEncoding,
                PassFile = IsUseFile,
                RecalcContentLength = IsRecalcContentLength,
                ReqHeaders = ReqHeaders,
                ReqPayload = PayLoadData,
                Result = ResultCode,
            };

            RequestRecords.Add(rec);
        }

        private string AddGetRequestStream()
        {
            string getRespStreamCreationString = string.Empty;
            string jsonString = PayLoadData.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'");
            string encoding = string.IsNullOrEmpty(SelectedEncoding) ? "Default" : SelectedEncoding;
            if (IsUseFile)
            {
                getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}FileStream {RequestByteName} = new FileStream(\"{jsonString}\", FileMode.Open);\r\n";
                if (IsRecalcContentLength)
                {
                    getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}{ReqName}.ContentLength = {RequestByteName}.Length;\r\n";
                }
            }

            getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}Stream {ReqStream} = ";
            if (IsUseAync)
            {
                getRespStreamCreationString += $"await {ReqName}.GetRequestStreamAsync();\r\n";
            }
            else
            {
                getRespStreamCreationString += $"{ReqName}.GetRequestStream();\r\n";
            }

            if (IsUseFile)
            {
                getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}{RequestByteName}.CopyTo({ReqStream});\r\n";
                getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}{RequestByteName}.Close();\r\n";
            }
            else
            {
                getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}string {PayLoadDataName} = \"{jsonString}\";\r\n";
                getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}byte[] {RequestByteName} = Encoding.{encoding}.GetBytes({PayLoadDataName});\r\n";
                if (IsUseAync)
                {
                    getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}await {ReqStream}.WriteAsync({RequestByteName}, 0, {RequestByteName}.Length);\r\n";
                }
                else
                {
                    getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}{ReqStream}.Write({RequestByteName}, 0, {RequestByteName}.Length);\r\n";
                }

            }
            getRespStreamCreationString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}{ReqStream}.Close();\r\n";
            return getRespStreamCreationString;
        }

        private bool TestForInvalidSetting()
        {
            if (string.IsNullOrEmpty(ReqName))
            {
                MessageBox.Show("Request Name can't be empty!");
                return true;
            }

            if (string.IsNullOrEmpty(RespName))
            {
                MessageBox.Show("Respone Name can't be empty!");
                return true;
            }

            if (string.IsNullOrEmpty(StreamName))
            {
                MessageBox.Show("Stream Name can't be empty!");
                return true;
            }

            if (string.IsNullOrEmpty(StreamReaderName))
            {
                MessageBox.Show("Stream Reader Name can't be empty!");
                return true;
            }

            if (string.IsNullOrEmpty(ResName))
            {
                MessageBox.Show("Result Name can't be empty!");
                return true;
            }

            if (string.IsNullOrEmpty(MethodName))
            {
                MessageBox.Show("Method Name can't be empty!");
                return true;
            }

            int temp = 0;

            if (!int.TryParse(TabSize, out temp))
            {
                MessageBox.Show("Tab Size must be a interger number!");
                return true;
            }

            if (!int.TryParse(TabOffset, out temp))
            {
                MessageBox.Show("Tab Offset must be a interger number!");
                return true;
            }

            if (string.IsNullOrEmpty(ReqGeneral))
            {
                MessageBox.Show("Request General can't be empty!");
                return true;
            }

            if (string.IsNullOrEmpty(ReqHeaders))
            {
                MessageBox.Show("Request Headers can't be empty!");
                return true;
            }

            return false;
        }

        private string AddHeaders(Dictionary<string, string> reqHeaderKeyValues)
        {
            bool isIgnoreHeader = false;
            string HeaderString = string.Empty;
            foreach (string header in reqHeaderKeyValues.Keys)
            {
                isIgnoreHeader = false;
                HeaderString += $"{CreateTabAsString(int.Parse(TabOffset) + 1)}{ReqName}.";

                switch (header.ToLower())
                {
                    case "accept":
                        HeaderString += $"Accept = \"{reqHeaderKeyValues[header]}\"";
                        break;
                    case "connection":
                        HeaderString += $"Connection = \"{reqHeaderKeyValues[header]}\"";
                        break;
                    case "content-length":
                        HeaderString += $"ContentLength = long.Parse(\"{reqHeaderKeyValues[header]}\")";
                        break;
                    case "content-type":
                        HeaderString += $"ContentType = \"{reqHeaderKeyValues[header]}\"";
                        break;
                    case "date":
                        HeaderString += $"Date = DateTime.Parse(\"{reqHeaderKeyValues[header]}\")";
                        break;
                    case "expect":
                        HeaderString += $"Expect = \"{reqHeaderKeyValues[header]}\"";
                        break;
                    case "host":
                        HeaderString += $"Host = \"{reqHeaderKeyValues[header]}\"";
                        break;
                    case "if-modified-since":
                        HeaderString += $"IfModifiedSince = DateTime.Parse(\"{reqHeaderKeyValues[header]}\")";
                        break;
                    case "range":
                        HeaderString += $"AddRange(int.Parse(\"{reqHeaderKeyValues[header]}\"))";
                        break;
                    case "referer":
                        HeaderString += $"Referer = \"{reqHeaderKeyValues[header]}\"";
                        break;
                    case "transfer-encoding":
                        HeaderString += $"TransferEncoding = \"{reqHeaderKeyValues[header]}\"";
                        break;
                    case "user-agent":
                        HeaderString += $"UserAgent = \"{reqHeaderKeyValues[header]}\"";
                        break;
                    case "cookie":
                        if (!IsIgnoreCookie)
                        {
                            HeaderString += AddCookie(reqHeaderKeyValues[header], int.Parse(TabOffset) + 1);
                        }
                        else
                        {
                            isIgnoreHeader = true;
                        }

                        break;
                    case "proxy-connection":
                        isIgnoreHeader = true;
                        break;
                    default:
                        HeaderString += $"Headers.Add(\"{header.Trim(':')}\", \"{reqHeaderKeyValues[header]}\")";
                        break;
                }
                if (isIgnoreHeader)
                {
                    HeaderString = HeaderString.Substring(0, HeaderString.LastIndexOf(';'));
                }
                HeaderString += ";\r\n";
            }

            return HeaderString;
        }

        private string AddCookie(string cookieCollectionString, int tabCount)
        {
            string cookieCreation = "CookieContainer = new CookieContainer();\r\n";

            string[] cookieEntry = cookieCollectionString.Replace("; ", ";").Split(';');
            foreach (string item in cookieEntry)
            {
                string[] pair = item.Split('=');
                cookieCreation += $"{CreateTabAsString(tabCount)}{ReqName}.CookieContainer.Add(new Cookie(\"{pair[0].Trim()}\", \"{pair[1].Trim()}\") {{ Domain = {ReqName}.Host }} );\r\n";
            }

            return cookieCreation.Substring(0, cookieCreation.LastIndexOf('\r') - 1);
        }

        private string CreateTabAsString(int tabCount)
        {
            string res = string.Empty;
            for (int i = 0; i < (tabCount * int.Parse(TabSize)); i++)
            {
                res += ' ';
            }

            return res;
        }

        private Dictionary<string, string> ConvertStringToDictionary(string input)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string[] splitLines = input.Replace("\r\n", "\n").Split('\n');

            foreach (string item in splitLines)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string header = item.Substring(0, item.IndexOf(": "));
                    string value = item.Substring(item.IndexOf(": ") + 2);

                    keyValuePairs.Add(header, value);
                }
            }

            return keyValuePairs;
        }

        private void CopyCode(object sender, RoutedEventArgs e) => Clipboard.SetText(ResultCode);

        private void ShowHistory(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView lstView = sender as ListView;
                var selectItem = lstView.SelectedValue as RequestRecord;
                ReqGeneral = selectItem.RequestGeneral;
                ReqName = selectItem.RequestName;
                RespName = selectItem.ResponeName;
                StreamName = selectItem.StreamName;
                StreamReaderName = selectItem.StreamReaderName;
                ResName = selectItem.StringResData;
                IsUseAync = selectItem.UseAsync;
                IsWrapMethod = selectItem.WrapInMethod;
                UseHttpWebRespone = selectItem.UseHttpWebRespone;
                MethodName = selectItem.MethodName;
                TabSize = selectItem.TabSize.ToString();
                TabOffset = selectItem.TabOffset.ToString();
                IsIgnoreCookie = selectItem.IgnoreCookie;
                ReqStream = selectItem.GetRequestStreamName;
                PayLoadDataName = selectItem.DataPayloadName;
                RequestByteName = selectItem.ReqByteName;
                SelectedEncoding = selectItem.EncodingType;
                IsUseFile = selectItem.PassFile;
                IsRecalcContentLength = selectItem.RecalcContentLength;
                ReqHeaders = selectItem.ReqHeaders;
                PayLoadData = selectItem.ReqPayload;
                ResultCode = Result = selectItem.Result;
            }
            catch
            { }
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            RequestRecord rec = (btn.Parent as Grid).DataContext as RequestRecord;
            RequestRecords.Remove(rec);
        }

        private void SaveHistory(object sender, CancelEventArgs e)
        {
            string content = JsonConvert.SerializeObject(RequestRecords);
            File.WriteAllText(HistoryFileLocation, content);
        }
    }
}
