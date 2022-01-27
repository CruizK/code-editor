using System;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.GetAccess
{
    public interface IGetAccess
    {
        public Task<Guid> ExecuteAsync();
    }
    public class GetAccess : IGetAccess
    {
        public GetAccess() { }

        public Task<Guid> ExecuteAsync()
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}
