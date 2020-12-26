using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Web.Models
{
    public class DatabaseRepository
    {
        private readonly SqlConnection _sqlConnection = new SqlConnection(@"Server=.;Database=shihab;User Id=sa;Password=sa1234;");

        private DataTable _dataTable;
        private SqlDataAdapter _dataAdapter;

        public List<Student> GetAll()
        {
            _sqlConnection.Open();

            _dataTable = new DataTable();
            _dataAdapter = new SqlDataAdapter();

            using (var cmd = new SqlCommand("select * from students", _sqlConnection))
            {
                _dataAdapter.SelectCommand = cmd;
                _dataAdapter.Fill(_dataTable);
            }

            _sqlConnection.Close();
            _dataAdapter.Dispose();
            _sqlConnection.Dispose();

            return DataTableToList(_dataTable);
        }

        public List<Student> DataTableToList(DataTable dt)
        {
            var list = new List<Student>();

            foreach (DataRow row in dt.Rows)
            {
                var student = new Student { 
                    FirstName = row["FirstName"].ToString(), 
                    LastName = row["LastName"].ToString() 
                };

                list.Add(student);
            }

            return list;
        }

        public bool Add(Student model)
        {
            _sqlConnection.Open();

            var _query = $"INSERT INTO [dbo].[students]([FirstName],[LastName]) VALUES ('{model.FirstName}','{model.LastName}'";

            var count = 0;
            using (var cmd = new SqlCommand(_query, _sqlConnection))
            {
                count = cmd.ExecuteNonQuery();
            }

            _sqlConnection.Close();
            _sqlConnection.Dispose();

            return count > 0;
        }

        //T Get(int id) { }
        //bool Delete(int id) { }
        //void Update(T model) { }
    }
}
