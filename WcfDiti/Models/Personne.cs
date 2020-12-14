using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WcfDiti.Models
{
    public class Personne
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nom"), MaxLength(160, ErrorMessage = "La taille maximale est de 160 caractères"), Required(ErrorMessage = "*")]
        public string Nom { get; set; }
        [Display(Name = "Prénom"), MaxLength(160, ErrorMessage = "La taille maximale est de 160 caractères"), Required(ErrorMessage = "*")]
        public string Prenom { get; set; }
        //[Display(Name = "Identifiant"), MaxLength(20, ErrorMessage = "La taille maximale est de 20 caractères"), Required(ErrorMessage = "*")]
        //public string IdentifiantU { get; set; }
        [Display(Name = "Email"), MaxLength(160, ErrorMessage = "La taille maximale est de 160 caractères"), Required(ErrorMessage = "*")]
        public string Email { get; set; }
        [Display(Name = "Tel"), MaxLength(160, ErrorMessage = "La taille maximale est de 160 caractères"), Required(ErrorMessage = "*")]
        public string Tel { get; set; }
        [Display(Name = "Date Naissance"), DataType(DataType.DateTime), Required(ErrorMessage = "*")]
        public DateTime DateNaissance { get; set; }
        //[Display(Name = "User"), MaxLength(80, ErrorMessage = "La taille maximale est de 30")]
        //public string IdUser { get; set; }
    }
    public class PersonneViewModel
    {
        public string Email { get; set; }
        public string Tel { get; set; }
        public string debutDateNaissance { get; set; }
        public string finDateNaissance { get; set; }
    }
}