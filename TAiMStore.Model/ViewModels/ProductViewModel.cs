using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TAiMStore.Domain;

namespace TAiMStore.Model.ViewModels
{
    public class ProductViewModel
    {

        public IEnumerable<Product> Product { get; set; }
        //[HiddenInput(DisplayValue = false)]
        //public int Id { get; set; }

        //[Required(ErrorMessage = "Пожалуйста введите имя")]
        //public string Name { get; set; }

        //[DataType(DataType.MultilineText)]
        //[Required(ErrorMessage = "Пожалуйста введите описание")]
        //public string Description { get; set; }

        //[Required]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста введите положительную цену")]
        //public decimal Price { get; set; }

        //[DataType(DataType.MultilineText)]
        //public string DescriptionSecond { get; set; }

        //public byte[] ImageData { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public string ImageMimeType { get; set; }

        //[Required(ErrorMessage = "Please specify category")]
        //public string Category { get; set; }

    }
}