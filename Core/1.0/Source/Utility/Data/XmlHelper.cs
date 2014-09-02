using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Cdts.Utility.Data
{
    public class XmlHelper
    {
        private string xmlDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\XmlData.xml");

        public XDocument LoadXml(string path)
        {
            XDocument doc = XDocument.Load(path);
            return doc;
        }

        public void GetData()
        {
            XDocument doc = this.LoadXml(xmlDataPath);
            XElement group = doc.Element("Group");
            List<XElement> users = group.Elements().ToList();
            foreach (XElement user in users)
            {
                XElement name = user.Element("Name");
                string nameStr = name.Name.LocalName;
                string p1 = name.Attribute("property").Value;
                string user1 = name.Value;

                XElement password = user.Element("Password");
                string passwordStr = password.Name.LocalName;
                string en = password.Attribute("language").Value;
                string p123 = password.Value;
            }
        }

    }
}
