﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketSystem.Domain
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }

        public string SmtpUserName { get; set; }

        public string SmtpPassword { get; set; }

        public int SmptServerPort { get; set; }

        public bool EnableSsl { get; set; }

        public string EmailDisplayName { get; set; }

        public string SendersName { get; set; }


        public EmailSettings()
        {

        }

        public EmailSettings(string smtpServer,
            string smtpUserName,
            string smtpPassword,
            int smtpServerPort)
        {
            this.SmtpServer = smtpServer;
            this.SmtpUserName = smtpUserName;
            this.SmtpPassword = smtpPassword;
            this.SmptServerPort = smtpServerPort;
        }

    }
}
