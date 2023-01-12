using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace BERlogic.CallCenter.Models
{
    public class DatabaseConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigurationId { get; set; } = 0;
        [Required(ErrorMessage = "DatabaseConfiguration.ConfigurationName.ErrorMessage")]
        [MaxLength(20)]
        [DisplayName(displayName:"DatabaseConfiguration.ConfigurationName.DisplayName")]
        [DataType(DataType.Text)]
        public string ConfigurationName { get; set; }
        [DisplayName(displayName: "DatabaseConfiguration.LocalPathString.DisplayName")]
        [DataType(DataType.Text)]
        public string LocalPathString { get; set; }
        [Required(ErrorMessage = "DatabaseConfiguration.InitialCatalog.ErrorMessage")]
        [MaxLength(20)]
        [DisplayName(displayName: "DatabaseConfiguration.InitialCatalog.DisplayName")]
        [DataType(DataType.Text)]
        public string InitialCatalog { get; set; }
        [MaxLength(50, ErrorMessage = "DatabaseConfiguration.UserName.ErrorMessage")]
        [DisplayName(displayName: "DatabaseConfiguration.UserName.DisplayName")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [MaxLength(50, ErrorMessage = "DatabaseConfiguration.Password.ErrorMessage")]
        [DisplayName(displayName: "DatabaseConfiguration.Password.DisplayName")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName(displayName: "DatabaseConfiguration.DataSource.DisplayName")]
        [DataType(DataType.Text)]
        public string DataSource { get; set; }
        [DisplayName(displayName: "DatabaseConfiguration.IntegratedSecurity.DisplayName")]
        [Range(typeof(bool),"false","true")]
        public bool IntegratedSecurity { get; set; }
        [NotMapped]
        [DisplayName(displayName: "DatabaseConfiguration.ConnectionString.DisplayName")]
        [DataType(DataType.Text)]
        public string ConnectionString => new SqlConnectionStringBuilder
        {
            DataSource = DataSource,
            AttachDBFilename = (String.IsNullOrWhiteSpace(LocalPathString)) ? String.Empty : LocalPathString,
            InitialCatalog = (String.IsNullOrWhiteSpace(InitialCatalog)) ? String.Empty : InitialCatalog,
            UserID = (String.IsNullOrWhiteSpace(UserName)) ? String.Empty : UserName,
            Password = (String.IsNullOrWhiteSpace(Password)) ? String.Empty : Password,
            IntegratedSecurity = IntegratedSecurity
        }.ToString();
    }
}
