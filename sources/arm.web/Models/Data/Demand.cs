namespace arm_repairs_project.Models.Data
{
    using System;

    public class Demand
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DescriptionIssue { get; set; }
        public string Phone { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser Master { get; set; }
        public ApplicationUser Manager { get; set; }
        public decimal DecisionHours { get; set; }
        public string DecisionDescription { get; set; }
        public string Equipment { get; set; }
        public Priority Priority { get; set; }
        public DemandStatus Status { get; set; }
    }
}