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



































        public void Dispose()
        {
            Band.DeleteAll();
        }
    }
}
