using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SOP.Entidades
{
    public class Orcamento
    {
        public Orcamento()
        {

        }

        //[HiddenInput(DisplayValue = false)]
        public int Id_Orcamento_Capa
        {
            get;
            set;
        }

        public int Id_Cliente
        {
            get;
            set;
        }
        
        public double Frete
        {
            get;
            set;
        }

        public double ValorTotal
        {
            get;
            set;
        }


        [HiddenInput(DisplayValue = false)]
        public virtual int? Cd_Usua_Rgst
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Date)]
        public virtual DateTime? Dt_Incs_Rgst
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public virtual int? Cd_Usua_Altr
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Date)]
        public virtual DateTime? Dt_Altr_Rgst
        {
            get;
            set;
        }

    }
}
