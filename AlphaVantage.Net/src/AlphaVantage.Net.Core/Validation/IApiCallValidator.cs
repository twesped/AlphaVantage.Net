﻿using System.Collections.Generic;

namespace Tw.AlphaVantage.Net.Core.Validation
{
    /// <summary>
    /// Interface for request's validator. 
    /// </summary>
    /// <remarks>
    /// Implement it for your client in case you need pre-request validation
    /// </remarks>
    public interface IApiCallValidator
    {
        ValidationResult Validate(ApiFunction function, IDictionary<string, string> query);
    }
}