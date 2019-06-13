using Carter.DAL;
using Carter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter.ViewModels
{
    class HistoricoDeSalariosViewModel : BindableObject
    {
        private SalarioDAL _salarioDAL = new SalarioDAL();
        private IEnumerable<Salario> _salarios
        {
            get { return _salarioDAL.ObterHistoricoSalariosPorUsuario(); }
        }
        public IEnumerable<Salario> Salarios
        {
            get { return _salarios; }
        }
       
    }
}
