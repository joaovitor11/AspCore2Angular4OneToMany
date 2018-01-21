using System;
using System.Collections.Generic;

namespace AspCore2Angular4OneToMany.Models
{
    public partial class Empregado
    {
        public int EmpregadoId { get; set; }
        public int DepartamentoId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }

        public Departamento Departamento { get; set; }
    }
}
