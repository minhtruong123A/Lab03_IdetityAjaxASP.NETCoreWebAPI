﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Helper.Schema_Response
{
    public class ResponseModel<T>
    {
        [JsonPropertyName("status")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("error-code")]
        public int ErrorCode { get; set; }

        public ResponseModel() => Success = true;
    }
}
