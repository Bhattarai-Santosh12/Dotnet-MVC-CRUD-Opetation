namespace TicketSystem.Models.ViewModels

{
    public class CustomerViewModel
    {
        public int id { get; set; }
        public int Id { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }

        public string CraetedBy { get; set; }
        public bool Status { get; set; }
    }

}
