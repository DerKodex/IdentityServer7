// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer7.Extensions;
using IdentityServer7.Hosting;
using IdentityServer7.ResponseHandling;
using Microsoft.AspNetCore.Http;

namespace IdentityServer7.Endpoints.Results
{
    public class DeviceAuthorizationResult : IEndpointResult
    {
        public DeviceAuthorizationResponse Response { get; }

        public DeviceAuthorizationResult(DeviceAuthorizationResponse response)
        {
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.SetNoCache();

            var dto = new ResultDto
            {
                device_code = Response.DeviceCode,
                user_code = Response.UserCode,
                verification_uri = Response.VerificationUri,
                verification_uri_complete = Response.VerificationUriComplete,
                expires_in = Response.DeviceCodeLifetime,
                interval = Response.Interval
            };

            await context.Response.WriteJsonAsync(dto);
        }

        public class ResultDto
        {
            public string device_code { get; set; }
            public string user_code { get; set; }
            public string verification_uri { get; set; }
            public string verification_uri_complete { get; set; }
            public int expires_in { get; set; }
            public int interval { get; set; }
        }
    }
}