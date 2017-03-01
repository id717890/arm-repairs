using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using arm_repairs_project.Models.Data;

namespace arm_repairs_project.Models
{
    public class Demands
    {
        public IEnumerable<Demand> DemandsList { get; set; } 
    }

    public class DemandModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Описание проблемы")]
        public string DescriptionIssue { get; set; }

        [Required]
        [Display(Name = "Телефон пользователя")]
        public string Phone { get; set; }

        [Display(Name = "Затраченное время")]
        public decimal DecisionHours{ get; set; }

        [Display(Name = "Описание решения")]
        public string DecisionDescription { get; set; }

        [Display(Name = "Оборудование")]
        public string Equipment { get; set; }

        [Display(Name = "Назначить мастера")]
        public string Master { get; set; }

        [Display(Name = "Менеджер")]
        public string Manager { get; set; }

        [Required]
        [Display(Name = "Пользователь")]
        public string User { get; set; }

        [Required]
        [Display(Name = "Статус заявки")]
        public int Status { get; set; }

        [Required]
        [Display(Name = "Приоритет")]
        public int Priority{ get; set; }
    }
}