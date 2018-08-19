using System.Linq;
using AutoMapper;
using Landmarks.Data;
using Landmarks.Models;
using Landmarks.Services.Admin;
using Landmarks.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Landmarks.Tests.Services.Admin
{
    [TestClass]
    public class CategoryServiceTests
    {
        private LandmarksDbContext _dbContext;
        private IMapper _mapper;

        [TestMethod]
        public void GetCategories_WithAFewCategories_ShouldReturnAll()
        {
            //Arange   
            this._dbContext.Categories.Add(new Category { Id = 1, Name = "Category1" });
            this._dbContext.Categories.Add(new Category { Id = 2, Name = "Category2" });
            this._dbContext.Categories.Add(new Category { Id = 3, Name = "Category3" });
            this._dbContext.SaveChanges();          

            var service = new CategoryService(this._dbContext, this._mapper, null);

            //Act
            var categies = service.GetCategories();

            //Assert
            Assert.IsNotNull(categies);
            Assert.AreEqual(3, categies.Count());
        }

        [TestMethod]
        public void GetCategories_WithNoCategories_ShouldReturnNone()
        {
            //Arange         
            var service = new CategoryService(this._dbContext, this._mapper, null);

            //Act
            var categies = service.GetCategories();

            //Assert
            Assert.IsNotNull(categies);
            Assert.AreEqual(0, categies.Count());
        }

        [TestMethod]
        public void CreateCategory_WithName_ShouldSaveInDB()
        {
            //Arange         
            var service = new CategoryService(this._dbContext, this._mapper, null);

            //Act
            service.CreateCategory("Category name");
            service.CreateCategory("Test category name");

            //Assert
            Assert.AreEqual(2,this._dbContext.Categories.Count());
        }

        [TestMethod]
        public void GetCategory_WithValidId_ShouldReturnCategory()
        {
            //Arange         
            var service = new CategoryService(this._dbContext, this._mapper, null);
            this._dbContext.Categories.Add(new Category {Id = 1, Name = "Some category name"});
            this._dbContext.SaveChanges();

            //Act
            var category = service.GetCategory(1);

            //Assert
            Assert.IsNotNull(category);
            Assert.AreEqual("Some category name",category.Name);
        }

        [TestMethod]
        public void GetCategory_WithInValidId_ShouldReturnNull()
        {
            //Arange         
            var service = new CategoryService(this._dbContext, this._mapper, null);
            
            //Act
            var category = service.GetCategory(42);

            //Assert
            Assert.IsNull(category);
        }

        [TestMethod]
        public void GetCategory_WithValidName_ShouldReturnCategory()
        {
            //Arange         
            var service = new CategoryService(this._dbContext, this._mapper, null);
            this._dbContext.Categories.Add(new Category { Id = 1, Name = "Name" });
            this._dbContext.SaveChanges();

            //Act
            var category = service.GetCategoryByName("Name");

            //Assert
            Assert.IsNotNull(category);
            Assert.AreEqual("Name", category.Name);
        }

        [TestMethod]
        public void GetCategory_WithInValidName_ShouldReturnNull()
        {
            //Arange         
            var service = new CategoryService(this._dbContext, this._mapper, null);

            //Act
            var category = service.GetCategoryByName(string.Empty);

            //Assert
            Assert.IsNull(category);
        }

        [TestInitialize]
        public void InitializeTests()
        {
            this._dbContext = MockDbContext.GetContext();
            this._mapper = MockAutoMapper.GetMapper();
        }
    }
}
