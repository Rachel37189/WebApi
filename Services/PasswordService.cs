using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repository;
namespace Services
{
    public class PasswordService
    {
        public PasswordEntity CheckPasswordStrength(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            int levelPass = result.Score;
            PasswordEntity passRes = new PasswordEntity();
            passRes.Password = password;
            passRes.Strength = levelPass;
            return passRes;
        }
    }
}
