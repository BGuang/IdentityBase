﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;

namespace ServiceBase.IdentityServer.Public.EntityFramework.Entities
{
    public class ClientRedirectUri
    {
        public Guid Id { get; set; }
        public string RedirectUri { get; set; }
        public Client Client { get; set; }
    }
}