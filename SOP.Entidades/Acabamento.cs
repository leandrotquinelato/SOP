using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SOP.Entidades
{
    public class Acabamento
    {
        public Acabamento()
        {

        }

        [HiddenInput(DisplayValue = false)]
        public virtual int Id_Acabamento
        {
            get;
            set;
        }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public virtual string Nm_Acabamento
        {
            get;
            set;
        }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O campo Preço é obrigatório.")]
        public virtual double? Nu_Preco
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

        [HiddenInput(DisplayValue = false)]
        public virtual DateTime? Dt_Inat_Produto
        {
            get;
            set;
        }
    }
}
