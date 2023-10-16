using JwtStore.Core.Context.AccountContext.Entities;
using JwtStore.Core.Context.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Context.AcocuntContext.ValueObjects;

namespace JwtStore.Core.Context.AccountContext.UseCases.Create;

public class Handler
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(
        Request request,
        CancellationToken cancellationToken)
    {
        #region 01. Request Validation *********************************

        try
        {
            var res = Specification.Assert(request);
            if (!res.IsValid)
                return new Response(message: "Requisição inválida", status: 400, notifications: res.Notifications);
            
        }
        catch (Exception e)
        {
            return new Response(message: "Não foi possível validar sua requisição.", status: 500);
        }
        

        #endregion

        #region Generate Objects ***********************************

        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(name: request.Name, email: email, password: password);
            
        }
        catch(Exception ex)
        {
            return new Response(message: ex.Message, status: 400);
        }

        #endregion

        #region User Verification **********************************

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response(message: "Este e-mail já está em uso.", status: 400);
        }
        catch (Exception e)
        {
            return new Response(message: "Erro ao tentar verificar e-mail cadastrado.", status: 500);
        }

        #endregion

        #region Data Registration **********************************

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception e)
        {
            return new Response(message: "Falha ao persistir dados.", 500);
        }


        #endregion

        #region Send Verfication Email *****************************

        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch 
        {
            // do nothing...
        }
        #endregion

        return new Response(
            message: "Cadastro realizado com sucesso. Obrigado!",
            data: new ResponseData(Id: user.Id, Name: user.Name, Email: user.Email));
    }
}