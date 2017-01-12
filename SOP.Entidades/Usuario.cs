using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SOP.Entidades
{
    public class Usuario
    {
        public Usuario()
        {

        }

        [HiddenInput(DisplayValue = false)]
        public virtual int Id_Usua
        {
            get;
            set;
        }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public virtual string Nm_Usua
        {
            get;
            set;
        }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        public virtual string Login_Usua
        {
            get;
            set;
        }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public virtual string Senha_Usua
        {
            get;
            set;
        }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        public virtual string Email_Usua
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public virtual DateTime? Dt_Incs_Rgst
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
        public virtual DateTime? Dt_Inat_Usua
        {
            get;
            set;
        }
    }
}
