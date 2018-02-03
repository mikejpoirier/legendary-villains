using System.Collections.Generic;

namespace Villain
{
    public interface IVillainDAO
    {
        List<Villain> GetVillains();
        Villain GetVillain(string name);
        Villain InsertVillain(Villain villain);
        Villain UpdateVillain(Villain villain);
    }
}