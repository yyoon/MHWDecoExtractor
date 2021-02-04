namespace MHWDecoExtractorGUI
{
    using System;
    using System.Collections.Generic;
    using System.Text.Unicode;

    class DecorationNameComparer : IComparer<string>
    {
        private DecorationNameComparer() { }

        public static DecorationNameComparer Instance { get; } = new DecorationNameComparer();

        int IComparer<string>.Compare(string x, string y)
        {
            int minLength = Math.Min(x.Length, y.Length);
            for (int i = 0; i < minLength; ++i)
            {
                char xch = x[i];
                char ych = y[i];

                // Place Korean letters before English alphabets.
                if (IsHangul(xch) && IsEnglish(ych))
                    return -1;
                if (IsHangul(ych) && IsEnglish(xch))
                    return 1;

                if (xch < ych)
                    return -1;
                if (xch > ych)
                    return 1;
            }

            if (x.Length < y.Length)
                return -1;
            if (x.Length > y.Length)
                return 1;

            return 0;
        }

        private bool IsHangul(char ch)
        {
            return IsCharInRange(ch, UnicodeRanges.HangulSyllables);
        }

        private bool IsEnglish(char ch)
        {
            return IsCharInRange(ch, UnicodeRanges.BasicLatin) && char.IsLetter(ch);
        }

        private bool IsCharInRange(char ch, UnicodeRange range)
        {
            return range.FirstCodePoint <= ch && ch < range.FirstCodePoint + range.Length;
        }
    }
}
