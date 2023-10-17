using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Interfaces;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }


    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Request Validation *********************************

        try
        {
            var res = Specification.Assert(request);
            if (!res.IsValid)
                return new Response("Requisição inválida.", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição.", 500);
        }
        #endregion
        #region 02. Generate Objects ***********************************

        User? user;
        
        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return new Response("Usuário não encontrado.", 404);
        }
        catch 
        {
            return new Response("Não foi possível recuperar usuário.", 404);
        }
        
        #endregion
        #region 03. Verification ***************************************


        if (!user.Password.VerifyPassword(request.Password))
            return new Response("E-mail de usuário ou senha incorretos.", 400);
        
        
        #endregion
        #region 04. Verify Account Status *******************************

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response(message:"Sua conta não está ativada, cheque seu email para verificar sua conta.",
                    status: 400);
        }
        catch
        {
            return new Response(message:"Não foi possível verificar sua conta.",
                status: 500);
        }
        #endregion
        #region Data returning *****************************************

        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email.Address,
                Roles = user.Roles.Select(role => role.Name).ToArray()
            };

            return new Response(message: "Autenticação realizada com sucesso!", data: data);
        }
        catch 
        {
            return new Response(message: "Não foi possível realizar sua autenticação.", status: 500);
        }

        #endregion
    }
}