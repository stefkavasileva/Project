using System.Linq;
using AutoMapper;
using Landmarks.Common.Models.Admin.BindingModels;
using Landmarks.Data;
using Landmarks.Models;
using Landmarks.Services.Admin;
using Landmarks.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Landmarks.Tests.Services.Admin
{
    [TestClass]
    public class RegionServiceTests
    {
        private LandmarksDbContext _dbContext;
        private IMapper _mapper;

        [TestMethod]
        public void GetRegions_WithAFewRegion_ShouldReturnAll()
        {
            //Arange   
            this._dbContext.Regions.Add(new Region { Id = 1, Area = 1234, Population = 12455, Name = "Region Name" });
            this._dbContext.Regions.Add(new Region { Id = 2, Area = 11423, Population = 1272455, Name = "Region Name 2" });

            this._dbContext.SaveChanges();

            var service = new RegionService(this._dbContext, this._mapper, null);

            //Act
            var regions = service.GetRegions();

            //Assert
            Assert.IsNotNull(regions);
            Assert.AreEqual(2, regions.Count());
        }

        [TestMethod]
        public void GetRegions_WithNoRegions_ShouldReturnNone()
        {
            //Arange         
            var service = new RegionService(this._dbContext, this._mapper, null);

            //Act
            var regions = service.GetRegions();

            //Assert
            Assert.IsNotNull(regions);
            Assert.AreEqual(0, regions.Count());
        }

        [TestMethod]
        public void CreateRegion_WithValidModel_SholdSaveInDb()
        {
            //Arange         
            var service = new RegionService(this._dbContext, this._mapper, null);

            var validRegion = new AddEditRegionBindingModel { Id = 1, Name = "Region", Area = 1478 };
            //Act
            service.CreateRegion(validRegion);

            //Assert
            Assert.AreEqual(1, this._dbContext.Regions.Count());
        }

        [TestMethod]
        public void GetRegion_WithValidId_SholdReturnRegion()
        {
            //Arange         
            var service = new RegionService(this._dbContext, this._mapper, null);
            this._dbContext.Regions.Add(new Region { Id = 2, Area = 125, Name = "Test", Population = 166 });
            this._dbContext.SaveChanges();

            //Act
            var region = service.GetRegion(2);

            //Assert
            Assert.IsNotNull(region);
            Assert.AreEqual("Test", region.Name);
            Assert.AreEqual(125, region.Area);
            Assert.AreEqual(166, region.Population);
        }

        [TestMethod]
        public void GetRegion_WithInvalidId_SholdReturnNull()
        {
            //Arange         
            var service = new RegionService(this._dbContext, this._mapper, null);

            //Act
            var region = service.GetRegion(2);

            //Assert
            Assert.IsNull(region);
        }

        [TestInitialize]
        public void InitializeTests()
        {
            this._dbContext = MockDbContext.GetContext();
            this._mapper = MockAutoMapper.GetMapper();
        }
    }
}
