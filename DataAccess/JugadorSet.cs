//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class JugadorSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JugadorSet()
        {
            this.UsuarioSet = new HashSet<UsuarioSet>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string PuntuaciónAlta { get; set; }
        public string PantallasGanadas { get; set; }
        public string PartidasGanadas { get; set; }
        public string Elo { get; set; }
        public string Puntuación { get; set; }
        public int Ranking_Id { get; set; }
    
        public virtual RankingSet RankingSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioSet> UsuarioSet { get; set; }
    }
}
