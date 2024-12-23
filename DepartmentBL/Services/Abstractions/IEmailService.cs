﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentBL.Services.Abstractions
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string message);
    }
}