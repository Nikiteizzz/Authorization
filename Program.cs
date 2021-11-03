using System;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace NewAuthoriz
{
    class Program
    {
        struct User
        {
            public string login;
            public string password;
            public int role;
            public int idInDb;
        }
        static void Main()
        {
            User curr_user;
            string com, connectionString = "Server=.\\SQLEXPRESS;Database=UserData;Trusted_Connection=True;";
            bool flag = true;
            using (SqlConnection connector = new SqlConnection(connectionString))
            {
                connector.Open();
                Console.WriteLine("Подключение открыто!");
                while (flag)
                {
                    Console.Write("Введите логин: ");
                    curr_user.login = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    curr_user.password = Console.ReadLine();
                    com = ("SELECT * FROM tab WHERE Login = '" + curr_user.login + "' AND Password = '" + curr_user.password + "'");
                    SqlCommand command = new SqlCommand(com, connector);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        curr_user.role = Convert.ToInt32(reader.GetValue(2));
                        curr_user.idInDb = Convert.ToInt32(reader.GetValue(3));
                        flag = false;
                        Console.WriteLine("Успешно!");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Проверьте правильность ввода данных!");
                    }
                    reader.Close();
                }
                Console.ReadLine();
            }
        }
    }
}
