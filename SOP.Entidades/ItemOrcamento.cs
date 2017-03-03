using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SOP.Entidades
{
    public class ItemOrcamento
    {
        public ItemOrcamento()
        {

        }

        [HiddenInput(DisplayValue = false)]
        public virtual int Id_ItemOrcamento
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public virtual int Id_Orcamento
        {
            get;
            set;
        }

        [Display(Name = "Pedra")]
        [Required(ErrorMessage = "O campo Pedra é obrigatório.")]
        [UIHint("GridForeignKey")]
        public int Id_Pedra
        {
            get;
            set;
        }

        [Display(Name = "Comprimento")]
        [Required(ErrorMessage = "O campo Comprimento é obrigatório.")]
        public virtual double Comprimento
        {
            get;
            set;
        }

        [Display(Name = "Largura")]
        [Required(ErrorMessage = "O campo Largura é obrigatório.")]
        public virtual double Largura
        {
            get;
            set;
        }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public virtual double Quantidade
        {
            get;
            set;
        }

        [Display(Name = "Tipo Pedra")]
        [Required(ErrorMessage = "O campo Tipo Pedra é obrigatório.")]
        [UIHint("GridForeignKey")]
        public int Id_Tipo_Pedra
        {
            get;
            set;
        }

        [Display(Name = "Acabamento")]
        [UIHint("GridForeignKey")]
        public int Id_Tipo_Acabamento
        {
            get;
            set;
        }               

        [Display(Name = "Acabamento em cima")]        
        public virtual bool Cima
        {
            get;
            set;
        }

        [Display(Name = "Acabamento embaixo")]        
        public virtual bool Baixo
        {
            get;
            set;
        }

        [Display(Name = "Acabamento na Direita")]
        public virtual bool Direita
        {
            get;
            set;
        }

        [Display(Name = "Acabamento na Esquerda")]
        public virtual bool Esquerda
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public virtual string EsquerdaString
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public virtual string DireitaString
        {
            get;
            set;
        }
        
        [HiddenInput(DisplayValue = false)]        
        public virtual string CimaString
        {
            get;
            set;
        }
        
        [HiddenInput(DisplayValue = false)]
        public virtual string BaixoString
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public virtual double ValorTotal
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
