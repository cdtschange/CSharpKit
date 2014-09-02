using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Web;
using System.Net;

namespace Cdts.Utility.Mail
{
    public abstract class MailBase
    {
        protected IHttpSendBase httpClient = null;
        protected CookieContainer cookie = null;
        protected MailModel model = null;
        public string ErrorMessage = "";
        protected bool isLogin = false;

        public MailBase(MailModel model)
        {
            this.model = model;
            this.cookie = new CookieContainer();
            this.httpClient = new HttpSendBase();
        }


        public bool DeleteAllMail()
        {
            POP3Mail pop3 = new POP3Mail(model);
            try
            {
                return pop3.DeleteAllMail();
            }
            catch
            {
                ErrorMessage = pop3.ErrorMessage;
                return false;
            }
        }

        public bool MatchMail(string subject, string body)
        {
            POP3Mail pop3 = new POP3Mail(model);
            try
            {
                if (!pop3.MatchMail(subject, body))
                {
                    ErrorMessage = pop3.ErrorMessage;
                    return false;
                }
                return true;
            }
            catch
            {
                ErrorMessage = pop3.ErrorMessage;
                return false;
            }
        }

        public bool SendMail(MailModel model2, string subject, string body)
        {
            SMTPMail smtp = new SMTPMail(model);
            try
            {
                if (!smtp.SendMail(model2, subject, body))
                {
                    ErrorMessage = smtp.ErrorMessage;
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = smtp.ErrorMessage;
                return false;
            }
        }


        #region 抽象方法

        public abstract bool Login();

        public abstract bool RegistAccount();

        public abstract bool CheckRegistAccount();

        public abstract bool ChangePassword(string newPassword);

        public abstract bool OpenPOP3();

        public abstract bool SetAutoForward(string email);

        public abstract bool CancelAutoForward();

        public abstract bool ActiveCertification(MailModel nextLogin);

        #endregion

    }

}
