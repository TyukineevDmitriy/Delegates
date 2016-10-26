using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class TransactionProcessor
    {
        Func<TransactionRequest, bool> Check;
        Func<TransactionRequest, Transaction> Register;
        Action<Transaction> Save;

        public TransactionProcessor(Func<TransactionRequest, bool> check,
            Func<TransactionRequest, Transaction> register,
            Action<Transaction> save)
        {
            Check = check;
            Register = register;
            Save = save;
        }

        public Transaction Process(TransactionRequest request)
        {
            if (!Check(request))
                throw new ArgumentException();
            var result = Register(request);
            Save(result);
            return result;
        }
    }

    public class Transaction
    {
    }

    public class TransactionRequest
    {
    }
}
