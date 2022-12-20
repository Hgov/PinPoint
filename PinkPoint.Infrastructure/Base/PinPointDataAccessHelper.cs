using PinPoint.DataAccess.Helpers;

namespace PinPoint.Infrastructure.Base
{
    public class PinPointDataAccessHelper
    {
        public DataContext _dataContext;

        public PinPointDataAccessHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
