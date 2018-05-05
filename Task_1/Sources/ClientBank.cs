// MIT License
// 
// Copyright(c) 2018 Alexander Popelyuk
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


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

        public override string ToString()
        {
            return string.Format("{0} {1} {2}, operations: {3}",
                FirstName, LastName, MiddleName, Operations.Length);
        }
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
        // Clients balances at the end of the month.
        public ClinetBalance[] BalanceList;
        // Clients, which not withdraw money during the month.
        public ClientInfo[] LoyalClients;
        // Clients with maximal debit sum.
        public ClientInfo DebitFavorite;
        // Clients with maximal credit sum.
        public ClientInfo CreditFavorite;
        // Clinet with maximal balance at the end of the month.
        public ClientInfo BanaceFavorite;
    }
}
