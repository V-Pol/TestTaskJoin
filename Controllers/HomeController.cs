using JoinTable.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace JoinTable.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static SqlCommand command = new SqlCommand();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string connectionString = "Server=(local)\\SQLEXPRESS;Database=jointest;User Id=sa;Password=P@ssw0rd;";
            List<Report> reports = new List<Report>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection.State.ToString() == "Open")
                {
                    command.Connection = connection;
                    Console.WriteLine("Подключение к БД открыто");
                    reports = GetFromDB();
                }
                else
                {
                    Console.WriteLine("Не удалось подключиться к БД");
                }
            }
            Console.WriteLine("Подключение  к БД закрыто");
            return View(reports);
        }

        private static List<Report> GetFromDB()
        {
            string sqlExpression =
                @"SELECT ReportName, FrequencyText 
                FROM Reports JOIN FrequencyReports 
                ON(Reports.FrequencyReportId = FrequencyReports.FrequencyReportId) 
                ORDER BY Reports.FrequencyReportId";
            List<Report> reports = new List<Report>();
            command = new SqlCommand(sqlExpression, command.Connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var name = reader.GetValue(0).ToString()!;
                    var frequency = reader.GetValue(1).ToString()!;
                    Report report = new Report {ReportName = name, FrequencyText = frequency };
                    reports.Add(report);
                }
            }
            reader.Close();
            return reports;
        }
    }
}