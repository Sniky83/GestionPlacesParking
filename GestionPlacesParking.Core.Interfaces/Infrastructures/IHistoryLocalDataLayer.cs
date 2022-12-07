using GestionPlacesParking.Core.Models.Locals.History;

namespace GestionPlacesParking.Core.Interfaces.Infrastructures
{
    public interface IHistoryLocalDataLayer
    {
        HistoryLocal ExtractInfos();
    }
}
