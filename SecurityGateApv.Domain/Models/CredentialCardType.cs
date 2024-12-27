using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class CredentialCardType
    {
        [Key]
        public int CredentialCardTypeId { get; private set; }
        public string CredentialCardTypeName { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public ICollection<Visitor> Visitor { get; private set; }
        protected CredentialCardType()
        {
            Visitor = new List<Visitor>();
        }
        private CredentialCardType(int credentialCardTypeId, string credentialCardTypeName, string description, bool status)
        {
            CredentialCardTypeId = credentialCardTypeId;
            CredentialCardTypeName = credentialCardTypeName;
            Description = description;
            Status = status;
            Visitor = new List<Visitor>();
        }

        public static Result<CredentialCardType> Create(int credentialCardTypeId, string credentialCardTypeName, string description, bool status)
        {
            if (string.IsNullOrWhiteSpace(credentialCardTypeName))
            {
                return Result.Failure<CredentialCardType>(Error.CredentialCardTypeInputValid);
            }

            var credentialCardType = new CredentialCardType(credentialCardTypeId, credentialCardTypeName, description, status);
            return credentialCardType;
        }
    }
}
