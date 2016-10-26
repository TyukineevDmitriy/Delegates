using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Task2
    {
        public class TransactionProcessor<Request, Object>
        {
            Func<Request, bool> Check;
            Func<Request, Object> Register;
            Action<Object> Save;

            public TransactionProcessor(Func<Request, bool> check,
                Func<Request, Object> register,
                Action<Object> save)
            {
                Check = check;
                Register = register;
                Save = save;
            }

            public Object Process(Request request)
            {
                if (!Check(request))
                    throw new ArgumentException();
                var result = Register(request);
                Save(result);
                return result;
            }
        }
    }
}
