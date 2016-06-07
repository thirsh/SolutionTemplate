using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpRepository.Repository;
using SharpRepository.Repository.FetchStrategies;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using SolutionTemplate.DataModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolutionTemplate.Service.Test
{
    [TestClass]
    public class DoodadServiceTests
    {
        [TestMethod]
        public void GetDoodads()
        {
            var widgetId = (int)DateTime.Now.Ticks;

            var doodads = new List<Doodad>
            {
                new Doodad
                {
                    Id = (int)DateTime.Now.Ticks
                }
            };

            var claims = new Mock<IClaims>();
            var doodadRepository = new Mock<IRepository<Doodad>>();

            doodadRepository.Setup(x => x.FindAll(It.IsAny<Expression<Func<Doodad, bool>>>(), null)).Returns(doodads);

            var service = new DoodadService(claims.Object, doodadRepository.Object);

            var results = service.FindDoodads(widgetId);

            doodadRepository.Verify(x => x.FindAll(It.IsAny<Expression<Func<Doodad, bool>>>(), null), Times.Once);

            Assert.IsNotNull(results);
            Assert.AreEqual(doodads.Count, results.Count);
            Assert.AreEqual(doodads[0].Id, results[0].Id);
        }

        [TestMethod]
        public void GetDoodad()
        {
            var doodadId = (int)DateTime.Now.Ticks;

            var doodad = new Doodad
            {
                Id = doodadId
            };

            var claims = new Mock<IClaims>();
            var doodadRepository = new Mock<IRepository<Doodad>>();

            doodadRepository.Setup(x => x.Get(doodadId, It.IsAny<GenericFetchStrategy<Doodad>>())).Returns(doodad);

            var service = new DoodadService(claims.Object, doodadRepository.Object);

            var result = service.GetDoodad(doodadId);

            doodadRepository.Verify(x => x.Get(doodadId, It.IsAny<GenericFetchStrategy<Doodad>>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(doodadId, result.Id);
        }

        [TestMethod]
        public void CreateDoodad()
        {
            var doodad = new DoodadPost
            {
                Name = "New Doodad",
                Active = true
            };

            var claims = new Mock<IClaims>();
            var doodadRepository = new Mock<IRepository<Doodad>>();

            var service = new DoodadService(claims.Object, doodadRepository.Object);

            var result = service.CreateDoodad(doodad);

            doodadRepository.Verify(x => x.Add(It.IsAny<Doodad>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(doodad.Name, result.Name);
            Assert.AreEqual(doodad.Active, result.Active);
        }

        [TestMethod]
        public void UpdateDoodad()
        {
            var doodadId = (int)DateTime.Now.Ticks;

            var doodad = new DoodadPut
            {
                Id = doodadId,
                Name = "Existing Doodad",
                Active = true
            };

            var dataDoodad = new Doodad
            {
                Id = doodadId,
                Name = doodad.Name,
                Active = doodad.Active
            };

            var claims = new Mock<IClaims>();
            var doodadRepository = new Mock<IRepository<Doodad>>();

            doodadRepository.Setup(x => x.Get(doodadId)).Returns(dataDoodad);

            var service = new DoodadService(claims.Object, doodadRepository.Object);

            var result = service.UpdateDoodad(doodadId, doodad);

            doodadRepository.Verify(x => x.Get(doodadId), Times.Once);
            doodadRepository.Verify(x => x.Update(dataDoodad), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(doodadId, result.Id);
            Assert.AreEqual(doodad.Name, result.Name);
            Assert.AreEqual(doodad.Active, result.Active);
        }

        [TestMethod]
        public void DeleteDoodad()
        {
            var doodadId = (int)DateTime.Now.Ticks;

            var claims = new Mock<IClaims>();
            var doodadRepository = new Mock<IRepository<Doodad>>();

            doodadRepository.Setup(x => x.Exists(doodadId)).Returns(true);

            var service = new DoodadService(claims.Object, doodadRepository.Object);

            service.DeleteDoodad(doodadId);

            doodadRepository.Verify(x => x.Exists(doodadId), Times.Once);
            doodadRepository.Verify(x => x.Delete(doodadId), Times.Once);
        }
    }
}