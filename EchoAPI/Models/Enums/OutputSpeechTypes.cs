﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EchoAPI.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OutputSpeechType
    {
        PlainText,
        SSML
    }
}
