using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MassChecker.Models
{
    #region Enums

    public enum PaperKeyResultType
    {
        Correct,
        Incorrect,
        Invalid,
        Unshaded
    }

    public enum AnswerParserResultType
    {
        Valid,
        DisabledSetShaded,
        SetMultishaded,
        SetUnshaded,
        Undefined
    }

    #endregion

    public class PaperItem
    {
        public bool A { get; set; }
        public bool B { get; set; }
        public bool C { get; set; }
        public bool D { get; set; }
        public bool E { get; set; }

        public PaperItem(bool a, bool b, bool c, bool d, bool e)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
        }

        public PaperItem(string data)
        {
            A = data[0] == '1';
            B = data[1] == '1';
            C = data[2] == '1';
            D = data[3] == '1';
            E = data[4] == '1';
        }

        public new string ToString()
        {
            string data = "";
            data += A ? "1" : "0";
            data += B ? "1" : "0";
            data += C ? "1" : "0";
            data += D ? "1" : "0";
            data += E ? "1" : "0";
            return data;
        }

        public static PaperItem[] Parse(string data)
        {
            if (data.Length % 5 != 0) return null;

            int charIndex = 0;
            PaperItem[] ans = new PaperItem[data.Length / 5];
            for (int i = 0; i < ans.Length; i++)
            {
                ans[i] = new PaperItem(data.Substring(charIndex, 5));
                charIndex += 5;
            }

            return ans;
        }
    }
}
