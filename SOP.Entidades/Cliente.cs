using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SOP.Entidades
{
    public class Cliente
    {
        public Cliente()
        {

        }

        [HiddenInput(DisplayValue = false)]
        public virtual int Id_Cliente
        {
            get;
            set;
        }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public virtual string Nm_Cliente
        {
            get;
            set;
        }

        public virtual string Tel_Fixo_Cliente
        {
            get;
            set;
        }

        public virtual string Tel_Cel_Cliente
        {
            get;
            set;
        }

        public virtual string Nm_Rua
        {
            get;
            set;
        }

        public virtual string Nu_CEP
        {
            get;
            set;
        }

        public virtual string Nu_Residencia
        {
            get;
            set;
        }
        public virtual string Nu_Complemento
        {
            get;
            set;
        }

        public virtual string Email_Cliente
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public virtual DateTime Dt_Incs_Rgst
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
        public virtual DateTime? Dt_Inat_Cliente
        {
            get;
            set;
        }
    }
}
