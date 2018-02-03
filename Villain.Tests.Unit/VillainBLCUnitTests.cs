using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

namespace Villain
{
    public class VillainBLCUnitTests
    {
        private const string NEW = "new";
        private const string EXISTING = "existing";
        private VillainBLC _blc;

        public VillainBLCUnitTests()
        {
            var daoMock = new Mock<IVillainDAO>();
            daoMock.Setup(d => d.GetVillains())
                .Returns(new List<Villain>
                {
                    new Villain(),
                    new Villain()
                });

            daoMock.Setup(d => d.GetVillain(It.IsAny<string>()))
                .Returns((string name) => 
                {
                    if(name == NEW)
                        return null;
                    else
                        return new Villain 
                            {  
                                Name = name,
                                Edition = "oldEdition"
                            };
                });

            daoMock.Setup(d => d.InsertVillain(It.IsAny<Villain>()))
                .Returns((Villain villain) => villain);

            daoMock.Setup(d => d.UpdateVillain(It.IsAny<Villain>()))
                .Returns((Villain villain) => villain);

            _blc = new VillainBLC(daoMock.Object);
        }

        [Fact]
        public void GetVillains_ReturnsVillains()
        {
            var result = _blc.GetVillains();

            Assert.True(result.Count > 1);
        }

        [Fact]
        public void GetVillain_ValidName_ReturnsVillain()
        {
            var result = _blc.GetVillain(EXISTING);

            Assert.Equal(EXISTING, result.Name);
        }

        [Fact]
        public void PostVillain_ExistingVillain_UpdatesVillain()
        {
            var villain = new Villain
            {
                Name = EXISTING,
                Edition = "newEdition"
            };

            var result = _blc.PostVillain(villain);

            Assert.Equal(villain.Edition, result.Edition);
        }

        [Fact]
        public void PostVillain_NewVillain_InsertsVillain()
        {
            var villain = new Villain
            {
                Name = NEW
            };

            var result = _blc.PostVillain(villain);

            Assert.Equal(villain.Name, result.Name);
        }

        [Fact]
        public void PostVillains_NewAndExistingVillains_InsertsAndUpdatesVillains()
        {
            var villains = new List<Villain>
            {
                new Villain { Name = NEW, Edition = "Core" },
                new Villain { Name = EXISTING, Edition = "Core" }
            };

            var result = _blc.PostVillains(villains);

            Assert.True(result.All(m => villains.Any(mm => mm.Name == m.Name)));
        }
    }
}