using PinkPoint.DataAccess.Helpers;

namespace PinkPoint.Infrastructure.Base
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
