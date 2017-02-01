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

        public virtual string Pedra
        {
            get;
            set;
        }

        public virtual string Comprimento
        {
            get;
            set;
        }

        public virtual string Largura
        {
            get;
            set;
        }

        public virtual string Quantidade
        {
            get;
            set;
        }

        public virtual string Tipo_Pedra
        {
            get;
            set;
        }

        public virtual string Tipo_Acabamento
        {
            get;
            set;
        }

        public virtual bool Esquerda
        {
            get;
            set;
        }

        public virtual bool Direita
        {
            get;
            set;
        }

        public virtual bool Cima
        {
            get;
            set;
        }

        public virtual bool Baixo
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public virtual string ValorTotal
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
