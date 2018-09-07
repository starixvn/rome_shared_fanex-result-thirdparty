namespace Fanex.Result.ThirdParty.Constants
{
    using System.Collections.Generic;

    public static class LanguageConstants
    {
        private static readonly IDictionary<string, string> CommonLanguage = new Dictionary<string, string>
        {
            ["en-US"] = "en",
            ["zh-TW"] = "ch",
            ["zh-CN"] = "cs",
            ["ja-JP"] = "jp",
            ["ko-KR"] = "ko",
            ["th-TH"] = "th"
        };

        public static IDictionary<string, string> SupportedLanguage
        {
            get
            {
                var lang = CommonLanguage;
                lang["vi-VN"] = "vi";
                return lang;
            }
        }

        public const string DefaultLanguage = "en-US";

        public static IDictionary<string, string> Product30Language
        {
            get
            {
                var lang = CommonLanguage;
                lang["vi-VN"] = "vn";
                return lang;
            }
        }
    }
}