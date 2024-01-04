//------------------------------------------------------------------------------

namespace swaransoft_Assessment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class City
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
    
        public virtual State State { get; set; }
    }
}
