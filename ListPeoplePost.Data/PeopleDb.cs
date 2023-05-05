using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ListPeoplePost.Data
{
    public class PeopleDb
    {
        private string _connectionString;

        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAllPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var list = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],
                });
            }
            return list;
        }

        public void Add(Person p)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People(firstName, lastName, age) " +
                "VALUES (@fName, @lName, @age)";
            cmd.Parameters.AddWithValue("@fName", p.FirstName);
            cmd.Parameters.AddWithValue("@lName", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void AddMany(List<Person> people)
        {
            foreach (var person in people)
            {
                Add(person);
            }
        }
    }
}
