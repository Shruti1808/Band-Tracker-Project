using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class BandTest : IDisposable
    {
        public BandTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Band.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_EqualOverrideTrueForSameName()
        {
            //Arrange, Act
            Band firstBand = new Band("The Beatles");
            Band secondBand = new Band("The Beatles");

            //Assert
            Assert.Equal(firstBand, secondBand);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Band testBand = new Band("Grouplove");

            //Act
            testBand.Save();
            Band savedBand = Band.GetAll()[0];

            int result = savedBand.GetId();
            int testId = testBand.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Save()
        {
            //Arrange
            Band testBand = new Band("Metallica");
            testBand.Save();

            //Act
            List<Band> result = Band.GetAll();
            List<Band> testList = new List<Band>{testBand};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Find_FindsBandsInDatabase()
        {
            //Arrange
            Band testBand = new Band("The Rolling Stones");
            testBand.Save();
            //Act
            Band foundBand = Band.Find(testBand.GetId());

            //Assert
            Assert.Equal(testBand,foundBand);

        }

        [Fact]
        public void Test_Update_UpdateBandInDatabase()
        {
            //Arrange
            Band newBand = new Band ("Metallica");
            newBand.Save();

            string newName = "ColdPlay";

            //Act
            newBand.Update(newName);
            string result = newBand.GetName();

            //Assert.Equal
            Assert.Equal(newName, result);
        }

        [Fact]
        public void Test_Delete_DeleteSingleBandFromDatabase()
        {
            //Arrange
            Band testBand1 = new Band("ColdPlay");
            testBand1.Save();
            Band testBand2 = new Band("ColdPlay");
            testBand2.Save();

            //Act
            testBand1.Delete();
            List<Band> result = Band.GetAll();
            List<Band> resultList = new List<Band> {testBand2};

            Assert.Equal(result, resultList);
        }


        [Fact]
        public void Test_AddVenue_AddsVenueToBand()
        {
            //Arrange
            Band testBand = new Band("ColdPlay");
            testBand.Save();

            Venue testVenue = new Venue("KeyArena Theater");
            testVenue.Save();

            //Act
            testBand.AddVenue(testVenue);

            List<Venue> result = testBand.GetVenues();
            List<Venue> testList = new List<Venue>{testVenue};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_GetVenues_ReturnsAllBandVenues()
        {
            //Arrange
            Band testBand = new Band("ColdPlay");
            testBand.Save();

            Venue testVenue1 = new Venue("Roseland Theater");
            testVenue1.Save();

            Venue testVenue2 = new Venue("Star Theater");
            testVenue2.Save();

            //Act
            testBand.AddVenue(testVenue1);
            List<Venue> result = testBand.GetVenues();
            List<Venue> testList = new List<Venue> {testVenue1};

            //Assert
            Assert.Equal(testList, result);
        }

        public void Dispose()
        {
            Band.DeleteAll();
            Venue.DeleteAll();
        }
    }
}
