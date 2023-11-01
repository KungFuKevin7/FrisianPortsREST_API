namespace FrisianPortsREST_API.Models
{
    public class Cargo
    {
        public int Cargo_Id { get; set; }

        public string Cargo_Description { get; set; }
 
        public int Weight_In_Tonnes { get; set; }
    
        public int Cargo_Type_Id { get; set; }

        public int Transport_Id { get; set; }
    }
}
