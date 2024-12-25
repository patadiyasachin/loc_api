using loc_api_crud.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace loc_api_crud.Data
{
    public class CountryRepository
    {
        private readonly IConfiguration _configuration;

        public CountryRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public List<CountryModel> GetAllCountry()
        {
            string str = this._configuration.GetConnectionString("myConnString");
            List<CountryModel> list = new List<CountryModel>();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "loc_country_selectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CountryModel country = new CountryModel();
                    country.CountryID = Convert.ToInt32(reader["CountryID"]);
                    country.CountryName = reader["CountryName"].ToString();
                    country.CountryCode = reader["CountryCode"].ToString();
                    list.Add(country);
                }
                conn.Close();
            }
            return list;
        }

        public bool DeleteCountry(int CountryID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_Country_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CountryID", CountryID);
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public bool AddCountry(CountryModel countryModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_Country_Insert",conn);
            cmd.CommandType= System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CountryName", countryModel.CountryName);
            cmd.Parameters.AddWithValue("CountryCode", countryModel.CountryCode);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }   
            else
            {
                return false;
            }
        }

        public bool EditCountry(CountryModel countryModel,int countryID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_Country_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CountryID", countryID);
            cmd.Parameters.AddWithValue("CountryName", countryModel.CountryName);
            cmd.Parameters.AddWithValue("CountryCode", countryModel.CountryCode);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CountryModel GetByIDCountry(int countryID)
        {
            string str = this._configuration.GetConnectionString("myConnString");
                CountryModel country = new CountryModel();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectByPK";
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                SqlDataReader reader = cmd.ExecuteReader();
               
                while (reader.Read())
                {
                    country.CountryID = Convert.ToInt32(reader["CountryID"]);
                    country.CountryName = reader["CountryName"].ToString();
                    country.CountryCode = reader["CountryCode"].ToString();
                }
            }
            return country;
        }
    }
}
