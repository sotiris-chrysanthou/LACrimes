using System.ComponentModel.DataAnnotations;

namespace LACrimes.Model {
    public class Area {
        [Required] public Guid ID { get; set; }
        [Required] public String Code { get; set; } = null!;
        [Required] public String Name { get; set; } = null!;

        #region Constructors
        public Area() {

        }

        #region Relations
        public List<SubArea> SubAreas { get; set; } = null!;
        #endregion

        public Area(String code, String name) {
            ID = Guid.NewGuid();
            Code = code;
            Name = name;
        }

        #endregion


    }
}
