using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EvalDrum.API.Services;
using EvalDrum.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EvalDrum.API_Unit_Tests
{
    [TestClass]
    public class TestConnection
    {
        [TestMethod]
        public void TestMethod1()
        {
            var drum_data = new List<Drum>
            {
                new Drum{ Id = 1, DrumNumber = "BBB", DrumManager_Id = 1, Site_Id = 1, Status_Id = 1, CreatedOn = DateTime.UtcNow, InPositionSince = DateTime.UtcNow, Latitude = 55.555555, Longitude = 50.666666 }
            }.AsQueryable();

            var drum_mockset = new Mock<DbSet<Drum>>();
            drum_mockset.As<IQueryable<Drum>>().Setup(m => m.Provider).Returns(drum_data.Provider);
            drum_mockset.As<IQueryable<Drum>>().Setup(m => m.Expression).Returns(drum_data.Expression);
            drum_mockset.As<IQueryable<Drum>>().Setup(m => m.ElementType).Returns(drum_data.ElementType);
            drum_mockset.As<IQueryable<Drum>>().Setup(m => m.GetEnumerator()).Returns(drum_data.GetEnumerator());

            var drumManager_data = new List<DrumManager>
            {
                new DrumManager{ Id = 1, Name = "Nexans France", ContactEmail = "Nexans_France@test.fr"}
            }.AsQueryable();

            var drumManager_mockset = new Mock<DbSet<DrumManager>>();
            drumManager_mockset.As<IQueryable<DrumManager>>().Setup(m => m.Provider).Returns(drumManager_data.Provider);
            drumManager_mockset.As<IQueryable<DrumManager>>().Setup(m => m.Expression).Returns(drumManager_data.Expression);
            drumManager_mockset.As<IQueryable<DrumManager>>().Setup(m => m.ElementType).Returns(drumManager_data.ElementType);
            drumManager_mockset.As<IQueryable<DrumManager>>().Setup(m => m.GetEnumerator()).Returns(drumManager_data.GetEnumerator());

            var site_data = new List<Site>
            {
                new Site{ Id = 1, Name = "SERVAL_LOGISTIC_SITE_BORDEAUX_1"}
            }.AsQueryable();

            var site_mockset = new Mock<DbSet<Site>>();
            site_mockset.As<IQueryable<Site>>().Setup(m => m.Provider).Returns(site_data.Provider);
            site_mockset.As<IQueryable<Site>>().Setup(m => m.Expression).Returns(site_data.Expression);
            site_mockset.As<IQueryable<Site>>().Setup(m => m.ElementType).Returns(site_data.ElementType);
            site_mockset.As<IQueryable<Site>>().Setup(m => m.GetEnumerator()).Returns(site_data.GetEnumerator());

            var status_data = new List<Status>
            {
                new Status{ Id = 1, Status_name = "OnSite"}
            }.AsQueryable();

            var status_mockset = new Mock<DbSet<Status>>();
            status_mockset.As<IQueryable<Status>>().Setup(m => m.Provider).Returns(status_data.Provider);
            status_mockset.As<IQueryable<Status>>().Setup(m => m.Expression).Returns(status_data.Expression);
            status_mockset.As<IQueryable<Status>>().Setup(m => m.ElementType).Returns(status_data.ElementType);
            status_mockset.As<IQueryable<Status>>().Setup(m => m.GetEnumerator()).Returns(status_data.GetEnumerator());


            var mockContext = new Mock<EvalDrumContext>();
            mockContext.Setup(d => d.Drums).Returns(drum_mockset.Object);
            mockContext.Setup(dm => dm.DrumManagers).Returns(drumManager_mockset.Object);
            mockContext.Setup(s => s.Sites).Returns(site_mockset.Object);
            mockContext.Setup(st => st.Status).Returns(status_mockset.Object);



            var service = new DrumsService(mockContext.Object);
            service.CreatDrum("unitTest1", "Nexans France", "SERVAL_LOGISTIC_SITE_BORDEAUX_1", "OnSite", 23.000666, 302.666666);

            drum_mockset.Verify(d => d.Add(It.IsAny<Drum>()), Times.Once);
            mockContext.Verify(d => d.SaveChanges(), Times.Once);

        }
    }
}
