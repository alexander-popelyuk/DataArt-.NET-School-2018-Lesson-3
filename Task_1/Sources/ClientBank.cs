using System;
using System.Xml.Serialization;

namespace Task_1
{
    //
    // Summary:
    //   Class to represent finance operation.
    public class MoneyOperation
    {
        //
        // Summary:
        //   Money operation type.
        public enum Type
        {
            [XmlEnum(Name = "income")]
            Debit,
            [XmlEnum(Name = "expense")]
            Credit,
        }
        // Class members.
        [XmlAttribute("type")]
        public Type OperationType;
        public Decimal Amount;
        public DateTime Date;
        //
        // Summary:
        //   Convert operation to sane string representation.
        public override string ToString()
        {
            return string.Format("Date: {0}, Type: {1}, Amount: {2}",
                Date, Enum.GetName(typeof(MoneyOperation.Type), OperationType), Amount);
        }
    }
    //
    // Summary:
    //   Class to represent bank client.
    public class BankClient
    {
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public MoneyOperation[] Operations;
    }
    
    public class ClinetBalance
    {
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public decimal Balance;
    }
    
    public class ClientInfo
    {
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public DateTime FirstOperation;
    }
    
    public class ClientStatistics
    {
        public ClinetBalance[] BalanceList;
        public ClientInfo[] LoyalClients;
        public ClientInfo DebitFavorite;
        public ClientInfo CreditFavorite;
        public ClientInfo BanaceFavorite;
    }
}
