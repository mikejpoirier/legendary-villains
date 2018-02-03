using System.Collections.Generic;

namespace Villain
{
    public class VillainBLC : IVillainBLC
    {
        private IVillainDAO _dao;

        public VillainBLC(IVillainDAO dao)
        {
            _dao = dao;
        }

        public List<Villain> GetVillains()
        {
            return _dao.GetVillains();
        }

        public Villain GetVillain(string name)
        {
            return _dao.GetVillain(name);
        }

        public Villain PostVillain(Villain villain)
        {
            var result = _dao.GetVillain(villain.Name);

            if(result == null)
                return _dao.InsertVillain(villain);
            else
                return _dao.UpdateVillain(villain);
        }
        
        public List<Villain> PostVillains(List<Villain> villains)
        {
            var result = new List<Villain>();

            foreach(var villain in villains)
            {
                var exists = _dao.GetVillain(villain.Name);

                if(exists == null)
                    result.Add(_dao.InsertVillain(villain));
                else
                    result.Add(_dao.UpdateVillain(villain));
            }

            return result;
        }
    }
}