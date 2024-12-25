using loc_api_crud.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace loc_api_crud.Data
{
    public class StateRepository
    {
        private readonly IConfiguration _configuration;
        public StateRepository(IConfiguration _configration)
        {
            this._configuration = _configration;
        }
        public List<StateModel> GetAllStates()
        {
            string str = this._configuration.GetConnectionString("myConnString");
            List<StateModel> states = new List<StateModel>();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "loc_state_selectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StateModel state = new StateModel();
                    state.StateID = Convert.ToInt32(reader["StateID"]);
                    state.CountryID= Convert.ToInt32(reader["CountryID"]);
                    state.StateName = reader["StateName"].ToString();
                    state.StateCode = reader["StateCode"].ToString();
                    states.Add(state);
                }
                conn.Close();
            }
            return states;
        }

        public bool DeleteState(int StateID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_State_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("StateID", StateID);
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public bool AddState(StateModel stateModel)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_Satet_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("StateName", stateModel.StateName);
            cmd.Parameters.AddWithValue("StateCode", stateModel.StateCode);
            cmd.Parameters.AddWithValue("CountryID", stateModel.CountryID);
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public bool EditState(StateModel stateModel,int stateID)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("myConnString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_LOC_State_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("StateID", stateID);
            cmd.Parameters.AddWithValue("StateName", stateModel.StateName);
            cmd.Parameters.AddWithValue("StateCode", stateModel.StateCode);
            cmd.Parameters.AddWithValue("CountryID", stateModel.CountryID);
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public StateModel GetByIDState(int stateID)
        {
            string str = this._configuration.GetConnectionString("myConnString");
            StateModel state = new StateModel();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectByPK";
                cmd.Parameters.AddWithValue("@StateID", stateID);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    state.StateID = Convert.ToInt32(reader["StateID"]);
                    state.StateName = reader["StateName"].ToString();
                    state.StateCode = reader["StateCode"].ToString();
                }
            }
            return state;
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
    }
}
