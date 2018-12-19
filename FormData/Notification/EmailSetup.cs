using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace FormData.Notification
{
    public class EmailSetup
    {
        //MailMessage obj_mail;
        /// <summary>
        /// SmtpClient - it's a class - Simple Mail Transfer Protocol (SMTP) class
        /// </summary>
        SmtpClient client;

        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string ProductName { get; set; }
        public decimal? Total { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Items { get; set; }

        private string fromAddress = null;
        public string FromAddress
        {
            get { return fromAddress; }
            set { fromAddress = value; }
        }

        private string toAddress = null;
        public string ToAddress
        {
            get { return toAddress; }
            set { toAddress = value; }
        }
        private string subject = null;
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        private string massage = null;
        public string Massage
        {
            get { return massage; }
            set { massage = value; }
        }

        //  Test2018$
        private const string password = "Test2018$";

        // project2018
        // email address is euoz2018@gmail.com
        private const string userName = "";
        public string UserName
        {
            get { return userName; }
        }
        public string success = "Email sent successfully.";
        public string Success
        {
            get { return success; }
        }
        private string notSuccess = "Email did not sent.";
        public string NotSuccess
        {
            get { return notSuccess; }
        }


        //LogEditor log01 = new LogEditor();

        const string hostName = "smtp.gmail.com"; // smtp.gmail.com", port 587
        const int port = 587;

        /// <summary>
        /// SendIt method - setting/preparing SmtpClient email object with all necessary information 
        /// and sends an email.
        /// </summary>
        public string SendIt()
        {
            //toAddress ;
            fromAddress = "eugenebobalo@gmail.com";

            client = new SmtpClient(hostName, port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(fromAddress, password);
            //toAddress = "eugenebobalo@yahoo.com";
            try
            {
                client.Send(fromAddress, toAddress, subject, massage);
                 return Success;
            }
            catch (Exception ex)
            {
                return $"{ NotSuccess} - {ex}";
            }
        } // end of SendIt method
    } // end of EmailSetup
}