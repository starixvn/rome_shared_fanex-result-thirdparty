namespace Fanex.Result.ThirdParty.Constants
{
    using System.Collections.Generic;

    public static class LanguageConstants
    {
        public static readonly Dictionary<string, string> SupportedLanguage = new Dictionary<string, string>
        {
            ["en-US"] = "en",
            ["zh-TW"] = "ch",
            ["zh-CN"] = "cs",
            ["ja-JP"] = "jp",
            ["ko-KR"] = "ko",
            ["vi-VN"] = "vi",
            ["th-TH"] = "th",
        };

        public const string DefaultLanguage = "en-US";
    }
}
