﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreSecurity.Domain.ApplicationServices
{
    public class IdGenerator
    {
        public string NewEntityId() => Guid.NewGuid().ToString();
    }
}
