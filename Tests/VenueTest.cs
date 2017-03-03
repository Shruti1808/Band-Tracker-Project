using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class VenueTest : IDisposable
    {
        public VenueTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }


        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Venue.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_EqualOverrideTrueForSameName()
        {
            //Arrange, Act
            Venue firstVenue = new Venue("KeyArena Theater");
            Venue secondVenue = new Venue("KeyArena Theater");

            //Assert
            Assert.Equal(firstVenue, secondVenue);
        }

        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            //Arrange
            Venue testVenue = new Venue("Roseland Theater");

            //Act
            testVenue.Save();
            List<Venue> result = Venue.GetAll();
            List<Venue> testList = new List<Venue>{testVenue};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Find_FindsVenueInDatabase()
        {
            //Arrange
            Venue testVenue = new Venue("Roseland Theater");
            testVenue.Save();

            //Act
            Venue foundVenue = Venue.Find(testVenue.GetId());

            //Assert
            Assert.Equal(testVenue, foundVenue);
        }

        public void Dispose()
        {
            Band.DeleteAll();
            Venue.DeleteAll();
        }

    }
}
