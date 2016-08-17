using System;

namespace QuickStack.Core
{
    [Serializable]
    public class LanguageConfig
    {
        public string Lint { get; set; } = "mock lint";
        public string Compile { get; set; } = "comp";
        public string Run { get; set; } = "run";
    }
}