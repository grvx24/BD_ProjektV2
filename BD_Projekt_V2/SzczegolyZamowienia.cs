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
    
    public partial class SzczegolyZamowienia
    {
        public int SzczegolyZamId { get; set; }
        public Nullable<int> ZamowienieId { get; set; }
        public Nullable<int> ProduktId { get; set; }
        public decimal Cena { get; set; }
        public int Ilosc { get; set; }
        public float Rabat { get; set; }
    
        public virtual Produkty Produkty { get; set; }
        public virtual Zamowienia Zamowienia { get; set; }
    }
}