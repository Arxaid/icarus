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

        [JsonProperty("dbHost")]
        public string Host { get; private set; }

        [JsonProperty("dbName")]
        public string Name { get; private set; }

        [JsonProperty("dbUser")]
        public string User { get; private set; }

        [JsonProperty("dbPassword")]
        public string Password { get; private set; }
    }
}