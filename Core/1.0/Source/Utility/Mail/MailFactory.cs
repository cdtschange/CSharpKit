using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using Cdts.Utility.Mail.MailSets;

namespace Cdts.Utility.Mail
{
    public class MailFactory
    {
        public static string MailCarrierPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Mail");

        public static MailModel CreateMailModel(string fullName, string password)
        {
            MailModel model = new MailModel();
            string[] strs = fullName.Split('@');
            model.Account = strs[0];
            model.Password = password;
            string carrierName = strs[1];
            string path = MailCarrierPath;
            if (Directory.Exists(path))
            {
                path = Path.Combine(path, "MailCarrier.xml");
                XDocument loaded = XDocument.Load(path);
                XElement element = loaded.Element("Carriers");
                if (element != null)
                {
                    element = element.Elements("Carrier").FirstOrDefault(e => e.Attribute("Name").Value == carrierName);
                    if (element != null)
                    {
                        MailCarrier carrier = new MailCarrier();
                        carrier.MailDomain = carrierName;
                        if (element.Attribute("LoginUrl") != null)
                        {
                            carrier.LoginUrl = element.Attribute("LoginUrl").Value;
                        }
                        if (element.Attribute("IsPOP3") != null)
                        {
                            carrier.IsPOP3 = bool.Parse(element.Attribute("IsPOP3").Value);
                        }
                        if (element.Attribute("IsNeedActivation") != null)
                        {
                            carrier.IsNeedActivation = bool.Parse(element.Attribute("IsNeedActivation").Value);
                        }
                        if (element.Attribute("POP3IP") != null)
                        {
                            carrier.POP3IP = element.Attribute("POP3IP").Value;
                        }
                        if (element.Attribute("POP3Port") != null)
                        {
                            carrier.POP3Port = short.Parse(element.Attribute("POP3Port").Value);
                        }
                        if (element.Attribute("POP3SSL") != null)
                        {
                            carrier.POP3SSL = int.Parse(element.Attribute("POP3SSL").Value);
                        }
                        if (element.Attribute("SMTPIP") != null)
                        {
                            carrier.SMTPIP = element.Attribute("SMTPIP").Value;
                        }
                        if (element.Attribute("SMTPPort") != null)
                        {
                            carrier.SMTPPort = short.Parse(element.Attribute("SMTPPort").Value);
                        }
                        if (element.Attribute("SMTPSSL") != null)
                        {
                            carrier.SMTPSSL = int.Parse(element.Attribute("SMTPSSL").Value);
                        }
                        model.Carrier = carrier;
                    }
                }
            }
            return model;
        }

        public static MailBase CreateMailBase(MailModel model)
        {
            MailBase mail = null;
            switch (model.Carrier.MailDomain)
            {
                case "163.com":
                    mail = new Mail_163com(model);
                    break;
                case "126.com":
                    mail = new Mail_126com(model);
                    break;
                case "gmail.com":
                    mail = new Mail_Gmailcom(model);
                    break;
                case "qq.com":
                    mail = new Mail_QQcom(model);
                    break;
                case "sohu.com":
                    mail = new Mail_Sohucom(model);
                    break;
                case "yeah.net":
                    mail = new Mail_Yeahnet(model);
                    break;
                default:
                    break;
            }
            return mail;
        }
    }
}
