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
using System.Text;
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
        [XmlAttribute("type")]
        public Type OperationType;
        public Decimal Amount;
        public DateTime Date;
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
    //
    // Summary:
    //   Class to represent client month totals: sum of all operations
    //   client performed during the month.
    public class MonthTotal
    {
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public decimal Total;
        //
        // Summary:
        //   Default constructor, required by serialization/deserialization routines.
        public MonthTotal()
        {
        }
        //
        // Summary:
        //   Non default constructor, used to create new object instance.
        public MonthTotal(string first_name, string last_name, string middle_name, decimal total)
        {
            FirstName = first_name;
            LastName = last_name;
            MiddleName = middle_name;
            Total = total;
        }
        //
        // Summary:
        //   Convert class object to readable string representation.
        //
        // Return:
        //   Class object string representation.
        public override string ToString()
        {
            return string.Format("{0} {1} {2}, Total: {3}",
                FirstName, LastName, MiddleName, Total);
        }
    }
    //
    // Summary:
    //   Class to represent client identification information.
    public class ClientInfo
    {
        public string FirstName;
        public string LastName;
        public string MiddleName;
        public DateTime FirstOperation;
        //
        // Summary:
        //   Default constructor, required by serialization/deserialization routines.
        public ClientInfo()
        {
        }
        //
        // Summary:
        //   Non default constructor, used to create new object instance.
        public ClientInfo(string first_name, string last_name, string middle_name, DateTime first_operation)
        {
            FirstName = first_name;
            LastName = last_name;
            MiddleName = middle_name;
            FirstOperation = first_operation;
        }
        //
        // Summary:
        //   Convert class object to readable string representation.
        //
        // Return:
        //   Class object string representation.
        public override string ToString()
        {
            return string.Format("{0} {1} {2}, FirstOperation: {3}",
                FirstName, LastName, MiddleName, FirstOperation != default(DateTime) ? FirstOperation.ToString() : "unknown");
        }
    }
    //
    // Summary:
    //   Class to represent different client-bank statistics.
    public class ClientStatistics
    {
        public MonthTotal[] AprilTotals;
        public ClientInfo[] NoAprilDebit;
        public ClientInfo MaxTotalDebit;
        public ClientInfo MaxTotalCredit;
        public ClientInfo MaxAprilBanace;
        //
        // Summary:
        //   Convert class object to readable string representation.
        //
        // Return:
        //   Class object string representation.
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine("April Totals:");
            if (AprilTotals != null && AprilTotals.Length > 0)
                foreach (var total in AprilTotals) builder.AppendLine(total.ToString());
            else builder.AppendLine("none");
            builder.AppendLine();
            builder.AppendLine("No April Debit:");
            if (NoAprilDebit != null && NoAprilDebit.Length > 0)
                foreach (var info in NoAprilDebit) builder.AppendLine(info.ToString());
            else builder.AppendLine("none");
            builder.AppendLine();
            builder.AppendLine("Max Total Debit:");
            if (MaxTotalDebit != null) builder.AppendLine(MaxTotalDebit.ToString());
            else builder.AppendLine("none");
            builder.AppendLine();
            builder.AppendLine("Max Total Credit:");
            if (MaxTotalCredit != null) builder.AppendLine(MaxTotalCredit.ToString());
            else builder.AppendLine("none");
            builder.AppendLine();
            builder.AppendLine("Max April balance (1 May 00:00 AM):");
            if (MaxAprilBanace != null) builder.AppendLine(MaxAprilBanace.ToString());
            else builder.AppendLine("none");
            return builder.ToString();
        }
    }
}
