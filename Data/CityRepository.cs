using loc_api_crud.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace loc_api_crud.Data
{
    public class CityRepository
    {
        private readonly IConfiguration _configuration;
        public CityRepository(IConfiguration cofigration) {
            this._configuration = cofigration;
        }

        public List<CityModel> GetAllCities()
        {
            string str = this._configuration.GetConnectionString("myConnString");
            List<CityModel> cities= new List<CityModel>();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "loc_city_selectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CityModel city = new CityModel();
                    city.CityID = Convert.ToInt32(reader["CityID"]);
                    city.StateID = Convert.ToInt32(reader["StateID"]);
                    city.CountryID = Convert.ToInt32(reader["CountryID"]);
                    city.CityName = reader["CityName"].ToString();
                    city.CityCode = reader["CityCode"].ToString();
                    cities.Add(city);
                }
                conn.Close();
            }
            return cities;
        } 

        public bool DeleteCity(int CityID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_City_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CityID", CityID);
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public bool AddCity(CityModel cityModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_City_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CityName", cityModel.CityName);
            cmd.Parameters.AddWithValue("CityCode", cityModel.CityCode);
            cmd.Parameters.AddWithValue("CountryID", cityModel.CountryID);
            cmd.Parameters.AddWithValue("StateID", cityModel.StateID);
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public bool EditCity(CityModel cityModel,int cityID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_City_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CityID", cityModel.CityID);
            cmd.Parameters.AddWithValue("CityName", cityModel.CityName);
            cmd.Parameters.AddWithValue("CityCode", cityModel.CityCode);
            cmd.Parameters.AddWithValue("StateID", cityModel.StateID);
            cmd.Parameters.AddWithValue("CountryID", cityModel.CountryID);
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public List<CountryDropDown> GetAllCountries()
        {
            string connectionString = _configuration.GetConnectionString("myConnString");
            List<CountryDropDown> countries = new List<CountryDropDown>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "loc_CountryDropdown";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(new CountryDropDown
                            {
                                CountryID = Convert.ToInt32(reader["CountryID"]),
                                CountryName = reader["CountryName"].ToString()
                            });
                        }
                    }
                }
            }

            return countries;
        }

        public List<StateDropDown> GetAllStatesByCountryID(int countryID)
        {
            string connectionString = _configuration.GetConnectionString("myConnString");
            List<StateDropDown> states = new List<StateDropDown>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_LOC_State_SelectComboBoxByCountryID";
                    cmd.Parameters.AddWithValue("@CountryID", countryID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            states.Add(new StateDropDown
                            {
                                StateID = Convert.ToInt32(reader["StateID"]),
                                StateName = reader["StateName"].ToString()
                            });
                        }
                    }
                }
            }

            return states;
        }

        public CityModel GetByIDCity(int cityID)
        {
            string str = this._configuration.GetConnectionString("myConnString");
            CityModel city = new CityModel();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectByPK";
                cmd.Parameters.AddWithValue("@CityID", cityID);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    city.CityID = Convert.ToInt32(reader["CityID"]);
                    city.StateID = Convert.ToInt32(reader["StateID"]);
                    city.CountryID = Convert.ToInt32(reader["CountryID"]);
                    city.CityName = reader["CityName"].ToString();
                    city.CityCode = reader["CityCode"].ToString();
                }
            }
            return city;
        }
    }
}
