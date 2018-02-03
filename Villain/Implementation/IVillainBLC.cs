using System.Collections.Generic;

namespace Villain
{
    public interface IVillainBLC
    {
        List<Villain> GetVillains();
        Villain GetVillain(string name);
        Villain PostVillain(Villain villain);
        List<Villain> PostVillains(List<Villain> villains);
    }
}