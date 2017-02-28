using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace arm_repairs_project.Models
{
    public class MangeUserViewModels
    {
        /// <summary>
        /// Модель для отображения списка пользователей
        /// </summary>
        public class MangeUsers
        {
            public IEnumerable<ApplicationUser> Users { get; set; } 
        }
    }
}