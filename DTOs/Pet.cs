using System.Collections.Generic;

namespace RestAPIAutomation.DTOs
{
    public class Pet
    {
        public double id { get; set; }
        public PetCategory category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<PetTag> tags { get; set; }
        public string status { get; set; }
    }
}
