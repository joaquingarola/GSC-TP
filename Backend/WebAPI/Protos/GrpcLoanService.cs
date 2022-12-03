using AutoMapper;
using Backend.Entities;
using Backend.WebAPI.DataAccess.UnitOfWork;
using Backend.WebAPI.DTO;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.IdentityModel.Tokens;

namespace Backend.WebAPI.Protos
{
    public class GrpcLoanService : ProtoLoanService.ProtoLoanServiceBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GrpcLoanService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        async public override Task<Response> CloseLoan(CloseLoanRequest request, ServerCallContext context)
        {
            {
                var loan = await _uow.LoanRepository.GetByIdAsync(request.IDLoan);
                if (loan is null)
                {
                    return await Task.FromResult(new Response { Message = "There is no loan with that ID" });
                }

                if (loan.Status)
                {

                    return await Task.FromResult(new Response { Message = "This loan has already been closed" });
                }

                loan.ReturnDate = DateTime.UtcNow;
                loan.Status = true;
                _uow.LoanRepository.Update(loan);
                await _uow.SaveChangesAsync();

                return await Task.FromResult(new Response { Message = $"The Loan has been closed." });
            }
        }

        async public override Task<GetAllResponse> GetAll(Empty request, ServerCallContext context)
        {
            var loans = await _uow.LoanRepository.GetAllAsync();

            IEnumerable<LoanRequest> listLoans = loans.Select(loan =>
            {
                return new LoanRequest
                {
                    ID = loan.ID,
                    Date = Timestamp.FromDateTime(DateTime.SpecifyKind(loan.Date, DateTimeKind.Utc)),
                    ReturnDate = loan.ReturnDate is not null ? Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)loan.ReturnDate, DateTimeKind.Utc)) : null,
                    Status = loan.Status,
                    Person = new gPRCPerson { ID = loan.Person.ID, Name = loan.Person.Name, Email = loan.Person.Email, PhoneNumber = loan.Person.PhoneNumber },
                    Thing = new gPRCThing { ID = loan.Thing.ID, Description = loan.Thing.Description, Category = loan.Thing.Category.ID}
                };
            });
            var resp = new GetAllResponse();
            resp.AllLoans.AddRange(listLoans);

            return await Task.FromResult(resp);
        }
    }
}
