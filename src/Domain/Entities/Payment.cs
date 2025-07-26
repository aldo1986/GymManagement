using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int MemberId { get; set; } // Clave foránea para el miembro
        public Member? Member { get; set; } // Propiedad de navegación
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int MembershipTypeId { get; set; } // Para saber qué membresía se pagó
        public MembershipType? MembershipType { get; set; }
    }
}
