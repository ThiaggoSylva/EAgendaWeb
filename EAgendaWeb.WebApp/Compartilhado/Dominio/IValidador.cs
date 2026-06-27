namespace EAgendaWeb.WebApp.Compartilhado.Dominio;

public interface IValidador<T>
{
    string[] Validar(T entidade);
}