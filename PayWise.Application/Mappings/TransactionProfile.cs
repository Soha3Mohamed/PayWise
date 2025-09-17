using AutoMapper;
using PayWise.Application.DTOs;
using PayWise.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Application.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionResponseDTO>();
            CreateMap<TransactionResponseDTO, Transaction>();
        }
    }
}
//System.InvalidOperationException: The expression 't.SourceWalletId' is invalid inside an 'Include' operation, since it does not represent a property access: 't => t.MyProperty'. To target navigations declared on derived types, use casting ('t => ((Derived)t).MyProperty') or the 'as' operator ('t => (t as Derived).MyProperty'). Collection navigation access can be filtered by composing Where, OrderBy(Descending), ThenBy(Descending), Skip or Take operations. For more information on including related data, see https://go.microsoft.com/fwlink/?LinkID=746393.