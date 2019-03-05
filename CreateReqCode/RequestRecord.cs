using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateReqCode
{
    public class RequestRecord
    {
        public string RequestGeneral { get; set; }
        public DateTime DateRequested { get; set; }
        public string Options { get; set; }

        //General Settings
        public string RequestName { get; set; }
        public string ResponeName { get; set; }
        public string StreamName { get; set; }
        public string StreamReaderName { get; set; }
        public string StringResData { get; set; }
        public bool UseAsync { get; set; }
        public bool WrapInMethod { get; set; }
        public bool UseHttpWebRespone { get; set; }
        public string MethodName { get; set; }
        public bool PassUrlAsArg { get; set; }
        public int TabSize { get; set; }
        public int TabOffset { get; set; }
        public bool IgnoreCookie { get; set; }


        //Post Method Settings
        public string GetRequestStreamName { get; set; }
        public string DataPayloadName { get; set; }
        public string ReqByteName { get; set; }
        public string EncodingType { get; set; }
        public bool PassFile { get; set; }
        public bool RecalcContentLength { get; set; }

        public string ReqHeaders { get; set; }
        public string ReqPayload { get; set; }
        public string Result { get; set; }
    }
}
