using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataType(typeof(UseMasterData))]
    public partial class User
    {

    }

    public partial class ProductMasterData
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}