//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BD_Projekt_V2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Koszyk_Przedmiot
    {
        public int KlientId { get; set; }
        public int ProduktId { get; set; }
        public int LiczbaSztuk { get; set; }
    
        public virtual Klienci Klienci { get; set; }
        public virtual Produkty Produkty { get; set; }
    }
}
