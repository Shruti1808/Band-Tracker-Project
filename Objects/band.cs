using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class Band
    {
        // Band properties
        private int _id;
        private string _name;

        // constructors, getters, setters
        public Band(string Name, int Id = 0)
        {
            _id = Id;
            _name = Name;
        }

        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        public void SetName(string newName)
        {
            _name = newName;
        }

        public override bool Equals(System.Object otherBand)
        {
            if (!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool idEquality = this.GetId() == newBand.GetId();
                bool nameEquality = this.GetName() == newBand.GetName();
                return (idEquality && nameEquality);
            }
        }

        public static List<Band> GetAll()
        {
            List<Band> allBands = new List<Band>{};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read()){
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);
                Band newBand = new Band(bandName,bandId);
                allBands.Add(newBand);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return allBands;

        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandName);", conn);
            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@BandName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if(rdr!=null)
            {
                rdr.Close();
            }
            if(conn!=null)
            {
                conn.Close();
            }
        }

        public static Band Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id= (@BandId);",conn);
            SqlParameter bandIdParameter = new SqlParameter();
            bandIdParameter.ParameterName = "@BandId";
            bandIdParameter.Value = id.ToString();
            cmd.Parameters.Add(bandIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundBandId = 0;
            string foundBandName = null;

            while(rdr.Read())
            {
                foundBandId = rdr.GetInt32(0);
                foundBandName = rdr.GetString(1);
            }

            Band foundBand = new Band(foundBandName,foundBandId);
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundBand;
        }

        public void Update (string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @NewName OUTPUT INSERTED.name WHERE id = @BandId;", conn);

            SqlParameter newNameParameter = new SqlParameter();
            newNameParameter.ParameterName = "@NewName";
            newNameParameter.Value = newName;
            cmd.Parameters.Add(newNameParameter);

            SqlParameter bandIdParameter = new SqlParameter();
            bandIdParameter.ParameterName = "@BandId";
            bandIdParameter.Value = this.GetId();
            cmd.Parameters.Add(bandIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._name = rdr.GetString(0);
            }

            if (rdr != null)
            {
                rdr.Close();
            }

            if (conn != null)
            {
                conn.Close();
            }

        }


        public void AddVenue(Venue newVenue)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);

            SqlParameter bandIdParameter = new SqlParameter();
            bandIdParameter.ParameterName = "@BandId";
            bandIdParameter.Value = this.GetId();

            SqlParameter venueIdParameter = new SqlParameter();
            venueIdParameter.ParameterName = "@VenueId";
            venueIdParameter.Value = newVenue.GetId();

            cmd.Parameters.Add(bandIdParameter);
            cmd.Parameters.Add(venueIdParameter);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public List<Venue> GetVenues()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN venues_bands  ON (bands.id = venues_bands .band_id) JOIN venues ON (venues_bands .venue_id = venues.id) WHERE bands.id = @BandId", conn);
      SqlParameter BandIdParameter = new SqlParameter();
      BandIdParameter.ParameterName = "@BandId";
      BandIdParameter.Value = this.GetId().ToString();

      cmd.Parameters.Add(BandIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> venues = new List<Venue>{};

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueDescription = rdr.GetString(1);
        Venue newVenue = new Venue(venueDescription, venueId);
        venues.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return venues;
    }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @BandId; DELETE FROM venues_bands WHERE band_id = @BandId;", conn);
            SqlParameter bandIdParameter = new SqlParameter();
            bandIdParameter.ParameterName = "@BandId";
            bandIdParameter.Value = this.GetId();

            cmd.Parameters.Add(bandIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }


        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE from bands; DELETE FROM venues_bands;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
