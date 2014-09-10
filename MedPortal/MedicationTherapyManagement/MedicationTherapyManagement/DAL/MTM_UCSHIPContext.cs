using System.Data.Entity;

namespace MedicationTherapyManagement.DAL
{
    public class MTM_UCSHIPContext :DbContext
    {
          public MTM_UCSHIPContext()
            : base("MTM_UCSHIP")
        {
              var _ = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        
        }
         
    }
}