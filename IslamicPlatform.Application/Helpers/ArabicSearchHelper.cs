using System.Globalization;
using System.Text;

namespace IslamicPlatform.Application.Helpers
{
    public static class ArabicSearchHelper
    {
        public static string NormalizeArabic(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalized = text.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c)
                    != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            text = sb.ToString()
                     .Normalize(NormalizationForm.FormC);

            text = text
                .Replace('أ', 'ا')
                .Replace('إ', 'ا')
                .Replace('آ', 'ا')
                .Replace('ؤ', 'و')
                .Replace('ٱ', 'ا')
                .Replace('ئ', 'ي')
                .Replace('ى', 'ي');

            return text;
        }
    }
}