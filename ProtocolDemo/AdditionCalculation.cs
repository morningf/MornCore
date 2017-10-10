using MornCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolDemo
{
    public class AdditionCalculationRequest : MornRequest<AdditionCalculationResponse>
    {
        public int A { get; set; }
        public int B { get; set; }
        public List<int> L { get; set; }
        public string Remarks { get; set; }

        public override string ToString()
        {
            return string.Format("A : {0}\r\nB : {1}\r\nL : {2}\r\nRemarks : {3}",
                A, B, string.Join(',', L), Remarks);
        }
    }

    public class AdditionCalculationResponse : MornResponse
    {
        public int ABResult { get; set; }
        public int LResult { get; set; }
        public string Remarks { get; set; }

        public override string ToString()
        {
            return string.Format("ErrorCode : {0}\r\nErrorMessage : {1}\r\nABResult : {2}\r\nLResult : {3}\r\nnRemarks : {4}",
                ErrorCode, ErrorMessage, ABResult, LResult, Remarks);
        }
    }
}
