// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using Newtonsoft.Json;

namespace Icarus.Utilities
{
    public struct ConfigJson
    {
        [JsonProperty("botToken")]
        public string Token { get; private set; }

        [JsonProperty("botPrefix")]
        public string Prefix { get; private set; }
    }
}