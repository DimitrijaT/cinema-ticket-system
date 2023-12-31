﻿using CinemaTicketSystem.Domain.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaTicketSystem.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<EmailMessage> allMails);

    }
}


