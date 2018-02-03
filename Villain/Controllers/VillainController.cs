using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Villain
{
    [Route("api/[controller]")]
    public class VillainController : Controller
    {
        private IVillainBLC _blc;

        public VillainController(IVillainBLC blc)
        {
            _blc = blc;
        }

        [HttpGet]
        public List<Villain> Get()
        {
            try
            {
                return _blc.GetVillains();
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        [HttpGet("{name}")]
        public Villain Get(string name)
        {
            try
            {
                if(String.IsNullOrWhiteSpace(name))
                    throw new HttpRequestException("Invalid name");

                return _blc.GetVillain(name);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        [HttpPost("SingleVillain")]
        public Villain Post([FromBody] Villain villain)
        {
            try
            {
                if(villain == null)
                    throw new HttpRequestException("Invalid Villain");

                return _blc.PostVillain(villain);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        [HttpPost("MultipleVillains")]
        public List<Villain> Post([FromBody] List<Villain> villains)
        {
            try
            {
                if(villains == null || villains.Count == 0)
                    throw new HttpRequestException("Invalid Villains");

                return _blc.PostVillains(villains);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }
    }
}
