using System;
using System.Collections.Generic;

namespace AspCore2Angular4OneToMany.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empregado = new HashSet<Empregado>();
        }

        public int DepartamentoId { get; set; }
        public string Nome { get; set; }

        public ICollection<Empregado> Empregado { get; set; }
    }
}
