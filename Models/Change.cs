using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Emergency_Services.Models
{
    public class Change
    {
        public string getHash(String getValue)
        {
            using (SHA1Managed has = new SHA1Managed())
            {
                var cypher = has.ComputeHash(Encoding.UTF8.GetBytes(getValue));
                var text = new StringBuilder(cypher.Length * 2);

                foreach (byte gethashed in cypher)
                {
                    text.Append(gethashed.ToString("X2"));
                }
                return text.ToString();
            }

        }
    }
}
