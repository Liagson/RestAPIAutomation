using System.ComponentModel;

namespace RestAPIAutomation
{
    public class Constants
    {
        public enum PetStates
        {
            [Description("available")] available,
            [Description("sold")] sold,
            [Description("pending")] pending
        }
    }
}
