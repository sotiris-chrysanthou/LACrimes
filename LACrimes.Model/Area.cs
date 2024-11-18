using System.ComponentModel.DataAnnotations;

namespace LACrimes.Model {
    public class Area {
        [Required] public Guid ID { get; set; }
        [Required] public int Code { get; set; }
        [Required] public string Name { get; set; } = null!;

        #region Constructors
        public Area() {

        }

        #region Relations
        public List<SubArea> SubAreas { get; set; } = null!;
        #endregion

        public Area(int code, string name) {
            ID = Guid.NewGuid();
            Code = code;
            Name = name;
        }

        #endregion


    }
}
