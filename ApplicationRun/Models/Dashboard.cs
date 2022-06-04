using ApplicationRun.Db;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRun.DashboardNamespace
{
    public struct RevenueByDate
    {
        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class Dashboard : DbConnection
    {
        //Fields & Properties
        private DateTime startDate;
    private DateTime endDate;
    private int numberDays;
    public int NumCustomers { get; private set; }
    public int NumSuppliers { get; private set; }
    public int NumProducts { get; private set; }
    public List<KeyValuePair<string, long>> TopProductsList { get; private set; }
    public List<KeyValuePair<string, long>> UnderstockList { get; private set; }
    public List<RevenueByDate> GrossRevenueList { get; private set; }
    public int NumOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalProfit { get; set; }
    //Constructor
    public Dashboard()
    {
    }
    //Private methods
    private void GetNumberItems()
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new MySqlCommand())
            {
                command.Connection = connection;
                //Get Total Number of Customers
                command.CommandText = "select count(id) from medical_institution mi";
                NumCustomers = Convert.ToInt32(command.ExecuteScalar());
                //Get Total Number of Suppliers
                command.CommandText = "select count(id) from diagnosis d";
                NumSuppliers = Convert.ToInt32(command.ExecuteScalar());
                //Get Total Number of Products
                command.CommandText = "SELECT COUNT(DISTINCT(d.name_district)) from district d ";
                NumProducts = Convert.ToInt32(command.ExecuteScalar());
                //Get Total Number of Orders
                command.CommandText = @"SELECT COUNT(c.id) FROM conclusion c " +
                                        "WHERE c.dt_receipt BETWEEN @fromDate and @toDate";
                command.Parameters.Add("@fromDate", MySqlDbType.DateTime).Value = startDate;
                command.Parameters.Add("@toDate", MySqlDbType.DateTime).Value = endDate;
                NumOrders = Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }
    private void GetProductAnalisys()
    {
        TopProductsList = new List<KeyValuePair<string, long>>();
        UnderstockList = new List<KeyValuePair<string, long>>();
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new MySqlCommand())
            {
                MySqlDataReader reader;
                command.Connection = connection;
                //Get Top 5 products
                command.CommandText = @"SELECT d.name_of_diagnosis,COUNT(c.id) FROM conclusion c
                                            JOIN diagnosis d ON c.id_diagnosis = d.id
                                            WHERE c.dt_receipt BETWEEN @fromDate and @toDate
                                            GROUP BY 1
                                            ORDER BY 2 DESC LIMIT 5";
                command.Parameters.Add("@fromDate", MySqlDbType.DateTime).Value = startDate;
                command.Parameters.Add("@toDate", MySqlDbType.DateTime).Value = endDate;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TopProductsList.Add(
                        new KeyValuePair<string, long>(reader[0].ToString(), (long)reader[1]));
                }
                reader.Close();
                //Get Understock
                command.CommandText = @"SELECT  d.name_district from district d ";
                reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    UnderstockList.Add(
                //        new KeyValuePair<string, long>(Convert.ToString(reader[0]), (long)(reader[1])));
                //}
                reader.Close();
            }
        }
    }
    private void GetOrderAnalisys()
    {
        GrossRevenueList = new List<RevenueByDate>();
        TotalProfit = 0;
        TotalRevenue = 0;
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new MySqlCommand())
            {
                command.Connection = connection;
                command.CommandText = @"SELECT c.dt_receipt, COUNT(c.id) FROM conclusion c 
                                            WHERE c.dt_receipt BETWEEN @fromDate and @toDate
                                            group by 1";
                command.Parameters.Add("@fromDate", MySqlDbType.DateTime).Value = startDate;
                command.Parameters.Add("@toDate", MySqlDbType.DateTime).Value = endDate;
                var reader = command.ExecuteReader();
                var resultTable = new List<KeyValuePair<DateTime, long>>();
                while (reader.Read())
                {
                    resultTable.Add(
                        new KeyValuePair<DateTime, long>((DateTime)reader[0], (long)reader[1])
                        );
                    TotalRevenue += (long)reader[1];
                }
                TotalProfit = TotalRevenue * 0.2m;//20%
                reader.Close();
                //Group by Hours
                if (numberDays <= 1)
                {
                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by orderList.Key.ToString("hh tt")
                                       into order
                                        select new RevenueByDate
                                        {
                                            Date = order.Key,
                                            TotalAmount = order.Sum(amount => amount.Value)
                                        }).ToList();
                }
                //Group by Days
                else if (numberDays <= 30)
                {
                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by orderList.Key.ToString("dd MMM")
                                       into order
                                        select new RevenueByDate
                                        {
                                            Date = order.Key,
                                            TotalAmount = order.Sum(amount => amount.Value)
                                        }).ToList();
                }
                //Group by Weeks
                else if (numberDays <= 92)
                {
                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                            orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                       into order
                                        select new RevenueByDate
                                        {
                                            Date = "Week " + order.Key.ToString(),
                                            TotalAmount = order.Sum(amount => amount.Value)
                                        }).ToList();
                }
                //Group by Months
                else if (numberDays <= (365 * 2))
                {
                    bool isYear = numberDays <= 365 ? true : false;
                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by orderList.Key.ToString("MMM yyyy")
                                       into order
                                        select new RevenueByDate
                                        {
                                            Date = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                                            TotalAmount = order.Sum(amount => amount.Value)
                                        }).ToList();
                }
                //Group by Years
                else
                {
                    GrossRevenueList = (from orderList in resultTable
                                        group orderList by orderList.Key.ToString("yyyy")
                                       into order
                                        select new RevenueByDate
                                        {
                                            Date = order.Key,
                                            TotalAmount = order.Sum(amount => amount.Value)
                                        }).ToList();
                }
            }
        }
    }
    //Public methods
    public bool LoadData(DateTime startDate, DateTime endDate)
    {
        endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day,
            endDate.Hour, endDate.Minute, 59);
        if (startDate != this.startDate || endDate != this.endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.numberDays = (endDate - startDate).Days;
            GetNumberItems();
            GetProductAnalisys();
            GetOrderAnalisys();
            Console.WriteLine("Refreshed data: {0} - {1}", startDate.ToString(), endDate.ToString());
            return true;
        }
        else
        {
            Console.WriteLine("Data not refreshed, same query: {0} - {1}", startDate.ToString(), endDate.ToString());
            return false;
        }
    }
}
}
