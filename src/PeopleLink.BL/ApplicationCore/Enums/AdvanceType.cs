using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Enums
{
    public enum AdvanceType
    {
        [Display(Name = "Kurumsal")]
        Corporate,
        [Display(Name = "Bireysel")]
        Individual
    }
}
