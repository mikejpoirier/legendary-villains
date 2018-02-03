using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using System.Net.Http;

namespace Villain
{
    public class VillainControllerUnitTests
    {
        private VillainController _ctrl;

        public VillainControllerUnitTests()
        {
            var blcMock = new Mock<IVillainBLC>();
            blcMock.Setup(b => b.GetVillains())
                .Returns(new List<Villain>
                {
                    new Villain(),
                    new Villain()
                });

            blcMock.Setup(b => b.GetVillain(It.IsAny<string>()))
                .Returns((string name) => new Villain { Name = name });

            blcMock.Setup(b => b.PostVillain(It.IsAny<Villain>()))
                .Returns((Villain villain) => villain);

            blcMock.Setup(b => b.PostVillains(It.IsAny<List<Villain>>()))
                .Returns((List<Villain> villains) => villains);

            _ctrl = new VillainController(blcMock.Object);
        }

        [Fact]
        public void Get_ReturnsVillains()
        {
            var result = _ctrl.Get();

            Assert.True(result.Count > 1);
        }

        [Fact]
        public void Get_ValidName_ReturnsVillain()
        {
            var name = "test";

            var result = _ctrl.Get(name);

            Assert.Equal(name, result.Name);
        }

        [Fact]
        public void Get_Null_ThrowsException()
        {
            Assert.Throws<HttpRequestException>(() => _ctrl.Get(null));
        }

        [Fact]
        public void Post_SingleVillain_ReturnsVillain()
        {
            var villain = new Villain { Name = "test" };

            var result = _ctrl.Post(villain);

            Assert.Equal(villain.Name, result.Name);
        }

        [Fact]
        public void Post_MultipleVillains_ReturnsVillains()
        {
            var villains = new List<Villain>
            {
                new Villain { Name = "test1" },
                new Villain { Name = "test2" }
            };

            var result = _ctrl.Post(villains);

            Assert.True(result.All(m => villains.Any(mm => mm.Name == m.Name)));
        }
    }
}