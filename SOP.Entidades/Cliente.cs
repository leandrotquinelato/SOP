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
        [Display(Name = "E-Mail")]
        public virtual string Email_Cliente
        {
            get;
            set;
        }

        [Display(Name = "Tel. Celular")]
        [Required(ErrorMessage = "O campo Tel. Celular é obrigatório.")]
        [DataType(DataType.PhoneNumber)]
        [UIHint("Telefone_Celular")]
        public virtual string Tel_Cel_Cliente
        {
            get;
            set;
        }

        [Display(Name = "Tel. Fixo")]
        [DataType(DataType.PhoneNumber)]
        [UIHint("Telefone")]
        public virtual string Tel_Fixo_Cliente
        {
            get;
            set;
        }


        [Display(Name = "Nome Rua")]
        public virtual string Nm_Rua
        {
            get;
            set;
        }

        [Display(Name = "Nº Residência")]
        public virtual string Nu_Residencia
        {
            get;
            set;
        }

        [Display(Name = "Complemento")]
        public virtual string Nu_Complemento
        {
            get;
            set;
        }        

        [Display(Name = "Bairro")]
        public virtual string Nm_Bairro
        {
            get;
            set;
        }

        [Display(Name = "CEP")]
        [UIHint("CEP")]
        public virtual string Nu_CEP
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

        [HiddenInput(DisplayValue = false)]
        public virtual DateTime? Dt_Inat_Cliente
        {
            get;
            set;
        }
    }
}
